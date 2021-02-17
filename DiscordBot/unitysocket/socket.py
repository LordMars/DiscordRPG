import socket
from _thread import *
import os

ss = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
conn = None

#binds socket and listens for a connection from the unity games socket.
#socket.accept() blocks the thread. Currently this is run in main, as the rest
#of the processes in main shouldnt be running unless the socket is connected
def startSocketAsServer():
    serverip = os.getenv('IP')
    serverport = int(os.getenv('PORT'))
    print('Socket Made')

    ss.bind((serverip, serverport)) #local host
    ss.listen()
    print('Socket Listening ... ')

    conn, addr = ss.accept()
    start_new_thread(receivethread, (conn, addr))
    return conn

#loops the conn.recv() function. This function is blocking, meaning it halts the execution of the thread its on until it receives its result
#for this reason, this function needs to be called in a thread seperate from main
def receivethread(conn, addr):
    with conn:
        print('Connected by', addr)
        while True:
            buffer = conn.recv(1024)
            if len(buffer) > 0:
                print(buffer)
