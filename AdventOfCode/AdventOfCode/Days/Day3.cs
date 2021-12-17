using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    class Day3
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day3.txt");
            StreamReader reader = new StreamReader(fileStream);

            String line = reader.ReadLine();
            List<string> valuesOxygen = new List<string>();
            List<string> valuesCO2 = new List<string>();
            int[,] frequencies = new int[line.Length, 2];
            for (int b = 0; b < line.Length; b++)
                frequencies[b, 0] = frequencies[b, 1] = 0;

            while (line != null)
            {
                valuesOxygen.Add(line);
                valuesCO2.Add(line);
                for (int b = 0; b < line.Length; b++)
                {
                    int bit = line[b] - 48;
                    frequencies[b, bit]++;
                }

                line = reader.ReadLine();
            }

            double gammaRate = 0;
            double epsilonRate = 0;

            for (int b = 0; b < frequencies.GetLength(0); b++)
            {
                if (frequencies[b, 1] > frequencies[b, 0])
                    gammaRate += Math.Pow(2, frequencies.GetLength(0) - 1 - b);
                else
                    epsilonRate += Math.Pow(2, frequencies.GetLength(0) - 1 - b);
            }

            Console.WriteLine("Gamma Rate = " + gammaRate +
                "\nEpsilon Rate = " + epsilonRate +
                "\nPower Consumption = " + (epsilonRate * gammaRate));


            char oBitRule;
            char cBitRule;
            if (frequencies[0, 0] > frequencies[0, 1])
            {
                oBitRule = '0';
                cBitRule = '1';
            }
            else
            {
                oBitRule = '1';
                cBitRule = '0';
            }

            for (int b = 0;
                 b < frequencies.GetLength(0) && (valuesOxygen.Count > 1 || valuesCO2.Count > 1);
                 b++)
            {
                int i = 0;
                int[] newFrequenciesO = new int[2];
                int[] newFrequenciesC = new int[2];
                while (i < valuesOxygen.Count && valuesOxygen.Count > 1)
                {
                    if (valuesOxygen[i][b] != oBitRule)
                        valuesOxygen.RemoveAt(i);
                    else
                    {
                        if (b + 1 < frequencies.GetLength(0))
                        {
                            int nextBit = valuesOxygen[i][b + 1] - 48;
                            newFrequenciesO[nextBit]++;
                        }
                        i++;
                    }
                }
                i = 0;
                while (i < valuesCO2.Count && valuesCO2.Count > 1)
                {
                    if (valuesCO2[i][b] != cBitRule)
                        valuesCO2.RemoveAt(i);
                    else
                    {
                        if (b + 1 < frequencies.GetLength(0))
                        {
                            int nextBit = valuesCO2[i][b + 1] - 48;
                            newFrequenciesC[nextBit]++;
                        }
                        i++;
                    }
                }
                oBitRule = newFrequenciesO[0] > newFrequenciesO[1] ? '0' : '1';
                cBitRule = newFrequenciesC[0] > newFrequenciesC[1] ? '1' : '0';
            }

            double oxygenRate = Convert.ToInt32(valuesOxygen[0], 2);
            double co2Rate = Convert.ToInt32(valuesCO2[0], 2);

            Console.WriteLine("Oxygen Generator Rating = " + oxygenRate +
                "\nCO2 Scrubber Rating = " + co2Rate +
                "\nLife Suport Rating = " + (oxygenRate * co2Rate));
            reader.Close();
            fileStream.Close();
        }
    }
}
