import serial

def init_serial():
    global ser
    ser = serial.Serial(port = "/dev/ttyACM0", baudrate = 115200)

def get_motion():
    cmd = "get motion\n"
    cmd_bytes = str.encode(cmd)
    ser.flush()
    g = ser.write(cmd_bytes)
    response = ''
    sleep(0.1)

    while ser.in_waiting > 0:
        response += ser.read().decode('utf-8')
    res = re.search("(?:.+:)(.+)",response)
    return res.group(1)