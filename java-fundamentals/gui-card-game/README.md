## GUI Card Game
This project implements a simple GUI card game. 

### How to run the game
1. Extract the downloaded zip file to extract all project files
2. Open the project in Eclipse
3. Right click on the Main.java file and select "Run As > Java Application"
4. Follow the on-screen instructions to play the card game

### Gameplay instructions
- The player is given an initial budget of $100
- Click "Start" to draw an initial 3 cards
- Place a bet by entering a value in the "Bet:" field
- Click "Replace Card 1/2/3" to exchange drawn cards, up to 2 replacements
- Click "Result" to reveal cards and determine the winner
- Win/lose money based on the outcome
- Play repeats until the player runs out of money

### Key classes
- Main - Contains main method to launch the GUI
- CardGameFrame - GUI framework and controller logic
- Card - Represents a playing card object
- Deck - Manages the card deck and draws/shuffles cards
