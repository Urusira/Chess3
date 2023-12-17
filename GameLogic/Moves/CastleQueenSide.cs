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
            FromPos = kingPos;
            ToPos = new Position(kingPos.Row, 2);
            rookFromPos = new Position(kingPos.Row, 0);
            rookToPos = new Position(kingPos.Row, 3);
            kex = Guid.NewGuid().ToString();
        }

        public override void Execute(Board board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);
        }

        public override void ReverseExecute(Board board) 
        {
            board[rookFromPos] = board[rookToPos];
            board[rookFromPos].HasMoved = false;
            board[rookToPos] = null;
            board[FromPos] = board[ToPos];
            board[FromPos].HasMoved = false;
            board[ToPos] = null;
        }
    }
}
