/**
  @file MainWindow.xaml.cs
  @brief Класс главного окна, взаимодействует с интерфейсом и соединяет программу воедино
  @author Листопад В., Тюканов В., Шабанов М.
\par Использует классы:
- @ref Piece
- @ref Image
- @ref Move
- @ref Player
- @ref Position
- @ref GameState
\par Содержит класс:
  @ref MainWindow
*/

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GameLogic;

namespace GameUI
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];                                ///< Массив изображений
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];                         ///< Массив ячеек для подсветки
        private Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();        ///< Словарь для удержания ходов для выбранной фигуры


        public GameState gameState;                                                             ///< Переменная нового игрового состояния
        private Position selectedPos = null;                                                    ///< Переменная для хранения выбранной фигуры
        private Dictionary<string, Move> moveLogger = new Dictionary<string, Move>();           ///< Словарь для логирования ходов
        //TextBlock Logger = new TextBlock();

        /// Метод главного окна
        /** Инициализирует окно, доску, иициализируется новая игра путём
            создания нового игрового состояния, тут же происходит отрисовка доски
        */
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            Logger.Text = "Ходов не было.\n";
            /** Здесь инициализируем геймстейт, тем самым запуская игру. Мы вызываем инициализацию доски, а так же
                выбираем в качестве текущего игрока того, кто играет за белых. Сразу после отрисовываем доску, отправляя
                в метод отрисовки текущую доску. Её состояние храниться как раз таки в геймстейте
            */
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
        }

        /// Инициализация доски
        /** Этот метод при запуске помещает в каждый квадрат сетки элемент "изображение", это всё позволит легко
            получить доступ к управлению изображением в заданном квадрате. Например выставить нужный спрайт фигуры в квадрате,
            координаты которого соответствуют координате какой-либо фигуры
        */
        private void InitializeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Image image = new Image();
                    pieceImages[i, j] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[i, j] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        /// Отрисовка доски
        /** Данный метод отрисовывает доску принимая её саму в качестве параметра
            Мы проходимся по всей доске, получаем фигуру в каждой из позиций и на каждую позицию
            вызываем метод из класса вывода изображения, по сути именно здесь идёт уже отрисовка фигур на нужных позициях
        \param board игровое поле, содержащее в себе фигуры
        */
        private void DrawBoard(Board board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = board[i, j];
                    pieceImages[i, j].Source = Images.GetImage(piece);

                    HideHighLights();
                }
            }
        }

        /// Метод обработки нажатия клавиши мыши
        /** Метод обрабатывает нажатия клавишей мыши внутри окна и в зависимости от того, есть в данный
            момент выбранная фигура или нет, вызывает соответствующий метод, передавая позицию клика.
            Тут же вызывается преобразование позиции курсора
        \param sender
        \param e точка нажатия, по стандарту передаваемая в метод обработки нажатия
        \return 
        */
        private void BoardGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsMenuOnScreen())
            {
                
                return;
            }

            Point point = e.GetPosition(BoardGrid);

            if (point.X >= 200)
            {
                Position pos = ToSquarePosition(point);

                if (selectedPos == null)
                {
                    OnFromPositionSelected(pos);
                }
                else
                {
                    OnToPositionSelected(pos);
                }
            }
            
        }

        /// Метод преобразования координат клика под формат доски
        /** "Квадратизирует" позицию клика, мы получаем размер доски, делим на 8, получаем размер квадрата
            на полученное значение мы делим положение курсора по X и по Y, возвращает положение курсора
            преобразованное в формат клеток доски
        \param point точка нажатия, хранит координаты нажатия внутри окна-отправителя
        */
        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualHeight / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)((point.X - 200) / squareSize);
            return new Position(row, col);
        }

        /// Метод выбора фигуры
        /** Вызывается в случае, если выбранной позиции нет, то есть метод отвечает за выбор
            фигуры, которой мы хотим сходить. При его вызове, происходит проссчёт всех возможных ходов для фигуры в заданной позиции.
            Так же вызываемый метод LegalMoves не возвращает ничего, если выбрана фигура противника или пустая клетка.
        \param pos позиция клика в формате, адаптированном под игровое поле
        */
        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMoves(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        /// Метод выбора позиции хода
        /** Этот метод соотносит нажатие пользователя. Он смотрит, есть ли в данный момент в кэше ход для позиции клика
            Если такой ход существует, то этот ход передаётся в хэндл, который и реализует ход
        \param pos позиция клика в формате, адаптированном под игровое поле
        */
        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighLights();

            if (moveCache.TryGetValue(pos, out Move move))
            {
                if(move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }

        /// Продвигает ход, конструирует сообщения в лог
        /** Вызывает метод совершения хода, говорит геймстейту что игрок хочет
            сделать ход и передаём ему ход, который хотим. Следом идёт переотрисовка доски.
            Здесь же конструируется сообщение для лога действий.
        \param move ход, который мы хотим совершить
        */
        private void HandleMove(Move move)
        {
            if (!Cansel_Button.IsEnabled)
            {
                Logger.Text = "";
            }
            string player;
            if (gameState.CurrentPlayer == Player.White)
            {
                player = "Белый ";
            }
            else
            {
                player = "Черный ";
            }
            if (move.Type == MoveType.CastleQueenSide || move.Type == MoveType.CastleKingSide)
            {
                Logger.Text = player + "совершил рокировку\n" + Logger.Text;
            }
            else if (move.Type == MoveType.Normal){
                    string piece, word = "съел ", eaten = "";
                    if (gameState.Board[move.ToPos] == null)
                    {
                        word = "сходил на ";
                    }
                    else
                    {
                        switch (gameState.Board[move.ToPos].Type)
                        {
                            case PieceType.Pawn:
                                {
                                    eaten = "пешку ";
                                    break;
                                }
                            case PieceType.Rook:
                                {
                                    eaten = "ладью ";
                                    break;
                                }
                            case PieceType.Knight:
                                {
                                    eaten = "коня ";
                                    break;
                                }
                            case PieceType.Bishop:
                                {
                                    eaten = "слона ";
                                    break;
                                }
                            default:
                                {
                                    eaten = "ферзя ";
                                    break;
                                }
                        }
                    }
                    switch (gameState.Board[move.FromPos].Type)
                    {
                        case PieceType.Pawn:
                            {
                                if (player == "Белый ")
                                {
                                    player = "Белая ";
                                }
                                else {
                                    player = "Черная ";
                                }
                                if (word == "съел ")
                                {
                                    word = "съела ";
                                }
                                else {
                                    word = "сходила на ";
                                }
                                piece = "пешка ";
                                break;
                            }
                        case PieceType.Rook:
                            {
                                if (player == "Белый ")
                                {
                                    player = "Белая ";
                                }
                                else {
                                    player = "Черная ";
                                }
                                if (word == "съел ")
                                {
                                    word = "съела ";
                                }
                                else {
                                    word = "сходила на ";
                                }
                                piece = "ладья ";
                                break;
                            }
                        case PieceType.Knight:
                            {
                                piece = "конь ";
                                break;
                            }
                        case PieceType.Bishop:
                            {
                                piece = "слон ";
                                break;
                            }
                        case PieceType.Queen:
                            {
                                piece = "ферзь ";
                                break;
                            }
                        default:
                            {
                                piece = "король ";
                                break;
                            }
                    }
                    char[] cols = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                    char[] rows = { '8', '7', '6', '5', '4', '3', '2', '1' };
                    char[] from = new char[2];
                    from[0] = cols[move.FromPos.Column];
                    from[1] = rows[move.FromPos.Row];
                    char[] to = new char[2];
                    to[0] = cols[move.ToPos.Column];
                    to[1] = rows[move.ToPos.Row];
                    Logger.Text = player + piece + from[0] + from[1] + ' ' + word + eaten + to[0] + to[1] + ".\n" + Logger.Text;
                }
            Cansel_Button.IsEnabled = true;
            moveLogger[move.kex] = move;
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            if (gameState.Board.IsInCheck(gameState.CurrentPlayer))
            {
                if (gameState.CurrentPlayer == Player.Black)
                {
                    Logger.Text = "Белый поставил шах!" + Logger.Text;
                }
                else
                {
                    Logger.Text = "Черный поставил шах!" + Logger.Text;
                }
            }

            if (gameState.IsGameOver())
            {
                if(gameState.Board.IsInCheck(gameState.CurrentPlayer))
                {
                    if (gameState.CurrentPlayer == Player.Black)
                    {
                        Logger.Text = "Белый поставил мат!" + Logger.Text;
                    }
                    else
                    {
                        Logger.Text = "Черный поставил мат!" + Logger.Text;
                    }
                }
                else
                {
                    Logger.Text = "Результат игры - пат!" + Logger.Text;
                }
                ShowGameOver();
            }
        }

        /// Повышение пешки
        /** Метод принимает две позиции хода, конструирует соответствующее сообщение для лога,
            вызывает меню выбора фигуры и позволяет выбрать саму фигуру и после выбора фигуры,
            вызывает метод вызова соответствующего хода в соответствующем классе
        \param from позиция, откуда сходила пешка
        \param to позиция, куда сходила пешка
        */
        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                string player, word = "съев ", eaten = "",piece;
                if (gameState.CurrentPlayer == Player.White)
                {
                    player = "Белый превратил пешку ";
                }
                else
                {
                    player = "Черный превратил пешку ";
                }
                if (gameState.Board[to] == null)
                {
                    word = "сходив на ";
                }
                else
                {
                    switch (gameState.Board[to].Type)
                    {
                        case PieceType.Rook:
                        {
                            eaten = "ладью ";
                            break;
                        }
                        case PieceType.Knight:
                        {
                            eaten = "коня ";
                            break;
                        }
                        case PieceType.Bishop:
                        {
                            eaten = "слона ";
                            break;
                        }
                        default:
                        {
                            eaten = "ферзя ";
                            break;
                        }
                    }
                }
                switch (type)
                {
                    case PieceType.Rook:
                        {
                            piece = "ладью ";
                            break;
                        }
                    case PieceType.Knight:
                        {
                            piece = "коня ";
                            break;
                        }
                    case PieceType.Bishop:
                        {
                            piece = "слона ";
                            break;
                        }
                    default:
                        {
                            piece = "ферзя ";
                            break;
                        }
                }
                char[] cols = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                char[] rows = { '8', '7', '6', '5', '4', '3', '2', '1' };
                char[] From = new char[2];
                From[0] = cols[from.Column];
                From[1] = rows[from.Row];
                char[] To = new char[2];
                To[0] = cols[to.Column];
                To[1] = rows[to.Row];
                Logger.Text = player + From[0] + From[1] + " в " + piece + word + eaten + To[0] + To[1] + ".\n" + Logger.Text;
                MenuContainer.Content = null;
                Move promMove = new PawnPromiton(from, to, type);
                HandleMove(promMove);
            };
        }


        /// Этот метод получает для выбранной фигуры коллекцию всех допустимых ходов для выбранной позиции
        /** Очищает словарь, заполненный ходами предыдущей выбранной фигуры, а после перебирает переданный
            в него словарь, содержащий все возможные в данной ситуации ходы для конкретной фигуры
            и заносит в данный словарь moveCache
        \param moves словарь, содержащий в себе ходы
         */
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        /// Подсветка клеток доски
        /** В этом методе выбирается в альфа-ргб палитре цвет для выделения клеток, а также
            происходит сама подсветка: перебираются все позиции находящиеся в данный момент в кэше и каждая из этих позиций подсвечивается
        */
        private void ShowHighlights()
        {
            HideHighLights();

            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        /// В этом же методе перебираются все позиции, находящиеся в данный момент в кеше и их выделение стирается
        private void HideHighLights()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    highlights[i, j].Fill = Brushes.Transparent;
                }
            }
        }

        /// Обработка клика по кнопке рестарта
        /** Стандартный обработчик клика по кнопке, убирает меню с экрана и
            вызывает метод рестарта игры.
        \param sender
        \param e
         */
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            MenuContainer.Content = null;
            RestartGame();
        }

        /// Обработка клика по кнопке отмены хода
        /** Стандартный обработчик клика по кнопке, если у нас в логе содержатся
            какие то ходы, то для последнего хода в списке вызывается его метод обратного хода.
            Если у нас единственный ход в словаре, то он отменяется, а кнопка отмены блокируется.
            Тут же конструируется соответствующее сообщение для лога.
            Вызывается очистка кэша ходов, скрывается подсветка если таковая имеется и вызывает отрисовку доски.
        \param sender
        \param e
         */
        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            if (moveLogger.Count() > 1)
            {
                gameState.MakeReverseMove(moveLogger.Values.Last());
                moveLogger.Remove(moveLogger.Values.Last().kex);
            }
            else
            {
                gameState.MakeReverseMove(moveLogger.Values.Last());
                moveLogger.Remove(moveLogger.Values.Last().kex);
                Cansel_Button.IsEnabled = false;
            }
            Logger.Text = "Ход отменен.\n" + Logger.Text;
            selectedPos = null;
            moveCache.Clear();
            HideHighLights();
            DrawBoard(gameState.Board);
        }

        /// Обработка клика по кнопке выхода
        /** Стандартный обработчик клика по кнопке, вызывает стандартный метод завершения приложения
        \param sender
        \param e
         */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// Проверка, выведено ли меню на экран
        /**
        \return true или false в зависимости от того, находится ли меню на экране
        */
        bool IsMenuOnScreen()
        {
            return MenuContainer.Content == null;
        }

        /// Обработка завершения игры
        /** Метод создаёт новое завершение игры и выводит меню с объявлением победителя
            и двумя кнопками - рестарта и завершения игры
         */
        void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        /// Обработка рестарта игры
        /** Метод очищает лог и кеш, скрывает подсветку, блокирует кнопку отмены хода и инициирует новое игровое состояние
         */
        void RestartGame()
        {
            HideHighLights();
            moveCache.Clear();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            moveLogger.Clear();
            Logger.Text = "Ходов не было.\n";
            Cansel_Button.IsEnabled = false;
        }

        /// Обработка клика по кнопке справки
        /** Стандартный обработчик клика по кнопке, вызывает окно со справкой
        \param sender
        \param e
         */
        private void Reference_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Reference re = new Reference();
            re.Show();
        }

    }
}
