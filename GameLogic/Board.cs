/**
  @file Board.cs
  @brief Класс доски - игрового поля с фигурами
  @author Листопад В., Тюканов В.
\par Использует классы:
- @ref Piece
- @ref Player
- @ref Position
- @ref Bishop
- @ref King
- @ref Knight
- @ref Pawn
- @ref Queen
- @ref Rook
\par Содержит класс:
  @ref Board
*/

namespace GameLogic
{
    /** Этот класс хранит фигуры, находящиеся на доске. В нашем случае удобнее всего использовать для хранения
        двумерный массив. Так как он закрытый, воспользуемся так называемым индексатором
    */
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        ///  Этот индексатор по сути просто принимает позицию, "распаковвывает" её и вызывает первый индексатор
        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        ///  Статический метод, он инициализирует доску (создаёт и выставляет фигуры на исходные позиции)
        public static Board Initial()
        {
            Board board = new Board();  /// Создаём новую доску
            board.AddStartPieces(); ///  Вызываем метод добавления фигур на стартовых позициях
            return board;   /// Возвращаем доску
        }

        ///  Метод для создания новых фигур на стартовых позициях на доске
        private void AddStartPieces()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);

            /// В цикле выводим пешки обеих сторон. Куда короче и проще, чем прописывать все их вручную
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }
        }

        ///  Вспомогательный метод, позволяет понять, лежит данная позиция внутри доски или выходит за его пределы
        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        ///  Так же вспомогательный метод. Проверяет, пуста текущая позиция или нет
        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Position> PiecePositions()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++) 
                { 
                    Position pos = new Position(r, c);

                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }

        public bool IsInCheck(Player player)
        {
            return PiecePositionsFor(player.swap()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureOpponentKing(pos, this);
            });
        }

        public Board Copy()
        {
            Board copy = new Board();

            foreach (Position pos in PiecePositions()) 
            {
                copy[pos] = this[pos].Copy();
            }

            return copy;
        }
    }
}
