using System;
using System.Linq;

namespace DSA_Prac2
{
    public class BitProblems
    {
        public BitProblems()
        {
            TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
            var seconds = t.TotalSeconds;//64 bit - 2^6 = 2 bytes
        }

        public static void Test()
        {
            BitOp(3, 2);
            CountBitsToFlipTest();
            FindElementThatAppearsOnce();
        }

        public static int GetNumberOfSetBits(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }

            return count;
        }

        private static void FindElementThatAppearsOnce()
        {
            /*
             Input Arr = [6,2,4,3,4,2,3]
            Soln: When XOR same number it becomes 0
            Eg. 101 ^ 101 = 000
            So 1^2^2 = 001 ^ 010 ^ 010 =011 ^ 010 = 001 = '1'

             */

            var arr = new int[] { 6, 2, 4, 3, 4, 2, 3};

            int xorVal = arr[0];
            for (int index = 1; index < arr.Length; index++)
            {
                xorVal = xorVal ^ arr[index];
            }

            Console.WriteLine($"The element that occurs only one time = {xorVal}");
        }

        private static void BitOp(int v1, int v2)
        {
            //3 and 2 = 2 = 011 & 010 = 010 = 2
            //3 or 2 = 3 = 011 | 010 = 011 = 3
            //3 xor 2 = 1 = 011 ^ 010 = 001 = (int)1
            //3 rightshift 1 (ie.1 bit and discard that bit) = 3 >> 1 = 011 >> 1 = 01 = 1
            //3 rightshift 2 (i.e shift 2 bits and discard those bits) = 3 >> 2 = 011 >> 2 = 0 = 0
            Console.WriteLine($"v1 & v3 {v1 & v2}");
            Console.WriteLine($"v1 | v3 {v1 | v2}");
            Console.WriteLine($"v1 & v3 {v1 ^ v2}");
            Console.WriteLine($"v1 >> 1 {v1 >> 1}");
            Console.WriteLine($"v1 >> 2 {v1 >> 2}");

            //Convert a number to binary or base 2
            var binary = Convert.ToString(11, 2);
            Console.WriteLine($"Binary rep of 11 is {binary}");

            
            //Convert a string to binary or base 2
            var plainTextToBytes = System.Text.Encoding.UTF8.GetBytes("a123GeW");
            Console.WriteLine($"String to binary 'a123GeW'={string.Join(" ", plainTextToBytes.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')))}");

            char chr = 'a';
            int chrInt = (int)(chr);
            Console.WriteLine("Int value of chr="+ (int)chr);


            plainTextToBytes = System.Text.Encoding.UTF8.GetBytes("a123GeW");
            var base64String = Convert.ToBase64String(plainTextToBytes);
            Console.WriteLine($"Base64 rep of a123GeW is {base64String}");

            //plainTextToBytes = System.Text.Encoding.UTF8.GetBytes("a123GeW");
            //base64String = Convert.ToString(plainTextToBytes, 62);
            //Console.WriteLine($"Base64 rep of a123GeW is {base64String}");
        }

        private static void CheckIfNthBitIsSetTest()
        {
            //Is Set means whether it is 1
            //https://www.youtube.com/watch?v=ldhT2uVSdUQ
        }

        private static void CountBitsToFlipTest()
        {
            /*
             Input : a = 10, b = 20
                Output : 4
                Binary representation of a is 00001010
                Binary representation of b is 00010100
                We need to flip highlighted four bits in a
                to make it b.
             */
            int a = 5, b = 3;

            var numberOfBitsToFlip = CountBitsToFlip(a, b);
            Console.WriteLine($"number of bits to flip={numberOfBitsToFlip}");
        }

        private static int CountBitsToFlip(int a, int b)
        {
            /*
             * https://www.youtube.com/watch?v=J6dv1vC7jUk
             * https://www.youtube.com/watch?v=9CWVhu38a-Q
             * a = 5 or 101
             * b = 3 or 011
             * To make a as b we need to change from 101 to 011 or 2 bits (10 in a) to be changed to 01
             * Use XOR since it gives bit '1' if the bit in 'a' and 'b' are different
             * 101
             * 011
             = 110 (Here 1s are called set bits) and number of setbits is the answer

            From the XOR result how to find the number of set bits in an integrer
            https://www.youtube.com/watch?v=9CWVhu38a-Q
            */

            var xorVal = a ^ b;

            //Find setbits or bits with 1 in xorVal

            int cntSetBits = 0;

            //What is happening below
            /*
             xorVal = 110
              1)  xorVal & 1 is
                110
                001
               =000 (so false and not counted)

                Right shift xorVal(i.e. 110) = 11
            2) xorVal & 1 is
                 11
                001
               =001 (so true and counted)

            Right shift xorVal(i.e. 11) = 1

            3) xorVal & 1 is
                  1
                001
               =001 (so true and counted)
            Right shift xorVal(i.e. 1) = 0

            Condition of while if xorVal = 0 break
             */

            while (xorVal != 0)
            {
                if((xorVal & 1) == 1)
                    cntSetBits++;

                //right shift xorVal
                xorVal = xorVal >> 1;
            }

            return cntSetBits;
        }
    }
}
