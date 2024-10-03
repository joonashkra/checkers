
namespace assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checkers");

            Board board = new();

            Player p1 = new();
            Player p2 = new();

            p1.PlayerNum = 1;
            p2.PlayerNum = 2;

            board.InitBoard();

            int i = 0;

            while (true)
            {
                Player player;

                if (i % 2 == 0) player = p1;
                else player = p2;

                Console.WriteLine("Player {0}: What piece do you want to move? (1-8, A-H): ", player.PlayerNum);
                string? startPosition = Console.ReadLine();

                Console.WriteLine();

                Console.WriteLine("And where do you want to move it? (1-8, A-H): ");
                string? endPosition = Console.ReadLine();

                if (startPosition == null || endPosition == null) return;

                int[] start = ParsePosition(startPosition);
                int[] end = ParsePosition(endPosition);

                if (Player.Move(board, player, start, end) == -1) i++;

                Console.WriteLine();

                i++;
            }
        }

        static int[] ParsePosition(string position)
        {
            char[] cols = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];
            int row = Convert.ToInt32(position[..1]) - 1;
            int col = Array.IndexOf(cols, position[1]);
            return [row, col];
        }
    }
}
