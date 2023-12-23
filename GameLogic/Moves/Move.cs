/**
  @file Move.cs
  @brief Абстрактный класс хода, определяет шаблон хода для всех его видов
  @author Листопад В., Тюканов В.
\par Наследуется классами:
  @ref CastleKingSide
  @ref CastleQueenSide
  @ref NormalMove
  @ref PawnPromotion
\par Использует классы:
- @ref Position
- @ref MoveType
- @ref NormalMove
- @ref Board
\par Содержит класс:
  @ref Move
*/


namespace GameLogic
{
    /** Абстрактный класс для движения, будет содержать базовые характеристики для каждого из видов движения, актуальные
        для каждой фигуры. Во первых это тип движения, во вторых две позиции: откуда ходим и куда ходим, а так же 
        метод, который позволит реализовать ход, экзекьютнуть. Он будет выполнять сам себя на текущей доске
    */
    public abstract class Move
    {
        public abstract MoveType Type { get; }   ///< Определяем тип хода
        public abstract Position FromPos { get; set; }   ///< Откуда ходит фигура
        public abstract Position ToPos { get; set; }   ///< Куда ходит фигура
        public abstract string kex { get; }     ///< Уникальный идентификатор хода


        /// Метод запуска хода
        /** Запускает сконструированный ход
        \param board Текущее игровое поле
        */
        public abstract void Execute(Board board);

        /// Метод запуска обратного хода
        /** Отменяет совершённый ход данного типа
        \param board Текущее игровое поле
        */
        public abstract void ReverseExecute(Board board);

        /// Метод, определяющий легальность хода
        /** Создаёт копию доски, выполняет на ней ход и смотрит, будет ли король под атакой (шахом)
        \param board Текущее игровое поле
        \return Логическое значение true или false - показывающее, приведёт ли ход к шаху
        */
        public virtual bool IsLegal(Board board)
        {
            Player player = board[FromPos].Color;
            Board boardCopy = board.Copy();
            Execute(boardCopy);
            return !boardCopy.IsInCheck(player);
        }
    }
}
