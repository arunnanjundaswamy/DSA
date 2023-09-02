using System;
using System.Security.Cryptography;
using System.Text;

namespace DSA_Prac2.Programming
{
    public class EncryptionAndHashing
    {
        public static void Test()
        {
            HashingTest();

            SymmeticalEncryption();
        }

        private static void SymmeticalEncryption()
        {
            var name = "please Test this new string";
            Console.WriteLine("Plain data {0}", name);

            var key = "aaaaabbbb";
            Console.WriteLine("Key {0}", key);



            using (SymmetricAlgorithm sym = Aes.Create())
            {
                using (var encryptor = sym.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(name)))
                {
                    // encryptor.
                }
            }

        }

        private static void HashingTest()
        {
            var name = "please Test this new string";
            Console.WriteLine("Plain data {0}", name);

            //lesser length more chances for hash collision or dictionary attack
            //MD5(16) → SHA1(20) → SHA256(32) → SHA384(48) → SHA512(64)
            using (var sha = SHA512.Create())
            {
                var bytArr = sha.ComputeHash(Encoding.UTF32.GetBytes(name));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytArr.Length; i++)
                {
                    builder.Append(bytArr[i].ToString("x2"));
                }
                //SHA256 Hashed data 0d3934e9914aeb316bf4b0852c6dac49f52b95b218749884a4c389d08e3d7784
                //SHA512: Hashed data c29b8a6ccfbc7a7a700eda852af22d9a7ee05ec087c209b70dc917f773d59df6899ab6e4a4f27a118eb7bee6cd1618b8440cf787417e5bd7fa48e0987b2940d5
                Console.WriteLine("Hased data {0}", builder.ToString());
            }

            using (var hash = MD5.Create())
            {
                var x = hash.ComputeHash(Encoding.UTF8.GetBytes(name));
                var hasedStr = Encoding.UTF8.GetString(x);
                Console.WriteLine("hashed String= {0}", hasedStr);

                var hashedStr1 = Convert.ToBase64String(x);
                Console.WriteLine("hashed String= {0}", hashedStr1);

            }
        }
    }

    public interface interface1
    {
        void method1(string x);
        void method2();
    }

    public interface interface2
    {
        void method1();
    }

    public class Ding : interface1, interface2
    {
        void interface2.method1()
        {
            throw new NotImplementedException();
        }

        public void method1(string x)
        {
            throw new NotImplementedException();
        }

        public void method2()
        {
            throw new NotImplementedException();
        }
    }

    public class Dong
    {
        public static void Test()
        {
            interface2 i1 = new Ding();
            i1.method1();
        }
    }
}
