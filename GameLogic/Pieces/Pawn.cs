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
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
        }

        // Метод проверяет, может ли фигура сходить в данную позицию, то есть пуста эта позиция и лежит ли она в пределах доски
        private static bool CanMoveTo(Position pos, Board board)
        {
        }

        // Метод по сути так же проверяет, может ли фигура сходить на эту позицию, и если не может, то он возвращает false(не может сходить)
        // В противном случае смотрит, какого цвета фигура впереди и возвращает результат сравнения
        private bool CanCaptureAt(Position pos, Board board)
        {
        }

        /*
         * Метод отвечает за движение перёд. Если фигура может сходить, то метод возвращает один ход на одну клетку вперёд,
         * если пешка ходит на последнюю клетку, то происходит преобразование пешку в более сильную фигуру
         * если фигура ранее не ходила, и может сделать ещё один ход, то возвращается ещё один ход сразу же следом.
         */
        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
        }

        /*
         * Меьлж перебирает циклом два диагональных направления (вернее он рассматривает клетку слева и клетку справа
         * относительно клетки перед фигурой), смотрит, может ли фигура съесть ту фигуру, которая будет стоять
         * слева или справа и возвращает эти позиции как потенциальные ходы,
         * а также если пешка переходит в последнюю строку, то происходит преобразование пешки в более сильную фигуру
         */
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
        }

        // Метод реализации хода, передаёт в метод получения ходов все найденные ранее ходы + возможности есть по диагоналям,
        // если таковые имеются
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
        }
    }
}
