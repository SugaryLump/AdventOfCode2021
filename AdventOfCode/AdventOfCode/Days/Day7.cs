using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    class Day7
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day7.txt");
            StreamReader reader = new StreamReader(fileStream);

            string[] lineTokens = reader.ReadLine().Split(',');
            List<int> values = new List<int>();
            int sum = 0;

            for (int i = 0; i < lineTokens.Length; i++)
            {
                int value = Convert.ToInt32(lineTokens[i]);
                sum += value;

                //Sorted insert for median
                int v = 0;
                while (v < values.Count)
                {
                    if (value > values[v])
                        v++;
                    else
                        break;
                }
                values.Insert(v, value);
            }

            int average = sum / values.Count;
            int median;

            //Is pair?
            int remainder;
            Math.DivRem(values.Count, 2, out remainder);
            if (remainder == 0)
            {
                double div = (values[values.Count / 2] + values[values.Count / 2 - 1]) / 2;
                median = (int)Math.Round(div);
            }
            else
                median = values[values.Count / 2];

            int cost1 = 0;
            int cost2 = 0;
            foreach (int value in values)
            {
                cost1 += Math.Abs(value - median);

                int movingPos = value;
                for (int accumulatedCost = 1; movingPos != average; accumulatedCost++) {
                    cost2 += accumulatedCost;
                    movingPos += Math.Sign(average - value);
                }
            }

            Console.WriteLine("Optimal consumption with constant fuel usage is " + cost1 +
                              "\nReadjusting fuel usage... new optimal consumption is " + cost2);

            reader.Close();
            fileStream.Close();
        }
    }
}
