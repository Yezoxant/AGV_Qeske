using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotPlatformMonitor
{
    public partial class FormMain : Form
    {
        com_protocol Protocol;
        UInt16[] RcInfo = new UInt16[10];
        vesc_values Left_motor;
        vesc_values Right_motor;

        int Gas_value = 0;
        int Steer_value = 0;

        enum vesc_fault_code
        {
            FAULT_CODE_NONE = 0,
            FAULT_CODE_OVER_VOLTAGE,
            FAULT_CODE_UNDER_VOLTAGE,
            FAULT_CODE_DRV8302,
            FAULT_CODE_ABS_OVER_CURRENT,
            FAULT_CODE_OVER_TEMP_FET,
            FAULT_CODE_OVER_TEMP_MOTOR
        };

        // VESC Types
        [StructLayout(LayoutKind.Sequential)]
        class vesc_values
        {
            public Int32 v_in;               //mV
            public Int16 temp_fet_filtered;
            public Int16 temp_motor_filtered;
            public Int32 avg_motor_current;
            public Int32 avg_input_current;

            public Int32 avg_id;             //mA
            public Int32 avg_iq;             //mA
            public Int32 rpm;
            public Int16 duty_now;           //divide 1000
            public Int32 amp_hours;          //mAh
            public Int32 amp_hours_charged;  //mAh
            public Int32 watt_hours;         //mWh
            public Int32 watt_hours_charged; //mWh	
            public Int32 tachometer;
            public Int32 tachometer_abs;
            public vesc_fault_code fault_code;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 58)]
            byte[] lfFaceName;
        }

        static vesc_values GetVescValuesFromBff(byte[] bff)
        {
            vesc_values logFont = new vesc_values();
            IntPtr ptPoit = Marshal.AllocHGlobal(92);
            Marshal.Copy(bff, 0, ptPoit, 92);
            logFont = (vesc_values)Marshal.PtrToStructure(ptPoit, typeof(vesc_values));
            Marshal.FreeHGlobal(ptPoit);
            return logFont;
        }


        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer_connect.Enabled = true;
            timer_update.Enabled = true;
            reload_rcinfo_form();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_connect.Stop();
            timer_update.Stop();
            if (Protocol != null)
            {
                Protocol.stop();
            }
        }

        private void timer_connect_Tick(object sender, EventArgs e)
        {
            if (Protocol != null)
            {
                if (Protocol.Connected())
                {
                    return;
                }
            }

            bool connected = false;
            foreach(var port in SerialPort.GetPortNames())
            {
                Protocol = new com_protocol();
                if (Protocol.com_protocol_set_config_p2p(port))
                {
                    var tmp = Protocol.com_protocol_SendCommand_p2p(com_commands.GET_firmwareVersionMainMCU);
                    if (tmp != null)
                    {
                        if ((tmp.command == com_commands.RESPONSE_DATA) && (tmp.dataLength == 2))
                        {
                            connected = true;
                            textBox_hotswap_state.Text = "Connected - " + port.ToString();
                            textBox_hotswap_state.ForeColor = Color.Green;
                            textBox_fw_version.Text = ((((UInt16)tmp.data[0]) << 8) + tmp.data[1]).ToString("X4");
                            timer_update.Enabled = true;
                            break;
                        }
                    }
                }
            }

            if (connected == false)
            {
                Protocol = null;
                textBox_hotswap_state.Text = "Disconnected";
                textBox_hotswap_state.ForeColor = Color.Red;
                textBox_fw_version.Text = "";

                reload_rcinfo_form();
            }
        }

        private void timer_update_Tick(object sender, EventArgs e)
        {
            get_data_from_controller();
            reload_rcinfo_form();

            send_data_to_controller();
        }

        private bool GetDataFromClient_byte(byte command, out byte retVal)
        {
            retVal = 0;
            var ret = Protocol.com_protocol_SendCommand_p2p(command);
            if (ret != null)
            {
                if ((ret.command == com_commands.RESPONSE_DATA) && (ret.dataLength == 1))
                {
                    retVal = ret.data[0];
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool GetDataFromClient_Uint16(byte command, int batIndex, out UInt16 retVal)
        {
            retVal = 0;
            var ret = Protocol.com_protocol_SendDataCommand_p2p(command, new byte[] { (byte)batIndex }, 1);
            if (ret != null)
            {
                if ((ret.command == com_commands.RESPONSE_DATA) && (ret.dataLength == 2))
                {
                    retVal = (UInt16)((((UInt16)ret.data[0]) << 8) + ret.data[1]);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool GetDataFromClient_Int16(byte command, int batIndex, out Int16 retVal)
        {
            retVal = 0;
            var ret = Protocol.com_protocol_SendDataCommand_p2p(command, new byte[] { (byte)batIndex }, 1);
            if (ret != null)
            {
                if ((ret.command == com_commands.RESPONSE_DATA) && (ret.dataLength == 2))
                {
                    retVal = (Int16)((((UInt16)ret.data[0]) << 8) + ret.data[1] - 32768);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool GetDataFromClient_MotorStatus(int vescIndex, out vesc_values retVal)
        {
            retVal = null;
            var ret = Protocol.com_protocol_SendDataCommand_p2p(com_commands.GET_MOTOR_STATUS, new byte[] { (byte)vescIndex }, 1);
            if (ret != null)
            {
                if ((ret.command == com_commands.RESPONSE_DATA) && (ret.dataLength >= 2))
                {
                    retVal = GetVescValuesFromBff(ret.data);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        bool update_rc = false;
        private void get_data_from_controller()
        {
            if (Protocol != null)
            {
                if (Protocol.Connected())
                {
                    if (update_rc)
                    {
                        for (int i = 0; i < RcInfo.Length; i++)
                        {
                            if (!GetDataFromClient_Uint16(com_commands.GET_RC_CHANNEL, i, out RcInfo[i]))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        GetDataFromClient_MotorStatus(0, out Left_motor);
                        GetDataFromClient_MotorStatus(0, out Right_motor);
                    }
                    update_rc = !update_rc;
                }
            }
        }

        private void send_data_to_controller()
        {
            if (Protocol != null)
            {
                if (Protocol.Connected())
                {
                    byte[] buffer = new byte[2];
                    buffer[0] = (byte)((Gas_value + 100));
                    buffer[1] = (byte)((Steer_value + 100));

                    var ret = Protocol.com_protocol_SendDataCommand_p2p(com_commands.SET_MOTORS, buffer, 2);
                }
            }
        }

        private void reload_rcinfo_form()
        {
            textBox_RC_0.Text = RcInfo[0].ToString();
            textBox_RC_1.Text = RcInfo[1].ToString();
            textBox_RC_2.Text = RcInfo[2].ToString();
            textBox_RC_3.Text = RcInfo[3].ToString();
            textBox_RC_4.Text = RcInfo[4].ToString();
            textBox_RC_5.Text = RcInfo[5].ToString();
            textBox_RC_6.Text = RcInfo[6].ToString();
            textBox_RC_7.Text = RcInfo[7].ToString();
            textBox_RC_8.Text = RcInfo[8].ToString();
            textBox_RC_9.Text = RcInfo[9].ToString();

            if (Left_motor != null)
            {
                textBox_vesc_vin_left.Text = Left_motor.v_in.ToString();
                textBox_vesc_tfet_left.Text = Left_motor.temp_fet_filtered.ToString();
                textBox_vesc_mcurrent_left.Text = Left_motor.avg_motor_current.ToString();
                textBox_vesc_icurrent_left.Text = Left_motor.avg_input_current.ToString();
                textBox_vesc_id_left.Text = Left_motor.avg_id.ToString();
                textBox_vesc_iq_left.Text = Left_motor.avg_iq.ToString();
                textBox_vesc_rpm_left.Text = Left_motor.rpm.ToString();
                textBox_vesc_duty_left.Text = Left_motor.duty_now.ToString();
                textBox_vesc_fault_left.Text = Left_motor.fault_code.ToString();
            }

            if (Right_motor != null)
            {
                textBox_vesc_vin_right.Text = Right_motor.v_in.ToString();
                textBox_vesc_tfet_right.Text = Right_motor.temp_fet_filtered.ToString();
                textBox_vesc_mcurrent_right.Text = Right_motor.avg_motor_current.ToString();
                textBox_vesc_icurrent_right.Text = Right_motor.avg_input_current.ToString();
                textBox_vesc_id_right.Text = Right_motor.avg_id.ToString();
                textBox_vesc_iq_right.Text = Right_motor.avg_iq.ToString();
                textBox_vesc_rpm_right.Text = Right_motor.rpm.ToString();
                textBox_vesc_duty_right.Text = Right_motor.duty_now.ToString();
                textBox_vesc_fault_right.Text = Right_motor.fault_code.ToString();
            }
        }

        private void button_control_update_Click(object sender, EventArgs e)
        {
            Gas_value = (int)numericUpDown_motor_gas.Value;
            Steer_value = (int)numericUpDown_motor_steer.Value;
        }

        private void button_control_stop_Click(object sender, EventArgs e)
        {
            Gas_value = 0;
            Steer_value = 0;
        }
    }
}
