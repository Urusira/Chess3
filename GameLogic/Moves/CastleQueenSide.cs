/**
  @file CastleQueenSide.cs
  @page CastleQueenSide
  @brief Класс хода - рокировки со стороны королевы
  @author Шабанов М.
\par Наследует класс:
  @ref Move
\par Использует классы:
- @ref Position
- @ref MoveType
- @ref NormalMove
- @ref Board
\par Содержит класс:
  @ref CastleQueenSide
*/

namespace GameLogic
{
    /// Класс рокировки со стороны королевы
    public class CastleQueenSide : Move
    {
        public override MoveType Type => MoveType.CastleQueenSide;   ///< Определяем тип хода
        public override Position FromPos { get; set; }   ///< Откуда ходит фигура
        public override Position ToPos { get; set; }   ///< Куда ходит фигура
        public override string kex { get; }     ///< Уникальный идентификатор хода

        private readonly Position rookFromPos;   ///< Откуда ходит ладья
        private readonly Position rookToPos;   ///< Куда ходит ладья

        /// Конструктор
        /** Создаёт экземпляр хода типа "Рокировка со стороны короля"
        \param kingPos Позиция короля
        */
        public CastleQueenSide(Position kingPos)
        {
            FromPos = kingPos;
            ToPos = new Position(kingPos.Row, 2);
            rookFromPos = new Position(kingPos.Row, 0);
            rookToPos = new Position(kingPos.Row, 3);
            kex = Guid.NewGuid().ToString();
        }

        /// Переопределённый метод запуска хода
        /** Запускает сконструированный ход
        \param board Текущее игровое поле
        */
        public override void Execute(Board board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);
        }

        /// Переопределённый метод запуска обратного хода
        /** Отменяет совершённый ход данного типа
        \param board Текущее игровое поле
        */
        public override void ReverseExecute(Board board) 
        {
            board[rookFromPos] = board[rookToPos];
            board[rookFromPos].HasMoved = false;
            board[rookToPos] = null;
            board[FromPos] = board[ToPos];
            board[FromPos].HasMoved = false;
            board[ToPos] = null;
        }
    }
}
