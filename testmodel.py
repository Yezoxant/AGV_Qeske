from keras.models import load_model
# from keras.models import Model
import csv
import numpy as np
from matplotlib import pyplot
import cv2
import time

# os.chdir("testdata/")
# create variable lists

time.sleep(1)
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
            steering.append(ang)
        count += 1

images = np.array(images)
# steering = np.array(steering)

model = load_model('full_model.h5')
predictions = []
predictions = model.predict(images)

#pyplot.plot(range(len(images)),predictions,'g^',range(len(images)), steering, 'g-')