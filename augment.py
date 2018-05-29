import cv2
import csv,time
import numpy as np
import os
import shutil
import argparse


class AugmentDataset():

    def __init__(self,csv_path, chance = 1):
        self.csv_path = csv_path
        self.images = []
        self.base_chance = chance

        with open(csv_path,"r") as csv_file:
            reader = csv.reader(csv_file)
            loadcount = 0
            for row in reader:
                # Load all source images and steering inputs. images is a list of lists [[[cv2 img_matrix],throttle,steering],...]
                if loadcount < 50:
                    self.save_image("img"+str(loadcount),cv2.imread(row[0]))
                    loadcount+= 1
                    continue #Go to next for loop iteration (skips everything under this)

                if row[1] == "0.0": #filter out 0 throttle images
                    continue
                if row[2] == '0.0':
                    if np.random.randint(0, 2) == 0:
                        continue
                self.images.append([cv2.imread(row[0]), row[1], row[2]])

        if self.images[0] == None:
            print("No images imported")


    def run_augmentation(self,output_folder, brightness = False, shadows = False, hflip = False, blur = False):
        #This method runs the selected augmentations on a dataset of pictures with steering angles.
        os.mkdir(output_folder)
        os.chdir(output_folder)
        augdata_csv = open("augdata.csv", 'a',newline='')
        writer = csv.writer(augdata_csv)
        count = 0

        base_chance = self.base_chance #chance of applying an augment is 1/base_chance

        for image in self.images:
            img = image[0]
            img_name = "image" + str(count) + ".jpeg"
            self.save_image(img_name, img)
            writer.writerow([img_name, image[1], image[2]])
            count += 1

        if shadows:

            for image in self.images:

                if np.random.randint(0, base_chance) == 0:
                    out_img = self.add_random_shadow(image[0])
                    img_name = "image" + str(count) + ".jpeg"
                    self.save_image(img_name,out_img)
                    writer.writerow([img_name,image[1],image[2]])
                    count += 1

        if brightness:

            for image in self.images:

                if np.random.randint(0, base_chance) == 0:
                    out_img = self.augment_brightness_camera_images(image[0])
                    img_name = "image"+str(count)+".jpeg"
                    self.save_image(img_name,out_img)
                    writer.writerow([img_name, image[1], image[2]])
                    count += 1

        if hflip:

            for image in self.images:

                #if np.random.randint(0, base_chance) == 0:
                out_img = self.hflip(image[0])
                img_name = "image"+str(count)+".jpeg"
                self.save_image(img_name,out_img)
                writer.writerow([img_name, image[1], float(image[2])*-1]) #invert steering angle
                count += 1

        if horizontal_translate:

            for image in self.images:

                if np.random.randint(0, base_chance) == 0:
                    out_img = self.trans_image(image[0],image[2],0.01)
                    img_name = "image" + str(count) + ".jpeg"
                    self.save_image(img_name, out_img)
                    writer.writerow([img_name, image[1], float(image[2])*-1])
                    count += 1
        if blur:
            for image in self.images:

                if np.random.randint(0, base_chance) == 0:
                    out_img = self.blur_image(image[0])
                    img_name = "image" + str(count) + ".jpeg"
                    self.save_image(img_name, out_img)
                    writer.writerow([img_name, image[1], float(image[2])*-1])
                    count += 1

        augdata_csv.close()


    def save_image(self,name,image):
        #All images get saved by this method. We set size and colorspace here.
        cropped_image = image[crop_bot:270-crop_top, 0:480] #imsize is 480x270, crop top and botton 70 pixels
        resized_image = cv2.resize(cropped_image,(200,66))
        hsv_image = cv2.cvtColor(resized_image, cv2.COLOR_RGB2HSV)
        cv2.imwrite(name, hsv_image)


    #Augmentation functions: Take an image and output an augmented image
    def blur_image(self,image):
        kernelsize = np.random.randint(3,6)
        image_out = cv2.blur(image,(kernelsize, kernelsize))
        return image_out

    def augment_brightness_camera_images(self,image):

        image1 = cv2.cvtColor(image, cv2.COLOR_RGB2HSV)
        image1 = np.array(image1, dtype=np.float64)
        random_bright = .5 + np.random.uniform()
        image1[:, :, 2] = image1[:, :, 2] * random_bright
        image1[:, :, 2][image1[:, :, 2] > 255] = 255
        image1 = np.array(image1, dtype=np.uint8)
        image1 = cv2.cvtColor(image1, cv2.COLOR_HSV2RGB)
        return image1

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
        random_bright = .5
        cond1 = shadow_mask == 1
        cond0 = shadow_mask == 0
        if np.random.randint(2) == 1:
            image_hls[:, :, 1][cond1] = image_hls[:, :, 1][cond1] * random_bright
        else:
            image_hls[:, :, 1][cond0] = image_hls[:, :, 1][cond0] * random_bright
        image = cv2.cvtColor(image_hls, cv2.COLOR_HLS2RGB)
        return image

    def hflip(self,image):
        out_img = cv2.flip(image,1)
        return out_img



if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='augment data')
    parser.add_argument('--folder', type=str,
                        help='folder with data to be augmented')
    parser.add_argument('--probability',type = int, default = 2,help = "augment chance = 1/x")
    parser.add_argument('--crop_top',type = int, default = 30,help = "crop top")
    parser.add_argument('--crop_bot',type = int, default = 70,help = "crop bottom ")

    args = parser.parse_args()
    folder = args.folder
    prob = args.probability
    crop_top = args.crop_top
    crop_bot = args.crop_bot
    os.chdir(folder)
    data1 = AugmentDataset("data.csv", prob)

    #Set args to False to disable the augmentation tecnique entirely
    data1.run_augmentation(folder + "_aug",brightness=True,shadows=True,hflip=True,blur = True)





