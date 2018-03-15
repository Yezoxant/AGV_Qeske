import RPi.GPIO as GPIO
from time import sleep

GPIO.setmode(GPIO.BCM)  # BCM pin numbering
GPIO.setup(3, GPIO.IN)  # Use pin3 as input

while True:
    active = GPIO.input(3)
    print(active)
    sleep(1)