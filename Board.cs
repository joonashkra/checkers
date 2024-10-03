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

                    if(i < 3)
                    {
                        if ((i + j) % 2 == 1) board[i, j] = 'O';
                    }
                    else if(i>4)
                    {
                        if ((i + j) % 2 == 1) board[i, j] = 'X';
                    }

                    Console.Write(" {0}", board[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void UpdateBoard(Player player, int[] start, int[] end)
        {
            board[start[0], start[1]] = '#';

            if (player.PlayerNum == 1) board[end[0], end[1]] = 'X';
            else board[end[0], end[1]] = 'O';

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
