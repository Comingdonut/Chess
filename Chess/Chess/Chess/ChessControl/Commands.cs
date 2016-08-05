using Chess.ChessModels;
using Chess.ChessView;
using System;

namespace Chess.ChessControl
{
    public class Commands
    {
        #region Variables
        private Utility util;
        private Print print;
        #endregion
        public Commands()
        {
            util = new Utility();
            print = new Print();
        }
        /// <summary>
        /// Reads in lines from a file and checks for commands.
        /// </summary>
        /// <param name="line">A line from a file.</param>
        public void ReadLine(string line)
        {
            print.PrintBoard(util.Board);
            if (line.Length == util.Place_Piece)
            {
                util.ProcessLine(line, @"([KQBNRP])([ld])([a-h])([1-8])");
            }
            else if (line.Length == util.Move_Piece)
            {
                util.ProcessLine(line, @"([a-h])([1-8])([ ])([a-h])([1-8])");
            }
            else if (line.Length == util.Capture_Piece)
            {
                util.ProcessLine(line, @"([a-h])([1-8])([ ])([a-h])([1-8])([*])");
            }
            else if (line.Length == util.King_Side_Piece)
            {
                util.ProcessLine(line, @"([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])");
            }
            print.PrintBoard(util.Board);
            Console.WriteLine("<--------------------------------------------------->");
        }
        //-----------------------------------------------------------------------------------
    }
}
