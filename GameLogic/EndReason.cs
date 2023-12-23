/**
  @file EndReason.cs
  @brief Перечисление причин окончания матча
  @author Тюканов В.
*/

namespace GameLogic
{
    public enum EndReason
    {
        Checkmate,
        Stalemate,
        FiftyMoveRule,
        InsufficientMetrial,
        ThreefoldRepetition
    }
}
