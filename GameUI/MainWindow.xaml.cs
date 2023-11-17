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

        private GameState gameState;    // Создаём гейм стейт

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
                for(int j = 0; j < 8; j++)
                {
                    Image image = new Image();
                    pieceImages[i, j] = image;
                    PieceGrid.Children.Add(image);
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
            for( int i = 0;i < 8; i++)
            {
                for( int j = 0; j < 8;j++)
                {
                    Piece piece = board[i,j];
                    pieceImages[i,j].Source = Images.GetImage(piece);
                }
            }
        }
    }
}
