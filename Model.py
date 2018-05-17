import numpy as np
import pandas as pd
import csv
import cv2
import random
import matplotlib as mpl
import matplotlib.image as mpimg
mpl.use('Agg')
import matplotlib.pyplot as plt
import argparse
import os
import time

from os import path
from collections import defaultdict
from numpy import sin, cos
from scipy.misc import imread, imresize, imsave
from scipy import ndimage
from scipy import misc

from keras.callbacks import EarlyStopping, ModelCheckpoint
from keras.models import Model, Sequential
from keras.preprocessing import image
from keras.layers import Dense, Flatten, Dropout, Input, BatchNormalization
from keras.layers import Convolution2D, MaxPooling2D, AveragePooling2D, GlobalAveragePooling2D
from keras.layers.advanced_activations import ELU
from keras.optimizers import Adam
from keras import backend as K

from PIL import Image
from PIL import ImageOps


def build_cnn(image_size=None,weights_path=None):
    

    img_input = Input(66,200,3)

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

    # FC 1
    y = Dense(100, activation='elu', init='he_normal')(y)

    # FC 2
    y = Dense(50, activation='elu', init='he_normal')(y)

    # FC 3
    y = Dense(10, activation='elu', init='he_normal')(y)

    # Output Layer
    y = Dense(1, init='he_normal')(y)

    model = Model(input=img_input, output=y)
    model.compile(optimizer=Adam(lr=1e-4), loss = 'mse')
    return model

def normalize_input(x):
    return (x - 128.) / 128.

def main():
    parser = argparse.ArgumentParser(prog="Autonomous driving cart")
    parser.add_argument('batch_size', type=int, default=64, help='training batch size')
    parser.add_argument('epoch', type=int, default=10, help='# of training epoch')