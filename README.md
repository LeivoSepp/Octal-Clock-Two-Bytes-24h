# Two Bytes Octal Clock 24h
This two bytes octal clock is used by the Paradox Security systems.
![Paradox](Readme/Paradox.png)

### Two bytes for a clock

These two bytes has to be a clock but I didn't know how. 

Byte1 : Byte2 = 0000 0000 : 0000 0000

### Algorithm

1. Byte2 is increasing in every minute by 16 bit. <br>
One way to show it is to use octal numeric system by adding 20 in every minute. (OCT) 00 20 40 60 100
2. When Byte2 reach it's maximum (OCT) 360 then one bit is added to Byte1. <br>
1 byte added into Byte1 in every 16 minute. One hour is: Byte1 increased by 3 bit and Byte2 increased by 176 bit.
3. Every hour the Byte1 will increase by 8 bit or by 10 (OCT). (OCT) 0h - 0; 1h - 10; 2h - 20; .. 8h - 100.

|Time|Byte1-Byte2|
|---|---|
| 00:00 | 00000000-00000000 |
| 00:01 | 00000000-00010000 |
| 00:02 | 00000000-00100000 |
| 00:03 | 00000000-00110000 |
| 00:04 | 00000000-01000000 |
| 00:05 | 00000000-01010000 |
| 00:06 | 00000000-01100000 |
| 00:07 | 00000000-01110000 |
| 00:08 | 00000000-10000000 |
| 00:09 | 00000000-10010000 |
| 00:10 | 00000000-10100000 |
| 00:11 | 00000000-10110000 |
| 00:12 | 00000000-11000000 |
| 00:13 | 00000000-11010000 |
| 00:14 | 00000000-11100000 |
| 00:15 | 00000000-11110000 |
| 00:16 | 00000001-00000000 |
| 00:17 | 00000001-00010000 |
| 00:18 | 00000001-00100000 |
| 00:19 | 00000001-00110000 |
| 00:20 | 00000001-01000000 |
| 00:21 | 00000001-01010000 |
| 00:22 | 00000001-01100000 |
| 00:23 | 00000001-01110000 |
| 00:24 | 00000001-10000000 |
| 00:25 | 00000001-10010000 |
| 00:26 | 00000001-10100000 |
| 00:27 | 00000001-10110000 |
| 00:28 | 00000001-11000000 |
| 00:29 | 00000001-11010000 |
| 00:30 | 00000001-11100000 |
| 00:31 | 00000001-11110000 |
| 00:32 | 00000010-00000000 |
| 00:33 | 00000010-00010000 |
| 00:34 | 00000010-00100000 |
| 00:35 | 00000010-00110000 |
| 00:36 | 00000010-01000000 |
| 00:37 | 00000010-01010000 |
| 00:38 | 00000010-01100000 |
| 00:39 | 00000010-01110000 |
| 00:40 | 00000010-10000000 |
| 00:41 | 00000010-10010000 |
| 00:42 | 00000010-10100000 |
| 00:43 | 00000010-10110000 |
| 00:44 | 00000010-11000000 |
| 00:45 | 00000010-11010000 |
| 00:46 | 00000010-11100000 |
| 00:47 | 00000010-11110000 |
| 00:48 | 00000011-00000000 |
| 00:49 | 00000011-00010000 |
| 00:50 | 00000011-00100000 |
| 00:51 | 00000011-00110000 |
| 00:52 | 00000011-01000000 |
| 00:53 | 00000011-01010000 |
| 00:54 | 00000011-01100000 |
| 00:55 | 00000011-01110000 |
| 00:56 | 00000011-10000000 |
| 00:57 | 00000011-10010000 |
| 00:58 | 00000011-10100000 |
| 00:59 | 00000011-10110000 |
| 01:00 | 00001000-00000000 |
| 01:01 | 00001000-00010000 |
| 01:02 | 00001000-00100000 |
| 01:03 | 00001000-00110000 |
| 01:04 | 00001000-01000000 |
|...|...|
| 23:59 | 10111011-10110000 |

### Two Solutions
There is a two different solutions. 
1. Mathemathical calculation based on octal numeric system. This was an initial solution.
2. Using traditional shift-operations in binary numbers. 

#### 1. Mathemathical
Reverse engineering found that the clock is based on the octal numeral system. If you look at the solution 2 then you will see that this is not true.
Anyway, this is how I started. To understand all of the complexity I had to acquire the ability to calculate simultaneously in all four numeral systems.

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
```

#### 2. Traditional binary shift
Look at the numbers in binary format. The clock is much simpler than it was at the beginning.

<img src="Readme/binary_solution.png" alt="drawing" width="200"/>

```c#
//getting minute and hour with shift operations
int b1Shift = Byte1 << 4;
int tHour = Byte1 >> 3;
int tMinute = (b1Shift & 32) + (b1Shift & 16) + (Byte2 >> 4);
```

Output of this program.

![Output1](Readme/output1.png)
## Thanks, Paradox

Thanks, Paradox for this challenge. Math is cool. Learn it and you will understand it.

This project is related directly with my Paradox Security System Spectra 1738 serial output reverse engineering project.</br>
https://github.com/LeivoSepp/Paradox-Spectra-1738-SerialOutput 

### Resources used during the project
Working with octal, byte, hex numbers.

These links were used to build the octal generator for a clock reverse engineering.</br>
https://stackoverflow.com/questions/34362859/add-two-octal-numbers-directly-without-converting-to-decimal </br>
https://stackoverflow.com/questions/3781764/how-can-we-convert-binary-number-into-its-octal-number-using-c </br>
https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter.tostring?view=net-5.0 </br>
https://stackoverflow.com/questions/1139957/convert-integer-to-hexadecimal-and-back-again </br>
