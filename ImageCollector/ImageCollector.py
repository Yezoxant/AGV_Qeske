from time import sleep
from picamera import PiCamera


def captureImage():
    camera = PiCamera()
    camera.resolution = (320, 240)
    camera.start_preview()
    sleep(2)
    while(active == true):
        camera.capture('test.jpeg')
    camera.stop_preview()

def ActivateCamera():
    global active
    active = True
    captureImage()

def DeactivateCamera():
    global active
    active = False
