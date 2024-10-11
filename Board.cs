using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class Board
    {
        private readonly char[,] board = new char[8, 8];
        public char[,] BoardArray
        {
            get { return board; }
        }

        public void InitBoard()
        {
            PrintColumnLabels();

            for (int i = 0; i < 8; i++)
            {
                PrintRowLabels(i);

                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = '#';

                    if (i > 4)
                    {
                        if ((i + j) % 2 == 1) board[i, j] = 'X';
                    }
                    else if (i < 3)
                    {
                        if ((i + j) % 2 == 1) board[i, j] = 'O';
                    }

                    Console.Write(" {0}", board[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void UpdateBoard(int playerNum, int[] startCoords, int[] endCoords)
        {
            board[startCoords[0], startCoords[1]] = '#';

            if (playerNum == 1)
                board[endCoords[0], endCoords[1]] = 'X';
            else
                board[endCoords[0], endCoords[1]] = 'O';

            if (IsCapture(playerNum, startCoords, endCoords))
            {
                int capturedRow = (startCoords[0] + endCoords[0]) / 2;
                int capturedCol = (startCoords[1] + endCoords[1]) / 2;
                board[capturedRow, capturedCol] = '#';
            }

            PrintColumnLabels();

            for (int i = 0; i < 8; i++)
            {
                PrintRowLabels(i);
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(" {0}", board[i, j]);
                }
                Console.WriteLine();
            }
        }

        public bool ValidateMove(int playerNum, int[] startCoords, int[] endCoords)
        {
            char startChar = board[startCoords[0], startCoords[1]];
            char endChar = board[endCoords[0], endCoords[1]];

            if (IsOutOfBounds(endCoords))
                return false;

            switch (playerNum)
            {
                case 1:
                    {
                        if (startChar != 'X')
                        {
                            Console.WriteLine("You can only move pieces of your own.");
                            return false;
                        }

                        if (startCoords[0] - 1 != endCoords[0] && !IsCapture(playerNum, startCoords, endCoords))
                        {
                            Console.WriteLine("You can only move one step forward from here.");
                            return false;
                        }

                        break;
                    }
                case 2:
                    {
                        if (startChar != 'O')
                        {
                            Console.WriteLine("You can only move pieces of your own.");
                            return false;
                        }

                        if (startCoords[0] + 1 != endCoords[0] && !IsCapture(playerNum, startCoords, endCoords))
                        {
                            Console.WriteLine("You can only move one step forward from here.");
                            return false;
                        }
                        break;
                    }
            }

            if (Math.Abs(endCoords[1] - startCoords[1]) != 1 && !IsCapture(playerNum, startCoords, endCoords))
            {
                Console.WriteLine("You can only move one one step diagonally.");
                return false;
            }

            if (endChar != '#')
            {
                Console.WriteLine("This position is already taken.");
                return false;
            }

            if (MustCapture(playerNum) && !IsCapture(playerNum, startCoords, endCoords))
            {
                return false;
            }

            return true;
        }

        private bool IsCapture(int playerNum, int[] startCoords, int[] endCoords)
        {
            char opponentPawn = playerNum == 1 ? 'O' : 'X';
            if (board[endCoords[0] - 1, endCoords[1] - 1] != opponentPawn)
                return false;

            return Math.Abs(startCoords[0] - endCoords[0]) == 2 && Math.Abs(startCoords[1] - endCoords[1]) == 2;
        }

        public bool MustCapture(int playerNum)
        {
            char opponentPawn = playerNum == 1 ? 'O' : 'X';
            char playerPawn = playerNum == 1 ? 'X' : 'O';

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == playerPawn)
                    {
                        if (i + 2 < 8 && j + 2 < 8 && board[i + 1, j + 1] == opponentPawn && board[i + 2, j + 2] == '#')
                            return true;

                        if (i + 2 < 8 && j - 2 >= 0 && board[i + 1, j - 1] == opponentPawn && board[i + 2, j - 2] == '#')
                            return true;

                        if (i - 2 >= 0 && j + 2 < 8 && board[i - 1, j + 1] == opponentPawn && board[i - 2, j + 2] == '#')
                            return true;

                        if (i - 2 >= 0 && j - 2 >= 0 && board[i - 1, j - 1] == opponentPawn && board[i - 2, j - 2] == '#')
                            return true;
                    }
                }
            }

            return false;
        }

        private static bool IsOutOfBounds(int[] coords)
        {
            return coords[0] < 0 || coords[0] >= 8 || coords[1] < 0 || coords[1] >= 8;
        }



        private static void PrintColumnLabels()
        {
            Console.Write("   ");
            for (char c = 'A'; c <= 'H'; c++)
            {
                Console.Write(" {0}", c);
            }
            Console.WriteLine("\n   -----------------");
        }

        private static void PrintRowLabels(int i)
        {
            Console.Write("{0} |", i + 1);
        }
    }
}
