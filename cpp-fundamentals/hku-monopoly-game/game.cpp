#include "game.h"

#include <iostream>
#include <cstdlib> // for calling rand()
#include <fstream> // to read from and write to files
#include <string>  
#include <vector>  // for using vector
#include <map>     // for using map

#include "print.h"
#include "player.h"
#include "property.h"

using namespace std;
 
Game::Game()
{
    game_turn = 0;
    players_choice = 0;
    number_of_player = 0;
    playing = true;
}

//Functions

// print the logo of monopoly
void Game::Logo()
{
    PrintMonopoly();
}

// to initiate the game
// (1) asking the user how many players is playing
// (2) asking the players name
// (3) add player to the vector players
void Game::initGame()
{
    string name;
    cout << "Number of player: " << endl;
    cin >> number_of_player;

    for (int i = 0; i < number_of_player; i++){
      cout << "Player " << i + 1 << " Name: (Please Write a single word)" << endl;
      cin >> name;
      Player player(name);
      players.push_back(player);
    }
}

// to let user to choose
// (0) Exit
// (1) playing new game
// (2) loading game
// (3) printing game rule
void Game::StartMenu()
{
    PrintMenu();
    cin >> players_choice;
    cout << endl;
    switch (players_choice) {
      case 0:
        playing = false;
        break;
      case 1:
        playing = true;
        LoadProperty();
        initGame();
        break;
      case 2:
        playing = true;
        Load();
        break;
      case 3:
        PrintRule();
        StartMenu();
        break;
      default:
        StartMenu();
        break;
    }
}

// to print the board if player is not in jail or bankrupt
// let player to choose 
// (0) Exit
// (1) Roll the dice
// (2) Save
void Game::MainMenu()
{
    if ( players[current()].isBankrupt() ) {     // true if player is bankrupt
        game_turn += 1;
    } else if ( players[current()].isJail() ) {  // true if player is Jail
        cout << "You are in jail! " << endl; 
        players[current()].GetOutOfJail();
        game_turn += 1;
    } else {
        PrintBoard(players, game_turn, current());
        cin >> players_choice;
        cout << endl;
        switch (players_choice) {
          case 0:
            playing = false;
            break;
          case 1:
            RollDice();
            CheckPosition();
            Winner();
            game_turn += 1;
            break;
          case 2:
            playing = false;
            Save();
            break;
          case 3:
            PrintRule();
            MainMenu();
            break;
          default:
            MainMenu();
            break;
        }
    }        
}

// Roll the Dice
// Print the Dice
// to let player move
void Game::RollDice()
{
    int dice1 = rand() % 6 + 1, dice2 = rand() % 6 + 1;
    PrintDice(dice1, dice2);
    players[current()].Move(dice1 + dice2);
}

// to check the position of player
void Game::CheckPosition()
{
    switch (players[current()].getPosition()) {
      case 0:  // New Semester
      case 10: // Reading Week
        break;  
      case 1:  // Luxury Tax
        cout << "You have steped on the Luxury Tax !" << endl;
        players[current()].SpendMoney(200);
        break;
      case 5:  // Jail
        cout << "You have steped on the Jail !" << endl;
        cout << "You are not in jail !" << endl;
        break;
      case 15: // Go to jail 
        cout << "Go to jail !" << endl;
        players[current()].GoToJail();
        break;
      case 9:  // Chance
      case 17: // Chance
        Chance();
        break;
      case 19: // Income Tax
        cout << "You have steped on the Income Tax !" << endl;
        players[current()].SpendMoney(200);
        break;
      default: // Property
        int pos = players[current()].getPosition();
        if ( properties[pos].owner == -1 ) {
            BuyProperty(pos);               // buy properties
        } else if (properties[pos].owner == current()) { 
            UpgradeProperty(pos);           // upgrade properties
        } else {
            LandProperty(pos);              // land properties
        break;
        }
    }
    PressToContinue();
    // in case of player Bankrupt;
    Bankrupt();
}

// print the word of the Chance card 
// let player get money or lose money or go to jail
// if Chance = 1: players will lose 1000 dollar
void Game::Chance()
{
    int Chance = rand() % 6;
    PrintChance(Chance);
    switch (Chance) {
      case 0:
        players[current()].CollectMoney(2000);
        break;
      case 1:
        players[current()].SpendMoney(1000);
        break;
      case 2:
        players[current()].Move(-3);
        if ( properties[players[current()].getPosition()].owner == -1 ) {
            BuyProperty(players[current()].getPosition());               // buy properties
        } else if (properties[players[current()].getPosition()].owner == current()) { 
            UpgradeProperty(players[current()].getPosition());           // upgrade properties
        } else {
            LandProperty(players[current()].getPosition());              // land properties
        }
        break;             
      case 3:
        players[current()].CollectMoney(100);
        break;          
      case 4:
        players[current()].GoToJail();
        break;          
      case 5:
        for (int i = 0; i < players.size(); i++) {
            if (i != current() && !(players[i].isBankrupt())) {
                players[i].CollectMoney(250);
                players[current()].SpendMoney(250);
            }
        }
        break;  
    }
}

