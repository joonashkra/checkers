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

        public void UpdateBoard(Player player, int[] startCoords, int[] endCoords)
        {
            board[startCoords[0], startCoords[1]] = '#';

            if (player.PlayerNum == 1)
                board[endCoords[0], endCoords[1]] = 'X';
            else
                board[endCoords[0], endCoords[1]] = 'O';

            if (IsCapture(startCoords, endCoords))
            {
                int capturedRow = (startCoords[0] + endCoords[0]) / 2;
                int capturedCol = (startCoords[1] + endCoords[1]) / 2;
                board[capturedRow, capturedCol] = '#';
                Console.WriteLine("Capture!\n");
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

        public static bool IsCapture(int[] startCoords, int[] endCoords)
        {
            return Math.Abs(startCoords[0] - endCoords[0]) == 2 && Math.Abs(startCoords[1] - endCoords[1]) == 2;
        }

        private void PrintColumnLabels()
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
