# Discord RPG
The discordrpg bot is an attempt to create an rpg in unity that is controlled via messages sent through discord.
These messages are recognized and processed by a discord bot, which will then be sent to the running unity game
by way of sockets. 

# Run
1. First, run python discordrpg.py. This will set up the socket server
2. Run the unity game. This will set up the client socket and connect it to the server
3. Stream the unity game to the discord voice channel your players are in

Now you can start sending messages to the bot in the format specified in actions.py