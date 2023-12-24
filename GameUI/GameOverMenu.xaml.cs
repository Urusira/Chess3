/**
  @file GameOverMenu.xaml.cs
  @page GameOverMenu
  @brief Класс окна с объявлением победителя в матче
  @author Тюканов В.
\par Использует классы:
- @ref Result
- @ref Option
- @ref Player
- @ref EndReason
- @ref GameState
\par Содержит класс:
  @ref GameOverMenu
*/


using GameLogic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GameUI
{
    public partial class GameOverMenu : UserControl
    {
        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
