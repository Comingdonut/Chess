using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Views
{
    internal class Menu
    {
        //string[] promptPiece;
        //string[] promptSpace;
        internal string AsciiMenu { get; set;  }
        internal string[] MenuOptions { get; set; }
        internal Menu()
        {
            AsciiMenu = "  |_|_|_|  |_|   |_|  |_|_|_|_|    |_|_|_|    |_|_|_|\n"
                      + "|_|        |_|   |_|  |_|        |_|        |_|      \n"
                      + "|_|        |_|_|_|_|  |_|_|_|      |_|_|      |_|_|  \n"
                      + "|_|        |_|   |_|  |_|              |_|        |_|\n"
                      + "  |_|_|_|  |_|   |_|  |_|_|_|_|  |_|_|_|    |_|_|_|  ";
            MenuOptions = new string[] { "Play", "Rules", "Quit" };
            //promptPiece = new string[] { "What column is the piece you would like on?", "What row is the piece you would like on?" };
            //promptPiece = new string[] { "What row is the space you would like to move on?", "What space is the place you would like to move on?" };
        }
    }
}
