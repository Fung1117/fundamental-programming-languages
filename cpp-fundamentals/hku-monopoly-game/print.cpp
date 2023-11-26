#include "print.h"

#include <iostream>
#include <iomanip>   // for calling left, setw()
#include <string>    // for calling to_string()
#include <vector>

#include "player.h"

using namespace std; 

// the logo of Monopoly
void PrintMonopoly()
{
    cout << " /$$      /$$                                                   /$$                 "  << endl;
    cout << "| $$$    /$$$                                                  | $$                 "  << endl;
    cout << "| $$$$  /$$$$  /$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$   /$$$$$$ | $$ /$$   /$$       "  << endl;   
    cout << "| $$ $$/$$ $$ /$$__  $$| $$__  $$ /$$__  $$ /$$__  $$ /$$__  $$| $$| $$  | $$       "  << endl;   
    cout << "| $$  $$$| $$| $$  \\ $$| $$  \\ $$| $$  \\ $$| $$  \\ $$| $$  \\ $$| $$| $$  | $$  "  << endl;   
    cout << "| $$\\  $ | $$| $$  | $$| $$  | $$| $$  | $$| $$  | $$| $$  | $$| $$| $$  | $$      "  << endl;   
    cout << "| $$ \\/  | $$|  $$$$$$/| $$  | $$|  $$$$$$/| $$$$$$$/|  $$$$$$/| $$|  $$$$$$$      "  << endl;   
    cout << "|__/     |__/ \\______/ |__/  |__/ \\______/ | $$____/  \\______/ |__/ \\____  $$   "  << endl;   
    cout << "                                           | $$                     /$$  | $$       "  << endl;   
    cout << "                                           | $$                    |  $$$$$$/       "  << endl; 
    cout << "                                           |__/                     \\______/       "  << endl;
}

// press 3 to print the rule of game
void PrintRule()//
{
    cout << "Rule: " << endl
         << "Game Play: " << endl
         << "1. Starting with the Banker, each player throws the dice. The player with the highest total starts the play." << endl
         << "2. Each player starts with $10000 Then each player places his token on the corner marked \"GO\"" << endl
         << "   throw the dice and move the number of spaces indicated by the dice." << endl
         << endl;
    cout << "Go:" << endl
         << "Each time a player's token lands on or passes over GO " << endl
         << "the Banker pays that player a $1000 salary." << endl
         << endl;
    cout << "Buying Property :" << endl
         << "When you land on an unowned property you can buy that property from the Bank at its printed price." << endl
         << endl;
    cout << "Paying Rent :" << endl
         << "When you land on a property that is owned by another player, the owner collects rent from you." << endl
         << endl;
    cout << "Chance :" << endl
         << "When you land on either of these spaces, follow the instructions on the card." << endl
         << "The \"Get Out of Jail Free\" card is held until used and then returned to the bottom of the deck." << endl
         << endl;
    cout << "Income Tax :" << endl
         << "Pay $2000" << endl
         << endl;
    cout << "Luxury Tax :" << endl
         << "Pay $2000" << endl
         << endl;
    cout << "Go To Jail :" << endl
         << "When you step on Go to jail, Go to jail" << endl
         << endl;
    cout << "Jail :" << endl
         << "Wait for one round, You can still receive rent." << endl
         << "When you step on jail, nothing happen like reading week" << endl
         << endl;
    cout << "Free Parking :" << endl
         << "This is just a \"free\" resting place, so the player landing on it doesn't receive money, property, reward, or penalty of any kind." << endl
         << endl;
    cout << "Mortgage  :" << endl
         << "Mortgage with the half price of the original property price include the half cost of the building. You can only mortgage one time. " << endl
         << endl;
    cout << "Selling Property (when Bankrupt): " << endl
         << "Unimproved properties must be sold to bank only for one-half the price paid for them." << endl
         << "Houses and Hotels must be sold back to the bank for one-half the price paid for them." << endl
         << endl;
    cout << "Bankruptcy :" << endl
         << "You must sell all your properties to bank if you owe more than you can pay to another player" << endl
         << "If your amount of money is not enough for paying to another player, you go bankrupt!" << endl
         << endl;
    cout << "End of the Game :" << endl
         << "The last player left in the game is the winner!" << endl;
    cout << "House costs $200 \nHotel cost plus 1 house $500" << endl;
    cout << "****************************Properties reference****************************" << endl 
         << " Grand Hall  : Price $600  Rent $150  With 1 house $200  With 1 hotel $325  " << endl
         << "   Library   : Price $600  Rent $150  With 1 house $200  With 1 hotel $325  " << endl
         << "  Meng Wah   : Price $1000 Rent $250  With 1 house $300  With 1 hotel $425  " << endl
         << " Eliot Hall  : Price $1800 Rent $250  With 1 house $300  With 1 hotel $425  " << endl
         << "   Chi Wah   : Price $1800 Rent $250  With 1 house $300  With 1 hotel $425  " << endl
         << " HKU Station : Price $2000 Rent $500  With 1 house $550  With 1 hotel $675  " << endl
         << " Swire Hall  : Price $2400 Rent $600  With 1 house $650  With 1 hotel $775  " << endl
         << "  Swire Can  : Price $2600 Rent $650  With 1 house $700  With 1 hotel $825  " << endl
         << "   W.L.G.H   : Price $2600 Rent $650  With 1 house $700  With 1 hotel $825  " << endl
         << "   C.P.D     : Price $2800 Rent $700  With 1 house $750  With 1 hotel $875  " << endl
         << "  May Hall   : Price $3500 Rent $875  With 1 house $925  With 1 hotel $1050 " << endl
         << "  Inno Wing  : Price $4000 Rent $1000 With 1 house $1050 With 1 hotel $1175 " << endl;
} 

