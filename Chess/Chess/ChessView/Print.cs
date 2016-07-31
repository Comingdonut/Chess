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
        /// Prints out the parameters as a single string in order.
        /// </summary>
        /// <param name="first">First string.</param>
        /// <param name="second">Second string.</param>
        /// <param name="third">Third string.</param>
        public void PrintPlaceCommand(string first, string second, string third)
        {
            Console.WriteLine(first + " " + second + " " + third);
        }
        /// <summary>
        /// Prints the parameter out to the console.
        /// </summary>
        /// <param name="first">Where the piece at a specific location has moved to.</param>
        public void PrintCommand(string first)
        {
            Console.WriteLine(first);
        }
        /// <summary>
        /// Prints out 24 array values before moving on to the next set of 24
        /// </summary>
        /// <param name="boardSqaures">Represents a board. Required size for the array is 194</param>
        public void PrintBoard(string[,] boardSqaures)
        {

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 24; y++)
                {
                    Console.Write(boardSqaures[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
    //-----------------------------------------------------------------------------------
}
