namespace GameLogic
{
    public class Bishop : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        // Массив направлений, по которым может двигаться слон
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NortWest,
            Direction.NortEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        //  Чтобы задать свойство цвета используется конструктор
        public Bishop(Player color)
        {
            Color = color;
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        /*
         * Здесь перезаписываем метод получения возможных ходов, на вход даём позицию откуда надо сходить и текущую доску.
         * Он собственно реализует алгоритм из основного класса peaces, перебирая ходы по направлениям, заданным в самом верху
         * этого файла, там собственно прописаны все 4 диагональных направления
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
