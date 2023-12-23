/**
  @file PromotionMenu.xaml.cs
  @brief Класс окна с выбором фигуры, для повышения пешки
  @author Шабанов М.
\par Использует классы:
- @ref Image
- @ref Player
- @ref PieceType
\par Содержит класс:
  @ref PromotionMenu
*/

using GameLogic;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameUI
{
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;


        public PromotionMenu(Player player)
        {
            InitializeComponent();

            Queen.Source = Images.GetImage(player, PieceType.Queen);
            Bishop.Source = Images.GetImage(player, PieceType.Bishop);
            Rook.Source = Images.GetImage(player, PieceType.Rook);
            Knight.Source = Images.GetImage(player, PieceType.Knight);
        }

        private void Queen_Mouse(object sender, MouseEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }

        private void Bishop_Mouse(object sender, MouseEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
        }

        private void Rook_Mouse(object sender, MouseEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
        }

        private void Knight_Mouse(object sender, MouseEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
        }
    }
}
