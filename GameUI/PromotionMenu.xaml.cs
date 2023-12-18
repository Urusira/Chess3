using GameLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace GameUI
{
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;

        public PromotionMenu(Player player)
        {
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
