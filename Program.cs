
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checkers\n");

            Board board = new();
            Player p1 = new(1);
            Player p2 = new(2);

            board.InitBoard();

            Console.WriteLine();

            int i = 0;

            while (true)
            {
                Player player;

                if (i % 2 == 0) player = p1;
                else player = p2;

                if(board.MustCapture(player.PlayerNum))
                {
                    Console.WriteLine("Player {0} has the opportunity to jump their opponent's checker.\nThe rules state that you must capture if possible.\n", player.PlayerNum);
                }

                Console.WriteLine("Player {0}: What piece do you want to move? (1-8, A-H): ", player.PlayerNum);
                string startPosition = AskUserInput();

                Console.WriteLine();

                Console.WriteLine("Move piece from {0} to: ", startPosition);
                string endPosition = AskUserInput();

                Console.WriteLine();

                int[] startCoords = ParsePosition(startPosition);
                int[] endCoords = ParsePosition(endPosition);

                if (player.Move(board, startCoords, endCoords) == -1) i++;

                Console.WriteLine();

                i++;
            }
        }

        static string AskUserInput()
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

        static int[] ParsePosition(string position)
        {
            char[] cols = "ABCDEFGH".ToCharArray();
            int row = Convert.ToInt32(position[..1]) - 1;
            int col = Array.IndexOf(cols, position[1]);
            return new int[] { row, col };
        }



    }
}
