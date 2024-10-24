
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checkers\n");

            CheckerBoard board = new();
            Validator validator = new(board);
            Printer printer = new(board);

            Player p1 = new(1);
            Player p2 = new(2);

            board.InitBoard();

            int i = 0;

            while (true)
            {
                printer.PrintBoard();

                Console.WriteLine();

                Player player;

                if (i % 2 == 0) player = p1;
                else player = p2;

                validator.CheckCaptures(player.PlayerNum);

                Console.WriteLine("Player {0}: What piece do you want to move? (1-8, A-H): ", player.PlayerNum);
                string startPosition = AskUserInput();

                Console.WriteLine();

                Console.WriteLine("Move piece from {0} to: ", startPosition);
                string endPosition = AskUserInput();

                Console.WriteLine();

                int[] startCoords = ParsePosition(startPosition);
                int[] endCoords = ParsePosition(endPosition);

                bool legalMove = validator.ValidateMove(player.PlayerNum, startCoords, endCoords);

                if (legalMove) player.Move(board, startCoords, endCoords);
                else i++;

                Console.WriteLine();

                if(board.GetWinner() != null)
                {
                    Console.WriteLine("Player {0} wins! Game over.", board.GetWinner());
                    break;
                }

                i++;
            }
        }

        private static string AskUserInput()
        {
            string? input;
            string pattern = "^[1-8][A-H]$";
            while (true)
            {
                input = Console.ReadLine();
                if (input != null && Regex.IsMatch(input.ToUpper(), pattern)) break;
                else Console.WriteLine("Invalid input. Please try again: ");
            }
            return input.ToUpper();
        }

        public static int[] ParsePosition(string position)
        {
            char[] cols = "ABCDEFGH".ToCharArray();
            int row = Convert.ToInt32(position[..1]) - 1;
            int col = Array.IndexOf(cols, position[1]);
            return new int[] { row, col };
        }

        public static string ParsePosition(int[] coords)
        {
            char[] cols = "ABCDEFGH".ToCharArray();
            int row = coords[0] + 1;
            char col = cols[coords[1]];
            return $"{row}{col}";
        }
    }
}
