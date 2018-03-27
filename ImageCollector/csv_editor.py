import os
import csv

class csv_Editor():

    def __init__(self, csvfile, edittype):
        self.csvfile = csvfile
        self.edittype = edittype
        self.fileopen = False
    
    def open(self):
        self.file = open(self.csvfile, self.edittype)

    def checkFolder(self, path):
        files = os.listdir(path)
        image_counter = len(files)
        image_counter += 1
        return image_counter

    def editCsv(self, picture, throttle, steer):
        writer = csv.writer(self.file)
        writer.writerow([picture, throttle, steer])

    def close(self):
        if not self.file.closed:
            self.file.close()
            print('file is closed')
