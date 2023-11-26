#ifndef PLAYER_H
#define PLAYER_H

#include <string>

class Player
{
 public:
  Player(std::string);
  Player(std::string, int, int, bool, bool);
  //Functions
  void Move(int steps);
  void PassGo();
  void GoToJail();
  void GetOutOfJail();
  void SpendMoney(int money);
  void CollectMoney(int money);
  void Mortgage();
  inline bool isBankrupt() {return (player_money < 0);}
  //Accessors
  inline std::string getName() {return this->player_name;}
  inline int getMoney() {return this->player_money;}
  inline int getPosition() {return this->player_position;}
  inline bool isJail() {return this->player_jail;}
  inline bool isMortgage() {return this->player_mortgage;}
  
 private:
  std::string player_name;
  int player_money;
  int player_position;
  bool player_jail;
  bool player_mortgage;
};

#endif //PLAYER_H
