using System;
namespace DSA_Prac2.Programming
{
    public class TryEqualityTest
    {
        public static void Test()
        {
            var inst1 = new TryEquality();
            inst1.FirstName = "Arun";
            inst1.LastName = "N";

            var inst2 = new TryEquality();
            inst2.FirstName = "Arun";
            inst2.LastName = "Nanjundaswamy";

            Console.WriteLine(inst1.Equals(inst2));
        }
    }
    public class TryEquality: IEquatable<TryEquality>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public TryEquality()
        {
        }

        public bool Equals(TryEquality other)
        {
            return FirstName == other.FirstName;
        }
    }
}
