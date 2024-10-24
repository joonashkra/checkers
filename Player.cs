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
        private readonly int playerNum;

        public Player(int playerNum)
        {
            this.playerNum = playerNum;
        }

        public int PlayerNum
        {
            get { return playerNum; }
        }

        public int Move(CheckerBoard board, int[] startCoords, int[] endCoords)
        {
            board.UpdateBoard(playerNum, startCoords, endCoords);
            return 0;
        }
    }
}
