using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    //  Абстрактный класс, который будут наследовать ВСЕ фигуры, он имеет свойства, вроде типа и цвета, которые
    //  будут переопределять все фигуры уже внутри своих классов
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMovet { get; set; } = false;

        public abstract Piece Copy();
    }
}
