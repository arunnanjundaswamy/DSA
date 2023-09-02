using System;

namespace ConsoleApp1
{
    public class RotatedBinarySearch
    {
        public static void Test()
        {
            Console.WriteLine("Rotated Binary Search");

            //int[] inputArr = new int[] { 3, 6, 0, 1, 2 };

            //int[] inputArr = new int[] { 2, 3, 2, 2, 2 };

            //int[] inputArr = new int[] { 8, 9, 1, 1, 1, 1, 8 };

            int[] inputArr = new int[] { 8, 9, 8, 8, 8 };

            //int[] inputArr = new int[] { 3 };

            int target = 9;

            var targetIndex = FindIndex(inputArr, target);

            Console.WriteLine("Target Index={0}", targetIndex != -1 ? targetIndex.ToString() : "Not found");

            Console.ReadLine();
        }

        private static int FindIndex(int[] array, int target)
        {
            var l = 0;
            var r = array.Length - 1;

            var tv = target;

            while (l <= r)
            {
                var mid = (l + r) / 2;

                var mv = array[mid];

                if (mv == target) return mid;
                var lv = array[l];
                var rv = array[r];

                if (lv < mv)
                {
                    if (target >= lv && target < mv)
                    {
                        r = mid - 1;
                    }
                    else
                        l = mid + 1;
                }
                else if (lv > mv)
                {
                    if (target > mv && target <= rv)
                    {
                        l = mid + 1;
                    }
                    else
                    {
                        r = mid - 1;
                    }
                }
                else if( lv == mv)
                {
                    if(mv != rv)
                    {
                        l = mid + 1;
                    }
                    else
                    {
                        r = mid - 1;

                        ////same as binary search -- search both ends
                        ////Look towards left and then right

                        //if (tv < mv)
                        //{
                        //    r = mid - 1;
                        //}
                        //else if (target > mv)
                        //{
                        //    l = mid + 1;
                        //}
                    }
                }
            }
            return -1;
        }
    }
}


