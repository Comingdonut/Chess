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
        private string[] _square;
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
        public string CheckPiece(string piece)
        {
            switch (piece)
            {
                case "K":
                    return "King";
                case "Q":
                    return "Queen";
                case "B":
                    return "Bishop";
                case "N":
                    return "Knight";
                case "R":
                    return "Rook";
                case "P":
                    return "Pawn";
                default:
                    throw new Exception("Invalid character piece...");
            }
        }
        /// <summary>
        /// Checks character parameter if represents a color.
        /// </summary>
        /// <param name="color">
        /// l = "Light"
        /// d = "Dark"
        /// </param>
        /// <returns>Returns dark or light</returns>
        public string CheckColor(string color)
        {
            switch (color)
            {
                case "l":
                    return "White";
                case "d":
                    return "Black";
                default:
                throw new Exception("Invalid color character...");
            }
        }
        /// <summary>
        /// Places square in an incomplete sentence.
        /// </summary>
        /// <param name="square">Represents a square</param>
        /// <returns>returns a sentence regarding where the piece has been placed.</returns>
        public string PlacePiece(string square1)
        {
            return ("has been placed at " + square1 + ".");
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
        /// Describes the movment/capture of a piece.
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <returns>Returns a sentence about where a piece has been, where it went/captured.</returns>
        public string CapturePiece(string square1, string square2)
        {
            return ("The piece at " + square1 + " captured the piece at and moved to " + square2 + ".");
        }
        /// <summary>
        /// Describes the actions of king-side-castle.
        /// </summary>
        /// <param name="square1">Square where the king use to be.</param>
        /// <param name="square2">Square where the king will be.</param>
        /// <param name="square3">Square where the rook initially is.</param>
        /// <param name="square4">Square where the rook will be.</param>
        /// <returns></returns>
        public string KingSideCastle(string square1, string square2, string square3, string square4)
        {
            return "King has moved from " + square1 + " to " + square2 + ", rook moved from " + square3 + " to " + square4 + ".";
        }
        /// <summary>
        /// Sets the variables to the values passed in the parameters.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="color"></param>
        /// <param name="square"></param>
        public void StorePiece(string piece, string color, string square1, string square2)
        {
            _square = new string[4];
            if (piece != "")
            {
                Piece = CheckPiece(piece);
                Color = CheckColor(color);
            }
            Square1 = PlacePiece(square1);
            Square2 = MovePiece(square1, square2);
            Square3 = CapturePiece(square1, square2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <param name="square3"></param>
        /// <param name="square4"></param>
        public void StoreKingSideCastle(string square1, string square2, string square3, string square4)
        {
            _square = new string[4];
            Square4 = KingSideCastle(square1, square2, square3, square4);
        }
        #region Properties
        public string Piece { get {return _piece; } set {_piece = value; } }
        public string Color { get { return _color; } set { _color = value; } }
        public string Square1 { get { return _square[0]; } set { _square[0] = value; } }
        public string Square2 { get { return _square[1]; } set { _square[1] = value; } }
        public string Square3 { get { return _square[2]; } set { _square[2] = value; } }
        public string Square4 { get { return _square[3]; } set { _square[3] = value; } }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}
