using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class NormalMove : Move
    {
        // Перезаписываем все характеристики наследованного класса
        public override MoveType Type => MoveType.Normal;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set; }

        // В конструкторе запиываем полученные позиции начальной и конечной позиций хода
        public NormalMove(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
        }

        /*
         * Метод реализации хода, он выполняет сам ход, перемещает фигуру из начальной позиции
         * в конечную, после чего стирает содержимое начальной позиции и сообщает, что фигура была перемещена
         */
        public override void Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;
        }
    }
}
