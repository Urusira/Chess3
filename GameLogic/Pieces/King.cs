namespace GameLogic
{
    public class King : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        //  Чтобы задать свойство цвета используется конструктор
        public King(Player color)
        {
            Color = color;
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
