/**
  @file Rook.cs
  @brief Класс фигуры - ладьи
  @author Листопад В.
\par Наследует класс:
  @ref Piece
\par Использует классы:
- @ref Player
- @ref PieceType
- @ref Direction
- @ref Board
- @ref Position
- @ref Piece
\par Содержит класс:
  @ref Rook
*/

namespace GameLogic
{
    public class Rook : Piece
    {
        ///  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        /// Массив направлений, по которым может двигаться ладья
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West
        };

        ///  Чтобы задать свойство цвета используется конструктор
        public Rook(Player color)
        {
            Color = color;
        }

        ///  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        /** Здесь перезаписываем метод получения возможных ходов, на вход даём позицию откуда надо сходить и текущую доску.
            Он собственно реализует алгоритм из основного класса peaces, перебирая ходы по направлениям, заданным в самом верху
            этого файла, там собственно прописаны все 4 диагональных направления
        */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
