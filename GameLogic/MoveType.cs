/**
  @file MoveType.cs
  @page MoveType
  @brief Перечисление типов ходов
  @author Листопад В.
*/

namespace GameLogic
{
    /**
        @brief Типы ходов
    */
    public enum MoveType
    {
        Normal,             /**< Обычный ход */
        CastleKingSide,     /**< Рокировка со стороны короля */
        CastleQueenSide,    /**< Рокировка со стороны королевы */
        PawnPromotion       /**< Повышение пешки */
    }
}
