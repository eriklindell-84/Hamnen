using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamnen.Classes.Harbour
{
    class Harbour
    {
        public int DayCounter { get; set; }
        public int FreeSpots { get; set; }
        public int BoatsRejected { get; set; }

        public List<BoatsParent> harbourqueue = new List<BoatsParent>();
        public BoatsParent[] harbourspots = new BoatsParent[25];

        public string CreateThreeRandomCharsForBoatId()
        {
            Random r = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 3)
            .Select(s => s[r.Next(s.Length)]).ToArray());
        }
        public List<BoatsParent> CreateRandomBoatQueueObjects()
        {
            Random r = new Random();

            for (int i = 0; i < 5; i++)
            {
                var randomValue = r.Next(1, 4);
                if (randomValue == 1)
                {
                    harbourqueue.Add(new Motorboat
                    {
                        BoatId = $"M-{CreateThreeRandomCharsForBoatId()}",
                        DaysLeftAtHarbour = 1,
                        TopSpeed = r.Next(0, 60),
                        Weight = r.Next(200, 3000),
                        HorsePower = r.Next(10, 1000),
                        HarbourSpotsRequired = 1
                    });
                }
                if (randomValue == 2)
                    harbourqueue.Add(new SalingBoat
                    {
                        BoatId = $"S-{CreateThreeRandomCharsForBoatId()}",
                        DaysLeftAtHarbour = 2,
                        TopSpeed = r.Next(0, 12),
                        Weight = r.Next(800, 6000),
                        BoatLenght = r.Next(10, 60),
                        HarbourSpotsRequired = 2
                    });
                if (randomValue == 3)
                {
                    harbourqueue.Add(new CargoShip
                    {
                        BoatId = $"C-{CreateThreeRandomCharsForBoatId()}",
                        DaysLeftAtHarbour = 3,
                        TopSpeed = r.Next(0, 20),
                        Weight = r.Next(3000, 20000),
                        NumberOfCountainers = r.Next(0, 500),
                        HarbourSpotsRequired = 3
                    });
                }
            }
            return harbourqueue;

        }
        private BoatsParent[] GetHarbourCapacity()
        {
            return harbourspots;
        }
        public BoatsParent[] DisplayHarbourCapacity()
        {
            var emptyslots = 0;
            for (int i = 0; i < harbourspots.Count(); i++)
            {
                if (harbourspots[i] == null)
                {
                    emptyslots++;
                    Console.WriteLine("Spot " + i + " is empty");
                }
                else
                {
                    Console.WriteLine($"Spot{i}: {harbourspots[i].BoatId}, Days left in harbour: {harbourspots[i].DaysLeftAtHarbour}");
                }
            }
            Console.WriteLine("We have this many spots open: " + emptyslots);
            return harbourspots;
        }
        public BoatsParent AddBoatsToSlot(BoatsParent boat)
        {
            for (int spot = 0; spot < harbourspots.Count(); spot++)
            {
                if (harbourspots[spot] == null)
                {
                    var doesItFit = RoomForBoat(spot, boat.HarbourSpotsRequired);
                    if (doesItFit == true)
                    {
                        AddBoatToSlot(boat, spot);
                        return null;
                    }
                }
            }
            BoatsRejected++;
            return boat;
        }
        private void AddBoatToSlot(BoatsParent boat, int startSpot)
        {
            for (int i = 0; i < boat.HarbourSpotsRequired; i++)
            {
                harbourspots[startSpot + i] = boat;
            }
        }
        public void DisplayBoatsRejected()
        {
            Console.WriteLine($"{BoatsRejected} was rejected");
        }
        private bool RoomForBoat(int startSpot, int boatSize)
        {
            if (startSpot + boatSize > harbourspots.Length)
            {
                return false;
            }
            int emptySlots = 0;
            for (int i = startSpot; i < startSpot + boatSize; i++)
            {
                //Slot is occupied
                if (harbourspots[i] != null)
                {
                    return false;
                }
                emptySlots++;
            }
            if (emptySlots == boatSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DecrementDaysToStayFromBoatsAtEndOfDay()
        {
            var harbour = GetHarbourCapacity();
            var harbourList = harbour.Distinct().ToList();

            foreach (var harbourSpot in harbourList)
            {
                if (harbourSpot == null)
                {
                    continue;
                }
                harbourSpot.DaysLeftAtHarbour--;
            }
        }
        private void RemoveBoatFromSlot(int spot)
        {
            harbourspots[spot] = null;
        }
        public void RemoveLeavingBoatsFromHarbour()
        {
            var boatsAtHarbour = GetHarbourCapacity();
            for (int i = 0; i < boatsAtHarbour.Length; i++)
            {
                if (boatsAtHarbour[i] == null)
                {
                    continue;
                }
                if (boatsAtHarbour[i].DaysLeftAtHarbour == 0)
                {
                    RemoveBoatFromSlot(i);
                }
            }
        }

    }
}
