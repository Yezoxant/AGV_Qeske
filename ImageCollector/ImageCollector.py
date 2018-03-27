from time import sleep,strftime
from picamera import PiCamera
import RPi.GPIO as GPIO
import Capture_Steering as steering
from csv_editor import csv_Editor
import time
import os


#constants for pin numbers
INPUT_SAVE_DATA_PIN = 11
OUTPUT_PROGRAM_RUNNING_PIN = 5
OUTPUT_ERROR_PIN = 7
OUTPUT_SAVE_DATA_PIN = 3

save_data_active = False #global var for datasaving status



def init_GPIO():
    GPIO.setmode(GPIO.BOARD) #BOARD pin numbering
    GPIO.setup(INPUT_SAVE_DATA_PIN,GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #Use pin3 as input, pulldown so default state always 0
    GPIO.setup(OUTPUT_PROGRAM_RUNNING_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin5 as output for feedback LED:Program running
    GPIO.setup(OUTPUT_ERROR_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin7 as output for feedback LED:Error
    GPIO.setup(OUTPUT_SAVE_DATA_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin11 as output for feedback LED:Saving data

    GPIO.add_event_detect(INPUT_SAVE_DATA_PIN, GPIO.RISING)
    GPIO.add_event_callback(INPUT_SAVE_DATA_PIN,button_pushed)

#initialize camera
def initCamera():
    global camera
    camera = PiCamera()
    camera.resolution = (480, 320)
    camera.start_preview()
    sleep(2)
    camera.stop_preview()



def button_pushed(*args, **kwargs):
    #Callback function when save data button pressed
    global save_data_active
    save_data_active = not save_data_active  # If button pressed switch mode
    print("Button pushed, savedat ="+str(save_data_active)) # Print save_data_active is true or false
    if save_data_active:
        GPIO.output(OUTPUT_SAVE_DATA_PIN,GPIO.HIGH) #Turn led on/off
        editor.open() # Open csv file
    else:
        GPIO.output(OUTPUT_SAVE_DATA_PIN, GPIO.LOW)  # Turn led on/off



def captureImageLoop():
    # Create variables
    global save_data_active
    global path
    global editor
    path = '/media/pi/RASPBERRY/CapturedData/'
    csvfile = '/media/pi/RASPBERRY/data.csv'
    edittype = 'a'
    editor = csv_Editor(csvfile,edittype)
    image_counter = editor.checkFolder(path)

    while True:
        while save_data_active:
            start = time.time()
            print("Entered imagecapture loop")
            timestamp = strftime("%d-%m_%H-%M-%S") #TODO:Add milliseconds
            picturename = 'img({})_{}.jpeg'.format(image_counter,timestamp)
            test = time.time()
            print((test-start),"seconds: test loop1")
            picturesave = os.path.join(path,picturename)

            motion = steering.get_motion()
            throttle, steeringinput = motion
            test = time.time()
            print((test-start),"seconds: test loop2")

            if motion == None:
                GPIO.output(OUTPUT_ERROR_PIN, GPIO.HIGH)
                break
            else:
                camera.capture(picturesave)
                image_counter += 1
                test = time.time()
                print((test-start),"seconds: test loop3")
                editor.editCsv(picturename,throttle, steeringinput)#editing csv file
                test = time.time()
                print((test-start),"seconds: test loop4")

            stop = time.time()
            execTime = stop - start
            print((stop-start),"seconds: end loop")
            while execTime < 1:
                execTime = time.time()-start
            print(execTime)

        while not save_data_active: #Loop during pause status
            print("Waiting for button press")
            if hasattr(editor,'file'):
                editor.close()
            sleep(0.5)


        
if __name__ == "__main__":
    init_GPIO()
    initCamera()
    steering.init_serial()
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, GPIO.HIGH)
    captureImageLoop()
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, GPIO.LOW)

