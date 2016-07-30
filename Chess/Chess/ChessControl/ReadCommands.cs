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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public void ReadLine(string line)
        {
            util = new Utility();
            print = new Print();
            if (line.Length == PlacePiece)
            {
                ProcessFile(line, @"([KQBNRP])([ld])([a-h])([1-8])", print);
            }
            else if (line.Length == MovePiece)
            {
                ProcessFile(line, @"([a-h])([1-8])([ ])([a-h])([1-8])", print);
            }
            else if (line.Length == CapturePiece)
            {
                ProcessFile(line, @"([a-h])([1-8])([ ])([a-h])([1-8])([*])", print);
            }
            else if (line.Length == KingSide)
            {
                ProcessFile(line, @"([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])", print);
            }
        }
        /// <summary>
        /// Reads a text file line by line that contains commands for chess.
        /// Writes out the commands in readable english.
        /// </summary>
        /// <param name="print"></param>
        public void ProcessFile(string input, string pattern, Print print)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                for (int k = 1; k < match.Groups.Count; ++k)
                {
                    string line = match.Groups[k].Value;
                }
                if(match.Length == PLACE_PIECE)
                {
                    util.StorePiece(match.Groups[1].Value, match.Groups[2].Value, (match.Groups[3].Value + "" + match.Groups[4].Value), "");
                    print.PrintPlaceCommand(util.Color, util.Piece, util.Square1);
                }
                else if (match.Length == MOVE_PIECE)
                {
                    util.StorePiece("", "", (match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value));
                    print.PrintCommand(util.Square2);
                }
                else if (match.Length == CAPTURE_PIECE)
                {
                    util.StorePiece("", "", (match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value));
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