// to print the start menu
void PrintMenu()
{
    cout << "Press 0: Exit" << endl;
    cout << "Press 1: Game Start" << endl;
    cout << "Press 2: Load Game" << endl;
    cout << "Press 3: Game Rule" << endl;
    cout << "Choice: ";
}

// to return the player location  
// e.g. if only player 1 in New Semester and we totally have 4 players
//      it will return "    1       "
// e.g. if all players in New Semester and we totally have 4 players
//      it will return "    1 2 3 4 "
string Position(vector<Player> x, int position)
{
    char num = '1', text[] = "|            ";
    int pos = 5 + (4 - x.size()) * 2;
    for (int i = 0; i < x.size(); i ++){
        if (x[i].getPosition() == position){
            text[pos] = num + i;
        }
        pos += 2;
    }
    return text;
}

// to print the board
// to print the information of player
// to print the choice
void PrintBoard(vector<Player> x, int turn, int y)
{
    cout << "|------------|------------|------------|------------|------------|------------|" << endl;
    cout << "|            |            |            |            |            |            |" << endl;
    cout << "|New Semester| Luxury tax | Grand Hall |   Library  |  Meng Wah  |    Jail    |" << endl;
    for (int i = 0; i < 6; i++){
        cout << Position(x, i);
    }
    cout << "|" << endl;
    cout << "|------------|------------|------------|------------|------------|------------|" << endl;
    cout << "|            |                                                   |            |" << endl;
    cout << "| Income Tax |                                                   | Eliot Hall |" << endl;
    cout << Position(x, 19) << left << setw(52) << "|" << Position(x, 6)<< "|" << endl;
    cout << "|------------|                                                   |------------|" << endl;
    cout << "|            |                                                   |            |" << endl;
    cout << "|  Inno Wing |    Turn: " << left << setw(41) << turn + 1        << "|   Chi Wah  |" << endl;
    cout << Position(x, 18) << left << setw(52) << "|" << Position(x, 7)<< "|" << endl;
    cout << "|------------|   Player " << left << setw(41) << y + 1           << "|------------|" << endl;
    cout << "|            |    Name: " << left << setw(41) << x[y].getName()  << "|            |" << endl;
    cout << "|   Chance   |   Money: " << left << setw(41) << x[y].getMoney() << "| HKU Station|" << endl;
    cout << Position(x, 17) << left << setw(52) << "|" << Position(x, 8)<< "|" << endl;
    cout << "|------------| Press 0: Exit                                     |------------|" << endl;
    cout << "|            | Press 1: Roll                                     |            |" << endl;
    cout << "|  May Hall  | Press 2: Save                                     |   Chance   |" << endl;
    cout << Position(x, 16) << left << setw(52) << "| Press 3: Rule" << Position(x, 9)<< "|" << endl;
    cout << "|------------|------------|------------|------------|------------|------------|" << endl;
    cout << "|            |            |            |            |            |            |" << endl;
    cout << "| Go To Jail |    C.P.D   |   W.L.G.H  |  Swire Can | Swire Hall |Reading Week|" << endl;
    for (int i = 15; i > 9; i--){
        cout << Position(x, i);
    }
    cout << "|" << endl;
    cout << "|------------|------------|------------|------------|------------|------------|" << endl;
    cout << "Choice:";
}

