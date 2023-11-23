using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public enum Player
    {
        None,
        White,
        Black
    }

    public static class PlayerExtensions
    {
        public static Player swap(this Player Current_player)
        {
            switch (Current_player)
            {
                case Player.White: return Player.Black;
                case Player.Black: return Player.White;
                default: return Current_player;
            }
        }
    }
}
