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
        public Piece piece;
        // Тип новой фигуры
        private readonly PieceType newType;
        
        public PawnPromiton(Position from, Position to, PieceType newType)
        {
        }
        public override void Execute(Board board)
        {
        }
        public override void ReverseExecute(Board board)
        {
        }
    }
}
