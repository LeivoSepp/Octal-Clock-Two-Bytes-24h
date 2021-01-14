# Two Bytes Octal Clock 24h
This two bytes octal clock is used by the Paradox Security systems.
![Paradox](Readme/Paradox.png)
Have you heard about octal numeral system? What about clock based on octal numeral system?

Daily basis we are using decimal numeral system. Computers are using binary and the hexadecimal system is used to make it easier to represent. Recently I was engaged in reverse engineering of the Paradox Security system. 
I found myself solving surprising problem that how the Paradox time is working.

Reverse engineering found that the clock is based on the octal numeral system. To get there and understand all of this I had to acquire the ability to calculate simultaneously in all four numeral systems.

Thanks, Paradox for this challenge. The math is really cool. Learn it and you will understand it.

This project is related directly with my Paradox Security System Spectra 1738 serial output reverse engineering project.</br>
https://github.com/LeivoSepp/Paradox-Spectra-1738-SerialOutput 

Most difficult task in this reverse engineering project was to figure out how the clock is working. 
Initially I was thinking that bytes 3-4 from Paradox doesn't mean anything as they changed with no pattern at all. 
Somehow I started to look also a watch and I saw that these bytes are changing in every minute.

The actual outcome of this task is completely useless as it reads just the time reported by Paradox panel (24h format). 
After integration with Home Automation the clock is managed anyway by Rasperry PI and will taken and sychronized from the internet.

I started to solve it as this kind of unknown things are very interesting. I know that these two bytes has to be a clock but I dont know how. 
To solve this mathemathical clock challenge the first task was to build the clock generator.

During the calculator building I realized that the solution is based on octal numeric system. Huhh, crazy thing. 
Do you know what is Octal numeric system? The numbers are going up only to 7 and after that comes 10.
>Octal 0,1,2,3,4,5,6,7,10,11,12,13,14,15,16,17 ...

Some time examples:
* time 23:59 is in Octal 273 260 and in Hex 0xBB 0xB0.
* time 8:00 is in Octal 100 and in Hex 0x08.
* time 20:00 is in Otal 240 and in Hex 0xA0.

The final solution is a geniusly simple as it has just two lines of code (hours and minutes) with a little mathematics. 

```c#
int hour = msbDec / 8;
int minute = msbDec % 8 * 16 + lsb / 16;

TimeSpan time = new TimeSpan(hour, minute, 0);
DateTime dateTime = DateTime.Now.Date.Add(time);
Console.WriteLine($"{dateTime:t} ");
```

![Output](Readme/output.png)

### Resources used during the project
Working with octal, byte, hex numbers.

These links were used to build the octal generator for a clock reverse engineering.</br>
https://stackoverflow.com/questions/34362859/add-two-octal-numbers-directly-without-converting-to-decimal </br>
https://stackoverflow.com/questions/3781764/how-can-we-convert-binary-number-into-its-octal-number-using-c </br>
https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter.tostring?view=net-5.0 </br>
https://stackoverflow.com/questions/1139957/convert-integer-to-hexadecimal-and-back-again </br>
