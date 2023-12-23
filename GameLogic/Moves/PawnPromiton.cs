/**
  @file PawnPromiton.cs
  @brief Класс хода - повышения пешки
  @author Шабанов М.
\par Наследует класс:
  @ref Move
\par Использует классы:
- @ref Position
- @ref MoveType
- @ref NormalMove
- @ref Board
- @ref Piece
\par Содержит класс:
  @ref PawnPromiton
*/


namespace GameLogic
{
    /// Преборазование пешки в более сильную фигуру
    public class PawnPromiton : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set; }
        public override string kex { get; }

        public Piece EatenPiece = null;
        public Piece piece;
        
        private readonly PieceType newType; ///< Тип новой фигуры

        public PawnPromiton(Position from, Position to, PieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;
            kex = Guid.NewGuid().ToString();
        }
        public override void Execute(Board board)
        {
            Player Cur = board[FromPos].Color;
            board[FromPos] = null;
            EatenPiece = board[ToPos];
            switch (newType)
            {
                case PieceType.Knight:
                    board[ToPos] = new Knight(Cur);
                    break;
                case PieceType.Bishop:
                    board[ToPos] = new Bishop(Cur);
                    break;
                case PieceType.Rook:
                    board[ToPos] = new Rook(Cur);
                    break;
                default:
                    board[ToPos] = new Queen(Cur);
                    break;
            };
            board[ToPos].HasMoved = true;
        }
        public override void ReverseExecute(Board board)
        {
            board[FromPos] = new Pawn(board[ToPos].Color);
            board[ToPos] = EatenPiece;
        }
    }
}
