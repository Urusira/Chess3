namespace GameLogic
{
    public class Knight : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        //  Чтобы задать свойство цвета используется конструктор
        public Knight(Player color)
        {
            Color = color;
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        /*
         * Метод вычисляет пары позиций. Он берёт две пары по вертикали и две пары по горизонтали.
         * В теле метода прибавляется 2, так как конь перескакивает две клетки. Это позволяет
         * получить все 8 позиций для коня.
         */
        private static IEnumerable<Position> PotentialToPositions(Position from)
        {
            foreach (Direction vDir in new Direction[] {Direction.North, Direction.South})
            {
                foreach (Direction hDir in new Direction[] {Direction.West, Direction.East})
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + 2 * hDir + vDir;
                }
            }
        }

        /*
         * Этот метод перебирает все полученные потенциальные ходы из текущей позиции и возвращает только те, которые
         * находятся в пределах доски и не содержат союзных фигур
         */
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            return PotentialToPositions(from).Where(pos => Board.IsInside(pos) && (board.IsEmpty(pos) || board[pos].Color != Color));
        }

        /*
         * В этом методе перебираются все полученные ранее позиции для текущей позиции
         * А select выбирает эти позиции, заносит их в качестве to и создаёт движения с данными
         * координатами
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositions(from, board).Select(to => new NormalMove(from, to));
        }
    }
}
