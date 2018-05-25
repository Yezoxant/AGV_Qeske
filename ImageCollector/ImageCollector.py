from time import sleep, strftime
from picamera import PiCamera
import RPi.GPIO as GPIO
import Capture_Steering as steering
from csv_editor import csv_Editor
import time, logging, os
import numpy as np
from PIL import Image
import io

# constants for pin numbers
REMOTE_PIN = 11
OUTPUT_PROGRAM_RUNNING_PIN = 12
OUTPUT_ERROR_PIN = 15
OUTPUT_SAVE_DATA_PIN = 13

save_data_active = False  # global var for datasaving status


def init_GPIO():
    GPIO.setmode(GPIO.BOARD)  # BOARD pin numbering
    GPIO.setup(REMOTE_PIN, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)  # Use pin3 as input, pulldown so default state always 0
    GPIO.setup(OUTPUT_PROGRAM_RUNNING_PIN, GPIO.OUT,
               initial=GPIO.LOW)  # Use pin5 as output for feedback LED:Program running
    GPIO.setup(OUTPUT_ERROR_PIN, GPIO.OUT, initial=GPIO.LOW)  # Use pin7 as output for feedback LED:Error
    GPIO.setup(OUTPUT_SAVE_DATA_PIN, GPIO.OUT, initial=GPIO.LOW)  # Use pin11 as output for feedback LED:Saving data

    GPIO.add_event_detect(REMOTE_PIN, GPIO.RISING)
    GPIO.add_event_callback(REMOTE_PIN, button_pushed)


# initialize camera
def initCamera():
    global camera
    camera = PiCamera()
    camera.resolution = (480, 270)
    camera.vflip = True
    camera.hflip = True
    camera.exposure_mode = 'sports'
    camera.framerate = 32
    camera.start_preview()
    sleep(2)
    camera.stop_preview()


def button_pushed(*args, **kwargs):
    # Callback function when save data button pressed
    global save_data_active
    save_data_active = not save_data_active  # If button pressed switch mode
    print("Button pushed, savedat =" + str(save_data_active))  # Print save_data_active is true or false
    if save_data_active:
        GPIO.output(OUTPUT_SAVE_DATA_PIN, GPIO.HIGH)  # Turn led on/off
        editor.open()  # Open csv file
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
    editor = csv_Editor(csvfile, edittype)
    image_counter = editor.checkFolder(path)

    while True:
        while save_data_active:
            GPIO.output(OUTPUT_SAVE_DATA_PIN, GPIO.HIGH)
            #start = time.time()
            print("Entered imagecapture loop")
            #timestamp = strftime("%d-%m_%H-%M-%S")
            #stream = io.BytesIO()
            os.chdir(path)
            # output = np.empty((272,480,3), dtype=np.uint8)

            for j in camera.capture_continuous("default.jpg", use_video_port=True):
                picturename = 'image{}.jpg'.format(image_counter)
                os.rename("default.jpg",picturename)
                #image = Image.open(stream)
                #image.save(picturename)
                start = time.time()
                motion = steering.get_motion()
                test = time.time()
                print((test - start), "seconds: test loop2")

                if motion == None:
                    for i in range(5):
                        GPIO.output(OUTPUT_ERROR_PIN, GPIO.HIGH)
                        sleep(0.5)
                        GPIO.output(OUTPUT_ERROR_PIN, GPIO.LOW)
                    break
                else:
                    throttle, steeringinput = motion
                    # camera.capture(output, 'rgb', use_video_port=True)
                    timestamp = strftime("%d-%m_%H-%M-%S")
                    test = time.time()
                    print((test - start), "seconds: test loop3")
                    editor.editCsv(picturename, throttle, steeringinput)  # editing csv file
                    image_counter += 1
                    test = time.time()
                    print((test - start), "seconds: test loop4")

                stop = time.time()
                print((stop - start), "seconds: end loop")
                if not save_data_active:
                    print(image_counter)
                    break

        while not save_data_active:  # Loop during pause status.
            GPIO.output(OUTPUT_SAVE_DATA_PIN, GPIO.LOW)
            print("Waiting for button press")
            if hasattr(editor, 'file'):
                editor.close()
            sleep(0.5)


def setLogger():
    global logger
    logger = logging.getLogger('ImageCollector')
    logger.setLevel(logging.DEBUG)
    fh = logging.FileHandler('ImageCollectorLog.log', mode='w')
    formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    fh.setFormatter(formatter)
    fh.setLevel(logging.DEBUG)
    logger.addHandler(fh)
    logger.info("logger setup done")


if __name__ == "__main__":
    setLogger()
    init_GPIO()
    logger.info("initGPIO done")
    initCamera()
    logger.info("initCamera done")
    steering.init_serial()
    logger.info("steering init_serial done")
    GPIO.output(OUTPUT_PROGRAM_RUNNING_PIN, GPIO.HIGH)
    logger.info("started captureImageloop")
    captureImageLoop()

