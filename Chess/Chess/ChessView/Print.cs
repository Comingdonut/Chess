using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessView
{
    public class Print
    {
        /// <summary>
        /// Prints out th parameters as a single string in order.
        /// </summary>
        /// <param name="first">First string.</param>
        /// <param name="second">Second string.</param>
        /// <param name="third">Third string.</param>
        public void PrintCommand(string first, string second, string third)
        {
            Console.WriteLine(first + " " + second + " " + third);
        }
    }
    //-----------------------------------------------------------------------------------
}
