using System;

namespace ConsoleApp1
{
    public class RecursiveWork
    {
        public static void Test()
        {
            Fun(0, 0);
            OtherFun(0, 0);

            //OtherFun1(0, 0);

            Console.WriteLine("Factorial of 5 = {0}", Fact(5));

            Console.WriteLine("Factorial of 5 = {0}", FactOpt1(5));

        }

        private void PrintAllPermutationsOfAString(string str)
        {
            var start = 0; var end = str.Length;


        }

        private void PrintAllPermutationsOfAString(string str, int start, int end)
        {
            for(int index = start; index <= end; index++)
            {
                Swap(str, start, index);
                PrintAllPermutationsOfAString(str, start + 1, end);
                Swap(str, start, index);
            }
        }

        private void Swap(string str, int i, int j)
        {
            var temp = str[j];
            str = str.Replace(str[j], str[i]);
            str = str.Replace(str[i], temp);
        }

        private static int Fact(int t)
        {
            var n = t;
            var res = t;
            while (n > 1)
            {
                res = res * (n - 1);
                n--;
            }

            return res;
        }


        private static int FactOpt1(int n)
        {
            var res = n;
            if(n <= 1)
            {
                return 1;
            }

            res = res * FactOpt1(n - 1);
            return res;
        }

        private static int Fibonacci(int index)
        {
            return -1;
        }

        private static void Fun(int sum, int otherSum)
        {
            if (otherSum == 2) return;

            Fun(sum, otherSum + 1);

            //Callstack: 0-4, 0-3, 0-2,0-1,0-0 (Post-execution call)

            System.Console.WriteLine($"My Callstack is: {sum}-{otherSum}");
        }



        private static void OtherFun(int sum, int otherSum)
        {
            if (otherSum == 5) return;

            //Callstack: 0-0, 0-1, 0-2,0-3,0-4  (Pre-execution call)
            System.Console.WriteLine($"My Callstack is: {sum}-{otherSum}");
            OtherFun(sum, otherSum + 1);
        }

        //This is incorrect
        private static void OtherFun1(int sum, int otherSum)
        {
            System.Console.WriteLine($"My Callstack is: {sum}-{otherSum}");

            OtherFun1(sum, otherSum + 1);

            if (otherSum == 5) return;
        }
    }
}
