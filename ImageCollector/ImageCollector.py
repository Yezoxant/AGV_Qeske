from time import sleep,strftime
from picamera import PiCamera
import RPi.GPIO as GPIO

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


def init_GPIO():
    GPIO.setmode(GPIO.BOARD) #BOARD pin numbering
    GPIO.setup(INPUT_SAVE_DATA_PIN,GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #Use pin3 as input, pulldown so default state always 0
    GPIO.setup(OUTPUT_PROGRAM_RUNNING_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin5 as output for feedback LED:Program running
    GPIO.setup(OUTPUT_ERROR_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin7 as output for feedback LED:Error
    GPIO.setup(OUTPUT_SAVE_DATA_PIN,GPIO.OUT,initial=GPIO.LOW) #Use pin11 as output for feedback LED:Saving data

    GPIO.add_event_detect(INPUT_SAVE_DATA_PIN, GPIO.RISING)
    GPIO.add_event_callback(INPUT_SAVE_DATA_PIN,button_pushed)



def initCamera():
    global camera
    camera = PiCamera()
    camera.resolution = (320, 240)
    camera.start_preview()
    sleep(2)
    camera.stop_preview()


def captureImageLoop():
    image_counter = 0
    global save_data_active
    while True:
        while save_data_active:

            print("Entered imagecapture loop")
            timestamp = strftime("%d-%m-%H-%M-%S") #TODO:Add milliseconds
            path = '/media/pi/RASPBERRY/CapturedData/img'+str(image_counter)+"_"+timestamp+'.jpeg'
            camera.capture(path)
            image_counter += 1
            #TODO:Steeringinput capture
            #TODO:Save path & steering input to csv file

            sleep(0.5)


        while not save_data_active: #Loop during pause status
            print("Waiting for button press")
            sleep(0.5)



if __name__ == "__main__":
    init_GPIO()
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, True)
    initCamera()
    captureImageLoop()
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, False)

