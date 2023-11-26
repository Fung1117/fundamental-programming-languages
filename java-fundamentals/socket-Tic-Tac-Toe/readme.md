## Tic-Tac-Toe Game

This project implements a two player Tic Tac Toe game over a network using Java sockets for client-server communication.

### How to run the program:
1. Extract the zip file and open the project in Eclipse.
2. Right click on the Server.java file and run it as Java Application. This will start the server.
3. Right click on the Client.java file twice and run it as Java Application. This will open two client windows for two players.
4. Enter the name for each player when prompted.
5. The game will start with Player 1 (X mark). Click on any empty box on the board to place the mark.
6. The turn will switch to Player 2 (O mark). Click on any empty box to place the mark.
7. The turns will keep alternating between players until someone wins or it is a draw.
8. A message dialog will popup to display the winner or draw result.
9. You can exit the game anytime by clicking on Exit menu option.
10. Click on Help menu to see instructions during the game.
11. To end the program, close all open client windows after the game ends.

### How to run multiple games:
Keep running Client.java to start new client windows for additional games after the current game ends. 
The server will keep running in the background to facilitate multiple simultaneous games.
