/**
  @file GameState.cs
  @page GameState
  @brief Класс игрового состояния
  @author Листопад В., Тюканов В.
\par Использует классы:
- @ref Result
- @ref Player
- @ref Board
- @ref Position
- @ref Move
\par Содержит класс:
  @ref GameState
*/

namespace GameLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;
        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }

        /** В этом методе мы получаем все возможные ходы для заданной позиции. В случае, если идёт попытка
            рассчёта для пустой клетки или фигуры цвета, не соответствующего текущему игроку, метод
            ничего не делает, выдавая пустоту.
            Если же всё в порядке, то метод получает фигуру в заданной позиции и вызывает у неё метод получения всех ходов.
        */
        public IEnumerable<Move> LegalMoves(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.swap();
            CheckForGameOver();
        }
        public void MakeReverseMove(Move move)
        {
            move.ReverseExecute(Board);
            CurrentPlayer = CurrentPlayer.swap();
        }

        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        void CheckForGameOver()
        {
            if(!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if(Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.swap()); 
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }
    }
}
