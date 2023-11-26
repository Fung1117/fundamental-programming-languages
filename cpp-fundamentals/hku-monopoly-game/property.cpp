#include "property.h"

// when mortgaging
// reset the information of property
void Property::Selling()
{
    owner = -1;
    if ( house )
        cost -= 200;
    if ( hotel )
        cost -= 500;
    house = 0;
    hotel = 0;
}

// to upgrade the property
// increase the cost of property
// change house = true or hotel = true
void Property::Upgrade()
{
    if (!house) {
        cost += 200;
        house = !house;
    } else if (!hotel) {
        cost += 500;
        hotel = !hotel;
    }
}
