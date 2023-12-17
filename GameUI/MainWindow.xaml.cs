using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameLogic;

namespace GameUI
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8]; // Массив изображений
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;    // Создаём гейм стейт
        private Position selectedPos = null;
        private Dictionary<string, Move> moveLogger = new Dictionary<string, Move>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            /*
             * Здесь инициализируем геймстейт, тем самым запуская игру. Мы вызываем инициализацию доски, а так же
             * выбираем в качестве текущего игрока того, кто играет за белых. Сразу после отрисовываем доску, отправляя
             * в метод отрисовки текущую доску. Её состояние храниться как раз таки в геймстейте
             */
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
        }

        /* 
         * Этот метод при запуске помещает в каждый квадрат сетки элемент "изображение", это всё позволит легко
         *  получить доступ к управлению изображением в заданном квадрате. Например выставить нужный спрайт фигуры в квадрате,
         *  координаты которого соответствуют координате какой-либо фигуры
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

        /*
         * Данный метод отрисовывает доску принимая её саму в качестве параметра
         * Мы проходимся по всей доске, получаем фигуру в каждой из позиций и на каждую позицию
         * вызываем метод из класса вывода изображения, по сути именно здесь идёт уже отрисовка фигур на нужных позициях
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

        /*
         * Метод обрабатывает нажатия клавишей мыши внутри окна и в зависимости от того, есть в данный
         * момент выбранная фигура или нет, вызывает соответствующий метод, передавая позицию клика.
         * Тут же вызывается преобразование позиции курсора
         */
        private void BoardGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsMenuOnScreen())
            {
                return;
            }


            Point point = e.GetPosition(BoardGrid);
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

        // "Квадратизирует" позицию клика, мы получаем размер доски, делим на 8, получаем размер квадрата
        // на полученное значение мы делим положение курсора по X и по Y, возвращает положение курсора
        // преобразованное в формат клеток доски
        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        /*
         * Вызывается в случае, если выбранной позиции нет, то есть метод отвечает за выбор
         * фигуры, которой мы хотим сходить. При его вызове, происходит проссчёт всех возможных ходов для фигуры в заданной позиции.
         * Так же вызываемый метод LegalMoves не возвращает ничего, если выбрана фигура противника или пустая клетка.
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

        /*
         * Этот метод соотносит нажатие пользователя. Он смотрит, есть ли в данный момент в кэше ход для позиции клика
         * Если такой ход существует, то этот ход передаётся в хэндл, который и реализует ход
         */
        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;

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

        // Просто хэндл. Почему хэндл? Фиг знает, просто. Вызывает метод совершения хода, говорит геймстейту что игрок хочет
        // сделать ход и передаём ему ход, который хотим. Следом идёт переотрисовка доски.
        private void HandleMove(Move move)
        {
            Cansel_Button.IsEnabled = true;
            moveLogger[move.kex] = move;
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);

            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }
        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromiton(from, to, type);
                HandleMove(promMove);
            };
        }


        /*
         * Этот метод получает для выбранной фигуры коллекцию всех допустимых ходов для выбранной позиции
         */
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        /*
         * В этом методе выбирается в альфа-ргб палитре цвет для выделения клеток, а также
         * происходит сама подсветка: перебираются все позиции находящиеся в данный момент в кэше и каждая из этих позиций подсвечивается
         */
        private void ShowHighlights()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    highlights[i, j].Fill = Brushes.Transparent;
                }
            }

            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        // В этом же методе перебираются все позиции, находящиеся в данный момент в кеше и их выделение стирается
        private void HideHighLights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            selectedPos = null;
            moveCache.Clear();
            HideHighLights();
            DrawBoard(gameState.Board);
        }

        bool IsMenuOnScreen()
        {
            return MenuContainer.Content == null;
        }

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

        void RestartGame()
        {
            HideHighLights();
            moveCache.Clear();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            
        }
    }
}
