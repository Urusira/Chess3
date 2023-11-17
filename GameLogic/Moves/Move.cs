using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    /*
     * Абстрактный класс для движения, будет содержать базовые характеристики для каждого из видов движения, актуальные
     * для каждой фигуры. Во первых это тип движения, во вторых две позиции: откуда ходим и куда ходим, а так же 
     * метод, который позволит реализовать ход, экзекьютнуть. Он будет выполнять сам себя на текущей доске
     */
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }


        public abstract void Execute(Board board);
    }
}
