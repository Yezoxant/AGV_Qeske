from time import sleep
import time
from picamera import PiCamera

def captureImage():
    camera = PiCamera()
    camera.resolution = (320, 240)
    camera.start_preview()
    sleep(2)
    while (active):
        timestamp = time.strftime("%d-$m_%H:%M:%S")
        camera.capture('/media/pi/RASPBERRY/img'+timestamp+'.jpeg')
    camera.stop_preview()


def activateCamera():
    global active
    active = True
    captureImage()


def deactivateCamera():
    global active
    active = False
