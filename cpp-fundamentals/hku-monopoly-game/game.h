#ifndef GAME_H
#define GAME_H

#include <vector>
#include <map>

#include "player.h"
#include "property.h"

class Game
{
 public:
  Game();
  //Functions
  void Logo();
  void initGame();
  void StartMenu();
  void MainMenu();
  void PressToContinue();
  void RollDice();
  void CheckPosition();
  void Chance();
  void BuyProperty(int);
  void UpgradeProperty(int);
  void LandProperty(int);
  void Bankrupt();
  void Winner();
  void Save();
  void Load();
  void LoadProperty();  
 
  //Accessors
  int current() const {return game_turn % number_of_player;}
  inline bool getPlaying() const {return this-> playing;} 
   
 private:
  int game_turn;
  int players_choice;
  int number_of_player;
  bool playing;
  //player related
  std::vector<Player> players;
  //property related
  std::map<int, Property> properties;
};

#endif //GAME_H
