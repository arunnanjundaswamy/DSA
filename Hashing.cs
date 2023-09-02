using System;
using System.Collections.Generic;

namespace DSA_Prac2
{
    public class Hashing
    {
        public static void Test()
        {

        }

        private static void FindItineraryTest()
        {
            /*
             * Find Itinerary from a given list of tickets
             https://www.geeksforgeeks.org/find-itinerary-from-a-given-list-of-tickets/

            Input:
            "Chennai" -> "Banglore"
            "Bombay" -> "Delhi"
            "Goa"    -> "Chennai"
            "Delhi"  -> "Goa"

            Output: 
            Bombay->Delhi, Delhi->Goa, Goa->Chennai, Chennai->Banglore,
             */


            var fromAndTo = new Dictionary<string, string>();
            fromAndTo.Add("Chennai", "Banglore");
            fromAndTo.Add("Bombay", "Delhi");
            fromAndTo.Add("Goa", "Chennai");
            fromAndTo.Add("Delhi", "Goa");

            //The solution is: any 'from' not in 'to' is the starting point
            //once the starting point is identified, find the next point using the 'value' of the starting point


            var onlyTos = new HashSet<string>(fromAndTo.Values);
            var startingPoint = "";

            foreach (var item in fromAndTo.Keys)
            {
                startingPoint = !onlyTos.Contains(item) ? item : "";
            }


            if(startingPoint == "")
            {
                Console.WriteLine("Unable to find the starting point");
                return;
            }

            var q = new Queue<string>();

            var probingTo = fromAndTo[startingPoint];
            q.Enqueue($"{startingPoint}->{probingTo}");
            Console.WriteLine($"{startingPoint}->{probingTo}");

            while(startingPoint != null)
            {
                startingPoint = fromAndTo.ContainsKey(probingTo) ? fromAndTo[probingTo] : null;
                probingTo = fromAndTo.ContainsKey(startingPoint)? fromAndTo[startingPoint]: null;

                Console.WriteLine($"{startingPoint}->{probingTo}");
            }

        }
    }
}
