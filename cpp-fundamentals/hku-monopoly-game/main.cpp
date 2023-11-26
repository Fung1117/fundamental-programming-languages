#include "game.h"

int main() {
  Game monopoly;
  monopoly.Logo();
  monopoly.StartMenu();
  while (monopoly.getPlaying()){
    monopoly.MainMenu();
  }
}
