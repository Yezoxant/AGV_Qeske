import cv2
import csv,time
import numpy as np
import os
import shutil


class AugmentDataset():

    def __init__(self,csv_path):
        self.csv_path = csv_path
        self.images = []


        with open(csv_path,"r") as csv_file:
            reader = csv.reader(csv_file)
            for row in reader:
                # Load all source images and steering inputs. images is a list of lists [[[cv2 img_matrix],throttle,steering],...]
                self.images.append([cv2.imread(row[0]),row[1],row[2]])

    def run_augmentation(self,output_folder, brightness = False, shadows = False, horizontal_translate = False, hflip = False, blur = False):

        os.mkdir(output_folder)
        os.chdir(output_folder)
        augdata_csv = open("augdata.csv", "w")
        writer = csv.writer(augdata_csv)
        count = 0

        base_chance = 2 #chance of applying an augment is 1/base_chance
        if shadows:

            for image in self.images:
                if np.random.randint(1, base_chance) == 1:
                    out_img = self.add_random_shadow(image[0])
                    img_name = "image" + str(count) + ".jpeg"
                    cv2.imwrite(img_name,out_img)
                    writer.writerow([img_name,image[1],image[2]])
                    count += 1

        if brightness:

            for image in self.images:
                if np.random.randint(1, base_chance) == 1:
                    out_img = self.augment_brightness_camera_images(image[0])
                    img_name = "image"+str(count)+".jpeg"
                    cv2.imwrite(img_name,out_img)
                    writer.writerow([img_name, image[1], image[2]])
                    count += 1

        if hflip:

            for image in self.images:
                if np.random.randint(1, base_chance) == 1:
                    out_img = self.augment_brightness_camera_images(image[0])
                    img_name = "image"+str(count)+".jpeg"
                    cv2.imwrite(img_name,out_img)
                    writer.writerow([img_name, image[1], float(image[2])*-1]) #invert steering
                    count += 1

        if horizontal_translate:

            for image in self.images:
                if np.random.randint(1, base_chance) == 1:
                    out_img = self.trans_image(image[0],image[2],0.01)
                    img_name = "image" + str(count) + ".jpeg"
                    cv2.imwrite(img_name, out_img)
                    writer.writerow([img_name, image[1], float(image[2])*-1])
                    count += 1
        if blur:
            pass

        augdata_csv.close()


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
        steer_ang = float(steer) + tr_x / trans_range * 2 * .2
        # tr_y = 40 * np.random.uniform() - 40 / 2
        # tr_y = 0
        Trans_M = np.float32([1, 0, tr_x])
        image_tr = cv2.warpAffine(image, Trans_M, (cols, rows))

        return image_tr, steer_ang


    def add_random_shadow(self,image):

        top_y = 320 * np.random.uniform()
        top_x = 0
        bot_x = 160
        bot_y = 320 * np.random.uniform()
        image_hsv = cv2.cvtColor(image, cv2.COLOR_RGB2HSV)
        shadow_mask = 0 * image_hsv[:, :, 1]
        X_m = np.mgrid[0:image.shape[0], 0:image.shape[1]][0]
        Y_m = np.mgrid[0:image.shape[0], 0:image.shape[1]][1]
        shadow_mask[((X_m - top_x) * (bot_y - top_y) - (bot_x - top_x) * (Y_m - top_y) >= 0)] = 1
        # random_bright = .25+.7*np.random.uniform()
        random_bright = .5
        cond1 = shadow_mask == 1
        cond0 = shadow_mask == 0
        if np.random.randint(2) == 1:
            image_hsv[:, :, 1][cond1] = image_hsv[:, :, 1][cond1] * random_bright
        else:
            image_hsv[:, :, 1][cond0] = image_hsv[:, :, 1][cond0] * random_bright
        image = cv2.cvtColor(image_hsv, cv2.COLOR_RGB2HSV)
        return image


    def hflip(self,image):
        image_yuv = cv2.cvtColor(image, cv2.COLOR_RGB2HSV)
        out_img = cv2.flip(image_yuv,0)
        return out_img




if __name__ == "__main__":
    os.chdir("dataset_1/")
    data1 = AugmentDataset("data.csv")
    if os.path.isdir("output_1"):
        print("removing old output dir..")
        shutil.rmtree("output_1",ignore_errors=True)
    data1.run_augmentation("output_1",brightness=False,shadows=False,hflip=False,horizontal_translate=True)



