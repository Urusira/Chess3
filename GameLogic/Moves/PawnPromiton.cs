using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    // Преборазование пешки в более сильную фигуру
    public class PawnPromiton : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set; }
        public override string kex { get; }

        public Piece EatenPiece = null;
        // Тип новой фигуры
        private readonly PieceType newType;
        
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
            board[FromPos] = board[ToPos];
            board[ToPos] = EatenPiece;
        }
    }
}
