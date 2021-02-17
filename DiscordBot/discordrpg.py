from unitysocket.socket import startSocketAsServer
from botcommands.actions import *
import discord
import os
from dotenv import load_dotenv
load_dotenv('.env')

token = os.getenv('TOKEN')

client = discord.Client()
conn = None

#fires when the bot connects to discord
@client.event
async def on_ready():
    print("We have logged in as {0.user}".format(client))

#bots response when it observes messages
@client.event
async def on_message(msg):
    if msg.author == client.user: # does not respond if the message is sent from us
        return
    await act(msg, conn)

#connects bot to discord
def runclient():
    client.run(token)

def main():
    global conn
    conn = startSocketAsServer()
    runclient()

main()
