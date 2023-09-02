using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class SlidingWindowsProblemsTest
    {
        public static void Test()
        {
            GetMaximumSumSubArrayTest();

            FindFirstNegativeInArrOfSizeKTest();

            GetSmallestSubArrayForSumTest();

            GetMaximumOfSubarraysOfkTest();

            GetLargestSubArrayTest();

            GetMinSubstringTest();

            GetAnagramsCountTest();

            GetLongestSubstringWithCharsTest();

           
        }

        

        #region GetMaximumSumSubArray
        private static void GetMaximumSumSubArrayTest()
        {
            var arr = new int[] { 1, 2, 3, 4, 5, 6 };

            //var max = cls.GetMaximumSumSubArray(arr, 3);
            //Console.WriteLine(max);

            var val = GetMaximumSumSubArrayOpt1(arr, 3);
            Console.WriteLine($"Max sumsubarray={val}");

            arr = new int[] { 6, 5, 4, 3, 2, 1 };

            val = GetMaximumSumSubArrayOpt1(arr, 3);
            Console.WriteLine($"Max sumsubarray={val}");
        }

        //Refer this
        private static int GetMaximumSumSubArrayOpt1(int[] arr, int windowSize)
        {
            //Maximum Sum Subarray of Size K

            //1,2,3,4,5,6
            //1,2,3| 2,3,4 | 3,4,5

            int start = 0, end = 0;
            int max = Int32.MinValue;

            var windowVal = 0;

            while (end < arr.Length)
            {
                var windowScope = (end - start + 1);

                if (windowScope < windowSize)
                {
                    windowVal = windowVal + (arr[end]);
                    end++;
                }
                else if (windowScope == windowSize)
                {
                    windowVal = windowVal + (arr[end]);
                    max = Math.Max(windowVal, max);

                    //Before incrementing the start - subtract the value of the start index from sum
                    windowVal = windowVal - arr[start];
                    start++;
                    end++;
                }

                //This condition will never arise  as the sart is incremented
                //else if (windowScope > windowSize)
                //{
                //    windowVal = windowVal - arr[start];

                //    start++;
                //}
            }

            return max;
        }

        private static  int GetMaximumSumSubArray1(int[] arr, int windowSize)
        {
            int start = 0, end = 0;
            int max = Int32.MinValue;

            var windowVal = 0;

            while (end < arr.Length)
            {
                var windowScope = (end - start + 1);

                if (windowScope < windowSize)
                {
                    windowVal = windowVal + (arr[end]);
                    end++;
                }
                else if (windowScope == windowSize)
                {
                    windowVal = windowVal + (arr[end]);
                    max = Math.Max(windowVal, max);
                    end++;
                }
                else if (windowScope > windowSize)
                {
                    windowVal = windowVal - arr[start];
                    start++;
                }
            }
            return max;
        }

        //Maximum Sum Subarray of Size K

        //1,2,3,4,5,6
        //1,2,3| 2,3,4 | 3,4,5
        private static int GetMaximumSumSubArray(int[] arr, int windowSize)
        {
            int maxVal = 0;
            int windowStart = 0, windowEnd = 0;
            int windowVal = 0;

            while (windowEnd < arr.Length)
            {
                windowVal = windowVal + arr[windowEnd];

                if (windowEnd >= (windowSize - 1))
                {
                    maxVal = Math.Max(maxVal, windowVal);

                    windowVal = windowVal - arr[windowStart];

                    windowStart++;
                }

                windowEnd++;
            }


            return maxVal;
        }

        #endregion GetMaximumSumSubArray

        #region FindFirstNegativeInArrOfSizeK
        private static void FindFirstNegativeInArrOfSizeKTest()
        {
            //[-8,2,3,-6,10]
            //[-8,0,-6,-6]
            FindFirstNegativeInArrOfSizeKBrute(new int[] { -8, 2, 3, -6, 10 }, 2);

            FindFirstNegativeInArrOfSizeKBrute(new int[] { 12, -1, -7, 8, -15, 30, 16, 28 }, 3);

            FindFirstNegativeInArrOfSizeK(new int[] { -8, 2, 3, -6, 10 }, 2);

            FindFirstNegativeInArrOfSizeK(new int[] { 12, -1, -7, 8, -15, 30, 16, 28 }, 3);
        }

        /// <summary>
        /// 1. Maintain an array with indexes of the negative number
        /// 2. The first element of this array will be the result of the first negative number
        /// 3. Once the window is slide and if this array contains start index, then this will be removed from the indexes array 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static int[] FindFirstNegativeInArrOfSizeK(int[] arr, int size)
        {
            /*
             Input : arr[] = {-8, 2, 3, -6, 10}, k = 2
                Output : -8 0 -6 -6
                First negative integer for each window of size k
                {-8, 2} = -8
                {2, 3} = 0 (does not contain a negative integer)
                {3, -6} = -6
                {-6, 10} = -6

                Input : arr[] = {12, -1, -7, 8, -15, 30, 16, 28} , k = 3
                Output : -1 -1 -7 -15 -15 0
             */
            //This array stores the value
            List<int> ans = new List<int>();

            //This array temporarily stores the index of negative numbers
            List<int> aux = new List<int>();

            int start = 0, end = 0;

            while (end < arr.Length)
            {
                if (end - start + 1 < size)
                {
                    if (arr[end] < 0)
                        aux.Add(end);

                    end++;
                    continue;
                }

                if (end - start + 1 == size)
                {
                    if (arr[end] < 0)
                        aux.Add(end);

                    if (aux.Count > 0)
                    {
                        Console.WriteLine("First Negative is {0}", arr[aux[0]]);
                        ans.Add(arr[aux[0]]);
                    }
                    else
                    {
                        Console.WriteLine("First Negative is {0}", 0);
                        ans.Add(0);
                    }

                    //Remove the index in aux array before it is slide
                    if (aux.Count > 0 && aux[0] == start)
                        aux.RemoveAt(0);

                    start++;
                    end++;
                }
            }

            return ans.ToArray();
        }


        private static int[] FindFirstNegativeInArrOfSizeKBrute(int[] arr, int size)
        {
            /*
             Input : arr[] = {-8, 2, 3, -6, 10}, k = 2
                Output : -8 0 -6 -6
                First negative integer for each window of size k
                {-8, 2} = -8
                {2, 3} = 0 (does not contain a negative integer)
                {3, -6} = -6
                {-6, 10} = -6

                Input : arr[] = {12, -1, -7, 8, -15, 30, 16, 28} , k = 3
                Output : -1 -1 -7 -15 -15 0
             */

            List<int> ans = new List<int>();

            for (int start = 0; start <= (arr.Length - size); start++)
            {
                bool isFound = false;
                for (int end = start; end < (start + size); end++)
                {
                    if (arr[end] < 0)
                    {
                        Console.WriteLine("Brute: First Negative is {0}", arr[end]);
                        ans.Add(arr[end]);
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    Console.WriteLine("Brute: First Negative is {0}", 0);
                    ans.Add(0);
                }
            }

            return ans.ToArray();
        }

        #endregion FindFirstNegativeInArrOfSizeK

        #region GetSmallestSubArrayForSum
        private static void GetSmallestSubArrayForSumTest()
        {
            var arr = new int[] { 6, 5, 4, 3, 2, 1 };

            var max = GetSmallestSubArrayForSum(arr, 9);
            Console.WriteLine(max);

            arr = new int[] { 2, 1, 5, 2, 8 };

            max = GetSmallestSubArrayForSum(arr, 7);
            Console.WriteLine(max);

            arr = new int[] { 3, 4, 1, 1, 6 };

            max = GetSmallestSubArrayForSum(arr, 8);
            Console.WriteLine(max);
        }

        //Variable length problem
        private static int GetSmallestSubArrayForSum(int[] arr, int sum)
        {
            /*
                arr = new int[] { 6, 5, 4, 3, 2, 1 };
                sum: 9
                Output: 5,4 
             */
            int minElems = -1;

            int start = 0, end = 0;
            int windowVal = 0;

            while (end < arr.Length)
            {
                windowVal = windowVal + arr[end];

                while (windowVal >= sum && start <= end)
                {
                    //if (minElems == -1 || (end - start + 1) < minElems)
                    //{
                    //    minElems = (end - start + 1);
                    //}

                    minElems = Math.Min(minElems, ((end - start) + 1));

                    windowVal = windowVal - arr[start];
                    start++;
                }

                end++;
            }

            return minElems;
        }
        #endregion GetSmallestSubArrayForSum

        #region GetMaximumOfSubarraysOfk

        private static void GetMaximumOfSubarraysOfkTest()
        {
            GetMaximumOfSubarraysOfkBrutal(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3);

            GetMaximumOfSubarraysOfk(new int[] { 1, 3, 2, 0, 8 }, 3);
        }

        private static int[] GetMaximumOfSubarraysOfk(int[] arr, int size)
        {
            /*
           * Input: arr[] = {1, 2, 3, 1, 4, 5, 2, 3, 6}, K = 3 
            Output: 3 3 4 5 5 5 6
            Explanation: 
            Maximum of 1, 2, 3 is 3
            Maximum of 2, 3, 1 is 3
            Maximum of 3, 1, 4 is 4
            Maximum of 1, 4, 5 is 5
            Maximum of 4, 5, 2 is 5 
            Maximum of 5, 2, 3 is 5
            Maximum of 2, 3, 6 is 6
           * **/
            //Sample Input:1,3,2,0,8
            //Ans: 3,3,8 (No of answers should be arrr.Length - size + 1

            //The first index in the list will be the max element always
            //If the new element to be added is greater than the previous element in the aux - then remove all
            //the elements lesser than that
            //Should maintain a decreasing order in aux list from '0 to count'

            if (arr == null || arr.Length == 0 || arr.Length < size) return default(int[]);

            List<int> ans = new List<int>();

            List<int> aux = new List<int>();

            int start = 0;
            for (int end = 0; end < (arr.Length); end++)
            {
                //This will imbalance the order of elements in aux
                //So remove if anything previously added is less than the current element
                while (aux.Count > 0 && arr[aux[aux.Count - 1]] < arr[end])
                {
                    aux.RemoveAt(aux.Count - 1);
                }

                aux.Add(end);

                if ((end - start + 1) < size)
                    continue;

                if (end - start + 1 == size)
                {
                    if (aux.Count > 0)
                    {
                        Console.WriteLine($"windowMax = {arr[aux[0]]}");
                        ans.Add(arr[aux[0]]);
                    }


                    if (aux.Count > 0 && aux[0] == start)
                        aux.RemoveAt(0);

                    start++;
                }
            }

            return ans.ToArray();
        }


        private static int[] GetMaximumOfSubarraysOfkBrutal(int[] arr, int size)
        {
            //Sample Input:1,3,2,0,8
            //Ans: 3,3,8 (No of answers should be arrr.Length - size + 1
            /*
             * Input: arr[] = {1, 2, 3, 1, 4, 5, 2, 3, 6}, K = 3 
                Output: 3 3 4 5 5 5 6
                Explanation: 
                Maximum of 1, 2, 3 is 3
                Maximum of 2, 3, 1 is 3
                Maximum of 3, 1, 4 is 4
                Maximum of 1, 4, 5 is 5
                Maximum of 4, 5, 2 is 5 
                Maximum of 5, 2, 3 is 5
                Maximum of 2, 3, 6 is 6
             * **/

            if (arr == null || arr.Length == 0 || arr.Length < size) return default(int[]);

            List<int> ans = new List<int>();


            for (int start = 0; start < (arr.Length - size + 1); start++)
            {
                int windowMax = int.MinValue;

                for (int end = start; end < (start + size); end++)
                {
                    if (windowMax < arr[end])
                        windowMax = arr[end];
                }

                Console.WriteLine($"windowMax = {windowMax}");

                ans.Add(windowMax);
            }

            return ans.ToArray();
        }

        #endregion GetMaximumOfSubarraysOfk

        #region GetLargestSubarray

        private static void GetLargestSubArrayTest()
        {
            //cls.GetLargestSubArrayBrute(new int[] { 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            //cls.GetLargestSubArrayBrute(new int[] { -22, 12, -1, -1, -1, -2, 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            //cls.GetLargestSubArray(new int[] { 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            //cls.GetLargestSubArray(new int[] { -22, 12, -1, -1, -1, -2, 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            GetLargestSubArray1(new int[] { 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            GetLargestSubArrayWithAux(new int[] { 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            GetLargestSubArrayWithAux(new int[] { -22, 12, -1, -1, -1, -2, 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);
        }

        //Refer this
        private static int GetLargestSubArray1(int[] arr, int total)
        {
            //2,1,2,4,1,5
            //5

            //2,2,3,4,1,5
            //2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            //-22,12,-1,-1,-1,-2, 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);
            //This approach will not work for the above - use hashmap technique instead

            int maxElements = 0;
            var windowSum = 0;

            int start = 0, end = 0;

            while (end < arr.Length)
            {
                windowSum = windowSum + arr[end];

                if (windowSum < total)
                {
                    end++;
                    continue;
                }

                if (windowSum == total)
                {
                    //Extract the data:
                    maxElements = Math.Max(maxElements, (end - start + 1));// ? maxElements : (end - start + 1);
                    end++;
                    continue;
                }

                if (windowSum > total)
                {
                    while (windowSum > total)
                    {
                        windowSum = windowSum - arr[start];
                        start++;
                    }

                    end++;
                }
            }

            Console.WriteLine(maxElements);
            return maxElements;
        }


        /// <summary>
        /// This works even for negative numbers
        /// Refere this
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private static int GetLargestSubArrayWithAux(int[] arr, int total)
        {
            //-20, 2,3
            //5

            //-5,0,0,5,5
            //5

            //2,1,2,4,1,5
            //5

            //-5,5,-5,5,5
            //5

            //How it works
            //Input: 2,1,2,0,2,0,0,1,1,1
            //Sum: 5
            //Ans: 0,2,0,0,1,1,1 - 7 Digits
            //Approach:
            //Dict: Sum - Index
            //0-0,2-1,3-2,5-3,7-5,8-8,9-9,10-10

            int end = 0;
            int maxElements = 0;
            int sum = 0;

            Dictionary<int, int> hash = new Dictionary<int, int>();

            //Initialize the dictionary
            hash.Add(0, 0);

            while (end < arr.Length)
            {
                sum = sum + arr[end];

                if (!hash.ContainsKey(sum))
                {
                    hash[sum] = end;
                }

                if (hash.ContainsKey(sum - total))
                {
                    maxElements = Math.Max(maxElements, end - hash[sum - total]);// maxElements < end - hash[sum - total] ? end - hash[sum - total] : maxElements;
                }

                end++;
            }

            Console.WriteLine(maxElements);
            return maxElements;
        }

        public int GetLargestSubArrayBrute(int[] arr, int total)
        {
            //2,1,2,4,1,5
            //5
            //2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            //-22,12,-1,-1,-1,-2, 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            int maxElements = 0;

            for (int start = 0; start < arr.Length; start++)
            {
                var windowSum = 0;
                for (int end = start; end < arr.Length; end++)
                {
                    windowSum = windowSum + arr[end];

                    if (windowSum < total) continue;

                    if (windowSum > total)
                        break;

                    if (windowSum == total)
                    {
                        maxElements = maxElements > (end - start + 1) ? maxElements : (end - start + 1);
                        break;
                    }
                }
            }

            Console.WriteLine(maxElements);
            return maxElements;
        }


        public int GetLargestSubArray(int[] arr, int total)
        {
            //2,1,2,4,1,5
            //5

            //2,2,3,4,1,5
            //2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);

            //-22,12,-1,-1,-1,-2, 2, 1, 2, 1, 1, 1, 4, 5, 22 }, 5);
            //This approach will not work for the above - use hashmap technique instead

            int maxElements = 0;
            var windowSum = 0;

            int start = 0, end = 0;

            while (end < arr.Length)
            {
                windowSum = windowSum + arr[end];

                if (windowSum < total) end++;

                if (windowSum == total)
                {
                    maxElements = Math.Max(maxElements, (end - start + 1));// ? maxElements : (end - start + 1);
                    end++;
                    continue;
                }

                if (windowSum > total)
                {
                    while (windowSum > total)
                    {
                        windowSum = windowSum - arr[start];
                        start++;
                    }

                    end++;
                }
            }

            Console.WriteLine(maxElements);
            return maxElements;
        }

        

        #endregion

        #region GetMinSubstring

        private static void GetMinSubstringTest()
        {
            var val = GetMinSubstring();
            Console.WriteLine($"Min Substring={val}");
        }

        //Refer this
        private static string GetMinSubstring(string s = null, string t = null)
        {
            /*
             * https://logicmojo.com/sub_videos/38
             * 
             Input: s = "ADOBECODEBANC", t = "ABC"
                Output: "BANC"
                Explanation: The minimum window substring "BANC" includes 'A', 'B', and 'C' from string t.
             */
            s = s ?? "ADOBECODEBANCQ";
            t = t ?? "ABC";

            if (s == null || t == null || s.Length == 0 || t.Length == 0 || s.Length < t.Length)
                return "";

            int start = 0, end = 0;

            //Take the pattern counter and a reference counter
            var strDictCntr = new Dictionary<char, int>();//this will be the counter for each char that matches pattern char
            //var patternDictCntrRef = new Dictionary<char, int>();

            var charCount = 0;
            string resultSubstring = null;

            foreach (var chr in t)
            {
                if (strDictCntr.ContainsKey(chr))
                {
                    strDictCntr[chr]++;
                }
                else
                {
                    strDictCntr[chr] = 1;
                    charCount++;
                }
            }

            while (end < s.Length)
            {

                if (!strDictCntr.ContainsKey(s[end]))
                {
                    end++;
                    continue;
                }
                strDictCntr[s[end]]--;

                if (strDictCntr.ContainsKey(s[end]) && strDictCntr[s[end]] == 0)
                {
                    charCount--;
                }

                //when all characters matches
                if (charCount == 0)
                {
                    //QAABCD - ABC

                    //Clean up the start - Remove Q and A above
                    while (start <= end && (!strDictCntr.ContainsKey(s[start]) || (strDictCntr.ContainsKey(s[start]) && strDictCntr[s[start]] < 0)))
                    {
                        if ((strDictCntr.ContainsKey(s[start]) && strDictCntr[s[start]] < 0))
                            strDictCntr[s[start]]++;

                        start++;
                    }

                    if (resultSubstring == null)
                        resultSubstring = s.Substring(start, end - start + 1);
                    else
                    {
                        var newStr = s.Substring(start, end - start + 1);
                        resultSubstring = newStr.Length <= resultSubstring.Length ? newStr : resultSubstring;
                    }

                }
                end++;
            }
            resultSubstring = resultSubstring ?? "";

            Console.WriteLine("Substring = " + resultSubstring);

            return resultSubstring;
        }

        private static string GetMinSubstring1(string s, string t)
        {
            //abacdaef
            /*
             Input: s = "ADOBECODEBANC", t = "ABC"
                Output: "BANC"
                Explanation: The minimum window substring "BANC" includes 'A', 'B', and 'C' from string t.
             */
            int[] m = new int[256];

            // Answer
            // Length of ans
            int ans = int.MaxValue;

            // Starting index of ans
            int start = 0;
            int count = 0, i = 0;


            // Creating map
            for (i = 0; i < t.Length; i++)
            {
                if (m[t[i]] == 0)
                    count++;

                m[t[i]]++;
            }

            // References of Window
            i = 0;
            int j = 0;

            // Traversing the window
            while (j < s.Length)
            {

                // Calculations
                m[s[j]]--;

                if (m[s[j]] == 0)
                    count--;

                // Condition matching
                if (count == 0)
                {
                    while (count == 0)
                    {

                        // Sorting ans
                        if (ans > j - i + 1)
                        {
                            ans = Math.Min(ans, j - i + 1);
                            start = i;
                        }

                        // Sliding I
                        // Calculation for removing I
                        m[s[i]]++;

                        if (m[s[i]] > 0)
                            count++;

                        i++;
                    }
                }
                j++;
            }

            if (ans != int.MaxValue)
                return String.Join("", s).Substring(start, ans);
            else
                return "-1";
            //
            //int minElements = 0;

            //Dictionary<char, int> ptrnLkp = new Dictionary<char, int>();

            //foreach (var item in pattern)
            //{
            //    if (ptrnLkp.ContainsKey(item))
            //        ptrnLkp[item]++;
            //    else
            //        ptrnLkp[item] = 0;
            //}

            //int start = 0, end = 0;

            //while (end < str.Length)
            //{

            //}

            //return minElements;
        }
        #endregion GetMinSubstring

        #region GetAnagramsCount
        private static void GetAnagramsCountTest()
        {
            var str = "abba";
            var pattern = "ab";
            str = "baa";
            pattern = "aa";
            var max = GetAnagramsCount(str, pattern);
            Console.WriteLine(max);

            //str = "abba";
            //pattern = "ab";
            //max = cls.GetAnagramsCount1(str, pattern);
            //Console.WriteLine(max);

            str = "ddbaadcbaa";
            pattern = "aab";
            max = GetAnagramsCount(str, pattern);
            Console.WriteLine(max);
        }

        //abccba
        //abc
        //2
        //abcabfgcba
        //abc
        //4
        //refer this
        private static int GetAnagramsCount(string str, string pattern)
        {
            int anagramCnt = 0;

            //Put pattern to a dictionary
            var patternLookup = new Dictionary<char, int>();
            var count = 0;
            foreach (var item in pattern)
            {
                if (patternLookup.ContainsKey(item))
                    patternLookup[item]++;
                else
                {
                    patternLookup[item] = 1;
                    count++;
                }

            }

            int begin = 0, end = 0;

            int windowSize = pattern.Length;


            while (end < str.Length)
            {
                var windowFrame = end - begin + 1;

                if (patternLookup.ContainsKey(str[end]))
                {
                    patternLookup[str[end]]--;

                    if (patternLookup[str[end]] == 0)
                        count--;
                }


                if (windowFrame == windowSize)
                {
                    if (count == 0)
                    {
                        anagramCnt++;
                    }

                    if (patternLookup.ContainsKey(str[begin]))
                    {
                        patternLookup[str[begin]]++;

                        if (patternLookup[str[begin]] == 1)
                            count++;
                    }

                    begin++;
                }

                end++;
            }



            return anagramCnt;
        }

        //abccba
        //abc
        //2
        //abcabfgcba
        //abc
        //4
        /// <summary>
        /// 1. Here the given pattern will be stored in a dictionary (pattern dictionary) along with the count of each characters
        /// 2. This count will be decremented once the same character is identified in the input string
        /// 3. Extraction: when the window size is reached, check whether the count of all characters in the pattern index is zero and if yes - that is an anagram
        /// 4. Post Extraction: When the start index is to be incremented, then if the character  of the start index is present in the dictionary, then the count
        /// will be incremented
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int GetAnagramsCount1(string str, string pattern)
        {
            int anagramCnt = 0;

            //Put pattern to a dictionary
            var patternLookup = new Dictionary<char, int>();

            foreach (var item in pattern)
            {
                if (patternLookup.ContainsKey(item))
                    patternLookup[item]++;
                else
                    patternLookup[item] = 1;
            }

            int begin = 0, end = 0;

            //Fixed window size
            int windowSize = pattern.Length;

            while (end < str.Length)
            {
                //Still window size is not reached
                if ((end - begin + 1) < windowSize)
                {
                    if (patternLookup.ContainsKey(str[end]))
                    {
                        patternLookup[str[end]]--;
                    }
                    end++;
                }
                else if ((end - begin + 1) == windowSize)
                {
                    if (patternLookup.ContainsKey(str[end]))
                    {
                        patternLookup[str[end]]--;
                    }

                    //Extraction: Check whether our condition is met
                    if (patternLookup.Values.All(v => v == 0))
                    {
                        anagramCnt++;
                    }

                    //Post Extraction: Before sliding the start index, add the count of char at start index
                    if (patternLookup.ContainsKey(str[begin]))
                    {
                        patternLookup[str[begin]]++;
                    }
                    begin++;
                    end++;

                }
            }

            return anagramCnt;
        }

        #endregion GetAnagramsCount

        #region GetLongestSubstringWithChars

        private static void GetLongestSubstringWithCharsTest()
        {
            GetLongestSubstringWithUniqueChars("aaabcccd", 3);

            GetLongestSubstringWithUniqueNoRepeatChars("abaccdefgh");

            GetLongestSubstringWithUniqueNoRepeatChars1("abaccdefgh");
        }

        //refer this
        private static int GetLongestSubstringWithUniqueChars(string str, int noOfUniqueChars)
        {
            //abaccdefghi, 3
            //Ans: abacc
            Dictionary<char, int> dict = new Dictionary<char, int>();

            int start = 0, end = 0;

            int maxElems = 0;

            while (end < str.Length)
            {
                if (dict.ContainsKey(str[end]))
                {
                    dict[str[end]]++;
                }
                else
                {
                    dict[str[end]] = 1;
                }


                if (dict.Count < noOfUniqueChars)
                {
                    end++;
                    continue;
                }

                if (dict.Count == noOfUniqueChars)
                {
                    var totalElem = 0;

                    foreach (var k in dict.Keys)
                    {
                        totalElem = totalElem + dict[k];
                    }

                    maxElems = maxElems > totalElem ? maxElems : totalElem;

                    end++;
                    continue;
                }

                //remove from start
                if (dict.Count > noOfUniqueChars)
                {
                    while (dict.Count > noOfUniqueChars)
                    {
                        if (dict.ContainsKey(str[start]))
                        {
                            dict[str[start]]--;

                            if (dict[str[start]] == 0)
                                dict.Remove(str[start]);
                        }

                        start++;
                    }

                    //end++;
                }

            }

            Console.WriteLine(maxElems);
            return maxElems;
        }

        //refer this
        private static int GetLongestSubstringWithUniqueNoRepeatChars1(string str)
        {
            //abaccdefghi
            Dictionary<char, int> dict = new Dictionary<char, int>();

            int start = 0, end = 0;

            int maxElems = 0;

            while (end < str.Length)
            {
                if (dict.ContainsKey(str[end]))
                {
                    dict[str[end]]++;
                }
                else
                {
                    dict[str[end]] = 1;
                }


                if (dict.Count == (end - start + 1))
                {
                    maxElems = Math.Max(maxElems, end - start + 1);

                    end++;
                    continue;
                }

                if (dict.Count < (end - start + 1))
                {
                    while (dict.Count < (end - start + 1))
                    {
                        if (dict.ContainsKey(str[start]))
                        {
                            dict[str[start]]--;

                            if (dict[str[start]] == 0)
                                dict.Remove(str[start]);

                        }

                        start++;
                    }

                    end++;
                }

            }

            Console.WriteLine(maxElems);
            return maxElems;
        }

        private static int GetLongestSubstringWithUniqueNoRepeatChars(string str)
        {
            //abaccdefghi
            //Ans: cdefghi
            Dictionary<char, int> dict = new Dictionary<char, int>();

            int start = 0, end = 0;

            int maxElems = 0;

            while (end < str.Length)
            {
                if (dict.ContainsKey(str[end]))
                {
                    while (start < end)
                    {
                        if (dict.ContainsKey(str[start]))
                        {
                            dict.Remove(str[start]);
                            break;
                        }
                        start++;
                    }

                    dict[str[end]] = 1;
                }
                else
                {
                    dict[str[end]] = 1;
                }


                maxElems = Math.Max(maxElems , (end - start + 1));

                end++;
            }

            Console.WriteLine(maxElems);
            return maxElems;
        }

        #endregion GetLongestSubstringWithChars
    }




    public class Others
    {
        private  static void Get3NumbersSum()
        {
            Get3NumbersSum(new int[] { 12, 3, 1, 2, -6, 5, -8, 6 }, 0);

        }
        //Note: This is not a sw problem
        private static List<List<int>> Get3NumbersSum(int[] arr, int sum)
        {
            //Input: 12,3,1,2,-6,5,-8,6
            //Sum: 0
            //Ans: [[-8, 2, 6], [-8, 3, 5], [-6, 1, 5]]
            //1.Sort the array initiall in ascending order
            //Iterate through each number and its left and from right most
            //Sorted arr:-8,-6,1,2,3,5,6,12

            var results = new List<List<int>>();
            Array.Sort(arr);

            //Anchor the elements and start iterating
            for(int i = 0; i < arr.Length - 2; i++)
            {
                int j = i + 1;
                int k = arr.Length - 1;
                int total = 0;

                while(j < k)
                {
                    total = arr[i] + arr[j] + arr[k];

                    if(total < sum)
                    {
                        j++;;
                    }

                    if(total == sum)
                    {
                        var res = new List<int> { arr[i], arr[j], arr[k] };
                        results.Add(res);

                        //Try finding other combination for the anchor element
                        j++;
                        k--;
                    }

                    if(total > sum)
                    {
                        k--;
                    }
                }
            }
            Console.WriteLine("Get3NumbersSum Input Arr {0} & sum {1}", arr, sum);
            Console.WriteLine("Get3NumbersSum Results", results);
            return results;
        }

        /*
           public int MaxSubstring(string str, int noOfDistinctChars)
           {
               HashSet<char> uniques = new HashSet<char>();

               int start = 0, end = 0;
               int max = 1;
               uniques.Add(str[0]);

               while (end < str.Length)
               {

                   if (!uniques.Contains(str[end]))
                   {
                       max = Math.Max(max, (end - start + 1));
                   }
                   else
                   {
                       if (uniques.Count > noOfDistinctChars)
                       {

                       }
                   }




                   else
                   {
                       uniques.Add(str[0]);
                   }

                   end++;
               }


           }
           */

    }
}
