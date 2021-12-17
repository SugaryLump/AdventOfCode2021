using System;
using System.IO;

namespace AdventOfCode.Days
{
    class Day4
    {
        public static void run()
        {
            FileStream fileStream = File.OpenRead("day4.txt");
            StreamReader reader = new StreamReader(fileStream);

            string[] pullStrings = reader.ReadLine().Split(",");
            BingoBoards boards = new BingoBoards();

            String line = reader.ReadLine();
            while (line != null)
            {
                string[,] boardStrings = new string[5, 5];
                for (int r = 0; r < 5; r++)
                {
                    string[] row = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    for (int c = 0; c < 5; c++)
                    {
                        boardStrings[r, c] = row[c];
                    }
                }
                boards.addBoard(boardStrings);
                line = reader.ReadLine();
            }

            int winnerScore = -1;
            int loserScore = -1;
            for (int i = 0; i < 100 && (winnerScore == -1 || loserScore == -1); i++)
            {
                int pull = int.Parse(pullStrings[i]);
                if (winnerScore == -1)
                {
                    winnerScore = boards.markNumberAndCheckForWin(pull, true);
                }
                else
                {
                    boards.markNumberAndCheckForWin(pull, false);
                    loserScore = boards.getLoserScore(pull);
                }
            }

            Console.WriteLine("We have a winner! And their score is " + winnerScore +
                              "\nAnd our loser has a score of " + loserScore);

            reader.Close();
            fileStream.Close();
        }
    }
}
