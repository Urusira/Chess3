/**
  @file Piece.cs
  @brief Абстрактный класс фигуры
  @author Листопад В., Тюканов В.
\par Использует классы:
- @ref Player
- @ref PieceType
- @ref Direction
- @ref Board
- @ref Position
- @ref Piece
\par Наследуется классами:
- @ref Bishop
- @ref King
- @ref Knight
- @ref Pawn
- @ref Queen
- @ref Rook
\par Содержит класс:
  @ref Piece
*/

namespace GameLogic
{
    /** Абстрактный класс, который будут наследовать ВСЕ фигуры, он имеет свойства, вроде типа и цвета, которые
        будут переопределять все фигуры уже внутри своих классов
    */
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;

        public abstract Piece Copy();

        /** Метод, принимающий текущую позицию и доску в качестве параметров, он будет возвращать
            коллекцию всех возможных для фигуры ходов из текущей позиции. Уже впоследствии из этой
            коллекции мы будем выбирать желаемый ход, соотнося позицию клика мыши с этой коллекцией
            и посылать эти данные в Execute, чтобы переместить эту фигуру на выбранную позицию
        */ 
        public abstract IEnumerable<Move> GetMoves(Position from, Board board);

        /** Ещё одно гениальное решение. Сюда подаются текущая позиция, доска и направление, в котором мы грубо говоря
            пускаем луч, который идёт до тех пор, пока не натолкнётся на фигуру либо край доски. Если фигура
            противоположного текущей цвета, то луч останавливается на ней. В противном случае делает шаг назад и останавливается.
            Всё это позволяет получить все возможные ходы в одном конкретном направлении, очень полезно для
            слона, ладьи и королевы
        */
        protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir)
        {
            /** В цикле мы как бы идём в заданном направлении по одной клетке за раз до тех пор,
                пока не натолкнёмся на границу поля
            */
            for (Position pos = from + dir; Board.IsInside(pos); pos += dir)
            {
                /// Если текущая позиция пуста, то принимаем её и двигаемся дальше
                if (board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }

                /// В случае нахождения фигуры, мы получаем её
                Piece piece = board[pos];

                /// Если её цвет не равен цвету текущей фигуры, то возвращаем эту позицию
                if (piece.Color != Color)
                {
                    yield return pos;
                }

                /// И досрочно завершаем цикл
                yield break;
            }
        }

        /** Здесь мы просто выбираем несколько коллекций, которые собирает предыдущий метод, на вход
            посылаем несколько направлений сразу и для каждого из них у нас вызывается предыдущий метод
        */
        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
        }

        public virtual bool CanCaptureOpponentKing(Position from, Board board)
        {
            return GetMoves(from, board).Any(move =>
            {
                Piece piece = board[move.ToPos];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
