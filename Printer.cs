using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class Printer
    {
        private readonly CheckerBoard checkerBoard;

        public Printer(CheckerBoard checkerBoard)
        {
            this.checkerBoard = checkerBoard;
        }

        public void PrintBoard()
        {
            PrintColumnLabels();

            for (int i = 0; i < 8; i++)
            {
                PrintRowLabels(i);
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(" {0}", checkerBoard.GetPawn(new int[] { i, j }));
                }
                Console.WriteLine();
            }
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
