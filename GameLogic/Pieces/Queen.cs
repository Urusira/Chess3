namespace GameLogic
{
    public class Queen : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; }

        // Массив направлений, по которым может двигаться ферзь
        private static readonly Direction[] dirs = new Direction[]
        {
        };

        //  Чтобы задать свойство цвета используется конструктор
        public Queen(Player color)
        {
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
        }

        /*
         * Здесь перезаписываем метод получения возможных ходов, на вход даём позицию откуда надо сходить и текущую доску.
         * Он собственно реализует алгоритм из основного класса peaces, перебирая ходы по направлениям, заданным в самом верху
         * этого файла, там собственно прописаны все 4 диагональных направления
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
        }
    }
}
