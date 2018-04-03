import os
import csv
import logging

csv_logger = logging.getLogger('ImageCollector.csv_editor')

class csv_Editor():

    def __init__(self, csvfile, edittype):
        self.logger = logging.getLogger('ImageCollector.csv_editor')
        self.csvfile = csvfile
        self.edittype = edittype
        self.fileopen = False
        self.logger.info('init finished')
    
    def open(self):
        self.file = open(self.csvfile, self.edittype)
        self.logger.info('opened %s',self.csvfile)

    def checkFolder(self, path):
        files = os.listdir(path)
        image_counter = len(files)
        image_counter += 1
        self.logger.info('Imagecount =%d',image_counter)
        return image_counter

    def editCsv(self, picture, throttle, steer):
        writer = csv.writer(self.file)
        writer.writerow([picture, throttle, steer])
        self.logger.info('Edited csv:%s,%s,%s',str(picture),str(throttle),str(steer))

    def close(self):
        if not self.file.closed:
            self.file.close()
            self.logger.info('closed csv')
