using System;
using System.Numerics;
using System.Collections;
namespace matmalap3
{
    public class Program
    {
        ulong k;
        public int RandomNumber1(int a = 128, int b = 256) //random prime numbers with 8 bits, 16 bits
        {
            Random random = new Random();
            int k = random.Next(a, b);               //random number in [a,b)
            while (IsProbablePrime(k, 15) == false)   //check number is prime 
            {
                k = random.Next(a, b);              //random number in [a,b)
            }
            return k;
        }
        public ulong random2()
        {
            Random random = new Random();                    //random prime numbers with 64 bits
            var bytearr = new byte[8];
            var arr = new Byte[1];
            random.NextBytes(bytearr);
            arr[0] = bytearr[7];
            k = BitConverter.ToUInt64(bytearr, 0);          //created random numbers 0-64 bits
            BitArray d = new BitArray(arr);

            while (true)
            {
                if (d[7] == true && IsProbablePrime(k) == true) //check numbers is 64 bits and is prime
                    return k;
                else
                {                                         //created random numbers 0-64 bits
                    random.NextBytes(bytearr);
                    arr[0] = bytearr[7];
                    k = BitConverter.ToUInt64(bytearr, 0);
                    d = new BitArray(arr);
                }
            }

        }
        public bool IsProbablePrime(BigInteger source, int certainty = 50)  //check number is prime ??(Miller-Rabin algorithm)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            Random random = new Random();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    random.NextBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            Program a = new Program();
            a.RandomNumber1();
            int number816bit = a.RandomNumber1(); //created random number 8 bit and is a prime number
            Console.WriteLine(number816bit);
            int somumax = (int)Math.Pow(2, 16);
            int somumin = (int)Math.Pow(2, 15);
            number816bit = a.RandomNumber1(somumin, somumax);//created random number 16 bit and is a prime number
            Console.WriteLine(number816bit);
            ulong number64bit = a.random2();  //created random number 64 bit and is a prime number
            Console.WriteLine(number64bit);
            BigInteger[] z = new BigInteger[10];
            z[0] = 3;
            z[1] = 7;
            z[2] = 31;
            z[3] = 127;
            z[4] = 8191;
            z[5] = 131071;
            z[6] = 524287;
            z[7] = 2147483647;
            z[8] = 2305843009213693951;
            string positiveString = "618970019642690137449562111";
            z[9] = BigInteger.Parse(positiveString); // 10 first Mersenne prime numbers
            int i = 0;
            Console.WriteLine("Determine 10 largest prime number under 10 first Mersenne prime numbers");
            while (i <= 9)
            {
                z[i] = z[i] - 1;
                bool k1 = a.IsProbablePrime(z[i]);
                if (k1 == true)
                {

                    Console.WriteLine(z[i]);
                    i++;
                }


            }//10 largest prime number under 10 first Mersenne prime numbers
            Console.WriteLine("input a number <=2^89-1");
            string input = Console.ReadLine();
            BigInteger big = BigInteger.Parse(input);
            int k = BigInteger.Compare(big, BigInteger.Pow(2, 89)); //compare 2^89 and input
            if (k == -1) //input<2^89
            {
                if (a.IsProbablePrime(big))
                    Console.WriteLine("true");
                else
                    Console.WriteLine("false");
            }
            else
            {
                Console.WriteLine("bigger than 2^89-1");
            }
            Console.WriteLine("input two number, find the gcd");
            string Input;
            Input = Console.ReadLine();
            BigInteger big1;
            BigInteger big2;
            big1 = BigInteger.Parse(Input);
            Input = Console.ReadLine();
            big2 = BigInteger.Parse(Input);
            BigInteger gcd = BigInteger.GreatestCommonDivisor(big1, big2); //gcd with two numbers
            Console.WriteLine("result");
            Console.WriteLine(gcd);
            Console.WriteLine("input find the a^x mod n");
            Console.WriteLine("input a");
            Input = Console.ReadLine();
            BigInteger big3 = BigInteger.Parse(Input);
            Console.WriteLine("input x");
            Input = Console.ReadLine();
            BigInteger big4 = BigInteger.Parse(Input);
            Console.WriteLine("input n");
            Input = Console.ReadLine();
            BigInteger big5 = BigInteger.Parse(Input);
            BigInteger bigInteger = BigInteger.ModPow(big3, big4, big5); //bigInteger=a^x mod p
            Console.WriteLine("result");
            Console.WriteLine(bigInteger);

        }


    }
}
