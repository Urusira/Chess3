/**
  @file Player.cs
  @page Player
  @brief Класс игрока
  @author Листопад В., Шабанов М.
\par Содержит класс:
  @ref PlayerExtensions
*/

namespace GameLogic
{
    /**
        @brief Типы игроков
    */
    public enum Player
    {
        None,   /**< Отсутсвует */
        White,  /**< Белый */
        Black   /**< Чёрный */
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
