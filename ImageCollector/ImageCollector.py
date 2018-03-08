from time import sleep
from picamera import PiCamera


def captureImage():
    camera = PiCamera()
    camera.resolution = (320, 240)
    camera.start_preview()
    sleep(2)
    while (active):
        camera.capture('test.jpeg')
    camera.stop_preview()


def activateCamera():
    global active
    active = True
    captureImage()


def deactivateCamera():
    global active
    active = False
