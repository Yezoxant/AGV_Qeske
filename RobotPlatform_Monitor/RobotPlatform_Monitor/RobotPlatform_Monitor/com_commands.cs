using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotPlatformMonitor
{
    static class com_commands
    {
        public const byte GET_RC_CHANNEL = 0xA0;
        public const byte GET_MOTOR_STATUS = 0xA1;

        public const byte SET_MOTORS = 0xB0;

        public const byte GET_firmwareVersionMainMCU = 0x11;


        public const byte RESPONSE_ACK = 0xF1;
        public const byte RESPONSE_ERROR = 0xF2;
        public const byte RESPONSE_DATA = 0xF3;

    }
}
