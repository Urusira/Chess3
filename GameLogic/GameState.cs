using System.Drawing;

namespace GameLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }

        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }

        /*
         * В этом методе мы получаем все возможные ходы для заданной позиции. В случае, если идёт попытка
         * рассчёта для пустой клетки или фигуры цвета, не соответствующего текущему игроку, метод
         * ничего не делает, выдавая пустоту.
         * Если же всё в порядке, то метод получает фигуру в заданной позиции и вызывает у неё метод получения всех ходов.
         */
        public IEnumerable<Move> LegalMoves(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.swap();
        }
        public void MakeReverseMove(Move move)
        {
            move.ReverseExecute(Board);
            CurrentPlayer = CurrentPlayer.swap();
        }
    }
}
