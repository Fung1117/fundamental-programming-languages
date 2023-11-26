#include "player.h"

#include <iostream>
#include <string>

// initiate the player
Player::Player(std::string name)
{
    player_name = name;
    player_money = 10000;
    player_position = 0;
    player_jail = false;
    player_mortgage = false;
}

// loading the information of player
Player::Player(std::string name, int money, int position, bool jail, bool mortgage)
{
    player_name = name;
    player_money = money;
    player_position = position;
    player_jail = jail;
    player_mortgage = mortgage;
}

// to let player move
void Player::Move(int steps)
{
    player_position += steps;
    PassGo();    
}

// to check if player start a new semester
// if yes, let player collect 1000.
void Player::PassGo()
{
    while(player_position >= 20){
        player_position -= 20;
        player_money += 1000;
        std::cout << "You start a new Semester !!! Your parents give you $1000 as a reward !!!" << std::endl;
    }
}

// to let player go to jail
void Player::GoToJail()
{
    player_jail = true;
    player_position = 5;
}

// to let player get out of jail
void Player::GetOutOfJail()
{
    player_jail = false;
}

// to let player spend their money
void Player::SpendMoney(int money)
{
    player_money -=  money;
}

// to let player collect their money
void Player::CollectMoney(int money)
{
    player_money +=  money;
}

// as each player only have one chance to mortgaging
void Player::Mortgage()
{
    player_mortgage = true;
}
