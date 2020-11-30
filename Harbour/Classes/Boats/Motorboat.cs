using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen.Classes
{
    class Motorboat : BoatsParent
    {
        public int HorsePower { get; set; }
        public int NumberOfSpotsNeededAtHarbour = 1;
    }
}
