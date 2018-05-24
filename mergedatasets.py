import csv
import cv2
import argparse
import os
import shutil

parser = argparse.ArgumentParser(description='merge datasets')
parser.add_argument('--folder1', type=str)
parser.add_argument('--folder2', type=str)
parser.add_argument('--outputfolder', type=str)
args = parser.parse_args()
folder1 = args.folder1
folder2 = args.folder2
outputfolder = args.outputfolder

csv_path1 = folder1 + "/augdata.csv"
print("csv1:" + csv_path1)
csv_path2 = folder2 + "/augdata.csv"
print("csv2:" + csv_path2)
os.mkdir(outputfolder)
with open(outputfolder + "/augdata.csv", "w", newline='') as writefile:
    writer = csv.writer(writefile)
    counter = 0
    with open(csv_path1, "r") as csv_file1:

        reader1 = csv.reader(csv_file1)
        print("copying folder1")
        for row in reader1:
            filename = "img" + str(counter) + ".png"
            shutil.copyfile(folder1 + "/" + row[0], outputfolder + "/" + filename)
            writer.writerow([filename, row[1], row[2]])
            counter += 1
        print("done")

    with open(csv_path2, "r") as csv_file2:
        reader2 = csv.reader(csv_file2)
        print("copying folder2")
        for row in reader2:
            filename = "img" + str(counter) + ".png"
            shutil.copyfile(folder2 + "/" + row[0], outputfolder + "/" + filename)
            writer.writerow([filename, row[1], row[2]])
            counter += 1
        print("done")






