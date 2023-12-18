namespace GameLogic
{
    public class Position
    {
        // Будем исходить из того, что верхняя-левая клетка - позиция (0;0) и квадраты нумеруются сверху вниз; слева направо.
        // Т.е. 0,0 - верхний левый угол, 7,7 нижний правый угол, соответственно 7,0 и 0,7 нижний левый и правый верхний углы
        public int Row { get; }
        public int Column { get; }

        // Конструктор, принимающий строку и столбец и сохраняющий их в свойствах
        public Position(int row, int column)
        {
        }

        // Получаем хэш-код, чтобы класс позиции можно было использовать в качестве ключа в словаре, весьма удобно.
        // Получать его позволяет встроенная функция в вижуале, вызывается сочетанием клавиш "ctrl + ." и выбором соответствующего пункта
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
        }

        public static bool operator !=(Position left, Position right)
        {
        }

        // Перегрузка оператора +, позволяет добавлять к текущей позиции по вертикали и горизонтали разницу,
        // между ней и новой позицией
        public static Position operator +(Position pos, Direction dir)
        {
        }
    }
}
