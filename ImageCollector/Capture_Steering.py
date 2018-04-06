import serial
import re
from time import sleep
import logging
steering_logger = logging.getLogger('ImageCollector.Capture_Steering')

def init_serial():
    global ser
    ser = serial.Serial(port = "/dev/ttyACM0", baudrate = 115200)
    steering_logger.info("connected serial")

def get_motion():
    cmd = "get motion\n"
    cmd_bytes = str.encode(cmd)
    ser.flush()
    g = ser.write(cmd_bytes)
    steering_logger.info("sent get motion cmd")
    response = ''
    sleep(0.05)
    while ser.in_waiting > 0:
        response += ser.read().decode('utf-8')
    steering_logger.info("response was %s",response)
    res = re.search("(\d+|-\d+):(\d+|-\d+)",response)
    if res is not None:
        steering_logger.info("regex found result:%s",str(res.groups()))
        tempthrottle = str(res.group(1))
        tempsteering = str(res.group(2))
        throttle = int(tempthrottle) / 100
        steering = int(tempsteering) / 100
        print(throttle, steering)
        return (throttle,steering)
    else:
        steering_logger.error("Could not find motion info in response")
        return None