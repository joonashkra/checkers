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

        public Player(int playerNum)
        {
            this.playerNum = playerNum;
        }

        public int PlayerNum
        {
            get { return playerNum; }
        }

        public static int Move(Board board, Player player, int[] startCoords, int[] endCoords)
        {
            bool validMove = ValidateMove(board, player.PlayerNum, startCoords, endCoords);

            if (!validMove) return -1;

            board.UpdateBoard(player, startCoords, endCoords);
            return 0;
        }

        private static bool ValidateMove(Board board, int playerNum, int[] startCoords, int[] endCoords)
        {
            char[,] boardArray = board.BoardArray;

            char startChar = boardArray[startCoords[0], startCoords[1]];
            char endChar = boardArray[endCoords[0], endCoords[1]];

            if (startChar == '#')
            {
                Console.WriteLine("This piece does not exist.");
                return false;
            }

            switch (playerNum)
            {
                case 1:
                    {
                        if (startChar != 'X')
                        {
                            Console.WriteLine("You can only move pieces of your own.");
                            return false;
                        }

                        if (startCoords[0] - 1 != endCoords[0] && !Board.IsCapture(startCoords, endCoords))
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

                        if (startCoords[0] + 1 != endCoords[0] && !Board.IsCapture(startCoords, endCoords))
                        {
                            Console.WriteLine("You can only move one step forward from here.");
                            return false;
                        }
                        break;
                    }
            }

            if (Math.Abs(endCoords[1] - startCoords[1]) != 1 && !Board.IsCapture(startCoords, endCoords))
            {
                Console.WriteLine("You can only move one one step diagonally.");
                return false;
            }

            if (endChar != '#')
            {
                Console.WriteLine("This position is already taken.");
                return false;
            }

            return true;
        }
    }
}
