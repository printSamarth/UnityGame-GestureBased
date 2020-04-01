# File is used to flag when player is out and destroying thread

import numpy as np
import socket
import cv2
from _thread import *
import threading
cap = cv2.VideoCapture(0)
prevXg = 0
host = '127.0.0.1'
port = 5000
move = ''

 
def threaded(c):
    global prevXg
    global move
    file = open("data.txt",'r')
    count = 0
    while True:
        d = file.read()
        file.seek(0)
        if d == "Stop":
            print("stop")
            break
        ret, frame = cap.read()
        hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
        lower_yellow = np.array([166, 84, 141])
        upper_yellow = np.array([186, 255, 255])
        mask1 = cv2.inRange(hsv, lower_yellow, upper_yellow)
        yellcnts = cv2.findContours(mask1.copy(),cv2.RETR_EXTERNAL,cv2.CHAIN_APPROX_SIMPLE)[-2]
        if len(yellcnts) > 0:
            blue_area = max(yellcnts, key=cv2.contourArea)
            (xg, yg, wg, hg) = cv2.boundingRect(blue_area)
            cv2.rectangle(frame, (xg, yg), (xg+wg, yg+hg), (255, 0, 0), 3)
            if (prevXg - xg) < -1:
                move = 'left'
            if (prevXg - xg) > 1:
                move = 'right'
            cv2.putText(frame, move,(xg,yg),cv2.FONT_HERSHEY_SIMPLEX,1,(255,0,0),1,cv2.LINE_AA)
            prevXg = xg
        # Send the move variable data
        c.sendall(move.encode('utf-8'))

        #print("Active thread count",threading.active_count())
        move = ''
        cv2.imshow('Capture',frame)

        cv2.resizeWindow('Capture', 650, 520)
        cv2.moveWindow('Capture',7,30)
        k = cv2.waitKey(5)
        if k == 27:
            break
    print("EXITING From Thread")
    file.close()
    cv2.destroyAllWindows()


def Main():
    global cap
    cap = cv2.VideoCapture(0)
    global prevXg
    prevXg = 0
    global host
    global port
    port = 5000
    global move
    move = ''
    s = socket.socket()
    s.bind((host, port))
    s.listen(1)
    
    print("waiting for Connection")
    while True:
        c, addr = s.accept()
        print('Connected to :', addr[0], ':', addr[1])
        # With each connection request give them a thread
        start_new_thread(threaded, (c,))

        #s.close()


if __name__ == '__main__':
    Main() 
