using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class Validator
    {
        private readonly CheckerBoard checkerBoard;

        public Validator(CheckerBoard board)
        {
            this.checkerBoard = board;
        }

        public bool ValidateMove(int playerNum, int[] startCoords, int[] endCoords)
        {
            char startChar = checkerBoard.GetPawn(startCoords);
            char endChar = checkerBoard.GetPawn(endCoords);

            bool captureAttempt = checkerBoard.HandleCaptures(playerNum, startCoords, endCoords);
            bool possibleCaptures = CheckCaptures(playerNum);

            if (IsOutOfBounds(endCoords))
                return false;

            if (endChar != '#')
            {
                Console.WriteLine("This position is already taken.");
                return false;
            }

            if ((playerNum == 1 && startChar != 'X' && startChar != 'K') || (playerNum == 2 && startChar != 'O' && startChar != 'Q'))
            {
                Console.WriteLine("You can only move pieces of your own.");
                return false;
            }

            int rowsMoved = Math.Abs(endCoords[0] - startCoords[0]);
            int colsMoved = Math.Abs(endCoords[1] - startCoords[1]);

            if (startChar == 'X' || startChar == 'O')
            {
                if (rowsMoved == 1 && colsMoved == 1)
                {
                    if (startChar == 'X' && endCoords[0] >= startCoords[0])
                    {
                        Console.WriteLine("You can only move one step forward here.");
                        return false;
                    }
                    if (startChar == 'O' && endCoords[0] <= startCoords[0])
                    {
                        Console.WriteLine("You can only move one step forward here.");
                        return false;
                    }
                }
                else if (rowsMoved == 2 && colsMoved == 2)
                {
                    if (!captureAttempt)
                    {
                        Console.WriteLine("You can't capture here.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move.");
                    return false;
                }
            }

            if (startChar == 'K' || startChar == 'Q')
            {
                if (rowsMoved == 1 && colsMoved == 1)
                {
                    return true;
                }
                else if (rowsMoved == 2 && colsMoved == 2)
                {
                    if (!captureAttempt)
                    {
                        Console.WriteLine("You can't capture here.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move.");
                    return false;
                }
            }

            if (possibleCaptures && !captureAttempt)
            {
                return false;
            }

            return true;
        }

        public static bool IsOutOfBounds(int[] coords)
        {
            return coords[0] < 0 || coords[0] >= 8 || coords[1] < 0 || coords[1] >= 8;
        }

        public bool CheckCaptures(int playerNum)
        {
            char[,] board = checkerBoard.Board;

            char opponentPawn = playerNum == 1 ? 'O' : 'X';
            char playerPawn = playerNum == 1 ? 'X' : 'O';
            char promotedPlayerPawn = playerNum == 1 ? 'K' : 'Q';
            char promotedOpponentPawn = playerNum == 1 ? 'Q' : 'K';

            char[] opponentPawns = { opponentPawn, promotedOpponentPawn };
            char[] playerPawns = { playerPawn, promotedPlayerPawn };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    char currentPawn = board[i, j];

                    if (playerPawns.Contains(currentPawn))
                    {
                        bool legalCapture = false;

                        if (currentPawn == playerPawn)
                        {
                            if (playerNum == 1)
                            {
                                legalCapture = (i - 2 >= 0 && j + 2 < 8 && opponentPawns.Contains(board[i - 1, j + 1]) && board[i - 2, j + 2] == '#') ||
                                              (i - 2 >= 0 && j - 2 >= 0 && opponentPawns.Contains(board[i - 1, j - 1]) && board[i - 2, j - 2] == '#');
                            }
                            else
                            {
                                legalCapture = (i + 2 < 8 && j + 2 < 8 && opponentPawns.Contains(board[i + 1, j + 1]) && board[i + 2, j + 2] == '#') ||
                                              (i + 2 < 8 && j - 2 >= 0 && opponentPawns.Contains(board[i + 1, j - 1]) && board[i + 2, j - 2] == '#');
                            }
                        }
                        else if (currentPawn == promotedPlayerPawn)
                        {
                            legalCapture = (i + 2 < 8 && j + 2 < 8 && opponentPawns.Contains(board[i + 1, j + 1]) && board[i + 2, j + 2] == '#') ||
                                          (i + 2 < 8 && j - 2 >= 0 && opponentPawns.Contains(board[i + 1, j - 1]) && board[i + 2, j - 2] == '#') ||
                                          (i - 2 >= 0 && j + 2 < 8 && opponentPawns.Contains(board[i - 1, j + 1]) && board[i - 2, j + 2] == '#') ||
                                          (i - 2 >= 0 && j - 2 >= 0 && opponentPawns.Contains(board[i - 1, j - 1]) && board[i - 2, j - 2] == '#');
                        }

                        if (legalCapture)
                        {
                            int[] capturePosition = new int[] { i, j };
                            Console.WriteLine("Player {0} can capture from {1}. \n" +
                                "If you have the opportunity to capture your opponent's pawn, the rules state you must do it.\n", playerNum, Program.ParsePosition(capturePosition));
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
