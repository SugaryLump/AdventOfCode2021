using System;
using System.IO;

namespace AdventOfCode.Days
{
    class Day8
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day8.txt");
            StreamReader reader = new StreamReader(fileStream);

            string line = reader.ReadLine();
            int totalEasy = 0;
            int sum = 0;
            while (line != null)
            {
                string[] lineTokens = line.Split('|');
                string[] uniquePatterns = lineTokens[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string digit1 = null;
                string digit4 = null;
                for (int i = 0; digit1 == null || digit4 == null; i++)
                {
                    if (uniquePatterns[i].Length == 2)
                        digit1 = uniquePatterns[i];
                    else if (uniquePatterns[i].Length == 4)
                        digit4 = uniquePatterns[i];
                }

                string[] outputPatterns = lineTokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int p = 0; p < outputPatterns.Length; p++)
                {
                    int number = patternToNumber(outputPatterns[p], digit1, digit4);
                    sum += number * (int)Math.Pow(10, 3 - p);
                    if (number == 1 || number == 4 || number == 7 || number == 8)
                        totalEasy++;
                }

                line = reader.ReadLine();
            }

            Console.WriteLine("There are " + totalEasy + " instances of the digits 1, 4, 7 and 8." +
                              "\nSum of outputs is " + sum);
            reader.Close();
            fileStream.Close();
        }

        /**
         * Returns how many wires of comparisonDigit are missing in digit. Ignores
         * differences bigger than 2 because that's all we need for differenciating.
         */
        public static int missingWires(string pattern, string comparisonPattern)
        {
            int total = 0;
            for (int c = 0; c < comparisonPattern.Length && total < 2; c++)
            {
                if (!pattern.Contains(comparisonPattern[c]))
                    total++;
            }
            return total;
        }

        public static int patternToNumber(string pattern, string digit1, string digit4)
        {
            switch (pattern.Length)
            {
                case 2:
                    return 1;
                case 3:
                    return 7;
                case 4:
                    return 4;
                case 5:
                    if (missingWires(pattern, digit1) == 0)
                        return 3;
                    if (missingWires(pattern, digit4) == 1)
                        return 5;
                    return 2;
                case 6:
                    if (missingWires(pattern, digit1) == 1)
                        return 6;
                    if (missingWires(pattern, digit4) == 1)
                        return 0;
                    return 9;
                case 7:
                    return 8;
            }
            return -1;
        }
    }
}