// to let player to buy property
// if player have enough money and he choose to buy
// the owner of that player will be the buyer
// e.g if player 1 buy "HKU Station", the owner of "HKU Station" will be 0.
void Game::BuyProperty(int pos)
{
    PrintProperty(pos, properties[pos].cost);
    cin >> players_choice;
    if ( players_choice == 1 ) {
        if ( players[current()].getMoney() >= properties[pos].cost ) {
            properties[pos].owner = current();
            players[current()].SpendMoney(properties[pos].cost);
        } else {
            cout << "You do not have enough money !!!" << endl;
        }
    }
}

// to let player to upgrade their property
// we can build one house and one hotel only
void Game::UpgradeProperty(int pos)
{
    if ( properties[pos].CanUpgrade() ) {
        if ( !properties[pos].house ){
            PrintUpgrade(pos, 200, 0);
        } else {
            PrintUpgrade(pos, 500, 1);
        }
        cin >> players_choice;
        if ( players_choice == 1 ) {
            if (players[current()].getMoney() >= properties[pos].BuildingCost()){
                properties[pos].Upgrade();
                players[current()].SpendMoney(properties[pos].BuildingCost());
            } else {
                cout << "You do not have enough money !!!" << endl;
            }
        }
    }
}

// to let player collect their rent
// to let player pay thier rent
void Game::LandProperty(int pos)
{
    PropertyLocation(pos);
    cout << " is owned by " << players[properties[pos].owner].getName() << "." << endl;
    cout << "Pay : " << properties[pos].cost / 4 << endl;
    players[current()].SpendMoney(properties[pos].cost / 4);
    players[properties[pos].owner].CollectMoney(properties[pos].cost / 4);
}

// to let player mortgage
// mortage: exchange player's properties with banks to get money
//          avioding bankrupt
// each player only have one chance to do so
void Game::Bankrupt()
{
    if (players[current()].isBankrupt()) {
        if ( !(players[current()].isMortgage()) ) {
            players[current()].Mortgage();
            cout << "You got a second chance !!! Sell all your properties to bank!!!" << endl;
            map<int, Property>::iterator itr; 
            for (itr = properties.begin(); itr != properties.end(); itr++) {
                if ( (itr->second).owner == current()) {
                    (itr->second).Selling();
                    players[current()].CollectMoney(properties[itr->first].cost / 2);
                }
            }
            if ( players[current()].isBankrupt() ){
                cout << "Your properties are not enough to repay! GAME OVER FOR YOU!" << endl; 
            }
        } else {
            cout << "GAME OVER FOR YOU!" << endl;
        }
    }
}

// to end the game if we already have one winner
// to inform who is the winner
void Game::Winner() // may change the code of style
{
    int BankruptPlayer = 0;
    for (int i = 0; i < players.size(); i++)
        if (players[i].isBankrupt())
            BankruptPlayer += 1;
    if (BankruptPlayer == players.size() - 1){
        playing = false;
        for (int i = 0; i < players.size(); i++) {
            if (!(players[i].isBankrupt()))
                cout << "Winner is " << players[i].getName() << " !!! "<< endl;
        }
    }
}

// to save the game
// (1) game_turn (2) number_of_player
// (3) the information of every players
// (4) the information of every properties
void Game::Save()
{
    ofstream save;
    save.open("save.txt");
    if ( save.fail() ) {
        cout << "Sorry !!! Error in file opening !!!" << endl;
        exit(1);
    }
    save << game_turn << endl;
    save << number_of_player << endl;
    for (int i = 0; i < number_of_player; i++) {
        save << players[i].getName()  << " " << players[i].getMoney() << " " << players[i].getPosition() << " " 
             << players[i].isJail() << " " << players[i].isMortgage() << endl;
    }
    map<int, Property>::iterator itr; 
    for (itr = properties.begin(); itr != properties.end(); itr++) {
        save << itr->first << " " << (itr->second).owner << " " << (itr->second).cost << " "
             << (itr->second).house << " " << (itr->second).hotel << endl;
    }
    save.close();
}

// to load the game
// (1) game_turn (2) number_of_player
// (3) the information of every players
// (4) the information of every properties
void Game::Load()
{
    ifstream load;
    load.open("save.txt");
    if ( load.fail() ){
        cout << "You do not a save !!!" << endl;
        exit(1);
    }
    load >> game_turn >> number_of_player;
    for (int i = 0; i < number_of_player; i++){
        string name;
        int money, position; 
        bool jail, mortgage;
        load >> name >> money >> position >> jail >> mortgage;
        Player player(name, money, position, jail, mortgage);
        players.push_back(player);
    }
    for (int i = 0; i < 12; i++) {
        int place;
        Property p;
        load >> place >> p.owner >> p.cost >> p.house >> p.hotel;
        properties[place] = p;
    }
    load.close();
}

// to initiate the map of properties
void Game::LoadProperty()
{
    ifstream property;
    property.open("defaultproperty.txt");
    if ( property.fail() ) { 
        cout << "Please download defaultproperty.txt !!!" << endl;
        exit(1);
    }
    for (int i = 0; i < 12; i++) {
        int place, owner, cost;
        property >> place >> owner >> cost;
        properties[place] = {owner, cost, 0, 0};
    }
    property.close();
}

void Game::PressToContinue()
{
    cin.ignore();
    cout << "Press Enter to Continue ...";
    cin.get();
    cout << endl;
}
