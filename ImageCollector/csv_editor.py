import os
import csv

def checkFolder():
    image_counter
    files = os.listdir(path)
    image_counter = len(files)
    image_counter += 1
    return image_counter

def editCsv(picture,steer,edittype,csvfile): 
    with open(csvfile,edittype)as file:
        writer = csv.writer(file)
        writer.writerow([picture, steer])