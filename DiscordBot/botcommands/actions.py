import json

#should be called on every message that comes from the players. Reads message contexts and decides
async def act(msg, conn):
    if msg.content.startswith(".act"):
        obj = {}
        obj['channel'] = msg.channel.name
        obj['author'] = msg.author.name
        obj['command'] = msg.content

        objs = json.dumps(obj) # get object as string

        bytes = objs.encode('utf-8')
        conn.sendall(bytes)
        await msg.channel.send('Command Sent')

def addPlayer(msg, conn):
    player = msg.author
    bytes = msg.content.encode('utf-8')
    conn.sendall(bytes)