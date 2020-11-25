using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
namespace bai3._3
{
    public partial class Form1 : Form
    {
        BigInteger a;
        BigInteger b;
        BigInteger d;
        BigInteger n;
        BigInteger n1;
        BigInteger e1;
        public const int maxsize = 128;
        bool testprimenumber = false;
        int randomcomple = 0;
        

        public Form1()
        {
            InitializeComponent();
        }
        public bool IsProbablePrime(BigInteger source, int certainty = 50) 
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
        }//check number is prime ??(Miller-Rabin algorithm)
        public int RandomNumber1()
        {
            Random random = new Random();
            int k = random.Next(0, 1000);
            while (IsProbablePrime(k, 15) == false)
            {
                k = random.Next(20, 1000);
            }
            return k;
        }
        public BigInteger randoma() //created random number 128 byte
        {
            BigInteger k;
            Random random = new Random();
            var bytearr = new byte[maxsize];
            var bytearr1 = new byte[maxsize+1]; //created 129 byte [0]
            var arr = new Byte[1];
            random.NextBytes(bytearr); //random 128 byte
            arr[0] = bytearr[maxsize-1]; //arr[0]=byte[128]
            Array.Copy(bytearr, bytearr1, maxsize); 
            k = new BigInteger(bytearr1); //created random number 128 byte
            System.Collections.BitArray d = new System.Collections.BitArray(arr); //d= byte[128]

            while (true)
            {
                if (d[7] == true && IsProbablePrime(k) == true) //check bit 1024 bit is 1, and k is prime number
                    return k;
                else
                {
                    random.NextBytes(bytearr);
                    arr[0] = bytearr[maxsize-1];
                    Array.Copy(bytearr, bytearr1, maxsize);
                    k = new BigInteger(bytearr1); //created random number 128 byte
                    d = new System.Collections.BitArray(arr);
                }
            }
            
            
        }
        public BigInteger mod(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                // q is quotient 
                BigInteger q = a / m;
                BigInteger t = m;

                // m is remainder now, process same as 
                // Euclid's algo 
                m = a % m;
                a = t;
                t = y;

                // Update y and x 
                y = x - q * y;
                x = t;
            }

            // Make x positive 
            if (x < 0)
                x += m0;

            return x;
        }//Euclid’s algorithm

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            randomcomple = 0;
           
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            randomcomple = 0;
           
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            randomcomple = 0;
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" && richTextBox3.Text != ""&&richTextBox2.Text!="") //check input q,p,e is not null
            {
                if (richTextBox6.Text != "") //check input is not null
                {
                    BigInteger encry;
                    string en = richTextBox6.Text;
                    byte[] en1 = Encoding.UTF8.GetBytes(en);
                    if (comboBox1.Text== "string") //check string or number
                    { 
                        encry = new BigInteger(en1);
                        if (randomcomple == 0) //check if random()
                            math();
                        if (encry > n)
                        {
                            MessageBox.Show("text>n");
                        }
                        else
                        {
                            if (testprimenumber == true) //q,p,e is primenumber
                            {
                                encry = BigInteger.ModPow(encry, e1, n); //encry
                                if (encry <= 255)
                                {
                                    en1 = encry.ToByteArray();
                                    byte[] en2 = new byte[1];
                                    en2[0] = en1[0];
                                    richTextBox5.Text = Convert.ToBase64String(en2);
                                }
                                else
                                {
                                    en1 = encry.ToByteArray();
                                    richTextBox5.Text = Convert.ToBase64String(en1);
                                }
                            }
                        }

                    }
                    else
                        try
                        {
                            encry = BigInteger.Parse(richTextBox6.Text);
                            if (randomcomple == 0)
                                math();
                            if (encry > n)
                            {
                                MessageBox.Show("text>n");
                            }
                            else
                            {
                                if (testprimenumber == true)
                                {
                                    encry = BigInteger.ModPow(encry, e1, n);
                                    if (encry <= 255)
                                    {
                                        en1 = encry.ToByteArray();
                                        byte[] en2 = new byte[1];
                                        en2[0] = en1[0];
                                        richTextBox5.Text = Convert.ToBase64String(en2);
                                    }
                                    else
                                    {
                                        en1 = encry.ToByteArray();
                                        richTextBox5.Text = Convert.ToBase64String(en1);
                                    }
                                }
                            }
                        }
                        catch(Exception e2)
                        {
                            MessageBox.Show(e2.Message); //type is number but input is not number
                        }
                    
                }
                else
                    MessageBox.Show("please input plaintext");
            }
            else
                MessageBox.Show("please input q,p,e");
           
        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" && richTextBox3.Text != "" && richTextBox2.Text != "") //check input q,p,e is not null
            {
                if (richTextBox5.Text != "") //check input is not null
                {
                    BigInteger dencry;
                    byte[] newBytes;
                   newBytes = Convert.FromBase64String(richTextBox5.Text); //base64 to byte
                   if (newBytes.Length == 1)
                        {
                            byte[] new1 = new byte[2];
                            new1[0] = newBytes[0];
                            new1[1] = 0;
                            dencry = new BigInteger(new1);
                        }
                   else
                        { dencry = new BigInteger(newBytes); } //byte to biginterge
                    if (randomcomple == 0) //check random or not
                        math();
                    if (dencry > n)
                    {
                        MessageBox.Show("text >n");
                    }
                    else
                    {
                        
                        if (testprimenumber == true) //check q,p,e is prime
                        {
                            if (comboBox1.Text == "string") //type is string
                            {
                                dencry = BigInteger.ModPow(dencry, d, n);
                                newBytes = dencry.ToByteArray();
                                richTextBox6.Text = Encoding.UTF8.GetString(newBytes);
                            }
                            else ////type is number
                            {
                                dencry = BigInteger.ModPow(dencry, d, n);
                                richTextBox6.Text = dencry.ToString();
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("please input ciphertext");
            }
            else
                MessageBox.Show("please input q,p,e");
        }
        
        public void math() //created d,n 
        {
            if (richTextBox1.Text != "" && richTextBox3.Text != "" && richTextBox2.Text != "")//check input q,p,e is not null
            {
                BigInteger q = BigInteger.Parse(richTextBox1.Text);
                BigInteger p = BigInteger.Parse(richTextBox3.Text);
                BigInteger e = BigInteger.Parse(richTextBox2.Text);
                e1 = e;
                if (IsProbablePrime(q) != false && IsProbablePrime(p) != false && IsProbablePrime(e) != false)//check q,p,e is prime
                {
                    n = q * p; 
                    n1 = n - q - p + 1;
                    d = mod(e, n1);
                    testprimenumber = true;
                }
                else
                {
                    testprimenumber = false;
                    MessageBox.Show("wrong it is not prime number");
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e) //random q,p,e 128 byte
        {
            a = randoma();
            b = randoma();
            n = a * b;
            n1 = n - a - b + 1;
            richTextBox1.Text = a.ToString();
            richTextBox3.Text = b.ToString();
            try
            {
                e1 = RandomNumber1();
                d = mod(e1, n1);
            }
            catch
            {
                e1 = RandomNumber1();
                d = mod(e1, n1);
            }
            int compare=BigInteger.Compare(e1, d);
            while(compare==0)
            {
                e1 = RandomNumber1();
                d = mod(e1, n1);
                compare = BigInteger.Compare(e1, d);
            }
            richTextBox2.Text = e1.ToString();
            
            randomcomple = 1;
            testprimenumber = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("random q,p,e 128 byte");
        }
    }
}
