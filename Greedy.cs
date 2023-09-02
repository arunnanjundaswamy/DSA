using System;
using System.Collections.Generic;

namespace DSA_Prac2
{
    public class Greedy
    {
        public static void GreedyTest()
        {
            /*
             https://www.youtube.com/watch?v=bC7o8P_Ste4
             */
            MinimumWaitTimeTest();

            FindNumberOfPlatformsTest();

            FindmMaximumMeetingsInOneRoomTest();

            MaxProfitJobSequencingProblemTest();

            FindMinRightBulbsToOn();

            FindDisjoinIntervalsTest();

            FindMinCoinsForDenomiationTest();

            MinCandiesToKidsTest();

            FindLargestNumberWithSwapsTest();
        }

        private static void MinimumWaitTimeTest()
        {
            //Calculate the minimum wait time for the array
            //Input waittimes: [8,2,4,1]
            //E.g. If rest of the people has to wait till the processing is done, make sure that the entire process happens in min time
            //(8*3)+((2)*2)+((4)*1)+((1)*0)=
            //Total time spent = 24+4+4+0=32
            //Instead if we sort in ascending order
            //1,2,4,8, then
            //(1*3)+((2)*2)+((4)*1)+((8)*0)=3+4+4+0=11--this is more optimal
            //Ans:16
            //Input: 3, 2, 1, 2, 6
            //1,2,2,3,6
            //1*4 + 2*3 + 2*2 + 3*1 + 6*0
            //4+6+4+3+0 = 17

            var input = new List<int> { 3, 2, 1, 2, 6 };// { 8, 2, 4, 1 };
            var totalWaitTime = MinimumWaitTime(input);
            Console.WriteLine("Total time to process = {0}", totalWaitTime);
        }

        private static int MinimumWaitTime(List<int> inputArr)
        {
            inputArr.Sort();
            int end = 0;
            int totalWaitingTime = 0;
            int totalPeoplePendingToProcess = inputArr.Count;

            while (end < inputArr.Count)
            {
                totalPeoplePendingToProcess = totalPeoplePendingToProcess - 1;
                totalWaitingTime = totalWaitingTime + ((inputArr[end]) * (totalPeoplePendingToProcess));

                end++;
            }

            return totalWaitingTime;
        }

        private static void FindNumberOfPlatformsTest()
        {
            //https://www.geeksforgeeks.org/minimum-number-platforms-required-railwaybus-station/

            int[] arr = { 900, 940, 950, 1100, 1500, 1800 };
            int[] dep = { 910, 1200, 1120, 1130, 1900, 2000 };

            //Sort arrival:   900, 940, 950, 1100, 1500, 1800
            //Sort departure: 910, 1120,1130,1200, 1900, 2000
            //Ans: 3

            int n = arr.Length;

            Console.WriteLine("Minimum Number of "
                          + " Platforms Required = " + FindNumberOfPlatforms(arr, dep, n));
        }

        private static int FindNumberOfPlatforms(int[] arr, int[] dep, int n)
        {
            Array.Sort(arr);
            Array.Sort(dep);

            var arrIndex = 0;
            var depIndex = 0;
            var totalPlatforms = 0;
            var res = 0;

            while (arrIndex < arr.Length && depIndex < dep.Length)
            {
                if (arr[arrIndex] <= dep[depIndex])
                {
                    //Once new train arrived, increment the count
                    totalPlatforms++;
                    arrIndex++;
                }
                else if (arr[arrIndex] > dep[depIndex])
                {
                    //Once departed, decrement the count
                    totalPlatforms--;
                    depIndex++;
                }

                res = Math.Max(res, totalPlatforms);
            }

            return res;
        }

        private static void FindmMaximumMeetingsInOneRoomTest()
        {
            /*
             * https://www.geeksforgeeks.org/activity-selection-problem-greedy-algo-1/
             * Here we need to decide whether the meeting should happen or not and can ignore that meeting if required
             Input:
                N = 6
                start[] =   {1300,1500,1200,1700,2000,1700}
                end[] =     {1400,1600,1800,1900,2100,2100}
                Output: 4

            Approach: Go greedy and select the activities that end early
            After sort by endtime:
            1300,1500,1700,2000
            1400,1600,1900,2100
            
             */


            int[] start = { 1300, 1500, 1200, 1700, 2000, 1700 };
            int[] end = { 1400, 1600, 1800, 1900, 2100, 2100 };

            var activities = new List<Activity>();
            for (int i = 0; i < start.Length; i++)
            {
                activities.Add(new Activity(start[i], end[i]));
            }
            //Sort based on the endtime -- this will give the least job duration
            //activities.Sort();//this will also work
            activities.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));

