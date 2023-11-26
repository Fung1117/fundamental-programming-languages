#ifndef PRINT_H
#define PRINT_H

#include <string>
#include <vector>

#include "player.h"

void PrintMonopoly();
void PrintRule();
void PrintMenu();
std::string Position(std::vector<Player>, int);
void PrintBoard(std::vector<Player>, int, int);
void PrintChance(int);
void PropertyLocation(int);
void PrintProperty(int, int);
void PrintUpgrade(int, int, bool);
void PrintDice(int, int);

#endif //PRINT_H
