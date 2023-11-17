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
            Row = row;
            Column = column;
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
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        // Перегрузка оператора +, позволяет добавлять к текущей позиции по вертикали и горизонтали разницу,
        // между ней и новой позицией
        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }

        /* Если по простому, то теперь мы можем написать выражение типа:
        
        Position from = new Position(0, 4);
        Position to = from + 3 * Direction.SouthEast;

        Такое выражение позволяет переместить фигуру из позиции 0 по вертикали и 4 по горизонтали
        на 3 клетки в направлении юго-востока, то есть сходить по диагонали вправо-вниз на 3 клетки  
         */
    }
}