            var selectedActivities = new List<Activity>();
            var firstActivity = activities[0];
            selectedActivities.Add(firstActivity);
            Console.WriteLine($"start time:{ firstActivity.StartTime} and end time: {firstActivity.EndTime}");

            var previousIndex = 0;

            //next job
            for (int nextIndex = 1; nextIndex < activities.Count; nextIndex++)
            {
                if (activities[nextIndex].StartTime >= activities[previousIndex].EndTime)
                {
                    selectedActivities.Add(activities[nextIndex]);
                    Console.WriteLine($"start time:{ activities[nextIndex].StartTime} and end time: {activities[nextIndex].EndTime}");
                    previousIndex++;
                }
            }
        }

        public class Activity : IComparable<Activity>
        {
            public Activity(int startTime, int endTime)
            {
                this.StartTime = startTime;
                this.EndTime = endTime;
            }
            public int StartTime { get; set; }
            public int EndTime { get; set; }

            public int CompareTo(Activity other)
            {
                return this.EndTime.CompareTo(other.EndTime);
            }
        }

        private static void FindMinRightBulbsToOn()
        {
            /*
             * N light bulbs are connected by a wire. Each bulb has a switch associated with it, however due to faulty wiring, a switch also changes the state of all the bulbs to the right of current bulb. Given an initial state of all bulbs, find the minimum number of switches you have to press to turn on all the bulbs. You can press the same switch multiple times. 
                Note: 0 represents the bulb is off and 1 represents the bulb is on. 

                Input :  [0 1 0 1]
                Output : 4
                Explanation :
                    press switch 1 : [1 0 1 0]
                    press switch 2 : [1 1 0 1]
                    press switch 3 : [1 1 1 0]
                    press switch 4 : [1 1 1 1]

                Input : [1 0 0 0 0] 
                Output : 1
             */

            var res = 0;

            //if count = even and original state of that bulb is on, then no need to change
            //if count = odd and original state of that bulb is off, then no need to change

            //in other cases increment the count and move to the next
            var inputArr = new int[] { 0, 1, 0, 1 };
            var count = 0;

            for (var index = 0; index < inputArr.Length; index++)
            {
                var sc1 = (count % 2 == 0 && inputArr[index] == 1);
                var sc2 = (count % 2 == 1 && inputArr[index] == 0);
                if (sc1 || sc2) continue;

                if (count % 2 == 0 && inputArr[index] == 0)
                {
                    res++;
                    count++;
                    continue;
                }

                if (count % 1 == 0 && inputArr[index] == 1)
                {
                    res++;
                    count++;
                    continue;
                }
            }

            Console.WriteLine($"Number of times it has to flip= {res}");
        }

        private static void FindDisjoinIntervalsTest()
        {
            /*
                Input Arr: [1,4] [2, 3] [4,6] [8,9]
                Ans: 3 i.e [2,3] [4,6] [8,9]

            Algo:
            Sort by end time : [2,3] [1,4] [4,6] [8,9]
            check if 2nd start time is less than or equals first end time and if yes do not take into account
             */

            var inputArr = new List<Activity> {
                new Activity(1,4),
                new Activity(2,3),
                new Activity(4,6),
                new Activity(8,9),
            };

            //Sort
            inputArr.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));

            var prev = inputArr[0];
            var count = 1;
            for (int i = 1; i < inputArr.Count; i++)
            {
                if (inputArr[i].StartTime <= prev.EndTime)
                    continue;

                prev = inputArr[i];
                count++;
            }
            
        }

        

        

        private static void MaxProfitJobSequencingProblemTest()
        {
            /*Note: if start time and end time is given - go for dp
             * https://www.geeksforgeeks.org/job-sequencing-problem/?ref=lbp
             * https://www.youtube.com/watch?v=LjPx4wQaRIs
             Input: Four Jobs with following deadlines and profits
            JobID  Deadline  Profit
              a      4        20   
              b      1        10
              c      1        40  
              d      1        30
            Output: Following is maximum 
            profit sequence of jobs
                    c, a

            Algo:
            1. Arrange all jobs in decreasing order of price
            2. For each job (job m(i)), do a linear search to find the slot in the array of size n, where n = max deadline
            and m = total jobs
             */
            var maxProfit = 0;
            var inputArr = new List<Job>
            {
                new Job("a",4, 20),//here 4 means any slots between 1 to 4 (or index 0 to 3) 
                new Job("b",1, 10),
                new Job("c",1, 40),
                new Job("d",1, 30),
            };
            //In this case there can only be 2 items in list arr
            //4 and 1
            //But if input is
            /*
                new Job("a",4, 20),//here 4 means any slots between 1 to 4 (or index 0 to 3) 
                new Job("b",2, 10),//here 2 means any slots between 1 to 2 (or index 0 to 1) 
                new Job("c",2, 40),
                new Job("d",1, 30),

                Ans: 10, 30, 40, 20

                new Job("a",4, 20),//here 4 means any slots between 1 to 4 (or index 0 to 3) 
                new Job("b",2, 10),//here 2 means any slots between 1 to 2 (or index 0 to 1) 
                new Job("c",2, 40),
                new Job("d",1, 30),
                new Job("d",3, 300),

                Ans: 40, 300, 
             */



            //descending order
            inputArr.Sort((a, b) => b.Profit.CompareTo(a.Profit));
            //40-c, 30-d, 20-4, 10-b

            //Get the slots based on deadline
            var numberOfSlots = 0;
            for (int i = 0; i < inputArr.Count; i++)
            {
                numberOfSlots = Math.Max(numberOfSlots, inputArr[i].Deadline);
            }

            var slots = new int[numberOfSlots];

            foreach (var item in inputArr)
            {
                var slotIndex = GetSlotIndex(item, slots);

                if(slotIndex > -1)
                {
                    slots[slotIndex] = item.Profit;
                    maxProfit = maxProfit + item.Profit;
                }

                //if (!slot.ContainsKey(item.Deadline))
                //{
                //    slot.Add(item.Deadline, item.Id);
                //    maxProfit = maxProfit + item.Profit;
                //}
            }
        }

        private static int GetSlotIndex(Job item, int[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                //Any slot free lessthen that deadline can be taken
                //e.g. 3 is the deadline then 3,2,1 can be taken
                if ((slots[item.Deadline - 1] - i) == 0)//0 means is available
                    return (item.Deadline - 1) - i;
            }

            return -1;
        }

        public class Job
        {
            public string Id { get; set; }
            public int Deadline { get; set; }
            public int Profit { get; set; }

            public Job(string id, int deadline, int profit)
            {
                Id = id;
                Deadline = deadline;
                Profit = profit; 
            }
        }

        

        

        private static void FindMinCoinsForDenomiationTest()
        {
            //Denominations can be used infinitely
            //Ans:
            /*
             Input: V = 70
            Output: 2
            We need a 50 Rs note and a 20 Rs note.

            Input: V = 121
            Output: 3
            We need a 100 Rs note, a 20 Rs note and a 1 Rs coin.
             */
            int[] denominations = { 1, 2, 5, 10, 20,
                      50, 100, 500, 1000 };

            int totalAmount = 70;
            Console.Write("Following is minimal number " +
                          "of change for " + totalAmount + ": ");

            var currenciesUsed  = FindMinCoinsForDenomiation(totalAmount, denominations);

            currenciesUsed.ForEach(curr =>
            {
                Console.WriteLine($"Currencies used={curr}");
            });
        }

        private static List<int> FindMinCoinsForDenomiation(int totalAmount, int[] denominations)
        {
            Array.Sort(denominations);

            var currenciesUsed = new List<int>();

            //Start from the largest
            for(int index = denominations.Length -1; index >=0; index--)
            {
                //iterate using the same currency, otherwise use the next lower currency
                while(totalAmount >= denominations[index])
                {
                    totalAmount = totalAmount - denominations[index];
                    currenciesUsed.Add(denominations[index]);
                }
            }

            return currenciesUsed;
        }


        private static void MinCandiesToKidsTest()
        {
            /*
             * Given an array arr[] consisting of N positive integers representing the ratings of N children, the task is to find the minimum number of candies required for distributing to N children such that every child gets at least one candy and the children having the higher rating get more candies than its neighbours.
             * 
             Input: arr[] = {1, 0, 2}
                Output: 5
                Explanation:
                Consider the distribution of candies as {2, 1, 2} that satisfy the given conditions. Therefore, the sum of candies is 2 + 1 + 2 = 5, which is the minimum required candies.

                Input: arr[] = {1, 2, 2}
                Output: 4

                    102
                   -112
                   -211
                   =212=5

                     122
                    :121
                    :111
                    =121=4
             */

            var arr = new int[] { 1, 0, 2 };

            MinCandiesToKids(arr);

            //Incorrect --dont refer
            MinCandiesToKidsMoreOptimal(arr);
        }

        

        private static void MinCandiesToKids(int[] arr)
        {
            var leftToRight = new int[arr.Length];
            var rightToLeft = new int[arr.Length];
            var ans = new int[arr.Length];

            leftToRight[0] = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[i - 1])
                {
                    leftToRight[i] = leftToRight[i - 1] + 1;
                }
                else
                {
                    //default
                    leftToRight[i] = 1;
                }
            }

            rightToLeft[arr.Length - 1] = 1;
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] > arr[i + 1])
                {
                    rightToLeft[i] = rightToLeft[i + 1] + 1;
                }
                else
                {
                    //default
                    rightToLeft[i] = 1;
                }
            }

            //note:
            //if any kid has same value in any one neighbour and higher weightage on the other side, then assign '1' -
            //since that kid cannot compare

            Console.WriteLine("Kids candies = ");

            //compare and take highest
            for (int i = 0; i < arr.Length; i++)
            {
                ans[i] = Math.Max(leftToRight[i], rightToLeft[i]);
                Console.Write($"{ans[i]}");

            }

        }

        private static void MinCandiesToKidsMoreOptimal(int[] arr)
        {
            //Sort by ratings
            var sortedList = new List<(int data, int index)> ();

            for (int i = 0; i < arr.Length; i++)
            {
                sortedList.Add((arr[i], i));
            }

            sortedList.Sort((a, b) => a.data.CompareTo(b.data));

            var ans = new int[sortedList.Count];
            var totalCandies = 0;

            Console.WriteLine("Kids candies = ");


            foreach (var item in sortedList)
            {
                var itemIndex = item.index;
                ans[itemIndex] = 1;

                //check left
                if (itemIndex > 0 && arr[itemIndex] > arr[itemIndex - 1])
                    ans[itemIndex] = Math.Max(ans[itemIndex], ans[itemIndex - 1]+ 1);

                //and right
                if (itemIndex < sortedList.Count - 1 &&  arr[itemIndex] > arr[itemIndex + 1])
                    ans[itemIndex] = Math.Max(ans[itemIndex], ans[itemIndex + 1] + 1);

                Console.Write($"{ans[itemIndex]}");

                totalCandies = totalCandies + ans[itemIndex];
            }
        }

        private static void FindLargestNumberWithSwapsTest()
        {
            /*
             Input some continous number from 1 to n e.g. 1...5
            and m swaps e.g. 2 swaps
            input :
            arr[2,1,3] and k = 1
            Output: [3,1,2]


            arr[4,1,2,5,3] k = 3
            I1: [5,1,2,4,3]
            I2: [5,4,2,1,3]
            I3: [5,4,3,1,2]

            Imp: It is always continuous number so max number in the array will be the arr.Length
            so max val = 5 in above case
             */

            var inputArr = new int[] { 4, 1, 2, 5, 3 };
            var swaps = 3;

            var currentMaxNumberIndex = inputArr.Length - 1;

            //map of number and index
            var dict = new Dictionary<int, int>();

            for (int i = 0; i < inputArr.Length; i++)
            {
                dict.Add(inputArr[i], i);
            }

            for (int i = 0; i < inputArr.Length && swaps  > 0; i++)
            {
                //check if the max number is in the left most position --if not swap it

                //5 is in 0th pos?
                if (dict[currentMaxNumberIndex] == i)
                    continue;

                var temp = inputArr[i];
                var j = dict[currentMaxNumberIndex];

                inputArr[i] = inputArr[j];
                inputArr[j] = temp;

                //4,5,2,3,1//initial dict
                //5,4,2,3,1
                //so swap the dict also
                //dict[5] = 1
                //dict[4] = 0
                //change to:
                //dict[5] = 0
                //dict[4] = 1

                dict[currentMaxNumberIndex] = i;
                dict[temp] = j;

                currentMaxNumberIndex--;
                swaps--;
            }
        }
    }
    
}
