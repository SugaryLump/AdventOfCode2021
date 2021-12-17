using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode.Days;

namespace AdventOfCode
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            int day = 0;
            Stopwatch watch = new Stopwatch();
            while (day != -1)
            {
                Console.WriteLine("+________________________________________+\n" +
                                  "|Select advent problem day. '-1' to exit.|\n" +
                                  "+________________________________________+");
                Console.Write("> ");
                String input = Console.ReadLine();
                while (!int.TryParse(input, out day))
                    int.TryParse(input, out day);

                bool failed = false;
                watch.Start();
                switch (day)
                {
                    case -1:
                        failed = true;
                        break;
                    case 1:
                        Day1.run();
                        break;
                    case 2:
                        Day2.run();
                        break;
                    case 3:
                        Day3.run();
                        break;
                    case 4:
                        Day4.run();
                        break;
                    case 5:
                        Day5.run();
                        break;
                    case 6:
                        Day6.run();
                        break;
                    case 7:
                        Day7.run();
                        break;
                    default:
                        Console.WriteLine("Not a valid day.");
                        failed = true;
                        break;
                }
                watch.Stop();
                if (!failed)
                    Console.WriteLine("(Exec. time = " + watch.ElapsedMilliseconds + " ms)");
                watch.Reset();
            }
        }

        

        

        

        

        

        
    }
}
