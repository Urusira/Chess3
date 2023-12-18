namespace GameLogic
{
    /*  Этот класс будет удобен, так как фигуры движутся в определённых направлениях. Мы просто будем говорить фигурам
     "Иди на N клеток на НАЗВАНИЕ НАПРАВЛЕНИЯ (напр. Север, Запал, Юго-Восток)  */
    public class Direction
    {
        // Здесь задаём направления для движения по горизонтали, вертикали и диагоналям
        public readonly static Direction North = new Direction(-1, 0);
        public readonly static Direction South = new Direction(1, 0);
        public readonly static Direction West = new Direction(0, -1);
        public readonly static Direction East = new Direction(0, 1);
        public readonly static Direction NortWest = new Direction(-1, -1);
        public readonly static Direction NortEast = new Direction(-1, 1);
        public readonly static Direction SouthWest = new Direction(1, -1);
        public readonly static Direction SouthEast = new Direction(1, 1);

        // Представим направления как дельта строки и дельта столбца. По факту это означает изменение столбца и строки
        // по сравнению с заданным значением
        public int RowDelta { get; }
        public int ColumnDelta { get; }

        // Конструктор класса, инициализирует значения и записывает их в свойствах
        public Direction(int rowDelta, int columnDelta)
        {
        }
        
        // Переопределяем оператор умножения, чтобы можно было с его помощью масштабировать направления
        // Вроде бы это ещё называют перегруженными операторами...
        public static Direction operator *(int scalar, Direction dir)
        {
        }
    }
}
