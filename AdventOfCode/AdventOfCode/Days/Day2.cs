using System;
using System.IO;

namespace AdventOfCode.Days
{
    class Day2
    {
        public static void run()
        {
            int horizontalPosition = 0;
            int fakeDepthOrAim = 0;
            int realDepth = 0;

            FileStream fileStream = File.OpenRead("day2.txt");
            StreamReader reader = new StreamReader(fileStream);
            String line = reader.ReadLine();
            while (line != null)
            {
                String[] tokens = line.Split(' ');
                int value = int.Parse(tokens[1]);

                switch (tokens[0])
                {
                    case "up":
                        fakeDepthOrAim -= value;
                        break;
                    case "down":
                        fakeDepthOrAim += value;
                        break;
                    default:
                        horizontalPosition += value;
                        realDepth += value * fakeDepthOrAim;
                        break;
                }

                line = reader.ReadLine();
            }

            Console.WriteLine("Submarine Coordinates: " +
                              "(" + horizontalPosition + ", " + fakeDepthOrAim + ")" +
                              "\nX * Y = " + (horizontalPosition * fakeDepthOrAim) +
                              "\nWait, the manual was backwards? Here we go again!\n" +
                              "Submarine Coordinates: " +
                              "(" + horizontalPosition + ", " + realDepth + ")" +
                              "\nX * Y = " + (horizontalPosition * realDepth));

            reader.Close();
            fileStream.Close();
        }
    }
}
