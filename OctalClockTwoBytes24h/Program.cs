using System;
using System.Threading.Tasks;

namespace OctalClockTwoBytes24h
{
    //this clock is used in Paradox Security systems
    //to solve the clock, I wrote the generator as a temporary project
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Paradox Security System clock reverse engineering");
            OctalClock();
            Console.ReadLine();
        }

        static async void OctalClock()
        {
            int msb8 = 0;
            byte Byte2 = 0;
            int i = 0;
            bool FirstLoop = true;
            while (true)
            {
                if (!FirstLoop)
                {
                    if (Byte2 == 0)
                    {
                        if (i == 0)
                        {
                            msb8 = octal_sum(msb8, 5);
                            if (msb8 == 300)
                                msb8 = 0;
                        }
                        else
                        {
                            msb8 = octal_sum(msb8, 1);
                        }
                    }
                }
                int Byte1 = Convert.ToInt32(msb8.ToString(), 8); //convert from octal to decimal

                //getting minute and hour with shift operations
                int bHour = Byte1 >> 3;
                int bMinute = ((Byte1 & 3) << 4) + (Byte2 >> 4);

                //creating one 16 bit number and then reading clock from that number
                int twoBytes = (Byte1 << 8) + Byte2;
                int tHour = twoBytes >> 11;
                int tMinute = (twoBytes & 0x3F0) >> 4;

                //getting minute and hour in another mathemathical way
                int hour = Byte1 / 8;
                int minute = Byte1 % 8 * 16 + Byte2 / 16;

                Console.WriteLine($"| {(tHour < 10 ? $"0{tHour}" : $"{tHour}")}:{(tMinute < 10 ? $"0{tMinute}" : $"{tMinute}")} | {Convert.ToString(Byte1, 2).PadLeft(8, '0')}-{Convert.ToString(Byte2, 2).PadLeft(8, '0')} |");

                if (Byte2 >= 240 || i >= 59)
                {
                    Byte2 = 0;
                    i++;
                    if (i >= 59)
                        i = 0;
                }
                else
                {
                    Byte2 += 16;
                    i++;
                }
                FirstLoop = false;
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }
        }
        static int octal_sum(int a, int b)
        {
            int sum = 0, carry = 0, d = 0, m = 1;
            while (a > 0 || b > 0 || carry > 0)
            {
                d = 0;
                d = carry + (a % 10) + (b % 10);
                a /= 10; b /= 10;
                if (d > 7)
                {
                    carry = 1;
                    d = d % 8;
                }
                else
                {
                    carry = 0;
                }
                sum += d * m;
                m *= 10;
            }
            return sum;     //returns octal sum of a and b
        }
    }

}
