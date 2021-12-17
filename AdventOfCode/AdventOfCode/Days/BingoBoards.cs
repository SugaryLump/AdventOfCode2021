using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Days
{
    class BingoBoards
    {
        private int totalPulls;
        private Dictionary<int,BingoBoard> boards;
        private List<BingoBoard> playingBoards;
        private Dictionary<int, BingoNumber> numbers;
        private int loserScore;

        public BingoBoards() {
            this.boards = new Dictionary<int, BingoBoard>();
            this.playingBoards = new List<BingoBoard>();
            this.numbers = new Dictionary<int, BingoNumber>();
            this.totalPulls = 0;
            this.loserScore = -1;
        }

        public void addBoard (String[,] board)
        {
            int id = BingoBoard.count++;
            BingoNumber[,] boardNumbers = new BingoNumber[5,5];
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    int number = int.Parse(board[r, c]);
                    BingoNumber bingoNumber;
                    if (!numbers.TryGetValue(number, out bingoNumber))
                    {
                        bingoNumber = new BingoNumber(number);
                        numbers[number] = bingoNumber;
                    }
                    bingoNumber.addBoard(id, r, c);
                    boardNumbers[r, c] = bingoNumber;
                }
            }

            BingoBoard bingoBoard = new BingoBoard(boardNumbers, id);
            boards[id] = bingoBoard;
            playingBoards.Add(bingoBoard);
        }

        public int markNumberAndCheckForWin (int number, bool calculateScore)
        {
            BingoNumber bingoNumber;
            totalPulls++;
            if (numbers.TryGetValue(number, out bingoNumber))
            {
                bingoNumber.mark();
                if (totalPulls >= 5)
                {
                    foreach (KeyValuePair<int, KeyValuePair<int, int>> boardPair in bingoNumber.getBoards())
                    {
                        int id = boardPair.Key;
                        BingoBoard board = boards[id];

                        int row = boardPair.Value.Key;
                        int col = boardPair.Value.Value;

                        if (board.checkForWin(row, col))
                        {
                            playingBoards.Remove(board);
                            if (playingBoards.Count == 0 && loserScore == -1)
                                loserScore = board.getSumUnmarked() * number;
                            if (calculateScore)
                                return board.getSumUnmarked() * number;
                        }
                    }
                }
            }
            return -1;
        }

        public int getLoserScore(int number)
        {
            return loserScore;
        }
    }

    class BingoNumber
    {
        private int value;
        private bool marked;
        private Dictionary<int, KeyValuePair<int, int>> boards;

        public BingoNumber(int value)
        {
            this.value = value;
            this.marked = false;
            this.boards = new Dictionary<int, KeyValuePair<int, int>>();
        }

        public void mark()
        {
            this.marked = true;
        }

        public Dictionary<int, KeyValuePair<int, int>> getBoards() 
        {
            return this.boards;
        }

        public void addBoard (int boardID, int row, int col)
        {
            this.boards.Add(boardID, new KeyValuePair<int, int>(row, col));
        }

        public bool isMarked()
        {
            return marked;
        }

        public int getValue()
        {
            return value;
        }
    }

    class BingoBoard
    {
        public static int count = 0;

        private BingoNumber[,] board;
        private int id;

        public BingoBoard(BingoNumber[,] board, int id) {
            this.board = board;
            this.id = id;
        }

        //Checks for win at given row and column
        public bool checkForWin(int row, int col)
        {
            bool winnerRow = true;
            bool winnerCol = true;
            for (int i = 0; i < 5 && (winnerRow || winnerCol); i++)
            {
                if (!board[row, i].isMarked())
                    winnerRow = false;
                if (!board[i, col].isMarked())
                    winnerCol = false;
            }

            return winnerCol || winnerRow;
        }

        public int getSumUnmarked()
        {
            int sumUnmarked = 0;

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (!board[r,c].isMarked())
                        sumUnmarked+= board[r, c].getValue();
                }
            }

            return sumUnmarked;
        }
    }
}
