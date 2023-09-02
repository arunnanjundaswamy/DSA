using System;
using System.Linq;

namespace DSA_Prac2
{
	public class ArraySortTest
    {
		public static void Test()
        {
			var order = new int[] { 1,2,3 };
			var arr = new int[] { 2, 3, 1};
			var sortedVal = ArraySort.ThreeNumberSort(arr, order);
            foreach (var item in sortedVal)
            {
                Console.WriteLine(item);
            }

			var inputArr = new int[] { 22, 9, 2, 3, 1, 16 };
			sortedVal = ArraySort.MergeSort(inputArr);
			ArraySort.MergeSortOtherApproach(inputArr, 0, inputArr.Length - 1);
			sortedVal = inputArr;
			foreach (var item in sortedVal)
			{
				Console.WriteLine(item);
			}

			var jumps = ArraySort.MinNoOfJumpsTest();
			Console.WriteLine(jumps);

			ArraySort.GetNextGreaterNumberTest();

			NumberOfPlatformsTest();
		}

		private static void NumberOfPlatformsTest()
		{
			//Find the number of platforms required
			/*
			 * https://www.techiedelight.com/minimum-number-of-platforms-needed-avoid-delay-arrival-train/
                arr[]  = {9:00,  9:40, 9:50,  11:00, 15:00, 18:00}
                dep[]  = {9:10, 12:00, 11:20, 11:30, 19:00, 20:00}

            Number of Platforms = 3

            930- A 1000-D
            930- A 1030-D
            945-A 1500-D
            1200-A 1239-D


            930,930,945,1000,1030,1200,1230,1500
            1,1,1,-1,-1,1,-1,-1

            930->930->945->1200

            1000->1030->1230->1500
             */

			//Sort the arrival and departure
			int[] arr = { 900, 940, 950, 1100, 1500, 1800 };
			int[] dep = { 910, 1200, 1120, 1130, 1900, 2000 };

			//arr = 1
			//dep = -1

			Array.Sort(arr);
			Array.Sort(dep);

			int arrIndex = 0, depIndex = 0, maxPlatforms = 1;

			var platforms = 0;
			while (arrIndex < arr.Length && depIndex < dep.Length)
			{
				if (arr[arrIndex] < dep[depIndex])
				{
					platforms++;
					maxPlatforms = Math.Max(maxPlatforms, platforms);
					arrIndex++;
				}
				else
				{
					platforms--;
					depIndex++;
				}
			}

			Console.WriteLine($"Maximum platforms = {maxPlatforms}");
		}

	}

	public class ArraySort
    {
		public static int[] ThreeNumberSort(int[] array, int[] order)
		{
			// Write your code here.
			var firstIndex = 0;
			var probingIndex = 0;
			var thirdIndex = array.Length - 1;

			var firstVal = order[0];
			var secondVal = order[1];
			var thirdVal = order[2];

			while (probingIndex <= thirdIndex)
			{
				var val = array[probingIndex];

				if (val == thirdVal)
				{
					Swap(array, probingIndex, thirdIndex);
					thirdIndex--;
				}
				else if (val == firstVal)
				{
					Swap(array, probingIndex, firstIndex);
					firstIndex++;
					probingIndex++;
				}
				else if (val == secondVal)
				{
					probingIndex++;
				}
			}

			return array;
		}

		static void Swap(int[] array, int frm, int to)
		{
			var temp = array[to];
			array[to] = array[frm];
			array[frm] = temp;
		}


		public static int[] MergeSort(int[] arr)
        {
			//input: 4,3,2,1
			//Output: 1,2,3,4
			//4,3-2,1
			//4-3 2-1
			//3,4 1,2
			//1,2,3,4
			//Divide and conquer
			//Divide logically
			/*
			 input: 22, 9, 2, 3, 1, 16
			Output: 1,2,3,9,16,22
			
			 */
			var resultArr = new int[arr.Length];
			//DivideAndSort(arr, 0, arr.Length - 1, resultArr);
			MergeSortOtherApproach(arr, 0, arr.Length - 1);
			return arr;

		}

		/*
		 * input: 22, 9, 2, 3, 1, 16
			Output: 1,2,3,9,16,22
		 22,9,2-3,1,16
		 22-9,2 -- 3-1,16
		22,9
		9,2
		 */
		private static void DivideAndSort(int[] arr, int low, int high, int[] resultArr)
        {
			if (low == high) return;

			int mid = low + (high - low) / 2;

			DivideAndSort(arr, low, mid, resultArr);
			DivideAndSort(arr, mid + 1, high, resultArr);
			Merge(arr, low, mid, high, resultArr);
        }

