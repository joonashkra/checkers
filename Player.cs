using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class Player
    {
        private int playerNum;

        public int PlayerNum
        {
            get { return playerNum; }
            set { playerNum = value; }
        }

        public static int Move(Board board, Player player, int[] start, int[] end)
        {
            bool validMove = ValidateMove(board.BoardArray, player.PlayerNum, start, end);

            if (!validMove) return -1;

            board.UpdateBoard(player, start, end);
            return 0;
        }

        private static bool ValidateMove(char[,] board, int playerNum, int[] start, int [] end)
        {

            if (board[start[0], start[1]] == '#')
            {
                Console.WriteLine("This piece does not exist.");
                return false;
            }

            switch (playerNum)
            {
                case 1:
                    {
                        if (board[start[0], start[1]] != 'X')
                        {
                            Console.WriteLine("You can only move pieces of your own.");
                            return false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (board[start[0], start[1]] != 'O')
                        {
                            Console.WriteLine("You can only move pieces of your own.");
                            return false;
                        }
                        break;
                    }
            }

            return true;
        }
    }
}
