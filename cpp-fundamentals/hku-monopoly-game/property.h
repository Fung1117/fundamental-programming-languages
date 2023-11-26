#ifndef PROPERTY_H
#define PROPERTY_H

struct Property{
  int owner;
  int cost;
  bool house;
  bool hotel;
  void Selling();
  void Upgrade();
  int BuildingCost() const {return (house? 500: 200);}
  bool CanUpgrade() const {return ( (!house) || (!hotel) );}
};

#endif //PROPERTY_H
