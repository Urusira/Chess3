/**
  @file EndReason.cs
  @page EndReason
  @brief Перечисление причин окончания матча
  @author Тюканов В.
*/

namespace GameLogic
{
    /**
        @brief Причины завершения игры
    */
    public enum EndReason
    {
        Checkmate,              /**< Мат */
        Stalemate,              /**< Пат */
        FiftyMoveRule,          /**< Правило пятидесяти ходов */
        InsufficientMetrial,    /**< Недостаточно фигур */
        ThreefoldRepetition     /**< Тройное повторение */
    }
}
