using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    // Рокировка со стороны королевы
    public class CastleQueenSide : Move
    {
        public override MoveType Type => MoveType.CastleQueenSide;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set; }
        public override string kex { get; }

        private readonly Position rookFromPos;
        private readonly Position rookToPos;

        public CastleQueenSide(Position kingPos)
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