        private static void Merge(int[] arr, int low, int mid, int high, int[] result)
        {
			int i = low, j = mid + 1; int k = low;


			while(i <= mid && j <= high)
            {
				if(arr[i] <= arr[j])
                {
					result[k++] = arr[i++];
                }
				else
                {
					result[k++] = arr[j++];
                }
            }

			while(i <= mid)
            {
				result[k++] = arr[i++];
            }

			while (j <= high)
			{
				result[k++] = arr[j++];
			}

			//int arrIndex = 0;
			for(var arrIndex = low; arrIndex<= high; arrIndex++)
            {
				arr[arrIndex] = result[arrIndex];
			}
			
		}

		//Other approach

		public static void MergeSortOtherApproach(int[] inputArr, int low, int high)
        {
			if (low == high) return;

			var mid = low + (high - low) / 2;

			MergeSortOtherApproach(inputArr, low, mid);
			MergeSortOtherApproach(inputArr, mid + 1, high);
			Sort(inputArr, low, mid, high);
        }

        private static void Sort(int[] inputArr, int low, int mid, int high)
        {
			//Create two arrays
			//low to mid and mid+1 to high

			var arr1Len = (mid - low);
			int arr2Len = (high - (mid + 1));

			int[] arr1 = new int[arr1Len + 1];
			int[] arr2 = new int[arr2Len + 1];

			var loopIndex = 0;
            for (int index = low; index <= mid; index++)
            {
				arr1[loopIndex] = inputArr[index];
				loopIndex++;
			}

			loopIndex = 0;
			for (int index = mid+1; index <= high; index++)
			{
				arr2[loopIndex] = inputArr[index];
				loopIndex++;
			}

			int index1 = 0, index2 = 0;
			var pathIndex = low;

			while(index1 < arr1.Length && index2 < arr2.Length)
            {
				if(arr1[index1] <= arr2[index2])
                {
					inputArr[pathIndex]= arr1[index1];
					index1++;
					pathIndex++;

                }
				else
                {
					inputArr[pathIndex] = arr2[index2];
					index2++;
					pathIndex++;
				}
            }

			while(index1 < arr1.Length)
            {
				inputArr[pathIndex] = arr1[index1];
				index1++;
				pathIndex++;
			}

			while (index2 <  arr2.Length)
			{
				inputArr[pathIndex] = arr2[index2];
				index2++;
				pathIndex++;
			}
		}

        public static void MergeSortTest()
        {
			//Input: 2,1,0,8
			//Output: 0,1,2,8
			int[] arr = new int[] { 2, 1, 0, 8 };
			DivideAndSort1(arr, 0, arr.Length -1);
			var x = 6688;
        }

		private static void DivideAndSort1(int[] arr, int low, int high)
        {
			if (low >= high) return;

			int mid =  (low + high) / 2;

			DivideAndSort1(arr, low, mid);
			DivideAndSort1(arr, mid + 1, high);

			Merge1(arr, low, mid, high);
        }

		private static void Merge1(int[] arr, int low, int mid, int high)
		{
			int leftStartIndex = low;
			int rightStartIndex = high <= mid + 1? mid+1: 1;
			int processingIndex = low;

			int[] leftArr = new int[mid - low];
			int[] rightArr = new int[high - (mid + 1)];

			for (var startInd = 0; startInd < mid; startInd++)
			{
				leftArr[startInd] = arr[startInd];
			}

			for (var startInd = mid + 1; startInd < high; startInd++)
			{
				rightArr[startInd] = arr[startInd];
			}

			while (leftStartIndex < mid && rightStartIndex < high)
			{
				if (leftArr[leftStartIndex] <= rightArr[rightStartIndex])
				{
					arr[processingIndex] = leftArr[leftStartIndex];
					leftStartIndex++;
					processingIndex++;
				}
				else
				{
					arr[processingIndex] = rightArr[rightStartIndex];
					rightStartIndex++;
					processingIndex++;
				}
			}

			while(leftStartIndex < mid)
            {
				arr[processingIndex] = leftArr[leftStartIndex];
				leftStartIndex++;
				processingIndex++;
			}

			while (rightStartIndex < high)
			{
				arr[processingIndex] = rightArr[rightStartIndex];
				rightStartIndex++;
				processingIndex++;
			}

		}

		//Same repeatation of above threenumbersort
		//Searching - Maximum in array of numbers which is increasing and decreasing or increasing or decreasing
		//Note: It cannot be increasing and decreasing and then increasing
		//e.g. 4,5,6,3,2,1 or 1,2,3,4,5 or 5,4,3,2,1

