namespace GameLogic
{
    public class Rook : Piece
    {
        //  Здесь перезаписываем свойства класса, которые были наследованы от Piece
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        //  Чтобы задать свойство цвета используется конструктор
        public Rook(Player color)
        {
            Color = color;
        }

        //  Далее применяем метод Copy, в нём создаётся новый экземпляр фигуры с заданными параметрами
        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
