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
        public event Action<Option> OptionSelected;

        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();

            Result result = gameState.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, gameState.CurrentPlayer);
        }

        private static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => "Белый победил!",
                Player.Black => "Черный победил!",
                _ => "НИЧЬЯ!"
            };
        }

        static string PlayerString(Player player)
        {
            return player switch
            {
                Player.White => "Белый",
                Player.Black => "Черный",
                _ => ""
            };
        }

        static string GetReasonText(EndReason reason, Player currentPlayer)
        {
            return reason switch
            {
                EndReason.Stalemate => $"ПАТ - {PlayerString(currentPlayer)} неможет двигаться!",
                EndReason.Checkmate => $"МАТ - {PlayerString(currentPlayer)} неможет двигаться!",
                EndReason.FiftyMoveRule => $"писять ходов ПРАВИЛО",
                EndReason.InsufficientMetrial => "Insufficient Metrial",
                EndReason.ThreefoldRepetition => "Threefold Repetition",
                _ => ""
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
