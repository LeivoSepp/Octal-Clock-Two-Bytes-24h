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
            Console.WriteLine("Hello World!");
            OctalClock();
            Console.ReadLine();
        }

        static async void OctalClock()
        {
            int msb = 0;
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
                            msb = octal_sum(msb, 5);
                            if (msb == 300)
                                msb = 0;
                        }
                        else
                        {
                            msb = octal_sum(msb, 1);
                        }
                    }
                }
                int msbDec = Convert.ToInt32(msb.ToString(), 8); //convert from octal to decimal

                //calculate minute and hour
                int hour = msbDec / 8;
                int minute = msbDec % 8 * 16 + lsb / 16;

                TimeSpan time = new TimeSpan(hour, minute, 0);
                DateTime dateTime = DateTime.Now.Date.Add(time);
                Console.WriteLine($"{dateTime:t} ");

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
