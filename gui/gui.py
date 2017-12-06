import cv2 
import numpy as np

android_template = cv2.imread("android.png")

def print_on_android(i):
    global android_template
    m_a = cv2.inRange(i,(1,1,1), (255,255,255))
    m_a = cv2.cvtColor(m_a, cv2.COLOR_GRAY2BGR)
    m_i = cv2.bitwise_not(m_a)
    print (m_i.shape, i.shape)
    l_i = cv2.bitwise_and(m_i, i)
    l_a = cv2.bitwise_and(m_a, android_template)
    res = cv2.bitwise_or(l_i, l_a)
    return res

cv2.namedWindow("Application")
m = np.random.randn(android_template.shape[0], android_template.shape[1], android_template.shape[2])
cv2.imshow("Application", print_on_android(m))
cv2.waitKey(0)
cv2.destroyAllWindows()
