using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    class Day1
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day1.txt");
            StreamReader reader = new StreamReader(fileStream);
            List<int> values = new List<int>();

            int totalIncrements = 0;
            int totalIncrementsWindow = 0;
            int window = 0;
            int prev_window;

            string line = reader.ReadLine();
            for (int i = 0; line != null; line = reader.ReadLine(), i++)
            {
                int value = int.Parse(line);
                values.Add(value);

                prev_window = window;
                window += value;

                if (i > 0)
                {
                    if (value > values[i - 1])
                        totalIncrements++;

                    if (i > 2)
                    {
                        window -= values[i - 3];
                        if (window > prev_window)
                            totalIncrementsWindow++;
                    }
                }
            }

            Console.WriteLine("Increments: " + totalIncrements + "; " +
                              "Window Increments: " + totalIncrementsWindow);

            reader.Close();
            fileStream.Close();
        }
    }
}
