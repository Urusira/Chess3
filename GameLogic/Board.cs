namespace GameLogic
{
    //  Этот класс хранит фигуры, находящиеся на доске. В нашем случае удобнее всего использовать для хранения
    //  двумерный массив. Так как он закрытый, воспользуемся так называемым индексатором
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
        }

        //  Этот индексатор по сути просто принимает позицию, "распаковвывает" её и вызывает первый индексатор
        public Piece this[Position pos]
        {
        }

        //  Статический метод, он инициализирует доску (создаёт и выставляет фигуры на исходные позиции)
        public static Board Initial()
        {
        }

        //  Метод для создания новых фигур на стартовых позициях на доске
        private void AddStartPieces()
        {
        }

        //  Вспомогательный метод, позволяет понять, лежит данная позиция внутри доски или выходит за его пределы
        public static bool IsInside(Position pos)
        {
        }

        //  Так же вспомогательный метод. Проверяет, пуста текущая позиция или нет
        public bool IsEmpty(Position pos)
        {
        }

        public IEnumerable<Position> PiecePositions()
        {
        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
        }

        public bool IsInCheck(Player player)
        {
        }

        public Board Copy()
        {
        }
    }
}
