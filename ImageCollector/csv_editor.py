import os
import csv

class csv_Editor():

    def __init__(self, csvfile, edittype):
        self.csvfile = csvfile
        self.edittype = edittype
        self.fileopen = False
    
    def open(self):
        self.file = open(csvfile, edittype)

    def checkFolder(self, path):
        files = os.listdir(path)
        image_counter = len(files)
        image_counter += 1
        return image_counter

    def editCsv(self, picture, throttle, steer):
        writer = csv.writer(self.file)
        writer.writerow([picture, throttle, steer])

    def close(self):
        self.file.close()
