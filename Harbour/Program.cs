using Hamnen.Classes;
using Hamnen.Classes.Harbour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hamnen
{
    class Program
    {
        public static int DayIterations;
        static void Main(string[] args)
        {
            GameGreeting();
            DayIterations = 1;
            Harbour h = new Harbour();
            while (DayIterations < 20)
            {
                h.RemoveLeavingBoatsFromHarbour();
                var harbourQueue = h.CreateRandomBoatQueueObjects();
                foreach(var boat in harbourQueue)
                {
                    h.AddBoatsToSlot(boat);
                }
                h.DisplayHarbourCapacity();
                h.DisplayBoatsRejected();
                harbourQueue.Clear();
                DayIterations++;
                h.DecrementDaysToStayFromBoatsAtEndOfDay();
                Console.WriteLine("Press any key to Continue to the next day..");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine("The harbour sim is now complete..");
        }
        public static void GameGreeting()
        {
            Console.WriteLine("Hej och välkommen till Hamnen");
            Console.WriteLine("Tryck på valfritangent för att starta..");
            Console.ReadKey();
        }
    }
}
