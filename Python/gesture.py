import numpy as np
import socket
import cv2

cap = cv2.VideoCapture(0)
prevXg = 0
host = '127.0.0.1'
port = 5000
move = ''
s = socket.socket()
s.bind((host, port))
s.listen(2)
print("waiting for Connection")
c, addr = s.accept()
print("Connection from:" + str(addr))
print(c)

    
while True:
    ret, frame = cap.read()

    # Convert BGR to HSV
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)

    # define range of yellow and red color in HSV
    lower_yellow = np.array([23, 59, 119])
    lower_red = np.array([166, 84, 141])
    upper_yellow = np.array([54, 255, 255])
    upper_red = np.array([186, 255, 255])

    # Threshold the HSV image to get only selected colors
    #A pixel is set to 255 if it lies within the boundaries
    # specified otherwise set to 0. This way it returns the thresholded image.
    #mask0 = cv2.inRange(hsv, lower_red, upper_red)
    mask1 = cv2.inRange (hsv, lower_yellow, upper_yellow)
    #mask = mask0 + mask1
    yellcnts = cv2.findContours(mask1.copy(),cv2.RETR_EXTERNAL,cv2.CHAIN_APPROX_SIMPLE)[-2]
    #redcnts= cv2.findContours(mask0.copy(),cv2.RETR_EXTERNAL,cv2.CHAIN_APPROX_SIMPLE)[-2]

    if len(yellcnts)>0 :
        blue_area = max(yellcnts, key=cv2.contourArea)
        (xg,yg,wg,hg) = cv2.boundingRect(blue_area)
        cv2.rectangle(frame,(xg,yg),(xg+wg, yg+hg),(255,0,0),3)
        if (prevXg - xg) < -3:
            move = 'left'
        if (prevXg - xg) > 3:
            move = 'right'
        cv2.putText(frame, move,(xg,yg),cv2.FONT_HERSHEY_SIMPLEX,1,(255,0,0),1,cv2.LINE_AA)
        prevXg = xg

    #print("sending :" + str(count))
    c.sendall(move.encode('utf-8'))
    
    move = ''
    #c.close()
    # For red
    #if len(redcnts)>0 :
     #   red_area = max(redcnts, key=cv2.contourArea)
      #  (xr, yr, wr, hr) = cv2.boundingRect(red_area)
       # cv2.rectangle(frame, (xr, yr), (xr + wr, yr + hr), (255, 0, 0), 3)
        #cv2.putText(frame, 'red', (xr, yr), cv2.FONT_HERSHEY_SIMPLEX, 1, (255, 0, 0), 1, cv2.LINE_AA)

    cv2.imshow('frame',frame)
    #cv2.imshow('mask',mask)
    

    k = cv2.waitKey(5)
    if k == 27:
        break

#cap.release()
cv2.destroyAllWindows()
