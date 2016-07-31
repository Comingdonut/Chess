using Chess.ChessModels;
using Chess.ChessView;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chess.ChessControl
{
    public class ReadCommands
    {
        #region Variables
        private Utility util;
        private Print print;
        private const int PLACE_PIECE = 4;
        private const int MOVE_PIECE = 5;
        private const int CAPTURE_PIECE = 6;
        private const int KING_SIDE_PIECE = 11;
        public int PlacePiece{ get {return PLACE_PIECE; } set {; } }
        public int MovePiece { get { return MOVE_PIECE; } set {; } }
        public int CapturePiece { get { return CAPTURE_PIECE; } set {; } }
        public int KingSide { get { return KING_SIDE_PIECE; } set {; } }
        #endregion
        public ReadCommands()
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
            if (line.Length == PlacePiece)
            {
                ProcessLine(line, @"([KQBNRP])([ld])([a-h])([1-8])");
            }
            else if (line.Length == MovePiece)
            {
                ProcessLine(line, @"([a-h])([1-8])([ ])([a-h])([1-8])");
            }
            else if (line.Length == CapturePiece)
            {
                ProcessLine(line, @"([a-h])([1-8])([ ])([a-h])([1-8])([*])");
            }
            else if (line.Length == KingSide)
            {
                ProcessLine(line, @"([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])");
            }
            print.PrintBoard(util.Board);
            Console.WriteLine("<--------------------------------------------------->");
        }
        /// <summary>
        /// Reads a line that contains commands for chess and
        /// writes out the commands in readable english.
        /// </summary>
        /// <param name="input">A line from a file.</param>
        /// <param name="pattern">A pattern to interpret the line.</param>
        public void ProcessLine(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if(match.Length == PLACE_PIECE)
                {
                    util.Piece = util.CheckPiece(match.Groups[1].Value);//Type of piece is being set.
                    util.Color = util.CheckColor(match.Groups[2].Value);//Color is being set.
                    util.StorePiece((match.Groups[3].Value + "" + match.Groups[4].Value), "", false);
                    print.PrintCommand(util.Square1);
                }
                else if (match.Length == MOVE_PIECE)
                {
                    util.StorePiece((match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value), false);
                    print.PrintCommand(util.Square2);
                }
                else if (match.Length == CAPTURE_PIECE)
                {
                    util.StorePiece((match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value), true);
                    print.PrintCommand(util.Square3);
                }
                else if(match.Length == KING_SIDE_PIECE)
                {
                    util.StoreKingSideCastle((match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value), (match.Groups[7].Value + "" + match.Groups[8].Value), (match.Groups[10].Value + "" + match.Groups[11].Value));
                    print.PrintCommand(util.Square4);
                }
            }
        }
        //-----------------------------------------------------------------------------------
    }
}
