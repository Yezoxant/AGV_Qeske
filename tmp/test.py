def generator(samples, batch_size, training=False):
    """
    Generator for model.fit_generator()
    If training is False, only central camera is used and no augmentation is applied.
    """
    num_samples = len(samples)

    # choose images from every sample and optionally adjust steering angle
    # list of tuples (image_index, steering_adjustment)
    images_and_angles = [
        (0, 0.0),    # central camera
        (1, 0.15),   # left camera
        (2, -0.15),  # right camera
    ] if training else [(0, 0.0)] # just center if not training

    # loop forever so the generator never terminates
    while True:
        # shuffle input data
        sklearn.utils.shuffle(samples)
        for offset in range(0, num_samples, batch_size):
            # get batch
            batch_samples = samples[offset:offset+batch_size]
            images = []
            angles = []
            for batch_sample in batch_samples:
                for batch_sample_index, angle_adjust in images_and_angles:
                    # get image filename
                    image_file = batch_sample[batch_sample_index]
                    # read the file
                    image = cv2.imread(image_file)
                    # convert BGR to RGB
                    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
                    # adjust steering angle, if needed
                    angle = float(batch_sample[3]) + angle_adjust
                    if training:
                        # choose augmentation mode randomly
                        augment_mode = random.choice([None, 'flip', 'brightness'])
                        if augment_mode == 'flip':
                            image = cv2.flip(image, 1)
                            angle *= -1
                        elif augment_mode == 'brightness':
                            image = random_brightness(image)
                    images.append(image)
                    angles.append(angle)
            x = np.array(images)
            y = np.array(angles)
            yield sklearn.utils.shuffle(x, y)