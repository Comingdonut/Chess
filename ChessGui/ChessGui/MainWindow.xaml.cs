using Chess.ChessControl;
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

namespace ChessGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller _con;
        public MainWindow()
        {
            _con = new Controller();
            InitializeComponent();
            _con.CreateBoard(Board, Movement, PlayerTurns);
            _con.SetPromoteButtons(Promote);
        }
    }
}
