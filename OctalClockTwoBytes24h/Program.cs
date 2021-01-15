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
            Console.WriteLine("Octal clock reverse engineering used in Paradox Security Systems");
            OctalClock();
            Console.ReadLine();
        }

        static async void OctalClock()
        {
            int msb8 = 0;
            byte lsb = 0;
            int i = 0;
            bool FirstLoop = true;
            while (true)
            {
                if (!FirstLoop)
                {
                    if (lsb == 0)
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
                int msb = Convert.ToInt32(msb8.ToString(), 8); //convert from octal to decimal

                var msbBin = Convert.ToString(msb, 2);
                var lsbBin = Convert.ToString(lsb, 2);

                //getting minute and hour with shift operations
                int msbShift = msb << 4;
                int tHour = msb >> 3;
                int tMinute = (msbShift & 32) + (msbShift & 16) + (lsb >> 4);

                //getting minute and hour in another mathemathical way
                int hour = msb / 8;
                int minute = msb % 8 * 16 + lsb / 16;

                Console.WriteLine($"{(tHour < 10 ? $"0{tHour}" : $"{tHour}")}:{(tMinute < 10 ? $"0{tMinute}" : $"{tMinute}")} msb-lsb: {Convert.ToString(msb, 2).PadLeft(8, '0')}-{Convert.ToString(lsb, 2).PadLeft(8, '0')}");

                if (lsb >= 240 || i >= 59)
                {
                    lsb = 0;
                    i++;
                    if (i >= 59)
                        i = 0;
                }
                else
                {
                    lsb += 16;
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
