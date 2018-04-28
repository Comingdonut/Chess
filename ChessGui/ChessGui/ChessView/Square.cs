using Chess.ChessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessGui.ChessView
{
    public class Square : Button
    {
        public Image Pic { get; set; }
        public Location loc { get; set; }
        public StackPanel Panel { get {return _panel; } set {_panel = value; } }
        private StackPanel _panel;
        public Square(string image)
        {
            Pic = new Image();
            loc = new Location();
            _panel = new StackPanel();
            _panel.Orientation = Orientation.Horizontal;

            _panel.Margin = new System.Windows.Thickness(5);
            
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri("Images/" + image, UriKind.RelativeOrAbsolute);
            source.EndInit();

            Pic.Source = source;
            _panel.Children.Add(Pic);
            
            this.Content = _panel;
        }
    }
}
