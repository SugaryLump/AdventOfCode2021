using System;
using System.IO;

namespace AdventOfCode.Days
{
    class Day6
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day6.txt");
            StreamReader reader = new StreamReader(fileStream);

            string[] fishStrings = reader.ReadLine().Split(',');
            long[] fish = new long[9];
            for (int f = 0; f < fishStrings.Length; f++)
                fish[Convert.ToInt32(fishStrings[f])]++;
            long totalFish80 = 0;
            long totalFish = 0;

            for (int i = 1; i <= 256; i++)
            {
                long motherfish = fish[0];
                for (int f = 0; f < 8; f++)
                    fish[f] = fish[f + 1];

                fish[8] = motherfish;
                fish[6] += motherfish;

                if (i == 80)
                {
                    for (int j = 0; j < 9; j++)
                        totalFish80 += fish[j];
                }
            }

            for (int j = 0; j < 9; j++)
                totalFish += fish[j];

            Console.WriteLine("Wow! There will " + totalFish80 + " lanternfish here in 80 days.\n" +
                              "Thankfully they don't live forever, there'd be " + totalFish + " in 256 days!");
            reader.Close();
            fileStream.Close();
        }

        public static long totalFishChildren(int fishTimer, int days)
        {
            if (fishTimer >= days)
                return 0;

            long totalChildren = 0;
            long firstGen = 1 + (days - fishTimer - 1) / 7;

            for (long c = 0; c < firstGen && days - fishTimer - 1 - (7 * c) > 8; c++)
                totalChildren += totalFishChildren(8, (int)(days - fishTimer - 1 - (7 * c)));

            totalChildren += firstGen;
            return totalChildren;
        }
    }
}
