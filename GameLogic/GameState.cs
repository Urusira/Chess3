using System.Diagnostics.Tracing;
using System.Drawing;

namespace GameLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;
        public GameState(Player player, Board board)
        {
        }

        /*
         * В этом методе мы получаем все возможные ходы для заданной позиции. В случае, если идёт попытка
         * рассчёта для пустой клетки или фигуры цвета, не соответствующего текущему игроку, метод
         * ничего не делает, выдавая пустоту.
         * Если же всё в порядке, то метод получает фигуру в заданной позиции и вызывает у неё метод получения всех ходов.
         */
        public IEnumerable<Move> LegalMoves(Position pos)
        {
        }

        public void MakeMove(Move move)
        {
        }
        public void MakeReverseMove(Move move)
        {
        }

        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
        }

        void CheckForGameOver()
        {
        }

        public bool IsGameOver()
        {
        }
    }
}
