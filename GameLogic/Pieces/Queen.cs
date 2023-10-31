﻿namespace GameLogic
{
    public class Queen : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; }

        //  Чтобы задать свойство цвета используется конструктор
        public Queen(Player color)
        {
            Color = color;
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
