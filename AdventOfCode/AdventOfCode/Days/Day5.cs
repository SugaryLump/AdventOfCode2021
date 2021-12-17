using System;
using System.IO;

namespace AdventOfCode.Days
{
    class Day5
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day5.txt");
            StreamReader reader = new StreamReader(fileStream);

            int[,] straightTable = new int[1000, 1000];
            int[,] allTable = new int[1000, 1000];
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    straightTable[i, j] = 0;
                    allTable[i, j] = 0;
                }
            }

            int straightOverlap = 0;
            int allOverlap = 0;

            char[] separators = new char[] { ',', ' ', '-', '>' };
            string line = reader.ReadLine();

            while (line != null)
            {
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int x1 = Convert.ToInt32(tokens[0]);
                int y1 = Convert.ToInt32(tokens[1]);
                int x2 = Convert.ToInt32(tokens[2]);
                int y2 = Convert.ToInt32(tokens[3]);

                int i = x1;
                int j = y1;

                while (i != x2 || j != y2)
                {
                    allTable[i, j]++;
                    if (allTable[i, j] == 2)
                        allOverlap++;
                    if (x1 == x2 || y1 == y2)
                    {
                        straightTable[i, j]++;
                        if (straightTable[i, j] == 2)
                            straightOverlap++;
                    }

                    i += Math.Sign(x2 - i);
                    j += Math.Sign(y2 - j);
                }

                allTable[i, j]++;
                if (allTable[i, j] == 2)
                    allOverlap++;
                if (x1 == x2 || y1 == y2)
                {
                    straightTable[i, j]++;
                    if (straightTable[i, j] == 2)
                        straightOverlap++;
                }

                line = reader.ReadLine();
            }

            Console.WriteLine("The number of points where straight clouds overlap is " + straightOverlap +
                              "\nThe number of points were any shape of clouds overlap is " + allOverlap);

            reader.Close();
            fileStream.Close();
        }
    }
}
