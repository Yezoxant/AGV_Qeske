import RPi.GPIO as GPIO
from time import sleep

GPIO.setmode(GPIO.BCM)  # BCM pin numbering
GPIO.setup(3, GPIO.IN)  # Use pin3 as input
GPIO.setup(5,GPIO.IN)

while True:
    save_data_active = GPIO.input(3)
    test = GPIO.input(5)
    print(test)
    print(save_data_active)