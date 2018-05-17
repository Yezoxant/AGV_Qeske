import csv
import numpy as np
import cv2
import os

images = np.array()
steering = np.array()

csv_file_name = "augdata.csv"
with open(csv_file_name) as csv_file:
    reader = csv.reader(csv_file)
    for row in reader:
        img = cv2.imread(row[0])
        ang = row[2]
        img = np.array(img, dtype=np.float64)
        img = img[:,:,:] - 128
        img = img[:,:,:] *(1/128)

        images.append(img)
        steering.append(ang)