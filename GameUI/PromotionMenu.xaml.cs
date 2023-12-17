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
    /// <summary>
    /// Логика взаимодействия для PromotionMenu.xaml
    /// </summary>
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
