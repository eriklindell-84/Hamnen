using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen.Classes
{
    class CargoShip : BoatsParent
    {
        public int NumberOfCountainers { get; set; }

        public int NumberOfSpotsNeededAtHarbour = 3;
    }
}
