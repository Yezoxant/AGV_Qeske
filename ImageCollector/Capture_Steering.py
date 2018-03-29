import serial
import re
from time import sleep
import logging

def init_serial():
    global ser
    ser = serial.Serial(port = "/dev/ttyACM0", baudrate = 115200)

def get_motion():
    cmd = "get motion\n"
    cmd_bytes = str.encode(cmd)
    ser.flush()
    g = ser.write(cmd_bytes)
    response = ''
    sleep(0.05) #delay to give the driver board some time to respond
    while ser.in_waiting > 0:
        response += ser.read().decode('utf-8')
    
    res = re.search("(\d+|-\d+):(\d+|-\d+)",response)
    throttle = str(res.group(1))
    steering = str(res.group(2))
    print(throttle, "res1")
    print(steering, "res2")
    return (throttle,steering)