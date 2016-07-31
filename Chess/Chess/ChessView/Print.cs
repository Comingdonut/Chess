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
        /// Prints the parameter out to the console.
        /// </summary>
        /// <param name="command">Where the piece at a specific location has moved to.</param>
        public void PrintCommand(string command)
        {
            Console.WriteLine(command);
        }
        /// <summary>
        /// Prints out 24 array values before moving on to the next set of 24 values.
        /// </summary>
        /// <param name="boardSqaures">Represents a board. Required size for the 2-D array is 8 rows and 24 columns.</param>
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
