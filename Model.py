import numpy as np
#import pandas as pd
import csv
import cv2
#import random
#import matplotlib.image as mpimg
#mpl.use('Agg')

import argparse
import os
import time

#from os import path
# from collections import defaultdict
# from numpy import sin, cos
# from scipy.misc import imread, imresize, imsave
# from scipy import ndimage
# from scipy import misc

from keras.callbacks import EarlyStopping, ModelCheckpoint
from keras.models import Model, Sequential
#from keras.preprocessing import image
from keras.layers import Dense, Flatten, Dropout, Input, BatchNormalization
from keras.layers import Convolution2D, MaxPooling2D, AveragePooling2D, GlobalAveragePooling2D
#from keras.layers.advanced_activations import ELU
from keras.optimizers import Adam
#from keras import backend as K


def build_cnn():

    img_input = Input(shape=(66,200,3))

    # Layer 1
    x = Convolution2D(24, 5, 5, activation='elu', subsample=(2, 2), border_mode='valid', init='he_normal')(img_input)

    # Layer 2
    x = Convolution2D(36, 5, 5, activation='elu', subsample=(2, 2), border_mode='valid', init='he_normal')(x)

    # Layer 3
    x = Convolution2D(48, 5, 5, activation='elu', subsample=(2, 2), border_mode='valid', init='he_normal')(x)

    # Layer 4
    x = Convolution2D(64, 3, 3, activation='elu', subsample=(1, 1), border_mode='valid', init='he_normal')(x)

    # Layer 5
    x = Convolution2D(64, 3, 3, activation='elu', subsample=(1, 1), border_mode='valid', init='he_normal')(x)

    # Flatten
    y = Flatten()(x)

    #FC1 added after reviewing
    y = Dense(1164, activation='elu', kernel_initializer='he_normal')(y)

    # FC 2
    y = Dense(100, activation='elu', kernel_initializer='he_normal')(y)

    # FC 3
    y = Dense(50, activation='elu', kernel_initializer='he_normal')(y)

    # FC 4
    y = Dense(10, activation='elu', kernel_initializer='he_normal')(y)

    # Output Layer
    y = Dense(1, kernel_initializer='he_normal')(y)

    model = Model(input=img_input, output=y)
    #for layer in model.layers[:3]:
    #    layer.trainable = False
    model.compile(optimizer=Adam(lr=1e-4), loss = 'mse')
    #model.load_weights("model.h5")

    return model

def data_setup():
    os.chdir("Winkelrekken\Winkelrekken_aug")
    images = []
    steering = []

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

    images = np.array(images)
    steering = np.array(steering)
    npdata=(images,steering)
    return npdata

def main():
    #arguments to change for running
    parser = argparse.ArgumentParser(prog="Autonomous driving cart")

    parser.add_argument('--batch_size', type=int, default=64, help='training batch size')
    parser.add_argument('--epoch', type=int, default=10, help='# of training epoch')
    parser.add_argument('--vsplit', type=float, default=0.1, help='# of training epoch')
    args = parser.parse_args()


    model = build_cnn()
    #gather all the data
    npdata = data_setup()
    images = npdata[0]
    steering = npdata[1]
    os.chdir("../")
    #save model into new file every epoch
    callbacks = [ModelCheckpoint('model_sim_{epoch:02d}.h5', monitor='val_loss', verbose=0, save_best_only=False,save_weights_only=False,mode='min')]
    #run the model
    model.fit(images,
              steering,
              batch_size=args.batch_size
              ,epochs=args.epoch
              ,verbose=1
              ,callbacks=callbacks,
              validation_split= args.vsplit)

    #Testing model

if __name__ == '__main__':
    main()