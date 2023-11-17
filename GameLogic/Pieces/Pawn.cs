using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Pawn : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly Direction forward;

        //  Чтобы задать свойство цвета используется конструктор
        public Pawn(Player color)
        {
            Color = color;

            if (color == Player.White)
            {
                forward = Direction.North;
            }
            else if (color == Player.Black)
            {
                forward = Direction.South;
            }
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        // Метод проверяет, может ли фигура сходить в данную позицию, то есть пуста эта позиция и лежит ли она в пределах доски
        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmpty(pos);
        }

        // Метод по сути так же проверяет, может ли фигура сходить на эту позицию, и если не может, то он возвращает false(не может сходить)
        // В противном случае смотрит, какого цвета фигура впереди и возвращает результат сравнения
        private bool CanCaptureAt(Position pos, Board board)
        {
            if (!Board.IsInside(pos) || board.IsEmpty(pos))
            {
                return false;
            }

            return board[pos].Color != Color;
        }

        /*
         * Метод отвечает за движение перёд. Если фигура может сходить, то метод возвращает один ход на одну клетку вперёд,
         * если фигура ранее не ходила, и может сделать ещё один ход, то возвращается ещё один ход сразу же следом.
         */
        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            Position oneMovePos = from + forward;

            if (CanMoveTo(oneMovePos, board))
            {
                yield return new NormalMove(from, oneMovePos);

                Position twoMovesPos = oneMovePos + forward;

                if (!HasMoved && CanMoveTo(twoMovesPos, board))
                {
                    yield return new NormalMove(from, twoMovesPos);
                }
            }
        }

        /*
         * Меьлж перебирает циклом два диагональных направления (вернее он рассматривает клетку слева и клетку справа
         * относительно клетки перед фигурой), смотрит, может ли фигура съесть ту фигуру, которая будет стоять
         * слева или справа и возвращает эти позиции как потенциальные ходы
         */
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach (Direction dir in new Direction[] {Direction.West, Direction.East})
            {
                Position to = from + forward + dir;

                if (CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }

        // Метод реализации хода, передаёт в метод получения ходов все найденные ранее ходы + возможности есть по диагоналям,
        // если таковые имеются
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }
    }
}
