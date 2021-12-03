using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int day = 0;
            while (day != -1)
            {
                Console.WriteLine("Seleciona o dia do problema.");
                String input = Console.ReadLine();

                while (!int.TryParse(input, out day))
                    int.TryParse(input, out day);

                switch (day)
                {
                    case 1:
                        Day1();
                        break;
                }
            }
        }

        static void Day1()
        {
            StreamReader reader = new StreamReader(File.OpenRead("C:\\Users\\frien\\Documents\\Advent of Code\\day1.txt"));
            string line = reader.ReadLine();
            List<int> values = new List<int>();
            while (line != null)
            {
                values.Add(int.Parse(line));
                line = reader.ReadLine();
            }
            int prev = values[0];
            int total = 0;
            for (int i = 1; i < values.Count; i++)
            {
                if (values[i] > prev)
                    total++;
                prev = values[i];
            }
            Console.WriteLine(total);

            total = 0;
            int prev_window = values[0] + values[1] + values[2];
            int window = values[1] + values[2] + values[3];
            for (int i = 1; i < values.Count - 2; i++)
            {
                window -= values[i - 1];
                window += values[i + 2];
                if (window > prev_window)
                    total++;
                prev_window = window;
            }

            Console.WriteLine(total);
        }
    }
}
