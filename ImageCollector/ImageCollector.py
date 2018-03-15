from time import sleep
from picamera import PiCamera
import RPi.GPIO as GPIO

def init_GPIO():
    GPIO.setmode(GPIO.BCM) #BCM pin numbering
    GPIO.setup(3,GPIO.IN) #Use pin3 as input
    GPIO.setup(5,GPIO.OUT) #Use pin5 as output for green feedback LED
    GPIO.setup(7,GPIO.OUT) #Use pin7 as output for red feedback LED


def captureImage():

    active = True
    camera = PiCamera()
    camera.resolution = (320, 240)
    camera.start_preview()
    sleep(2)

    while active:


        timestamp = time.strftime("%d-$m_%H:%M:%S") #TODO:Add milliseconds
        camera.capture('/media/pi/RASPBERRY/img'+timestamp+'.jpeg')
        #TODO:Steeringinput capture
        #TODO:Save path & steering input to csv file

        active = GPIO.input(3) #Check pause status
        while nowTime < captureTime + 1:
            nowTime =
            if active: #break if pause button is pressed
                active  = False
                #TODO:wait for depress
                break

    while not active: #Loop during pause status
        active = GPIO.input(3)
        if active: #break if pause button is pressed again (back to main loop)
            # TODO:wait for depress
            break



# def activateCamera():
#     global active
#     active = True
#     captureImage()
#
#
# def deactivateCamera():
#     global active
#     active = False

if __name__ == "__main__":
    while True:
        captureImage()


