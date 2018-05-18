import keras
import h5py
from Model import build_cnn
import cv2
import csv
import os
import numpy as np
from csv_editor import csv_Editor




def data_setup():
    os.chdir("testdata/")
    #create variable lists
    images = []
    steering = []
    hsv_images = []
    imagename = []

    csv_file_name = "aug.csv"
    with open(csv_file_name) as csv_file:
        reader = csv.reader(csv_file)
        for row in reader:
            img = cv2.imread(row[0])
            ang = row[2]
            img = np.array(img, dtype=np.float64)
            img = img[:,:,:] - 128
            img = img[:,:,:] *(1/128)
            imagename.append(row[0])
            images.append(img)
            steering.append(ang)

    #convert images to hsv color space
    for i in images:
        hsv_images.append(cv2.cvtColor(i,cv2.COLOR_RGB2HSV))
    
    images = np.array(hsv_images)
    steering = np.array(steering)
    #create list with all nparrays
    npdata=(images,steering,imagename)
    return npdata

def Main():
    csvfile = 'testdata/'
    edittype = 'a'
    editor = csv_Editor(csvfile,edittype)
    model = build_cnn()
    npdata = data_setup()
    image_name = npdata[2]
    images = npdata[0]
    steering = npdata[1]
    prediction = model.predict(images)

    editor.editCsv(image_name,steering,prediction)
    

if __name__ == '__main__':
    Main()