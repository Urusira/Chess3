/**
  @file PromotionMenu.xaml.cs
  @page PromotionMenu
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


        public PromotionMenu()
        {
            InitializeComponent();
        }

        private void Queen_Mouse(object sender, MouseEventArgs e)
        {
        }

        private void Bishop_Mouse(object sender, MouseEventArgs e)
        {
        }

        private void Rook_Mouse(object sender, MouseEventArgs e)
        {
        }

        private void Knight_Mouse(object sender, MouseEventArgs e)
        {
        }
    }
}
