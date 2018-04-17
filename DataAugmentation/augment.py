import cv2
import csv,time
import numpy as np
import os

def preprocessImage(image):
    shape = image.shape
    # note: numpy arrays are (row, col)!
    image = image[math.floor(shape[0] / 5):shape[0] - 25, 0:shape[1]]
    image = cv2.resize(image, (new_size_col, new_size_row), interpolation=cv2.INTER_AREA)
    # image = image/255.-.5
    return image


def preprocess_image_file_train(line_data):
    i_lrc = np.random.randint(3)
    if (i_lrc == 0):
        path_file = line_data['left'][0].strip()
        shift_ang = .25
    if (i_lrc == 1):
        path_file = line_data['center'][0].strip()
        shift_ang = 0.
    if (i_lrc == 2):
        path_file = line_data['right'][0].strip()
        shift_ang = -.25
    y_steer = line_data['steer_sm'][0] + shift_ang
    image = cv2.imread(path_file)
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    image, y_steer, tr_x = trans_image(image, y_steer, 100)
    image = augment_brightness_camera_images(image)
    image = preprocessImage(image)
    image = np.array(image)
    ind_flip = np.random.randint(2)
    if ind_flip == 0:
        image = cv2.flip(image, 1)
        y_steer = -y_steer

    return image, y_steer




class AugmentDataset():

    def __init__(self,csv_path):
        self.csv_path = csv_path
        self.images = []

        with open(csv_path,"r") as csv_file:
            ff = csv.reader(csv_file)
            for row in ff:
                self.images.append(cv2.imread(row[0])) # Load all base images into the object

    def run_augmentation(self,output_folder):
        os.mkdir(output_folder)
        os.chdir(output_folder)
        count = 0
        for image in self.images:

            out_img = self.add_random_shadow(image)
            cv2.imwrite("image"+str(count)+".jpeg",out_img)
            count += 1

        for image in self.images:

            out_img = self.augment_brightness_camera_images(image)
            cv2.imwrite("image"+str(count)+".jpeg",out_img)
            count += 1

    def augment_brightness_camera_images(self,image):
        image1 = cv2.cvtColor(image, cv2.COLOR_RGB2HSV)
        image1 = np.array(image1, dtype=np.float64)
        random_bright = .5 + np.random.uniform()
        image1[:, :, 2] = image1[:, :, 2] * random_bright
        image1[:, :, 2][image1[:, :, 2] > 255] = 255
        image1 = np.array(image1, dtype=np.uint8)
        image1 = cv2.cvtColor(image1, cv2.COLOR_HSV2RGB)
        return image1

    def trans_image(self,image, steer, trans_range):
        # Translation
        tr_x = trans_range * np.random.uniform() - trans_range / 2
        steer_ang = steer + tr_x / trans_range * 2 * .2
        tr_y = 40 * np.random.uniform() - 40 / 2
        # tr_y = 0
        Trans_M = np.float32([[1, 0, tr_x], [0, 1, tr_y]])
        image_tr = cv2.warpAffine(image, Trans_M, (cols, rows))

        return image_tr, steer_ang

    def add_random_shadow(self,image):
        top_y = 320 * np.random.uniform()
        top_x = 0
        bot_x = 160
        bot_y = 320 * np.random.uniform()
        image_hls = cv2.cvtColor(image, cv2.COLOR_RGB2HLS)
        shadow_mask = 0 * image_hls[:, :, 1]
        X_m = np.mgrid[0:image.shape[0], 0:image.shape[1]][0]
        Y_m = np.mgrid[0:image.shape[0], 0:image.shape[1]][1]
        shadow_mask[((X_m - top_x) * (bot_y - top_y) - (bot_x - top_x) * (Y_m - top_y) >= 0)] = 1
        # random_bright = .25+.7*np.random.uniform()
        if np.random.randint(2) == 1:
            random_bright = .5
            cond1 = shadow_mask == 1
            cond0 = shadow_mask == 0
            if np.random.randint(2) == 1:
                image_hls[:, :, 1][cond1] = image_hls[:, :, 1][cond1] * random_bright
            else:
                image_hls[:, :, 1][cond0] = image_hls[:, :, 1][cond0] * random_bright
        image = cv2.cvtColor(image_hls, cv2.COLOR_HLS2RGB)
        return image




if __name__ == "__main__":
    os.chdir("dataset_1/")
    data1 = AugmentDataset("data.csv")
    data1.run_augmentation("output_1")



