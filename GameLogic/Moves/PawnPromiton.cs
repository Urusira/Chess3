using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class PawnPromiton : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set; }
        public override string kex { get; }
        public override void ReverseExecute(Board board) { }
        private readonly PieceType newType;
        
        public PawnPromiton(Position from, Position to, PieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;
            kex = Guid.NewGuid().ToString();
        }
        
        /*
        private Piece CreatePromotionPiece(Player color)
        {
            return newType switch
            {
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Rook => new Rook(color),
                PieceType.Queen => new Queen(color)
            };
        }
        */

        public override void Execute(Board board)
        {
            Player Cur = board[FromPos].Color;
            board[FromPos] = null;
            //board[ToPos] = CreatePromotionPiece(Cur);
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
    }
}
