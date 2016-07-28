using Chess.ChessModels;
using Chess.ChessView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessControl
{
    public class ReadCommands
    {
        #region Variables
        private Utility util;
        #endregion
        /// <summary>
        /// Constructor sets Utility to another utility.
        /// </summary>
        /// <param name="util"></param>
        public ReadCommands(Utility util)
        {
            this.util = util;
        }
        /// <summary>
        /// Reads a text file line by line that contains commands for chess.
        /// Writes out the commands in readable english.
        /// </summary>
        /// <param name="print"></param>
        public void ReadFile(Print print)
        {
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines("C:/text.txt");
            // Get the file's content by using a foreach loop.
            foreach (string line in lines)
            {
                //Creates and char array that has a length of the current line in the file.
                char[] commands = new char[line.Length];
                //Loops through the current line, setting each character to an index of commands
                for (int j = 0; j < line.Length; j++)
                {
                    commands[j] = line.ElementAt(j);
                }
                //Continous if char[] meets required size, otherwise it skips current line.
                if (commands.Length >= 4)
                {
                    //Checks if current line Places a pience on a square
                    if (commands[0] <= 90 && commands[0] >= 65)
                    {
                        util.StorePiece(commands[0], commands[1], (commands[2] + "" + commands[3]), "");
                        print.PrintPlaceCommand(util.Color, util.Piece, util.Square1);
                    }
                    //Checks to see if it moves a piece on a square to another square.
                    else if (commands[0] <= 122 && commands[0] >= 97)
                    {
                        util.StorePiece(' ', ' ', (commands[0] + "" + commands[1]), (commands[3] + "" + commands[4]));
                        print.PrintMoveCommand(util.Square2);
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------
    }
}
