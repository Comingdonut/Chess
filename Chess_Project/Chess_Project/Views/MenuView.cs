using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Views
{
    internal class MenuView
    {
        internal string AsciiMenu { get; private set;  }
        internal string[] MenuOptions { get; private set; }
        internal MenuView()
        {
            AsciiMenu = "  |_|_|_|  |_|   |_|  |_|_|_|_|    |_|_|_|    |_|_|_|\n"
                      + "|_|        |_|   |_|  |_|        |_|        |_|      \n"
                      + "|_|        |_|_|_|_|  |_|_|_|      |_|_|      |_|_|  \n"
                      + "|_|        |_|   |_|  |_|              |_|        |_|\n"
                      + "  |_|_|_|  |_|   |_|  |_|_|_|_|  |_|_|_|    |_|_|_|  ";
            MenuOptions = new string[] { "Play", "Rules", "Quit" };
        }
    }
}
