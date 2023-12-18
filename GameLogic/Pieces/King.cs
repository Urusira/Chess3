namespace GameLogic
{
    public class King : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }


        private static readonly Direction[] dirs = new Direction[]
        {
        };

        //  Чтобы задать свойство цвета используется конструктор
        public King(Player color)
        {
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
        }

        /*
         * Этот метод просто перебирает через цикл foreach все направления, которые прописаны вначале файла
         * и заносит в to все возможные ходы по этим направлениям (все клетки вокруг фигуры) и проверяет,
         * находится эта клетка внутри доски и находится ли в ней фигура противоположного цвета.
         */
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
        }

        /*
         * В цикле foreach перебираем все позицим и создаём возможные ходы по полученным координатам.
         * Цикл этот используется для того, чтобы получать возможные ходы, заносить их в переменную to
         * и создавать с ней новые экземпляры ходов.
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
        }
    }
}
