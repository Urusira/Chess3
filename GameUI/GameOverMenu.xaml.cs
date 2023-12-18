using GameLogic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GameUI
{
    /// <summary>
    /// Логика взаимодействия для GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;

        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();
        }

        private static string GetWinnerText(Player winner)
        {
        }

        static string PlayerString(Player player)
        {
        }

        static string GetReasonText(EndReason reason, Player currentPlayer)
        {
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