// to print the words of chance
void PrintChance(int chance)
{
    cout << "You get a Chance !!!" << endl;
    switch(chance){
      case 0:
        cout << "Early year and bonus \nAdvance to Go, \nCollect $2000" << endl;
        break;
      case 1:
        cout << "You forget to do assignment. \nPay $1000" << endl;
        break;
      case 2:
        cout << "You get stuck in a long queue on the way to canteen. \nGo back three space" << endl;
        break;
      case 3:
        cout << "You have received a scholarship \nCollect $100" << endl;
        break;
      case 4:
        cout << "You have been found for committing plagiarism \nGo to jail directly \nDo not pass GO \nDo not collect $2000" << endl;
        break;
      default:    // case 5
        cout << "You have been found for free riding \nPay each payer $250" << endl;
        break;
    }
}

void PropertyLocation(int place)
{
    cout << "You land in ";
    switch (place){
      case 2:
        cout << "Grand Hall\nGrand Hall";
        break;
      case 3:
        cout << "Library\nLibrary";
        break;
      case 4:
        cout << "Meng Wah\nMeng Wah";
        break;
      case 6:
        cout << "Eliot Hall\nEliot Hall";
        break;
      case 7:
        cout << "Chi Wah\nChi Wah";
        break;
      case 8:
        cout << "HKU Station\nHKU Station";
        break;
      case 11:
        cout << "Swire Hall\nSwire Hall";
        break;
      case 12:
        cout << "Swire Can\nSwire Can";
        break;
      case 13:
        cout << "W.L.G.H\nW.L.G.H";
        break;
      case 14:
        cout << "C.P.D\nC.P.D";
        break;
      case 16:
        cout << "May Hall\nMay Hall";
        break;
      case 18:
        cout << "Inno Wing\nInno Wing";
        break;
    }
}

void PrintProperty(int place, int cost)
{
    PropertyLocation(place);
    cout << " is not owned." << endl;
    cout << "Do you want to buy ? " << endl;
    cout << "Pay : " << cost << endl;
    cout << "Press 0: No" << endl;
    cout << "Press 1: Yes" << endl;
    cout << "Choice: ";
}

void PrintUpgrade(int place, int cost, bool hotel)
{
    PropertyLocation(place);
    
    if ( hotel ) {
        cout << " can build hotel." << endl;
    } else {
        cout << " can build house." << endl;
    }
    cout << "Upgrade properties ? " << endl;
    cout << "Pay : " << cost << endl;
    cout << "Press 0: No" << endl;
    cout << "Press 1: Yes" << endl;
    cout << "Choice: ";
}

// print two dice at the same time
void PrintDice(int dice1, int dice2)
{
    cout << "+---+ +---+\n";
    //print thr first part of dice 1
    switch (dice1){
      case 1:
        cout << "|   | ";
        break;
      case 2:
      case 3:
        cout << "|o  | ";
        break;
      default:  // for case 4, 5, 6  
        cout << "|o o| ";
        break;
    }
    //print thr first part of dice 2
    switch (dice2){
      case 1:
        cout << "|   |\n";
        break;
      case 2:
      case 3:
        cout << "|o  |\n";
        break;
      default: // for case 4, 5, 6  
        cout << "|o o|\n";
        break;
    }
    //print thr second part of dice 1
    switch (dice1){
      case 1:
      case 3:
      case 5:
        cout << "| o | ";
        break;
      case 2:
      case 4:
        cout << "|   | ";
        break;
      default: // for case 6  
        cout << "|o o| ";
        break;
    }
      //print thr second part of dice 2
      switch (dice2){
        case 1:
        case 3:
        case 5:
          cout << "| o |\n";
          break;
        case 2:
        case 4:
          cout << "|   |\n";
          break;
        default: // for case 6
          cout << "|o o|\n";
          break;
      }
      //print thr last part of dice 1
      switch (dice1){
        case 1:
          cout << "|   | ";
          break;
        case 2:  
        case 3:
          cout << "|  o| ";
          break;
        default: // for case 4, 5, 6
          cout << "|o o| ";
          break;
      }
      //print thr last part of dice 2
      switch (dice2){
        case 1:
          cout << "|   |\n";
          break;
        case 2:
        case 3:
          cout << "|  o|\n";
          break;
        default: // for case 4, 5, 6
          cout << "|o o|\n";
          break;
      }
      cout << "+---+ +---+\n";
}
