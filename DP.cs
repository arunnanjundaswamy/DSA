using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA_Prac2
{
    public class DP
    {
        public static void Test()
        {
            //others
            BuildAndPrintMatrix();
            
            BoundedKnapsackProblems();

            UnBoundedKnapsackProblems();

            FibonacciProblems();

            LongestCommonSubstringProblems();

        }
        

        #region Bounded Knapsack

        private static void BoundedKnapsackProblems()
        {
            //0-1Knapsack pattern
            KnapsackTest();

            //0-1Knapsack pattern
            IsEqualSumSetPossibleTest();

            //0-1Knapsack pattern
            IsSumSetPossibleTest();

            //0-1Knapsack pattern
            CountSumSetPossibleTest();

            //0-1Knapsack problem
            MinSumSubSetTest();


            MaxProfitStockBuySellTest();
        }

        

        #region Knapsack
        private static void KnapsackTest()
        {
            int[] items = new int[3] { 10, 20, 5 };

            int[] profits = new int[3] { 10000, 25000, 3000 };

            int knapsackCapacity = 25;

            int[,] mat = new int[profits.Length, items.Length];// { {-1,-1,-1 }, { -1, -1, -1 }, { -1, -1, -1 }, };

            for (int i = 0; i < profits.Length; i++)
            {
                for (int j = 0; j < profits.Length; j++)
                {
                    mat[i, j] = -1;
                }
            }

            //var val = KnapsackRecursive(items, itemValues, knapsackCapacity, items.Length, mat);

            var val = KnapsackTabular(items, profits, knapsackCapacity);

            Console.WriteLine($"Maximum profit = {val}");
        }

        private static int KnapsackTabular(int[] items, int[] itemValues, int knapsackCapacity)
        {
            //basic validations
            if (knapsackCapacity == 0 || items.Length == 0 || itemValues.Length == 0 || itemValues.Length != itemValues.Length)
                return 0;

            //Define dp array
            int[,] dp = new int[items.Length, knapsackCapacity + 1];

            //Initialize
            //1. When knapsackCapacity = 0
            //not possible to put anything in the bag
            for (int i = 0; i < items.Length; i++)
            {
                dp[i, 0] = 0;
            }
            //When i have only one item
            //Can put only that item for any capacity of the bag
            for (int capacity = 1; capacity <= knapsackCapacity; capacity++)
            {
                if (capacity >= items[0])
                    dp[0, capacity] = itemValues[0];
                else
                    dp[0, capacity] = 0;
            }

            //For others
            //if wt of current item is < capacity
            //check with this item and without this item an take which is max
            for (int row = 1; row < items.Length; row++)
            {
                for (int col = 1; col <= knapsackCapacity; col++)
                {
                    //if current index wt is less than capacity of sack
                    if (items[row] <= col)
                    {
                        //price without considering this item
                        var price1 = dp[row - 1, col];

                        //price considering this item and using other items for the remaining weight
                        var remainingWeight = knapsackCapacity - items[row];
                        var price2 = itemValues[row] + dp[row - 1, remainingWeight];
                        dp[row, col] = Math.Max(price1, price2);
                    }
                    else
                    {
                        //consider only the with previous elements for the same weight
                        dp[row, col] = dp[row - 1, col];
                    }
                }
            }

            return dp[items.Length - 1, knapsackCapacity];
        }

        #region KnapsackRecursive
        private static int KnapsackRecursive(int[] items, int[] itemValues, int knapsackCapacity, int totalItems, int[,] mat)
        {
            //itemsWt: laptop: 10kgs, mobile: 20kg, golden pot: 5kg : we are keeping wt only
            //item values: 10000 Rs, 8000 rs, 25000 rs
            //knapsack capacity: 15 kgs
            //Ans: 35000

            //Base conditions
            //if knapsackCapacity=0 or items.length = 0; return 0;

            //choice:
            //1. if weight of item is less than knapsack,
            //a.can consider 
            //--can consider current item and check for possible other optimal items
            //b. or need not consider current item if the optimal is for other items
            //2. if weight of item is more, need not consider
            //--check optimal for other items

            //base conditions
            if (knapsackCapacity <= 0 || totalItems == 0) return 0;

            //if (mat[knapsackCapacity, totalItems-1] != -1)
            //    return mat[knapsackCapacity, totalItems-1];

            int maxValue = 0;
            //choice
            //items will be removed on every iteration
            if (items[totalItems - 1] <= knapsackCapacity)
            {
                //considering current item and remaining
                var valueOfCurrentItem = itemValues[totalItems - 1];

                var remainingKnapsackCapacity = knapsackCapacity - items[totalItems - 1];

                var remainingItems = KnapsackRecursive(items, itemValues, remainingKnapsackCapacity, totalItems - 1, mat);

                var valueConsideringCurrentItem = valueOfCurrentItem + remainingItems;

                var ValueWoConsideringCurrentItem = KnapsackRecursive(items, itemValues, knapsackCapacity, totalItems - 1, mat);

                maxValue = Math.Max(valueConsideringCurrentItem, ValueWoConsideringCurrentItem);
                //mat[knapsackCapacity, totalItems] = maxValue;
            }
            else if (items[totalItems - 1] > knapsackCapacity)
            {
                maxValue = KnapsackRecursive(items, itemValues, knapsackCapacity, totalItems - 1, mat);
                //mat[knapsackCapacity, totalItems] = maxValue;
            }
            return maxValue;
        }
        #endregion KnapsackRecursive
        #endregion Knapsack

        #region IsSumSetPossible
        private static void IsSumSetPossibleTest()
        {
            int[] numbers = new int[] { 2, 3, 4, 5 };

            int totalSum = 10;

            var val = IsSumSetPossibleTabular(numbers, totalSum);

            Console.WriteLine($"Is Sum set possible={val}");
        }

        private static bool IsSumSetPossibleTabular(int[] numbers, int totalSum)
        {
            //inputs:[2,3,4,5]
            //totalSum: 5 - true
            //totalSum: 22 - false
            /*
                Input: {1, 2, 3, 7}, S=6
                Output: True
                The given set has a subset whose sum is '6': {1, 2, 3}

                Input: {1, 3, 4, 8}, S=6
                Output: False
                The given set does not have any subset whose sum is equal to '6'
             */

            if (totalSum == 0) return true;
            if (numbers.Length == 0 && totalSum > 0) return false;


            bool[,] dp = new bool[numbers.Length, totalSum + 1];//4,6

            //Initial conditions
            //if totalSum = 0
            for (int row = 0; row < numbers.Length; row++)
            {
                dp[row, 0] = true;
            }

            //if totalSum = 0
            for (int col = 1; col < totalSum + 1; col++)
            {
                dp[0, col] = numbers[0] == col;
            }

            for (int row = 1; row < (numbers.Length); row++)
            {
                for (int col = 1; col < (totalSum + 1); col++)
                {
                    if (numbers[row] <= col)
                    {
                        //anyways it is matching for this number or matching using previous numbers

                        //considering the current item or without considering the current item

                        var consideringCurrent = dp[row - 1, col - numbers[row]];

                        var woConsideringCurrent = dp[row - 1, col];

                        dp[row, col] = consideringCurrent || woConsideringCurrent;
                    }
                    else
                    {
                        dp[row, col] = dp[row - 1, col];
                    }
                }
            }

            return dp[numbers.Length - 1, totalSum];
        }

        #endregion IsSumSetPossible

        #region EqualSumSetPossible
        private static void IsEqualSumSetPossibleTest()
        {
            int[] numbers = new int[] { 2, 3, 4, 5 };

            int totalSum = numbers.Sum();

            var val = false;
            //check if sum is not even, then false
            if (totalSum % 2 != 0)
            {
                val = false;
                Console.WriteLine($"Is possible to do sumset={val}");
            }

            val = IsEqualSumSetPossibleTabular(numbers, totalSum / 2);

            Console.WriteLine($"Is possible to do sumset={val}");
        }

        private static bool IsEqualSumSetPossibleTabular(int[] numbers, int totalSum)
        {
            //inputs:[2,3,4,5]
            //totalSum: 7 - true

            if (totalSum == 0) return true;
            if (numbers.Length == 0 && totalSum > 0) return false;


            bool[,] dp = new bool[numbers.Length, totalSum + 1];//4,6
            //Initial conditions
            //if totalSum = 0
            for (int row = 0; row < numbers.Length; row++)
            {
                dp[row, 0] = true;
            }

            //if totalSum = 0
            for (int col = 1; col < totalSum + 1; col++)
            {
                dp[0, col] = (numbers[0] == col);
            }

            for (int row = 1; row < (numbers.Length); row++)
            {
                for (int col = 1; col < (totalSum + 1); col++)
                {
                    if (numbers[row] <= col)
                    {
                        //anyways it is matching for this number or matching using previous numbers

                        //considering the current item or without considering the current item

                        var consideringCurrent = dp[row - 1, col - numbers[row]];//What is the count, considering current element

                        var woConsideringCurrent = dp[row - 1, col];//What is the count wo considering current element

                        dp[row, col] = consideringCurrent || woConsideringCurrent;
                    }
                    else
                    {
                        dp[row, col] = dp[row - 1, col];
                    }
                }
            }

            return dp[numbers.Length - 1, totalSum];
        }

        #endregion EqualSumSetPossible

        #region CountSumSetPossibleTest
        //If we need to get the subsets then refer backtracking - since it checks for all the options of solution.
        private static void CountSumSetPossibleTest()
        {
            int[] numbers = new int[] { 2, 3, 4, 5 };
            int totalSum = 5;
            //output:{2,3}, {5} - so 2

            var val = CountSumSetPossible(numbers, totalSum);
            Console.WriteLine($"Number of subsets = {val}");
        }

        private static int CountSumSetPossible(int[] numbers, int totalSum)
        {
            //inputs:[2,3,4,5]
            //totalSum: 5
            //output:{2,3}, {5} - so 2 is ans

            if (totalSum == 0) return 1;
            if (numbers.Length == 0 && totalSum > 0) return 0;


            int[,] dp = new int[numbers.Length, totalSum + 1];//4,6
            //Initial conditions
            //if totalSum = 0
            for (int row = 0; row < numbers.Length; row++)
            {
                dp[row, 0] = 1;
            }

            //if totalSum = 0
            for (int col = 1; col < totalSum + 1; col++)
            {
                dp[0, col] = (numbers[0] == col) ? 1 : 0;
            }

            for (int row = 1; row < (numbers.Length); row++)
            {
                for (int col = 1; col < (totalSum + 1); col++)
                {
                    if (numbers[row] <= col)
                    {
                        //anyways it is matching for this number or matching using previous numbers
                        //considering the current item or without considering the current item
                        var consideringCurrent = dp[row - 1, col - numbers[row]];

                        var woConsideringCurrent = dp[row - 1, col];

                        dp[row, col] = consideringCurrent + woConsideringCurrent;//Add to the total
                    }
                    else
                    {
                        dp[row, col] = dp[row - 1, col];
                    }
                }
            }

            return dp[numbers.Length - 1, totalSum];
        }

        #endregion CountSumSetPossibleTest


        #region MinSumSubSetTest
        private static void MinSumSubSetTest()
        {
            //int[] numbers = new int[] { 2, 3, 4, 5 };
            //output:Ans = Range - 2* Set1
            //Ans: 14 - 2*7 = 0

            int[] numbers = new int[] { 8, 4, 10 };
            //output:Ans = Range - 2* Set1
            //Ans: 22 - 2*10 = 2
            var val = MinSumSubSet(numbers);

            Console.WriteLine($"Minimum subset = {val}");
        }



        private static int MinSumSubSet(int[] numbers)
        {
            /*
             Given a set of positive numbers, partition the set into two subsets with a
                minimum difference between their subset sums.
            Input: {1, 2, 3, 9}
            Output: 3
            Explanation: We can partition the given set into two subsets whe
            re the minimum absolute difference
            between the sum of numbers is '3'. Following are the two subsets
            : {1, 2, 3} & {9}.
             */
            //inputs:[2,3,4,5]
            //output:Ans = Range - 2* Set1
            //Ans: 14 - 2*7 = 0


            if (numbers.Length == 0) return 0;

            int totalSum = numbers.Sum() / 2;

            bool[,] dp = new bool[numbers.Length, totalSum + 1];//4,6
            //Initial conditions
            //if totalSum = 0
            for (int row = 0; row < numbers.Length; row++)
            {
                dp[row, 0] = true;
            }

            //if totalSum = 0
            for (int col = 1; col < totalSum + 1; col++)
            {
                dp[0, col] = (numbers[0] == col);
            }

            for (int row = 1; row < (numbers.Length); row++)
            {
                for (int col = 1; col < (totalSum + 1); col++)
                {
                    if (numbers[row] <= col)
                    {
                        //anyways it is matching for this number or matching using previous numbers
                        //considering the current item or without considering the current item
                        var consideringCurrent = dp[row - 1, col - numbers[row]];
                        var woConsideringCurrent = dp[row - 1, col];
                        dp[row, col] = consideringCurrent || woConsideringCurrent;
                    }
                    else
                    {
                        dp[row, col] = dp[row - 1, col];
                    }
                }
            }

            //Take the last element that is true
            int lastIndexVal = 0;
            for (int i = totalSum; i > 0; i--)
            {
                if (dp[numbers.Length - 1, i] == true)
                {
                    lastIndexVal = i;
                    break;
                }
            }
            var minDiff = numbers.Sum() - 2 * lastIndexVal;
            return minDiff;
        }

        #endregion MinSumSubSetTest


        private static void MaxProfitStockBuySellTest()
        {
            /*
             https://www.geeksforgeeks.org/maximum-profit-by-buying-and-selling-a-share-at-most-k-times/?ref=lbp
            https://www.algoexpert.io/questions/Max%20Profit%20With%20K%20Transactions
            Input:  
            Price = [10, 22, 5, 75, 65, 80]
                K = 2
            Output:  87
            Trader earns 87 as sum of 12 and 75
            Buy at price 10, sell at 22, buy at 
            5 and sell at 80
             */
            //Go with the tabular approach
            /*
                  10 22 5 75  65 80
                0 0  0  0  0  0  0
                1 0  12 12 70 70 75
                2 0  12 12 82 82 87

                  1  22 5  75  65 80
                0 0  0  0  0   0  0
                1 0  21 21 74  74 79
                2 0  21 21 91  91 96
             
             */
            /*
             transactions on y axis and day on x axis
             */
            var prices = new int[] { 10, 22, 5, 75, 65, 80 };
            int k = 2;

            //row - transactions
            //col = day prices
            var dp = new int[k + 1, prices.Length];//dp represents profit

            var cachedPrevBest = new Dictionary<string, int>();

            //Initialization:
            //1) When 0 transactions then no profit
            for (int col = 0; col < prices.Length; col++)
            {
                dp[0, col] = 0;
                if (col == 0)
                    cachedPrevBest[$"0:{col}"] = -0; //no prev
                else
                    cachedPrevBest[$"0:{col}"] = -prices[col - 1]; //no prev
            }

            //2) When you have only 1 day, you cannot sell on the same day and hence profit will be zero
            for (int row = 0; row <= k; row++)
            {
                dp[row, 0] = 0;
                cachedPrevBest[$"{row}:0"] = -prices[0]; //col 0 val
            }

            //Now for other transactions
            /*1) can take the profit of previous day if more than the current day i.e. profit[t, d-1] or
             *2) Sell on the current day & find which to buy by finding the min stock price of previous days (or do the transaction) + 
             *      profit of previous transaction if exists
             *      i.e. price[d] (selling price) - Least buying price + profit so far during the buying price day 
             *      i.e. price[d] + Max( -price(x) + profit(t-1, x)) where 0>=x<=d and d = current day 
             *      
             *3) Check for Max(1 or 2)
            */


            for (int row = 1; row <= k; row++)
            {
                for (int col = 1; col < prices.Length; col++)
                {
                    //1) Profit of previous day
                    var previousDayProfit = dp[row, col - 1];

                    //2)Current day sell
                    //current max profit + 
                    var currentDayPrice = prices[col];

                    var prevBest = FindPreviousMax(prices, col, row, dp, cachedPrevBest);

                    var currentDayProfit = (currentDayPrice + prevBest);

                    dp[row, col] = Math.Max(previousDayProfit, currentDayProfit);
                }
            }

            Console.WriteLine($"Maximum profit={dp[k, prices.Length -1]}");
        }

        //THis one:
        //Max( -price(x) + profit(t-1, x)) where 0>=x<=d and d = current day 
        private static int FindPreviousMax(int[] prices, int currentCol, int currentRow, int[,] dp, Dictionary<string, int> cachedPrevBest)
        {
            //Option 1
            /*
            var maxProfitOfPreviousTransaction = 0;

            for (int prevCol = 0; prevCol < currentCol; prevCol++)
            {
                maxProfitOfPreviousTransaction = Math.Max(maxProfitOfPreviousTransaction, -prices[prevCol] + dp[currentRow - 1, prevCol]);
            }

            return maxProfitOfPreviousTransaction;
            */

            //More optimized
            var prevBestBuy = cachedPrevBest.ContainsKey($"{currentRow}:{currentCol - 1 }") ? cachedPrevBest[$"{currentRow}:{currentCol - 1 }"] : 0;

            var currentBestBuy = Math.Max(prevBestBuy, (-prices[currentCol - 1] + dp[currentRow - 1, currentCol - 1]));
            cachedPrevBest.Add($"{currentRow}:{currentCol}", currentBestBuy);

            return currentBestBuy;
        }

        #endregion Bounded Knapsack

        #region UnBoundedKnapsackProblems

        private static void UnBoundedKnapsackProblems()
        {
            //UnboundKnapsack problem
            KnapsackUnboundTest();

            MaxSumOfNonAdjacentTest();
        }

        #region KnapsackUnboundTest
        private static void KnapsackUnboundTest()
        {
            /*
             Given two integer arrays to represent weights and profits of ‘N’ items, we
                need to find a subset of these items which will give us maximum profit
                such that their cumulative weight is not more than a given number ‘C’. We
                can assume an infinite supply of item quantities; therefore, each item can
                be selected multiple times.
             */
            //Ans: 10, 10, 5 i.e. 20000 + 20000 + 3000 = 43000 (10 is considered multiple times)
            int[] items = new int[3] { 10, 20, 5 };
            int[] itemValues = new int[3] { 20000, 25000, 3000 };
            int knapsackCapacity = 25;
            var tabularVal = KnapsackUnbound(items, itemValues, knapsackCapacity);

            Console.WriteLine($"Maximim profit = {tabularVal }");
        }

        private static int KnapsackUnbound(int[] items, int[] itemValues, int knapsackCapacity)
        {
            if (knapsackCapacity == 0) return 1;

            if (items.Length == 0) return 0;

            int[,] dp = new int[items.Length, knapsackCapacity + 1];

            //initialization
            //when knapsackcapacity is 0 then nothing
            for (int row = 0; row < items.Length; row++)
            {
                dp[row, 0] = 0;
            }
            for (int col = 1; col < knapsackCapacity + 1; col++)
            {
                if ((items[0] <= col))
                {
                    dp[0, col] = itemValues[0] + dp[0, col - items[0]];
                }
                else
                {
                    dp[0, col] = 0;
                }
            }

            for (int row = 1; row < items.Length; row++)
            {
                for (int col = 1; col < knapsackCapacity + 1; col++)
                {
                    if (items[row] <= col)
                    {
                        var option1 = dp[row - 1, col];
                        //Check this difference in option 2, we are considering the same item again as we can select it multiple times
                        var option2 = itemValues[row] + dp[row, col - items[row]];

                        dp[row, col] = Math.Max(option1, option2);
                    }
                    else
                    {
                        dp[row, col] = dp[row - 1, col];
                    }
                }
            }
            return dp[items.Length - 1, knapsackCapacity];
        }
        #endregion KnapsackUnboundTest

        private static void MaxSumOfNonAdjacentTest()
        {
            int[] arr = new int[] { 4, 2, 2, 4, 3, 2 };
            var val = MaxSumOfNonAdjacent(arr);

            Console.WriteLine($"Max nonadjacent = {val}");
        }

        private static int MaxSumOfNonAdjacent(int[] arr)
        {
            //Input: 4,2,2,4,3,2
            //Output: 10 i.e. 4,4,2
            //Other possibilities: 4,2,3=9 less than 10

            int[] dp = new int[arr.Length];

            //initialization
            dp[0] = arr[0];
            dp[1] = Math.Max(arr[0], arr[1]);//current element or the previous which is max, no 'prev to prev' to add

            for (int index = 2; index < arr.Length; index++)
            {
                //adding current element to previous to previous element
                int previousToPrevElement = index - 2;

                int currentVal = arr[index] + dp[previousToPrevElement];

                dp[index] = Math.Max(currentVal, dp[index - 1]);
            }

            return dp[arr.Length - 1];
        }

        #endregion UnBoundedKnapsackProblems

        #region LongestCommonSubstringProblems

        private static void LongestCommonSubstringProblems()
        {
            EditDistanceTest();

            WildcardMatchingPattern();
        }

        private static void EditDistanceTest()
        {
            /*
             * Convert S1 to S2
             * 
             Input: s1 = "bat"
                s2 = "but"
                Output: 1
                Explanation: We just need to replace 'a' with 'u' to transform s
                1 to s2.

            Input: s1 = "abdca"
                s2 = "cbda"
                Output: 2
                Explanation: We can replace first 'a' with 'c' and delete secon
                d 'c'.

            Input: s1 = "passpot"
                s2 = "ppsspqrt"
                Output: 3
                Explanation: Replace 'a' with 'p', 'o' with 'q', and insert 'r'.

            Approach:
            if s1[i1] == s2[i2]
                dp[i1][i2] = dp[i1-1][i2-1]
            else
                dp[i1][i2] = 1 + min(dp[i1-1][i2], // delete
                dp[i1][i2-1], // insert
                dp[i1-1][i2-1]) // replace
             */

            var s1 = "abdca";
            var s2 = "cbda";

            var dp = new int[s1.Length + 1, s2.Length + 1];


            //if s1 is empty and s2 exists then delete all s2 chars
            for (int i = 0; i < s2.Length; i++)
            {
                dp[0, i] = i;//4 chars in s2 means 4 deletions
            }

            //if s2 is empty and s1 exists - then all insert
            for (int i = 0; i < s1.Length; i++)
            {
                dp[i, 0] = i;
            }

            //when both s1 and s2 exists

            if(s1.Length > 0 && s2.Length > 0)
            {
                for (int row = 1; row <= s1.Length; row++)
                {
                    for (int col = 1; col <= s2.Length; col++)
                    {
                        //if(s1[row] == s2[col])
                        if(s1[row] == s2[col])
                        {
                            dp[row, col] = dp[row - 1, col - 1];
                        }
                        else
                        {
                            /*
                             *  dp[i1][i2] = 1 + 
                             *      min(dp[i1 - 1][i2], // delete
                                    dp[i1][i2 - 1], // insert
                                    dp[i1 - 1][i2 - 1]) // replace
                            */

                            dp[row, col] = 1 + (Math.Min(Math.Min(dp[row - 1, col], dp[row, col - 1]), dp[row - 1, col - 1]));
                        }
                    }
                }
            }

            var totalOps = dp[s1.Length, s2.Length];
            Console.WriteLine($"Total operations = {totalOps}");

        }



        #region WildcardMatchingPattern

        //refer this
        public static void WildcardMatchingPatternTest()
        {
            /*
              * ?’ – matches any single character 
                ‘*’ – Matches any sequence of characters (including the empty sequence)

                Text = "baaabab",
                Pattern = “*****ba*****ab", output : true
                Pattern = "baaa?ab", output : true
                Pattern = "ba*a?", output : true
                Pattern = "a*ab", output : false
             */

            var text = "baaabab";
            var pattern = "*****ba*****ab";

            var dp = new bool[text.Length + 1, pattern.Length + 1];

            //dp initialization
            //if no pattern and no char then true
            dp[0, 0] = true;

            //if no pattern is null but not char
            for (int row = 1; row < text.Length; row++)
            {
                dp[row, 0] = false;
            }

            for (int col = 1; col < pattern.Length; col++)
            {
                if (pattern[col] == '*')
                    dp[0, col] = true;
                else
                    dp[0, col] = false;
            }

            /*
             Approach:
                   a b ? * b
                 1 0 0 0 1 0
               a 0 1 0 0 1 0
               b 0 0 1 0 1 1
               c 0 0 0 1 1 0
               d 0 0 0 0 1 0 
               e 0 0 0 0 1 0
               b 0 0 0 0 1 1

            1) When pattern = '?'
               - Check if diagonal is true then true else false
            2) when pattern = '*'
                - true always
            3) When character matches
               - check if diagonal is true then true else false
            4) When text does not match the 'char' (e.g. a, b but not wildcard or ?) in pattern
               - false always
             
             */

            for (int row = 1; row <= text.Length; row++)
            {
                for (int col = 1; col <= pattern.Length; col++)
                {
                    if (text[row] == pattern[col])
                    {
                        dp[row, col] = dp[row - 1, col - 1];
                    }
                    else if (pattern[col] == '*')
                    {
                        dp[row, col] = dp[row - 1, col] || dp[row, col - 1];
                    }
                    else if (pattern[col] == '?')
                    {
                        dp[row, col] = dp[row - 1, col - 1];
                    }
                    else
                    {
                        dp[row, col] = false;
                    }
                }
            }
        }

        public static void WildcardMatchingPattern()
        {
            /*
             * ?’ – matches any single character 
‘*’ – Matches any sequence of characters (including the empty sequence)

                Text = "baaabab",
                Pattern = “*****ba*****ab", output : true
                Pattern = "baaa?ab", output : true
                Pattern = "ba*a?", output : true
                Pattern = "a*ab", output : false
            /*
         abc and ab
           a b
         a 1 0
         b 0 1
         c 0 0
         */

            /*
             abc and ?ab
                 ? a b
               1 0 0 0
             a 0 1 1 0
             b 0 0 0 1
             c 0 0 0 0
             */

            /*
             abc and a*bc
               a * b c
             a 1 1 0 0
             b 0 1 1 0
             c 0 1 0 1
             */

            /*
             abc and ab*
               a b *
             a 1 0 0
             b 0 1 1
             c 0 0 1
             */

            /*
             abc and a?bc
               a ? b c
             a 1 0 0 0
             b 0 1 0 1
             c 0 0 0 0
             
            */

            /*   * * * * * b a * * * * a b
             * b 1 1 1 1 1 1 0 0 0 0 0 0 0 
             * a 1 1 1 1 1 0 1 1 1 1 1 0 0
             * a 1 1 1 1 1 0 0 1 1 1 1 1 0
             * a 1 1 1 1 1 0 0 1 1 1 1 1 0
             * b 1 1 1 1 1 1 0 1 1 1 1 0 1
             * a 1 1 1 1 1 1 1 1 1 1 1 1 0
             * b 1 1 1 1 1 1 0 1 1 1 1 1 1

            /*
             If '?'
             - check if diagonal is 'true' or diagonal does not exist
             - else consider false if diagonal does not exist or false

            If '*'
            - True - If previous col does not exist or true or previous row does not exists or true
            - False - In other cases

            if char matches:
             - True check if diagonal is 'true' or diagonal does not exist check left
             - False else false for all

            1 1 0
            0 1 1
             */

            var text = "baaabab";
            var pattern = "*****ba*****ab";


            var dp = new bool[text.Length, pattern.Length];

            for (int rowIndex = 0; rowIndex <= text.Length - 1; rowIndex++)
            {
                Console.WriteLine(" ");
                for (int colIndex = 0; colIndex <= pattern.Length - 1; colIndex++)
                {
                    if (text[rowIndex] == pattern[colIndex])
                    {
                        if ((rowIndex == 0 && colIndex == 0))
                        {
                            dp[rowIndex, colIndex] = true; //very first element
                            Console.Write($",{dp[rowIndex, colIndex]}");
                            continue;
                        }

                        //if first row - check prev col
                        //if first col - check prev row

                        if (rowIndex == 0)
                        {
                            dp[rowIndex, colIndex] = dp[rowIndex, colIndex - 1];
                            Console.Write($",{dp[rowIndex, colIndex]}");
                            continue;
                        }
                        if (colIndex == 0)
                        {
                            dp[rowIndex, colIndex] = false;
                            Console.Write($",{dp[rowIndex, colIndex]}");
                            continue;
                        }

                        dp[rowIndex, colIndex] = dp[rowIndex - 1, colIndex - 1];
                        Console.Write($",{dp[rowIndex, colIndex]}");
                        continue;
                    }
                    if (pattern[colIndex] == '*')
                    {
                        if ((rowIndex == 0 && colIndex == 0))
                        {
                            dp[rowIndex, colIndex] = true; //very first element
                            Console.Write($",{dp[rowIndex, colIndex]}");
                            continue;
                        }

                        //if previous col is true
                        var prevColVal = colIndex > 0 && dp[rowIndex, colIndex - 1];
                        //if previous row is true
                        var prevRowVal = rowIndex > 0 && dp[rowIndex - 1, colIndex];

                        //if either previous row or col is true
                        dp[rowIndex, colIndex] = prevColVal || prevRowVal;
                        Console.Write($",{dp[rowIndex, colIndex]}");

                        continue;
                    }
                    if (pattern[rowIndex] == '?')
                    {
                        if ((rowIndex == 0 && colIndex == 0))
                        {
                            dp[rowIndex, colIndex] = true; //very first element
                            Console.Write($",{dp[rowIndex, colIndex]}");

                            continue;
                        }

                        if (rowIndex == 0)
                        {
                            dp[rowIndex, colIndex] = dp[rowIndex, colIndex - 1];
                            Console.Write($",{dp[rowIndex, colIndex]}");

                            continue;
                        }
                        if (colIndex == 0)
                        {
                            dp[rowIndex, colIndex] = false;
                            Console.Write($",{dp[rowIndex, colIndex]}");

                            continue;
                        }

                        dp[rowIndex, colIndex] = dp[rowIndex - 1, colIndex - 1];
                        Console.Write($",{dp[rowIndex, colIndex]}");

                        continue;
                    }

                    dp[rowIndex, colIndex] = false;
                    Console.Write($",{dp[rowIndex, colIndex]}");
                }
            }

            var isMatch = dp[text.Length - 1, pattern.Length - 1];

            Console.WriteLine($"Whether matches = {isMatch}");
        }
        #endregion WildcardMatchingPattern

        #endregion LongestCommonSubstringProblems

        #region Fibonacci series problems
        private static void FibonacciProblems()
        {
            CalculateFibonacciTest();

            StaircaseTraverseProblemsTest();

            StaircaseTraverseProblemsArbitraryStepsTest();

            HouseThiefProblemTest();

            FindNumberOfIslandsTest();

            MinNoOfJumpsTest();
        }

        #region fibonacci
        private static void CalculateFibonacciTest()
        {
            //0,1,1,2,3,5,8
            //Input: 6
            //Answer: 8
            var dp = new Dictionary<int, long>();

            //var fib = CalculateFibonacciRecursive(600, dp);
            //Console.WriteLine($"Fibonacci value= {fib}");

            var val = CalculateFibonacciDP(600);
            Console.WriteLine($"Fibonacci value= {val}");
        }

        //refer this one
        private static long CalculateFibonacciDP(int n)
        {
            //0,1,1,2,3,5,8
            long first = 0, second = 1, fib = 0;
            if (n == 0 || n == 1) return n;

            for (int i = 2; i < n; i++)
            {
                fib = first + second;
                first = second;
                second = fib;
            }

            return fib;
        }

        private static long CalculateFibonacciRecursive(int n, Dictionary<int, long> dp)
        {
            //0,1,1,2,3,5,8

            if (n == 0 || n == 1)
            {
                dp[n] = n;
                return n;
            }

            if (dp.ContainsKey(n)) return dp[n];

            dp[n] = CalculateFibonacciRecursive(n - 1, dp) + CalculateFibonacciRecursive(n - 2, dp);

            return dp[n];
        }

        #endregion fibonacci

        #region StaircaseTraverseProblems
        private static void StaircaseTraverseProblemsTest()
        {
            //Identifying the pattern is the key

            //https://www.youtube.com/watch?v=5o-kdjv7FD0
            //input: number of steps in staircase: n= 4
            //input: max number of steps a person can take: k= 2 (i.e. 1 or 2)
            //output: {1,1,1,1},{1,2,1},{2,1,1},{1,1,2},{2,2} = 5 ways


            //input: number of steps in staircase: 3
            //input: max number of steps a person can take: 2 (i.e. 1 or 2)
            //output: {1,1,1},{1,2},{2,1} = 5 ways


            //int val = StaircaseTraverseProblems();
            //Console.WriteLine("val===", val);

            //val = StaircaseTraverseProblems1();
            //Console.WriteLine("val===", val);

            //var dp = new Dictionary<int, int>();
            //val = StaircaseTraverseProblemsRecursive(5, 3, dp);
            //Console.WriteLine("val===", val);

            var val = StaircaseTraverseProblemsDP(5, 3);
            Console.WriteLine("val===", val);

        }

        //Fibonacci series problems
        //Refer this
        private static int StaircaseTraverseProblemsDP(int n, int k)
        {
            /*
             Approach:
            //If number of steps that can be taken is 1 or 2 and total steps = 5,then
            //StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2)
            //In bottom-up
            dp[0] = 1
            dp[1] = 1
            dp[2] = dp[1] + dp[0]
            dp[3] = dp[2] + dp[1]
            dp[4] = dp[3] + dp[2]
            dp[5] = dp[4] + dp[3]

            //If number of steps that can be taken is 1 or 2 or 3 and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3)
            //In bottom-up
            dp[0] = 1
            dp[1] = 1
            dp[2] = dp[1] + dp[0] 
            dp[3] = dp[2] + dp[1] + dp[0]
            dp[4] = dp[3] + dp[2] + dp[1]
            dp[5] = dp[4] + dp[3] + dp[2]

            //If number of steps that can be taken is 1 or 2 or 3 or 4 and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3) + StaircaseTraverseProblemsRecursive(n-4)

            //If number of steps that can be taken is 1 or 2 or 3 or 4 or ...k and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3) + StaircaseTraverseProblemsRecursive(n-4) + ..StaircaseTraverseProblemsRecursive(n-k)


            
             */

            //input: number of steps in staircase: n= 4
            //input: max number of steps a person can take: k= 2 (i.e. 1 or 2)
            //output: {1,1,1,1},{1,2,1},{2,1,1},{1,1,2},{2,2} = 5 ways

            //input: number of steps in staircase: 3
            //input: max number of steps a person can take: 2 (i.e. 1 or 2)
            //output: {1,1,1},{1,2},{2,1} = 3 ways
            //Soln Logic
            //Based on the number of steps that can be taken, the ans will be
            //CountWays(n) = CountWays(n-1) + CountWays(n-2) + CountWays(n-3) + ... + CountWays(n-k), for n >= k
            //int n = 4, k = 3;
            //k = 3, i.e user can take 1,2, or 3 steps at each step

            var dp = new Dictionary<int, int>();

            if (n == 0) { return 1; };
            if (n == 1) { return 1; };
            if (n == 2) { return 2; };

            //Initialization
            dp[0] = 1;//0 steps - 1 way - 0 i.e. [] subset is 1 way
            dp[1] = 1;//1 step - 1 way
            dp[2] = 2;//2 steps - [1,1][2] - 2ways

            for (int i = 3; i <= n; i++)
            {
                int numberOfJumps = 0;
                for (int loopIndex = 1; loopIndex <= k; loopIndex++)
                {
                    numberOfJumps = numberOfJumps + dp[i - loopIndex];
                }
                dp[i] = numberOfJumps;
            }

            return dp[n];
        }

        //Fibonacci series problems
        private static int StaircaseTraverseProblemsRecursive(int n, int k, Dictionary<int, int> dp)
        {
            /*
             Approach:
            //If number of steps that can be taken is 1 or 2 and total steps = 5,then
            //StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2)

            //If number of steps that can be taken is 1 or 2 or 3 and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3)

            //If number of steps that can be taken is 1 or 2 or 3 or 4 and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3) + StaircaseTraverseProblemsRecursive(n-4)

            //If number of steps that can be taken is 1 or 2 or 3 or 4 or ...k and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3) + StaircaseTraverseProblemsRecursive(n-4) + ..StaircaseTraverseProblemsRecursive(n-k)

             */

            //input: number of steps in staircase: n= 4
            //input: max number of steps a person can take: k= 2 (i.e. 1 or 2)
            //output: {1,1,1,1},{1,2,1},{2,1,1},{1,1,2},{2,2} = 5 ways



            //input: number of steps in staircase: 3
            //input: max number of steps a person can take: 2 (i.e. 1 or 2)
            //output: {1,1,1},{1,2},{2,1} = 5 ways
            //Soln Logic
            //Based on the number of steps that can be taken, the ans will be
            //CountWays(n) = CountWays(n-1) + CountWays(n-2) + CountWays(n-3) + ... + CountWays(n-k), for n >= k
            //int n = 4, k = 3;
            //k = 3, i.e user can take 1,2, or 3 steps at each step
            if (n == 0) { dp[0] = 1; return 1; };
            if (n == 1) { dp[1] = 1; return 1; };
            if (n == 2) { dp[2] = 2; return 2; };

            if (dp.ContainsKey(n)) return dp[n];


            int numberOfJumps = 0;

            for (int loopIndex = 1; loopIndex <= k; loopIndex++)
            {
                numberOfJumps = numberOfJumps + StaircaseTraverseProblemsRecursive(n - loopIndex, k, dp);
            }
            dp[n] = numberOfJumps;
            return numberOfJumps;
        }


        //Fibonacci series problems
        private static int StaircaseTraverseProblems()
        {
            //input: number of steps in staircase: n= 4
            //input: max number of steps a person can take: k= 2 (i.e. 1 or 2)
            //output: {1,1,1,1},{1,2,1},{2,1,1},{1,1,2},{2,2} = 5 ways

            //input: number of steps in staircase: 3
            //input: max number of steps a person can take: 2 (i.e. 1 or 2)
            //output: {1,1,1},{1,2},{2,1} = 5 ways
            //Soln Logic
            //Based on the number of steps that can be taken, the ans will be
            //CountWays(n) = CountWays(n-1) + CountWays(n-2) + CountWays(n-3) + ... + CountWays(n-k), for n >= k

            int n = 4, k = 2;
            int[] dp = new int[n + 1];

            //initialize
            dp[0] = dp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                int j = 1, val = 0;
                while (j <= k && (i - j) >= 0)
                {
                    val = val + dp[i - j];
                    j++;
                }
                dp[i] = val;
            }

            return dp[n];
        }


        //Fibonacci series problems
        private static int StaircaseTraverseProblems1()
        {
            //input: number of steps in staircase: n= 4
            //input: max number of steps a person can take: k= 2 (i.e. 1 or 2)
            //output: {1,1,1,1},{1,2,1},{2,1,1},{1,1,2},{2,2} = 5 ways

            //input: number of steps in staircase: 3
            //input: max number of steps a person can take: 2 (i.e. 1 or 2)
            //output: {1,1,1},{1,2},{2,1} = 5 ways
            //Soln Logic
            //Based on the number of steps that can be taken, the ans will be
            //CountWays(n) = CountWays(n-1) + CountWays(n-2) + CountWays(n-3) + ... + CountWays(n-k), for n >= k

            int n = 4, k = 2;
            int[,] dp = new int[k, n + 1];
            //initialize
            for (int col = 0; col <= n; col++)
            {
                dp[0, col] = 1;
            }
            for (int row = 1; row < k; row++)
            {
                dp[row, 0] = 1;
            }

            for (int row = 1; row < k; row++)
            {
                for (int col = 1; col <= n; col++)
                {
                    if (row + 1 <= col)
                    {
                        int j = col - 1, val = 0;
                        while (j >= (col - (row + 1)))
                        {
                            val = val + dp[row, j];
                            j--;
                        }
                        dp[row, col] = val;
                    }
                    else
                    {
                        dp[row, col] = dp[row, col - 1];
                    }
                }

            }

            return dp[k - 1, n];
        }
        #endregion StaircaseTraverseProblems

        #region StaircaseTraverseProblemsArbitrarySteps

        private static void StaircaseTraverseProblemsArbitraryStepsTest()
        {
            //https://www.youtube.com/watch?v=5o-kdjv7FD0
            //Identifying the pattern is the key

            var val = StaircaseTraverseProblemsArbitraryStepsDP(5, new int[] { 1, 3 });
            Console.WriteLine("val===", val);

        }

        //Fibonacci series problems
        //Refer this
        private static int StaircaseTraverseProblemsArbitraryStepsDP(int n, int[] steps)
        {
            /*
             Approach:
            //If number of steps that can be taken is 1 or 3 and total steps = 5,then
            //StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-3)
            //In bottom-up
            dp[0] = 1
            dp[1] = 1
            dp[2] = dp[1] + dp[0] or 0
            dp[3] = dp[2] + dp[0]
            dp[4] = dp[3] + dp[1]
            dp[5] = dp[4] + dp[2]
            dp[6] = dp[5] + dp[3]

            //If number of steps that can be taken is 1 or 3 or 4 and total steps = 5,then
            StaircaseTraverseProblemsRecursive(5) = StaircaseTraverseProblemsRecursive(n-1) + StaircaseTraverseProblemsRecursive(n-2) + StaircaseTraverseProblemsRecursive(n-3)
            //In bottom-up
            dp[0] = 1
            dp[1] = 1
            dp[2] = dp[1] + dp[0] or 0 + dp[-2] or 0
            dp[3] = dp[2] + dp[0] + dp[-1] or 0
            dp[4] = dp[3] + dp[1] + dp[0]
            dp[5] = dp[4] + dp[2] + dp[1]
            
             */

            //input: number of steps in staircase: 2
            //input: max number of steps a person can take: 1 or 3)
            //output: {1,1} = 1 ways

            //input: number of steps in staircase: n= 3
            //input: max number of steps a person can take:  1 or 3)
            //output: {1,1,1},{3} = 2 ways

            //input: number of steps in staircase: n= 4
            //input: max number of steps a person can take:  1 or 3)
            //output: {1,1,1,1},{1,3},{3,1} = 3 ways

            //input: number of steps in staircase: n= 5
            //input: max number of steps a person can take:  1 or 3)
            //output: {1,1,1,1,1},{1,1,3},{1,3,1}, {3,1,1} = 4 ways

            //input: number of steps in staircase: n= 6
            //input: max number of steps a person can take:  1 or 3)
            //output: {1,1,1,1,1,1},{1,1,1,3},{1,1,3,1},{1,3,1,1},{3,1,1,1},{3,3} = 6 ways

            //input: number of steps in staircase: n= 8
            //input: max number of steps a person can take:  1 or 3)
            //output: {1,1,1,1,1,1,1,1,1,1},{1,1,1,1,1,3},{1,1,1,1,3,1},{1,1,1,3,1,1},{1,1,3,1,1,1},{1,3,1,1,1,1},{3,1,1,1,1,1},{3,3,1,1},{3,1,3,1},{3,1,1,3},{1,3,1,3},{1,1,3,3},{1,3,3,1} = 13 ways

            //Soln Logic
            //Based on the number of steps that can be taken, the ans will be
            //CountWays(n) = CountWays(n-1) + CountWays(n-2) + CountWays(n-3) + ... + CountWays(n-k), for n >= k
            //int n = 4, k = 3;
            //k = 3, i.e user can take 1,2, or 3 steps at each step

            var dp = new Dictionary<int, int>();

            //steps = 1 or 3
            dp[0] = 1;
            dp[1] = 1;
            //dp[2] = 1;//applicable only for 1 or 3

            if (n == 0) { return 1; };
            if (n == 1) { return 1; };
            //if (n == 2) { return 2; };

            for (int i = 1; i < n; i++)
            {
                int numberOfJumps = 0;
                for (int loopIndex = 0; loopIndex < steps.Length; loopIndex++)
                {
                    if (i - steps[loopIndex] < 0)
                    {
                        numberOfJumps = 0;
                    }
                    else
                    {
                        numberOfJumps = numberOfJumps + dp[i - steps[loopIndex]];
                    }
                }
                dp[i] = numberOfJumps;
            }

            return dp[n - 1];
        }

        #endregion StaircaseTraverseProblemsArbitrarySteps

        #region HouseThiefProblem

        private static void HouseThiefProblemTest()
        {
            /*
                Given a number array representing the wealth of n houses, determine the
                maximum amount of money the thief can steal without alerting the
                security system.
                Example 1:
                Input: {2, 5, 1, 3, 6, 2, 4}
                Output: 15
                Explanation: The thief should steal from houses 5 + 6 + 4

                Input: {2, 10, 14, 8, 1}
                Output: 18
                Explanation: The thief should steal from houses 10 + 8
             */

            /*
             A thief can steal from current house (i) and next to next house (i+2)
            or
            Can steal from next house (i+1)
             */
            var inputArr = new int[] { 2, 5, 1, 3, 6, 2, 4 };
            var dp = new Dictionary<int, int>();
            dp[0] = 0;//no houses
            dp[1] = inputArr[0];//only one house

            for (int i = 1; i < inputArr.Length; i++)
            {
                var opt1 = inputArr[i] + dp[i - 1];///current item + prev to prev
                var opt2 = dp[i];//prev profit

                dp[i + 1] = Math.Max(opt1, opt2);

            }

            var val = dp[inputArr.Length];
            Console.WriteLine($"Max amount = {val}");
        }


        #endregion HouseThiefProblem

        #region FindNumberOfIslands
        private static void FindNumberOfIslandsTest()
        {
            /*
                010001
                011010
            Number of Islands = 3
             */
            var inputArr = PrepareInput1();
            var numOfIslands = FindNumberOfIslands(inputArr);
            Console.WriteLine($"number of islands={numOfIslands.Item1}");
            Console.WriteLine($"largest island={numOfIslands.Item2}");

            //Do not use this - incorrect
            //inputArr = PrepareInput1();
            //numOfIslands = FindNumberOfIslandsApproach2(inputArr);
            //Console.WriteLine($"number of islands={numOfIslands}");

        }

        private static (int, int) FindNumberOfIslands(int[,] inputArr)
        {
            if (inputArr == null || inputArr.GetLength(0) == 0 || inputArr.GetLength(1) == 0)
                return (0, 0);

            //Using DFS
            //Check if '1' - then check if there is any adjoint '1' to it and mark those as visited

            //visted dictionary
            var visited = new HashSet<string>();
            var result = 0;

            var currentIslandLength = 0;
            var islandLength = 0;
            //iterate through the node
            for (int rowIndex = 0; rowIndex < inputArr.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < inputArr.GetLength(1); colIndex++)
                {
                    //if node is '1' and not visited
                    if (inputArr[rowIndex, colIndex] == 1 && !visited.Contains($"{rowIndex}:{colIndex}"))
                    {
                        islandLength = Math.Max(currentIslandLength, islandLength);
                        currentIslandLength = 0;
                        DFSFindIslands(inputArr, rowIndex, colIndex, visited, ref currentIslandLength);
                        result = result + 1;
                    }
                }
            }
            return (result, islandLength);
        }

        //cover all '1' associated to the allowed '1' in calling fn
        private static void DFSFindIslands(int[,] inputArr, int rowIndex, int colIndex, HashSet<string> visited, ref int currentIslandLength)
        {
            var rows = inputArr.GetLength(0);
            var cols = inputArr.GetLength(1);

            //No need to visit if 0 and already visited
            if (rowIndex < 0 || rowIndex >= rows || colIndex < 0 || colIndex >= cols || visited.Contains($"{rowIndex}:{colIndex}")
                || inputArr[rowIndex, colIndex] == 0)
                return;

            //check left , right, top and bottom nodes that can be visited from current node and has value '1' and mark that as visited if '1'
            visited.Add($"{rowIndex}:{colIndex}");
            currentIslandLength++;

            DFSFindIslands(inputArr, rowIndex - 1, colIndex, visited, ref currentIslandLength);
            DFSFindIslands(inputArr, rowIndex + 1, colIndex, visited, ref currentIslandLength);
            DFSFindIslands(inputArr, rowIndex, colIndex - 1, visited, ref currentIslandLength);
            DFSFindIslands(inputArr, rowIndex, colIndex + 1, visited, ref currentIslandLength);

            return;
        }

        //This is the tabular approach or bottom-up
        private static int FindNumberOfIslandsApproach2(int[,] inputArr)
        {

            var rowSize = inputArr.GetLength(0);
            var colSize = inputArr.GetLength(1);

            var resArr = new int[rowSize, colSize];

            var lengthOfIsland = 0;
            var currlengthOfIsland = 0;

            for (var rowIndx = 0; rowIndx < rowSize; rowIndx++)
            {
                for (var colIndx = 0; colIndx < colSize; colIndx++)
                {
                    //check for left and top in the input array
                    //if island '1' and both top and left are 0 then rescellval = currentVal + Max(res[top], res[left])
                    //else
                    //rescellval = 0 + Max(res[top], res[left])

                    var leftOfCurrentRes = (colIndx - 1) >= 0 ? resArr[rowIndx, colIndx - 1] : 0;
                    var topOfCurrentRes = (rowIndx - 1) >= 0 ? resArr[rowIndx - 1, colIndx] : 0;

                    var left = (colIndx - 1) >= 0 ? inputArr[rowIndx, colIndx - 1] : 0;
                    var top = (rowIndx - 1) >= 0 ? inputArr[rowIndx - 1, colIndx] : 0;

                    if (inputArr[rowIndx, colIndx] == 1 && left == 0 && top == 0)
                    {
                        resArr[rowIndx, colIndx] = 1 + Math.Max(leftOfCurrentRes, topOfCurrentRes);
                        currlengthOfIsland = 1;
                        lengthOfIsland = Math.Max(currlengthOfIsland, lengthOfIsland);
                    }
                    else
                    {
                        resArr[rowIndx, colIndx] = Math.Max(leftOfCurrentRes, topOfCurrentRes);
                    }

                    //To find the bigger island
                    if (inputArr[rowIndx, colIndx] == 1 && (left == 1 || top == 1))
                    {
                        currlengthOfIsland++;
                    }

                }
            }
            lengthOfIsland = Math.Max(currlengthOfIsland, lengthOfIsland);

            //Last element in the result gives the result
            Console.WriteLine("Number of islands = " + resArr[rowSize - 1, colSize - 1]);
            Console.WriteLine("length of largest island = " + lengthOfIsland);

            return resArr[rowSize - 1, colSize - 1];
        }

        private static int[,] PrepareInput()
        {
            /*
             
                010
                011
            Number of Islands = 1
             */

            int[,] inputArr = new int[2, 3];
            inputArr[0, 0] = 0;
            inputArr[0, 1] = 1;
            inputArr[0, 2] = 0;
            inputArr[1, 0] = 0;
            inputArr[1, 1] = 1;
            inputArr[1, 2] = 1;

            //return inputArr;
            /*
             
                010
                001
            Number of Islands = 2
             */

            inputArr = new int[2, 3];
            inputArr[0, 0] = 0;
            inputArr[0, 1] = 1;
            inputArr[0, 2] = 0;
            inputArr[1, 0] = 0;
            inputArr[1, 1] = 0;
            inputArr[1, 2] = 1;

            return inputArr;
        }

        private static int[,] PrepareInput1()
        {
            /*
                011001
                011010
            Number of Islands = 3
             */

            int[,] inputArr = new int[2, 6];
            inputArr[0, 0] = 0;
            inputArr[0, 1] = 1;
            inputArr[0, 2] = 1;
            inputArr[0, 3] = 0;
            inputArr[0, 4] = 0;
            inputArr[0, 5] = 1;
            inputArr[1, 0] = 0;
            inputArr[1, 1] = 1;
            inputArr[1, 2] = 1;
            inputArr[1, 3] = 0;
            inputArr[1, 4] = 1;
            inputArr[1, 5] = 0;

            return inputArr;
        }
        #endregion FindNumberOfIslands

        

        

        #region MinNoOfJumps
        private static void MinNoOfJumpsTest()
        {
            int[] jumps = { 2, 1, 1, 1, 4 };
            //3
            Console.WriteLine(MinNoOfJumpsDP(jumps));

            jumps = new int[] { 1, 1, 3, 6, 9, 3, 0, 1, 3 };
            Console.WriteLine(MinNoOfJumpsDP(jumps));

            Console.WriteLine(" total = " + (MinNumOfJumpsDPDummy(jumps)));

            Console.WriteLine($"Min no of jumps = {MinNumOfJumpsBFSApproach(jumps)}");


            ////4
            //jumps = new int[] { 1, 2, 3, 1, 3 };
            //int total = 0;
            //Console.WriteLine(" total = " + (MinNumOfJumpsRecursive(jumps, 0, jumps[0])
            //));
            //total = 0;
            //jumps = new int[] { 1, 1, 3, 6, 9, 3, 0, 1, 3 };
            //Console.WriteLine(" total = " + (MinNumOfJumpsRecursive(jumps, 0, jumps[0])));

        }

        //refer this
        private static int MinNoOfJumpsDP(int[] jumps)
        {
            int[] dp = new int[jumps.Length];

            for (int i = 0; i < jumps.Length; i++)
            {
                dp[i] = -1000000;
            }

            for (int start = 0; start < jumps.Length - 1; start++)
            {
                for (int end = start + 1; end <= start + jumps[start] && end < jumps.Length; end++)
                {
                    dp[end] = Math.Min(dp[end], dp[start] + 1);
                }
            }

            return dp[jumps.Length - 1];
        }

        //Can refer this as well
        private static int MinNumOfJumpsDPDummy(int[] arr)
        {

            /*
             arr = {2,2,3,1,3}
                2-> 2:1,3:1= 1,1 - jump 1
                2-> 3:1, 1:dp[start = 2] + 1=2
                3-> 1:2, 3:dp[3] +1 = 2

            arr = {2,4,3,1,3}
                2-> 2:1, 4:1= 1,1 - jump 1
                4-> 3:1, 1:dp[start = 2], 3:dp[start]+1= 2 
                3-> 1:2, 3:dp[3] +1 = 2
                1-> 3:2

            arr = {2,4,5,1,3,4,7}
                2-> 4:1, 5:1= 1,1 - jump 1
                4-> 5:1, 1:dp[start = 4]=2, 3:dp[start]+1= 2, 4:2
                5-> 1:2, 3:2, 4:2, 7:dp[5]+1=2
                1-> 3:2
                3-> 4:

             arr = {2,1,5,4,1,3,4,7}
                2-> 1:1, 5:1
                1-> 5:1
                5-> 4:2, 1:2, 3:2, 4:2, 7:2 (break here)

            arr = {2,1,0,5,4,1,3,4,7}
                2-> 1:1, 0:1
                1-> 0:1 (ends here)
             */

            //arr = new int[] { 2, 2, 3, 1, 3 };
            //arr = new int[] { 2, 1, 0, 5, 4, 1, 3, 4, 7 };
            arr = new int[] { 2, 3, 0, 1, 4 };

            var dp = new Dictionary<int, int>();

            dp[0] = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0) continue;

                for (int j = i; j <= i + arr[i] && j < arr.Length; j++)
                {
                    if (!dp.ContainsKey(j) && dp.ContainsKey(i))
                    {
                        dp[j] = dp[i] + 1;
                    }
                }

                if (dp.ContainsKey(arr.Length - 1)) break;
            }

            return dp.ContainsKey(arr.Length - 1) ? dp[arr.Length - 1] : -100;//(unable to reach);
        }

        private static int MinNumOfJumpsBFSApproach(int[] arr)
        {
            //2,2,3,1,1,2
            /*
                2 - 2,3 - level 1
                2 - 3(level 1), 1(level 2), - 
                3 - 1(l2),1(l2),2(l2)--reached
            so number of levels = number of jumps


            //1, 1, 3, 6, 9, 3, 0, 1, 3
            1 - 1
            1 - 3
            3 - 6, 9, 3
            6 - 9, 3, 0, 1, 3
             */

            var visited = new HashSet<int>();

            var q = new Queue<(int index, int val, int level)>();
            q.Enqueue((0, arr[0], 0));

            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (current.index == arr.Length - 1)
                    return current.level;

                if (!visited.Contains(current.index))
                    visited.Add(current.index);

                for (var indx = current.index + 1; indx < (arr[current.index] + current.index + 1) && indx < arr.Length; indx++)
                {
                    if (!visited.Contains(indx))//index already in queue with min level updated --so no need
                        q.Enqueue((indx, arr[indx], current.level + 1));
                }
            }

            return -1;
        }


        //Refer this
        private static int MinNumOfJumpsRecursive1(int[] arr, int currentIndex)
        {
            /*
             1,2,3,1,3
                1->2->3->3

                1->2 Jumps: 2+1 = 3 (so min of immediate children + 1)
                2-> 3, 1 Jumps: 1 + 1 = 2

                3-> 1,3 Jumps: 1
                1-> 3 Jumps: 1

            [1,2,1]
            1->2 Jumps : 1 + 1
            2->1 Jumps: 1

            1,2,0,0,1
            1->2:
            2->0, 0:
            0-> jumps : max
            0-> jumps : max

             */

            if (currentIndex >= arr.Length - 1)
                return 0;

            if (arr[currentIndex] == 0)
                return int.MaxValue;

            var startIndex = currentIndex + 1;
            var endIndex = currentIndex + arr[currentIndex];

            var minJumps = Int32.MaxValue;

            for (int i = startIndex; i <= endIndex && i < arr.Length; i++)
            {
                var minVal = MinNumOfJumpsRecursive1(arr, i);
                if (minVal != int.MaxValue)
                    minJumps = Math.Min(minVal + 1, minJumps);
            }

            return minJumps;
        }

        private static int MinNumOfJumpsRecursive(int[] arr, int startIndex, int endIndex)
        {
            /*
             1,2,3,1,3
                1->2->3->3

                1->2 Jumps: 2+1 = 3 (so min of immediate children + 1)
                2-> 3, 1 Jumps: 1 + 1 = 2

                3-> 1,3 Jumps: 1
                1-> 3 Jumps: 1

            [1,2,1]
            1->2 Jumps : 1 + 1
            2->1 Jumps: 1
             */

            if (startIndex >= arr.Length - 1)
                return 0;


            var minJumps = Int32.MaxValue;

            for (int i = startIndex + 1; i <= endIndex && i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    var minVal = MinNumOfJumpsRecursive(arr, i, i + arr[i]);
                    minJumps = Math.Min(minVal + 1, minJumps);
                }
            }

            return minJumps;
        }

        #endregion MinNoOfJumps

        #endregion Fibonacci series problems


        private void TestCalculateMinTravelCost()
        {
            var mat = new int[22];
        }

        //private int CalculateMinTravelCost(int[][] mat, int sourceStn, int destStn)
        //{

        //}

        private static void BuildAndPrintMatrix()
        {
            int[,] mat = new int[2, 3] { { 0, 0, 2 }, { 1, 2, 3 } };

            int[,] a = new int[5, 2] { { 0, 0 }, { 1, 2 }, { 2, 4 }, { 3, 6 }, { 4, 8 } };

            foreach (int i in mat)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("\n");

            int intialval = 0;
            //{5,10,15},
            //{20,25,30}
            //assigning values to the array by using nested for loop
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    intialval += 5;
                    mat[i, j] = intialval;
                }
            }

            //printing
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(mat[i, j] + " ");
                }
            }
            //Console.ReadKey();
            a a1 = new b();

        }

    }

    public class a
    {
        public int aaa = 10;
        public a()
        {
            aaa = 100;
            Console.WriteLine("a");
        }
    }
    public class b:a
    {
        public b(): base()
        {
            Console.WriteLine("b");
            Console.WriteLine(aaa);
        }
    }


}

