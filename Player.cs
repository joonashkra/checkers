using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void Move(Board board, Player player, string startPosition, string endPosition)
        {
            board.UpdateBoard(player, startPosition, endPosition);
        }
    }
}
