using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    public class Utility
    {
        #region Variables
        private string _piece;
        private string _color;
        private string _square1;
        private string _square2;
        #endregion
        /// <summary>
        /// Checks character parameter if it represents a board piece.
        /// </summary>
        /// <param name="piece">
        /// K = King
        /// Q = Queen
        /// B = Bishop
        /// N = Knight
        /// R = Rook
        /// P = Pawn
        /// </param>
        /// <returns>Returns the name of a board piece</returns>
        public string CheckPiece(char piece)
        {
            if (piece == 'K' || piece == 'Q' || piece == 'B' || piece == 'N' || piece == 'R' || piece == 'P')
            {
                switch (piece)
                {
                    case 'K':
                        return "King";
                    case 'Q':
                        return "Queen";
                    case 'B':
                        return "Bishop";
                    case 'N':
                        return "Knight";
                    case 'R':
                        return "Rook";
                    case 'P':
                        return "Pawn";
                }
            }
            else
            {
                new Exception("Invalid character piece...");
            }
            return "";
        }
        /// <summary>
        /// Checks character parameter if represents a color.
        /// </summary>
        /// <param name="color">
        /// l = "Light"
        /// d = "Dark"
        /// </param>
        /// <returns>Returns dark or light</returns>
        public string CheckColor(char color)
        {
            if (color == 'l' || color == 'd')
            {
                switch (color)
                {
                    case 'l':
                        return "White";
                    case 'd':
                        return "Black";
                }
            }
            else
            {
                new Exception("Invalid color character...");
            }
            return "";
        }
        /// <summary>
        /// Places square in an incomplete sentence.
        /// </summary>
        /// <param name="square">Represents a square</param>
        /// <returns>returns a sentence regarding where the piece has been placed.</returns>
        public string PlacePiece(string square1)
        {
            return ("has been moved to " + square1 + ".");
        }
        /// <summary>
        /// Describes the movment of a piece.
        /// </summary>
        /// <param name="square1">Where the piece initially is.</param>
        /// <param name="square2">Where the piece will be.</param>
        /// <returns>Returns a sentence about where a piece has been and where it went.</returns>
        public string MovePiece(string square1, string square2)
        {
            return ("The piece at " + square1 + " moved to " + square2 + ".");
        }
        /// <summary>
        /// Sets the variables to the values passed in the parameters.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="color"></param>
        /// <param name="square"></param>
        public void StorePiece(char piece, char color, string square1, string square2)
        {
            Piece = CheckPiece(piece);
            Color = CheckColor(color);
            Square1 = PlacePiece(square1);
            Square2 = MovePiece(square1, square2);
        }
        public string Piece { get {return _piece; } set {_piece = value; } }
        public string Color { get { return _color; } set { _color = value; } }
        public string Square1 { get { return _square1; } set { _square1 = value; } }
        public string Square2 { get { return _square2; } set { _square2 = value; } }
        //-----------------------------------------------------------------------------------
    }
}
