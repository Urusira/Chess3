using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public override string kex { get; }

        public Piece EatenPiece = null;

        // В конструкторе запиываем полученные позиции начальной и конечной позиций хода
        public NormalMove(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
            kex = Guid.NewGuid().ToString();
        }

        /*
         * Метод реализации хода, он выполняет сам ход, перемещает фигуру из начальной позиции
         * в конечную, после чего стирает содержимое начальной позиции и сообщает, что фигура была перемещена
         */
        public override void Execute(Board board)
        {
            Piece piece = board[FromPos];
            Piece eaten = board[ToPos];
            if (board[ToPos] != null)
            {
                EatenPiece = eaten;
            }
            else
            {
                EatenPiece = null;
            }
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;
        }
        /*
         * Метод отмены хода, он обратен соврешённому ходу. Он помещает на координаты куда ходила фигура ту, что была съедена
         * и возвращает сходившую фигуру на её предыдущую позицию
         */
        public override void ReverseExecute(Board board)
        {
            Piece piece = board[ToPos];
            board[FromPos] = piece;
            board[ToPos] = EatenPiece;
        }
    }
}
