/**
  @file MainWindow.xaml.cs
  @page MainWindow
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
        public MainWindow()
        {
            InitializeComponent();

            Logger.Text = "Ходов не было.\n";
        }

        /// Обработка клика по кнопке рестарта
        /** Стандартный обработчик клика по кнопке, убирает меню с экрана и
            вызывает метод рестарта игры.
        \param sender
        \param e
         */
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
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

        private void Reference_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Reference re = new Reference();
            re.Show();
        }
    }
}
