using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    // Рокировка со стороны короля
    public class CastleKingSide : Move
    {
        public override MoveType Type => MoveType.CastleKingSide;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set; }
        public override string kex { get; }

        private readonly Position rookFromPos;
        private readonly Position rookToPos;

        public CastleKingSide(Position kingPos)
        {
            FromPos = kingPos;
            ToPos = new Position(kingPos.Row, 6);
            rookFromPos = new Position(kingPos.Row, 7);
            rookToPos = new Position(kingPos.Row, 5);
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
