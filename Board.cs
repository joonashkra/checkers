using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            char startChar = board[startCoords[0], startCoords[1]];

            if (playerNum == 1)
                board[endCoords[0], endCoords[1]] = 'X';
            else
                board[endCoords[0], endCoords[1]] = 'O';

            if (IsCaptureAttempt(playerNum, startCoords, endCoords))
            {
                int capturedRow = (startCoords[0] + endCoords[0]) / 2;
                int capturedCol = (startCoords[1] + endCoords[1]) / 2;
                board[capturedRow, capturedCol] = '#';
            }

            if (startChar == 'K') board[endCoords[0], endCoords[1]] = 'K';
            if (startChar == 'Q') board[endCoords[0], endCoords[1]] = 'Q';

            board[startCoords[0], startCoords[1]] = '#';

            PromotePawn();

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
                    if (!IsCaptureAttempt(playerNum, startCoords, endCoords))
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
                    if (!IsCaptureAttempt(playerNum, startCoords, endCoords))
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

            if (CheckCaptures(playerNum) && !IsCaptureAttempt(playerNum, startCoords, endCoords))
            {
                return false;
            }

            return true;
        }

        private bool IsCaptureAttempt(int playerNum, int[] startCoords, int[] endCoords)
        {
            char opponentPawn = playerNum == 1 ? 'O' : 'X';
            char promotedOpponentPawn = playerNum == 1 ? 'Q' : 'K';

            char[] opponentPawns = { opponentPawn, promotedOpponentPawn };

            if (Math.Abs(startCoords[0] - endCoords[0]) != 2 || Math.Abs(startCoords[1] - endCoords[1]) != 2)
                return false;

            int capturedRow = (startCoords[0] + endCoords[0]) / 2;
            int capturedCol = (startCoords[1] + endCoords[1]) / 2;

            if (IsOutOfBounds(new int[] { capturedRow, capturedCol }))
                return false;

            if (!opponentPawns.Contains(board[capturedRow, capturedCol]))
                return false;

            return true;
        }

        public bool CheckCaptures(int playerNum)
        {
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
                    if (playerPawns.Contains(board[i, j]))
                    {
                        bool legalCapture = false;

                        if (board[i, j] == playerPawn)
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
                        else if (board[i, j] == promotedPlayerPawn)
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

        private static bool IsOutOfBounds(int[] coords)
        {
            return coords[0] < 0 || coords[0] >= 8 || coords[1] < 0 || coords[1] >= 8;
        }

        public int? GetWinner()
        {
            List<char> pawns = new();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    pawns.Add(board[i, j]);
                }
            }

            if (pawns.Contains('X') && !pawns.Contains('O'))
                return 1;
            else if (pawns.Contains('O') && !pawns.Contains('X'))
                return 2;
            else
                return null;
        }

        public void PromotePawn()
        {
            for (int i = 0; i < 8; i++)
            {
                if (board[0, i] == 'X')
                {
                    board[0, i] = 'K';
                    Console.WriteLine("Player 1 pawn promoted to king!");
                }
                if (board[7, i] == 'O')
                {
                    board[7, i] = 'Q';
                    Console.WriteLine("Player 2 pawn promoted to queen!");
                }
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
