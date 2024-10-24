using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
    {
        class CheckerBoard
        {
            private readonly char[,] board = new char[8, 8];

            public char[,] Board
            {
                get { return board; }
            }

            public void InitBoard()
            {
                for (int i = 0; i < 8; i++)
                {
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
                    }
                }
            }

            public void UpdateBoard(int playerNum, int[] startCoords, int[] endCoords)
            {
                char startChar = board[startCoords[0], startCoords[1]];

                if (playerNum == 1)
                    board[endCoords[0], endCoords[1]] = 'X';
                else
                    board[endCoords[0], endCoords[1]] = 'O';

                HandleCaptures(playerNum, startCoords, endCoords);

                if (startChar == 'K') board[endCoords[0], endCoords[1]] = 'K';
                if (startChar == 'Q') board[endCoords[0], endCoords[1]] = 'Q';

                board[startCoords[0], startCoords[1]] = '#';

                HandlePromotions();
            }

            public char GetPawn(int[] coords)
            {
                if (Validator.IsOutOfBounds(coords))
                    throw new ArgumentOutOfRangeException("Coordinates are out of bounds.");
                return board[coords[0], coords[1]];
            }

            public void SetPawn(int[] coords, char pawn)
            {
                if (Validator.IsOutOfBounds(coords))
                    throw new ArgumentOutOfRangeException("Coordinates are out of bounds.");
                board[coords[0], coords[1]] = pawn;
            }

            public bool HandleCaptures(int playerNum, int[] startCoords, int[] endCoords)
            {
                char opponentPawn = playerNum == 1 ? 'O' : 'X';
                char promotedOpponentPawn = playerNum == 1 ? 'Q' : 'K';

                char[] opponentPawns = { opponentPawn, promotedOpponentPawn };

                int capturedRow = (startCoords[0] + endCoords[0]) / 2;
                int capturedCol = (startCoords[1] + endCoords[1]) / 2;

                if (Validator.IsOutOfBounds(new int[] { capturedRow, capturedCol }))
                    return false;

                if (Math.Abs(startCoords[0] - endCoords[0]) != 2 || Math.Abs(startCoords[1] - endCoords[1]) != 2)
                    return false;

                if (!opponentPawns.Contains(board[capturedRow, capturedCol]))
                    return false;

                board[capturedRow, capturedCol] = '#';
                return true;
            }

            private void HandlePromotions()
            {
                for (int i = 0; i < 8; i++)
                {
                    if (board[0, i] == 'X')
                    {
                        board[0, i] = 'K';
                    }
                    if (board[7, i] == 'O')
                    {
                        board[7, i] = 'Q';
                    }
                }
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
        }
    }
