import serial
from time import sleep
ser = serial.Serial(port = "COM9", baudrate = 115200)
steering_logger.info("connected serial")

cmd = "get motion\n"
cmd_bytes = str.encode(cmd)
ser.flush()
g = ser.write(cmd_bytes)
steering_logger.info("sent get motion cmd")
response = ''
sleep(0.05)
while ser.in_waiting > 0:
    response += ser.read().decode('utf-8')

print(response)