		private static int FindMaxValueTest()
		{
			int[] arr = new int[] { 4, 5, 6, 3, 2, 1 };
			return FindMaxValue(arr, 0, arr.Length - 1);
		}

		private static int FindMaxValue(int[] arr, int start, int end)
		{
			if (start == end) return arr[start];

			//only two items
			if ((start + 1) == end)
				return Math.Max(arr[end], arr[start]);

			int mid = end - (start + end) / 2;

			//Analyze each item in the array and it can be any of the below condition
			//Case 1: left of that item can be greater and right can be lesser in case of increasing and decreasing
			//Case 2: left can be decreasing and right can be increasing
			//Case 3: left can be increasing and right can be decreasing

			//left of mid and right of mid
			var leftOfMid = arr[mid - 1];
			var rightOfMid = arr[mid + 1];

			//
			if (leftOfMid <= arr[mid] && arr[mid] >= rightOfMid)
			{
				return leftOfMid;
			}
			else if (leftOfMid <= arr[mid])
			{
				return FindMaxValue(arr, mid + 1, end);
			}
			else
			{
				return FindMaxValue(arr, start, mid + 1);
			}
		}

		private static void ThreeNumberSortTest()
		{
			//three numbers 1,2,3 and an array consiist of only these three : sort it
			//e.g arr: [2,1,3,2,1,3,1,2]
			//ans: [1,1,1,2,2,2,3,3]

			//Will start putting the edge numbers and the middle number gets allocated automatically

			int[] arr = new int[] { 2, 1, 3, 2, 1, 3, 1, 2 };
			int firstNumber = 1, secondNumber = 2, thirdNumber = 3;
			int firstNumberIndex = 0; int thridNumnberIndex = arr.Length - 1;
			int probingIndex = 0;

			while (probingIndex <= thridNumnberIndex)
			{
				if (arr[probingIndex] == firstNumber)
				{
					Swap(arr, probingIndex, firstNumberIndex);
					firstNumberIndex++;
					probingIndex++;
				}
				else if (arr[probingIndex] == thirdNumber)
				{
					Swap(arr, probingIndex, thridNumnberIndex);
					thridNumnberIndex--;
				}
				else
				{
					probingIndex++;
				}
			}

		}

		public static int MinNoOfJumpsTest()
        {
			//int 2,3,1,1,4
			//Ans: 2
			//nums = [2,3,0,1,4]
			//Ans: 2
			//arr[] = {1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9}
			//3 (1-> 3 -> 8 -> 9)
			int[] nums = new int[] { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 };
			return MinNoOfJumps(nums);
		}

		private static int MinNoOfJumps(int[] nums)
		{
			int a = nums[0];
			int b = nums[0];
			int jumps = 1;

			for (int i = 1; i < nums.Length; i++)
			{
				a--;
				b--;
				if (nums[i] >= b)
				{
					b = nums[i];
				}
				if (a <= 0)
                {
					a = b;
					jumps++;
                }
				
			}
			return jumps;
		}


		public static void GetNextGreaterNumberTest()
        {
			int input = 218765;
			//output 251678
			int val = GetNextGreaterNumber(input);
            Console.WriteLine($"value={val}");
		}

		private static int GetNextGreaterNumber(int number)
		{
			//step 1: compare consequetive digits from units, tens i.e right side
			//2: if left number is less than right digit, swap left number with the least one on the right side 258761
			//3: Sort the next digit after 5

			//least number till 1
			int min = int.MaxValue;
			int minIndex = int.MaxValue;
			int indexOfLeastNoIndex = int.MaxValue;
			//1:
			string numStr = number.ToString();
			int[] numArr = numStr.ToCharArray().Select(rec=> int.Parse(rec.ToString())).ToArray();

			for (int i = numArr.Length -1; i >= 0; i--)
            {
				if ((numArr[i]) < min)
				{
					min = numArr[i];
					minIndex = i;
				}

				if (int.Parse(numStr[i - 1].ToString()) < int.Parse(numStr[i].ToString()))
				{
					//2:
					Swap(numArr, i - 1, minIndex);
					indexOfLeastNoIndex = i;
					break;
				}
			}

			//3:
			Array.Sort(numArr, indexOfLeastNoIndex, (numArr.Length - (indexOfLeastNoIndex)));

			string num = "";

            foreach (var item in numArr)
            {
				num += item.ToString();
			}

			return int.Parse(num);
		}
	}

}
