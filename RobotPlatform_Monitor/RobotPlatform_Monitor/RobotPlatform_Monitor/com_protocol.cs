using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotPlatformMonitor
{
    class com_protocol
    {
        private const int COM_PROTOCOL_BUFFER_SIZE = 128;
        private const int COM_PROTOCOL_MAX_RESPONSE_TIME_MS = 250;
        private const int COM_PROTOCOL_MAX_RESPONSE_TRIES = 2;
        private const byte COM_PROTOCOL_COMMAND_START = 0xC1;
        private const byte COM_PROTOCOL_COMMAND_ESC = 0xC0;
        private const byte COM_PROTOCOL_ESC_MASK = 0x80;
        private const UInt32 COM_PROTOCOL_ADDRESS_TO_ALL_NO_RESPONSE = 0xFFFFFFF0;
        private const UInt32 COM_PROTOCOL_ADDRESS_TO_ALL = 0xFFFFFFF2;

        private enum com_protocol_receiveStates { RCV_start, RCV_packetnumber, RCV_address3, RCV_address2, RCV_address1, RCV_address0, RCV_length, RCV_command, RCV_data, RCV_checksumH, RCV_checksumL };


        private SerialPort Serport;
        private UInt16 Checksum;
        private byte LastPacketNumber;
        private byte[] Buffer;
        private byte BufferIndex;
        private UInt32 DeviceAddress;
        private bool p2p_mode;
        private bool ReceivedEscape;
        private UInt16 ReceivedChecksum;
        private com_protocol_receiveStates CurrentReceiveState;

        public class receiveData
        {
            public UInt32 address;
            public byte command;
            public byte[] data;
            public byte dataLength;
            public byte packetNumber;
        }

        receiveData CurrentRecieveData = new receiveData();

        private void ClearChecksum()
        {
            Checksum = 0;
        }

        private UInt16 GetChecksum()
        {
            return Checksum;
        }

        private void UpdateChecksum(byte val)
        {
            Checksum = (UInt16)(((Checksum >> 1) & 0x7fff) | ((Checksum << 15) & 0x8000));
            Checksum ^= val;
        }

        private byte GetNextPacketNumber()
        {
            LastPacketNumber++;
            if (LastPacketNumber >= 128)
            {
                LastPacketNumber = 0;
            }
            return LastPacketNumber;
        }

        //Add value to send buffer
        //escapes value if necessary
        //returns true if value successfully added to the buffer
        private bool AddToSendBuffer(byte val)
        {
            if ((BufferIndex + 2) > COM_PROTOCOL_BUFFER_SIZE)
            {
                //buffer full
                return false;
            }
            else
            {
                //check if val is equal to a command. if so, escape it
                if ((val == COM_PROTOCOL_COMMAND_START) || (val == COM_PROTOCOL_COMMAND_ESC))
                {
                    Buffer[BufferIndex++] = COM_PROTOCOL_COMMAND_ESC;
                    Buffer[BufferIndex++] = (byte)(val & (~COM_PROTOCOL_ESC_MASK));
                }
                else
                {
                    Buffer[BufferIndex++] = val;
                }
                return true;
            }
        }

        public void stop()
        {
            if (Serport != null)
            {
                try
                {
                    Serport.Close();
                }
                catch (System.Exception) { };
                try
                {
                    Serport.Dispose();
                }
                catch (System.Exception) { };

                Serport = null;
            }
        }

        //synchronously receive a CAN command
        public receiveData com_protocol_ReceiveUpdateInternal(UInt32 listenAddress)
        {
            try
            {
                while (Serport.BytesToRead > 0)
                {
                    byte val = (byte)Serport.ReadByte();


                    if (val == COM_PROTOCOL_COMMAND_ESC)
                    {
                        ReceivedEscape = true;
                    }
                    else
                    {
                        if (val == COM_PROTOCOL_COMMAND_START)
                        {

                            ClearChecksum();
                            CurrentReceiveState = com_protocol_receiveStates.RCV_packetnumber;
                            ReceivedEscape = false;
                        }
                        else
                        {
                            if (ReceivedEscape)
                            {
                                ReceivedEscape = false;
                                val = (byte)(val | COM_PROTOCOL_ESC_MASK);
                            }

                            switch (CurrentReceiveState)
                            {
                                case com_protocol_receiveStates.RCV_start:
                                    break;
                                case com_protocol_receiveStates.RCV_packetnumber:
                                    CurrentRecieveData.packetNumber = val;
                                    if (p2p_mode)
                                    {
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_length;
                                    }
                                    else
                                    {
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_address3;
                                    }
                                    break;
                                case com_protocol_receiveStates.RCV_address3:
                                    CurrentRecieveData.address = ((UInt32)val) << 24;
                                    CurrentReceiveState = com_protocol_receiveStates.RCV_address2;
                                    break;
                                case com_protocol_receiveStates.RCV_address2:
                                    CurrentRecieveData.address = CurrentRecieveData.address + (((UInt32)val) << 16);
                                    CurrentReceiveState = com_protocol_receiveStates.RCV_address1;
                                    break;
                                case com_protocol_receiveStates.RCV_address1:
                                    CurrentRecieveData.address = CurrentRecieveData.address + (((UInt32)val) << 8);
                                    CurrentReceiveState = com_protocol_receiveStates.RCV_address0;
                                    break;
                                case com_protocol_receiveStates.RCV_address0:
                                    CurrentRecieveData.address = CurrentRecieveData.address + ((UInt32)val);

                                    if ((CurrentRecieveData.address == listenAddress) ||
                                        (CurrentRecieveData.address == COM_PROTOCOL_ADDRESS_TO_ALL_NO_RESPONSE) ||
                                        (CurrentRecieveData.address == COM_PROTOCOL_ADDRESS_TO_ALL))
                                    {
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_length;
                                    }
                                    else
                                    {
                                        //message not for us
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_start;
                                    }
                                    break;
                                case com_protocol_receiveStates.RCV_length:
                                    CurrentRecieveData.dataLength = val;
                                    if (CurrentRecieveData.dataLength > COM_PROTOCOL_BUFFER_SIZE)
                                    {
                                        //message is too big, so we are not even trying to receive it.
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_start;
                                    }
                                    else
                                    {
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_command;
                                    }
                                    break;
                                case com_protocol_receiveStates.RCV_command:
                                    CurrentRecieveData.command = val;
                                    if (CurrentRecieveData.dataLength > 0)
                                    {
                                        BufferIndex = 0;
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_data;
                                    }
                                    else
                                    {
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_checksumH;
                                    }
                                    break;
                                case com_protocol_receiveStates.RCV_data:
                                    Buffer[BufferIndex++] = val;
                                    if (BufferIndex >= CurrentRecieveData.dataLength)
                                    {
                                        CurrentReceiveState = com_protocol_receiveStates.RCV_checksumH;
                                    }
                                    break;
                                case com_protocol_receiveStates.RCV_checksumH:
                                    ReceivedChecksum = (UInt16)(((UInt16)val) << 8);
                                    CurrentReceiveState = com_protocol_receiveStates.RCV_checksumL;
                                    break;
                                case com_protocol_receiveStates.RCV_checksumL:
                                    ReceivedChecksum = (UInt16)(ReceivedChecksum + val);
                                    if (GetChecksum() == ReceivedChecksum)
                                    {
                                        //data valid
                                        CurrentRecieveData.data = Buffer;
                                        return CurrentRecieveData;

                                    }
                                    CurrentReceiveState = com_protocol_receiveStates.RCV_start;
                                    break;
                            }
                            if (CurrentReceiveState != com_protocol_receiveStates.RCV_checksumL)
                            {

                                UpdateChecksum(val);
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {

            }

            //no response
            return null;
        }

        public bool com_protocol_set_config_bus(string comport, UInt32 deviceAddress)
        {
            stop();
            Serport = new SerialPort();
            Serport.BaudRate = 115200;
            Serport.PortName = comport;
            Serport.ReadTimeout = 100;
            Serport.WriteTimeout = 100;
            try
            {
                Serport.Open();

                Buffer = new byte[COM_PROTOCOL_BUFFER_SIZE];
                DeviceAddress = deviceAddress;
                p2p_mode = false;
                LastPacketNumber = 0;
                CurrentReceiveState = com_protocol_receiveStates.RCV_start;
                ReceivedEscape = false;
            }
            catch (System.Exception)
            {
                stop();
                return false;
            }
            return true;
        }

        public bool com_protocol_set_config_p2p(string comport)
        {
            stop();
            Serport = new SerialPort();
            Serport.BaudRate = 115200;
            Serport.PortName = comport;
            Serport.ReadTimeout = 100;
            Serport.WriteTimeout = 100;
            try
            {
                Serport.Open();

                Buffer = new byte[COM_PROTOCOL_BUFFER_SIZE];
                DeviceAddress = 0;
                p2p_mode = true;
                LastPacketNumber = 0;
                CurrentReceiveState = com_protocol_receiveStates.RCV_start;
                ReceivedEscape = false;
            }
            catch (System.Exception)
            {
                stop();
                return false;
            }
            return true;
        }

        //synchronously receive a CAN command
        public receiveData com_protocol_ReceiveUpdate()
        {
            return com_protocol_ReceiveUpdateInternal(DeviceAddress);
        }

        //send a command to an address and wait for the client to response (peer to peer mode)
        //response is NULL if client does not response within maximum response time (CANCMD_MAX_RESPONSE_TIME_MS)
        public receiveData com_protocol_SendCommand_p2p(byte command)
        {
            return com_protocol_SendDataCommandInternal(0, command, null, 0, false, 0);
        }

        //send a command to an address and wait for the client to response (peer to peer mode)
        //response is NULL if client does not response within maximum response time (CANCMD_MAX_RESPONSE_TIME_MS)
        public receiveData com_protocol_SendDataCommand_p2p(byte command, byte[] data, byte dataLength)
        {
            return com_protocol_SendDataCommandInternal(0, command, data, dataLength, false, 0);
        }

        public receiveData com_protocol_SendResponse_p2p(receiveData receive_Data, byte response)
        {
            return com_protocol_SendDataCommandInternal(0, response, null, 0, true, receive_Data.packetNumber);
        }

        public receiveData com_protocol_SendDataResponse_p2p(receiveData receive_Data, byte response, byte[] data, byte dataLength)
        {
            return com_protocol_SendDataCommandInternal(0, response, data, dataLength, true, receive_Data.packetNumber);
        }

        //send a command to an address and wait for the client to response (bus mode)
        //response is NULL if client does not response within maximum response time (CANCMD_MAX_RESPONSE_TIME_MS)
        public receiveData com_protocol_SendCommand_bus(UInt32 address, byte command)
        {
            return com_protocol_SendDataCommandInternal(address, command, null, 0, false, 0);
        }

        public receiveData com_protocol_SendDataCommand_bus(UInt32 address, byte command, byte[] data, byte dataLength)
        {
            return com_protocol_SendDataCommandInternal(address, command, data, dataLength, false, 0);
        }

        public void com_protocol_SendResponse_bus(receiveData receive_Data, byte response)
        {
            com_protocol_SendDataCommandInternal(DeviceAddress, response, null, 0, true, receive_Data.packetNumber);
        }

        public void com_protocol_SendDataResponse_bus(receiveData receive_Data, byte response, byte[] data, byte dataLength)
        {
            com_protocol_SendDataCommandInternal(DeviceAddress, response, data, dataLength, true, receive_Data.packetNumber);
        }

        //send a command to an address and wait for the client to response (bus mode)
        //response is NULL if client does not response within maximum response time (CANCMD_MAX_RESPONSE_TIME_MS)
        protected receiveData com_protocol_SendDataCommandInternal(UInt32 address, byte command, byte[] data, byte dataLength, bool responseMessage, byte responsePacketNumber)
        {
            if (Connected() == false)
            {
                return null;
            }

            receiveData response = null;

            for (int i = 0; ((i < COM_PROTOCOL_MAX_RESPONSE_TRIES) && (response == null)); i++)
            {
                BufferIndex = 0;

                ClearChecksum();

                Buffer[BufferIndex++] = COM_PROTOCOL_COMMAND_START;
                byte packetnmbr;
                if (responseMessage)
                {
                    packetnmbr = responsePacketNumber;
                }
                else
                {
                    packetnmbr = GetNextPacketNumber();
                }

                AddToSendBuffer(packetnmbr); UpdateChecksum(packetnmbr);
                if (p2p_mode == false)
                {
                    AddToSendBuffer((byte)(address >> 24)); UpdateChecksum((byte)(address >> 24));
                    AddToSendBuffer((byte)(address >> 16)); UpdateChecksum((byte)(address >> 16));
                    AddToSendBuffer((byte)(address >> 8)); UpdateChecksum((byte)(address >> 8));
                    AddToSendBuffer((byte)address); UpdateChecksum((byte)address);
                }
                AddToSendBuffer(dataLength); UpdateChecksum(dataLength);
                AddToSendBuffer(command); UpdateChecksum(command);

                for (int j = 0; j < dataLength; j++)
                {
                    if (AddToSendBuffer(data[j]) == false)
                    {
                        //send buffer overflow
                        return null;
                    }

                    UpdateChecksum(data[j]);
                }
                if ((BufferIndex + 4) > COM_PROTOCOL_BUFFER_SIZE)
                {
                    //send buffer overflow
                    return null;
                }


                AddToSendBuffer((byte)(GetChecksum() >> 8));

                AddToSendBuffer((byte)GetChecksum());

                try
                {
                    Serport.Write(Buffer, 0, BufferIndex);
                    //successfully send the data
                    //delete any remaining data in the input buffer
                    Serport.DiscardInBuffer();
                }
                catch (System.Exception)
                {
                    return null;
                }

                if ((address == COM_PROTOCOL_ADDRESS_TO_ALL_NO_RESPONSE) || (responseMessage == true))
                {
                    //we are not expecting a response
                    break;
                }
                else
                {
                    CurrentReceiveState = com_protocol_receiveStates.RCV_start;
                    ReceivedEscape = false;
                    DateTime startTime = DateTime.Now;
                    while ((DateTime.Now - startTime).TotalMilliseconds <= COM_PROTOCOL_MAX_RESPONSE_TIME_MS)
                    {
                        response = com_protocol_ReceiveUpdateInternal(address);
                        if (response != null)
                        {
                            if ((((response.address != COM_PROTOCOL_ADDRESS_TO_ALL_NO_RESPONSE) &&
                                    (response.address != COM_PROTOCOL_ADDRESS_TO_ALL)) || p2p_mode) &&
                                    (response.packetNumber == packetnmbr))
                            {
                                //valid response received
                                break;
                            }
                            else
                            {
                                response = null;
                                if (p2p_mode)
                                {
                                    break;
                                }
                            }
                        }
						Thread.Sleep(5);
                    }
                }
            }

            CurrentReceiveState = com_protocol_receiveStates.RCV_start;
            ReceivedEscape = false;
            return response;
        }


        public bool Connected()
        {
            try
            {
                if (Serport != null)
                {
                    if (Serport.IsOpen)
                    {
                        return true;
                    }
                    else
                    {
                        stop();
                        return false;
                    }
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

    }
}
