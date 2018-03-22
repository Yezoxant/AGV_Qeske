from time import sleep,strftime
from picamera import PiCamera
import RPi.GPIO as GPIO
import os
import csv
import serial
import re
import time

#constants for pin numbers
INPUT_SAVE_DATA_PIN = 3
OUTPUT_PROGRAM_RUNNING_PIN = 5
OUTPUT_ERROR_PIN = 7
OUTPUT_SAVE_DATA_PIN = 11

save_data_active = False #global var for datasaving status

def button_pushed(*args, **kwargs):
    #Callback function when save data button pressed

    global save_data_active
    save_data_active = not save_data_active  # If button pressed switch mode
    print("Button pushed, savedat ="+str(save_data_active))
    if save_data_active:
        GPIO.output(OUTPUT_SAVE_DATA_PIN,GPIO.HIGH) #Turn led on/off
    else:
        GPIO.output(OUTPUT_SAVE_DATA_PIN, GPIO.LOW)  # Turn led on/off

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


def init_GPIO():
    GPIO.setmode(GPIO.BOARD) #BOARD pin numbering
    GPIO.setup(INPUT_SAVE_DATA_PIN,GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #Use pin3 as input, pulldown so default state always 0
    GPIO.setup(OUTPUT_PROGRAM_RUNNING_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin5 as output for feedback LED:Program running
    GPIO.setup(OUTPUT_ERROR_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin7 as output for feedback LED:Error
    GPIO.setup(OUTPUT_SAVE_DATA_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin11 as output for feedback LED:Saving data

    GPIO.add_event_detect(INPUT_SAVE_DATA_PIN, GPIO.RISING)
    GPIO.add_event_callback(INPUT_SAVE_DATA_PIN,button_pushed)

def checkFolder():
    global image_counter
    files = os.listdir(path)
    image_counter = len(files)
    image_counter += 1

def initCamera():
    global camera
    camera = PiCamera()
    camera.resolution = (320, 240)
    camera.start_preview()
    sleep(2)
    camera.stop_preview()


def captureImageLoop():
    global save_data_active
    global path
    global image_counter
    path = '/media/pi/RASPBERRY/CapturedData/'
    checkFolder()

    while True:
        while save_data_active:
            timestamp = strftime("%d-%m_%H-%M-%S") #TODO:Add milliseconds
            start = time.time()
            filename = 'img({})_{}.jpeg'.format(image_counter,timestamp)
            picturesave = os.path.join(path,filename)
            print("Entered imagecapture loop")
            steerinput = get_motion()
            if steerinput == None:
                GPIO.output(OUTPUT_ERROR_PIN, GPIO.HIGH)
                break
            else:
                camera.capture(picturesave)
                image_counter += 1
                editCsv(filename,steerinput)#Save path & steering input to csv file
            stop = time.time()
            print (stop-start), "seconds"
            sleep(0.5)
        while not save_data_active: #Loop during pause status
            print("Waiting for button press")
            sleep(0.5)

def editCsv(picture,steer): 
    with open('/media/pi/RASPBERRY/data.csv','a')as file:
        writer = csv.writer(file)
        writer.writerow([picture, steer])
        
if __name__ == "__main__":
    init_GPIO()
    initCamera()
    init_serial()
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, GPIO.HIGH)
    captureImageLoop()
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, GPIO.LOW)

