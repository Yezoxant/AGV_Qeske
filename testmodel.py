"""Loads a model saved by the save_model function in keras and then makes a preditions on a random set of images from a dataset.
Plots the output predicted by the netwerk vs the actual steering angle recorded."""

from keras.models import load_model
# from keras.models import Model
import csv
import numpy as np
from matplotlib import pyplot
import matplotlib
import cv2
import time

# os.chdir("testdata/")
# create variable lists
def random_datatest():

    images = []
    steering = []

    csv_file_name = "augdata.csv"
    with open(csv_file_name) as csv_file:
        reader = csv.reader(csv_file)
        row_count = sum(1 for row in reader)
        randomrows = []
        for n in range(10):  # Select 10 images randomly
            randomrows.append(np.random.randint(row_count))
        count = 0
        csv_file.seek(0)  # reset csv counter
        for row in reader:
            if count in randomrows:
                img = cv2.imread(row[0])

                ang = row[2]
                img = np.array(img, dtype=np.float64)
                img = img[:, :, :] - 128
                img = img[:, :, :] * (1 / 128)
                images.append(img)
                steering.append(float(ang))
            count += 1

    images = np.array(images)

    model = load_model('full_model.h5')
    predictions = model.predict(images)
    l_predictions = []
    for p in predictions:
        l_predictions.append(p)
        print(p)
    print(steering)
    #
    predict, = pyplot.plot(l_predictions,'ro',label='prediction')
    actual, = pyplot.plot(steering,'bo',label='actual')#range(len(images))

    pyplot.legend(handles=[predict , actual])
    pyplot.savefig("modeltest")


def continuous_datatest():

    images = []
    steering = []
    csv_file_name = "augdata.csv"
    with open(csv_file_name) as csv_file:
        reader = csv.reader(csv_file)
        count = 0
        csv_file.seek(0)  # reset csv counter
        for row in reader:
            img = cv2.imread(row[0])

            ang = row[2]
            img = np.array(img, dtype=np.float64)
            img = img[:, :, :] - 128
            img = img[:, :, :] * (1 / 128)
            images.append(img)
            steering.append(float(ang))
            count += 1

    images = np.array(images)

    model = load_model('full_model.h5')
    predictions = model.predict(images)
    l_predictions = []
    for p in predictions:
        l_predictions.append(p)
        print(p)
    print(steering)

    predict, = pyplot.plot(l_predictions, 'ro', label='prediction')
    actual, = pyplot.plot(steering, 'bo', label='actual')  # range(len(images))

    pyplot.legend(handles=[predict, actual])
    pyplot.savefig("modeltest")

if __name__ == "__main__":
    #random_datatest()
    continuous_datatest()