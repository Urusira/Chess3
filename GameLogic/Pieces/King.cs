namespace GameLogic
{
    public class King : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }


        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NortWest,
            Direction.NortEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        //  Чтобы задать свойство цвета используется конструктор
        public King(Player color)
        {
            Color = color;
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        /*
         * Этот метод просто перебирает через цикл foreach все направления, которые прописаны вначале файла
         * и заносит в to все возможные ходы по этим направлениям (все клетки вокруг фигуры) и проверяет,
         * находится эта клетка внутри доски и находится ли в ней фигура противоположного цвета.
         */
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;

                if (Board.IsInside(to) && (board.IsEmpty(to) || board[to].Color != Color))
                {
                    yield return to;
                }
            }
        }

        /*
         * В цикле foreach перебираем все позицим и создаём возможные ходы по полученным координатам.
         * Цикл этот используется для того, чтобы получать возможные ходы, заносить их в переменную to
         * и создавать с ней новые экземпляры ходов.
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }
            if (board[from].HasMoved == false && board[from.Row, 7].HasMoved == false)
            {
                Position pos1 = new Position(from.Row, 5);
                Position pos2 = new Position(from.Row, 6);
                Player player = board[from].Color;
                if (board.IsEmpty(pos1) && board.IsEmpty(pos2) && !board.IsInCheck(player))
                {
                    yield return new CastleKingSide(from);
                }
            }
            if (board[from].HasMoved == false && board[from.Row, 0].HasMoved == false)
            {
                Position pos1 = new Position(from.Row, 1);
                Position pos2 = new Position(from.Row, 2);
                Position pos3 = new Position(from.Row, 3);
                Player player = board[from].Color;
                if (board.IsEmpty(pos1) && board.IsEmpty(pos2) && board.IsEmpty(pos3) && !board.IsInCheck(player))
                {
                    yield return new CastleQueenSide(from);
                }
            }
            
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
