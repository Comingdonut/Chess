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
        private string[,] _boardSquares;
        private int[] startMove;
        private int[] endMove;
        #endregion
        public Utility()
        {
            _square = new string[4];
            _boardSquares = new string[8,24];
            startMove = new int[2];
            endMove = new int[2];
            SetBoard();
        }
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
        /// Places a piece on the board.
        /// </summary>
        /// <param name="square">Represents a square.</param>
        /// <returns>returns a sentence regarding where the piece has been placed.</returns>
        public string PlacePiece(string square1)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            if (Color[0] == 'B')
            {
                Board[startMove[0], startMove[1]] = Piece[0].ToString();
            }
            else
            {
                Board[startMove[0], startMove[1]] = Piece[0].ToString().ToLower();
            }
            return (Color + " " + Piece + " has been placed at " + square1 + ".");
        }
        /// <summary>
        /// Moves a piece on the board.
        /// </summary>
        /// <param name="square1">Where the piece initially is.</param>
        /// <param name="square2">Where the piece will be.</param>
        /// <returns>Returns the command in plain english.</returns>
        public string MovePiece(string square1, string square2)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            Board[endMove[0], endMove[1]] = Board[startMove[0], startMove[1]];
            Board[startMove[0], startMove[1]] = "-";
            return ("The piece at " + square1 + " moved to " + square2 + ".");
        }
        /// <summary>
        /// Moves a piece on a board to capture an enemy piece already there.
        /// </summary>
        /// <param name="square1">Square where the piece use to be.</param>
        /// <param name="square2">Square where the piece will be.</param>
        /// <returns>Returns the command in plain english.</returns>
        public string CapturePiece(string square1, string square2)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            Board[endMove[0], endMove[1]] = Board[startMove[0], startMove[1]];
            Board[startMove[0], startMove[1]] = "-";
            return ("The piece at " + square1 + " captured the piece at and moved to " + square2 + ".");
        }
        /// <summary>
        /// Moves a King left/right 2 squares, while moving a Rook to the right/left of the King.
        /// Describes the actions of king-side-castle.
        /// </summary>
        /// <param name="square1">Square where the king use to be.</param>
        /// <param name="square2">Square where the king will be.</param>
        /// <param name="square3">Square where the rook initially is.</param>
        /// <param name="square4">Square where the rook will be.</param>
        /// <returns>Returns the command in plain english.</returns>
        public string KingSideCastle(string square1, string square2, string square3, string square4)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            Board[endMove[0], endMove[1]] = Board[startMove[0], startMove[1]];
            Board[startMove[0], startMove[1]] = "-";
            startMove = GrabPiece(square3[0], square3[1]);
            endMove = GrabPiece(square4[0], square4[1]);
            Board[endMove[0], endMove[1]] = Board[startMove[0], startMove[1]];
            Board[startMove[0], startMove[1]] = "-";
            return "King has moved from " + square1 + " to " + square2 + ", rook moved from " + square3 + " to " + square4 + ".";
        }
        /// <summary>
        /// Sets the variables to the values passed in the parameters.
        /// </summary>
        /// <param name="square1">Stores a piece's location.</param>
        /// <param name="square2">Stores a piece's next location.</param>
        /// <param name="isCapturing">
        /// True if moving a piece will capture an enemy piece.
        /// False if moving a piece will not capture a piece.
        /// </param>
        public void StorePiece(string square1, string square2, bool isCapturing)
        {
            if (square1[0] <= 90 && square1[0] >= 65) {
                Square1 = PlacePiece(square1);
            }
            else
            {
                if (isCapturing == false)
                {
                    Square2 = MovePiece(square1, square2);
                }
                else
                {
                    Square3 = CapturePiece(square1, square2);
                }
            }
        }
        /// <summary>
        /// Sets the variables to the values passed in the parameters.
        /// </summary>
        /// <param name="square1">Square where the king use to be.</param>
        /// <param name="square2">Square where the king will be.</param>
        /// <param name="square3">Square where the rook initially is.</param>
        /// <param name="square4">Square where the rook will be.</param>
        public void StoreKingSideCastle(string square1, string square2, string square3, string square4)
        {
            Square4 = KingSideCastle(square1, square2, square3, square4);
        }
        /// <summary>
        /// Sets board squares and pieces to there rightful place.
        /// </summary>
        public void SetBoard()
        {
            //Creates squares
            for (int x = 0; x < 8; ++x)
            {
                Board[x, 0] = "[";
                Board[x, 2] = "]";
                Board[x, 3] = "[";
                Board[x, 5] = "]";
                Board[x, 6] = "[";
                Board[x, 8] = "]";
                Board[x, 9] = "[";
                Board[x, 11] = "]";
                Board[x, 12] = "[";
                Board[x, 14] = "]";
                Board[x, 15] = "[";
                Board[x, 17] = "]";
                Board[x, 18] = "[";
                Board[x, 20] = "]";
                Board[x, 21] = "[";
                Board[x, 23] = "]";
            }
            //Sets Black & White Pieces
            Board[0, 13] = "K";//Kings
            Board[7, 13] = "k";//Kings
            Board[0, 10] = "Q";//Queens
            Board[7, 10] = "q";//Queens
            for (int x = 7; x < 24; x+=9)
            {
                Board[0, x] = "B";//Black Bishops
                Board[7, x] = "b";//White Bishops
            }
            for (int x = 4; x < 21; x+=15)
            {
                Board[0, x] = "N";//Black Knight
                Board[7, x] = "n";//White Knight
            }
            for (int x = 1; x < 23; x+=21)
            {
                Board[0, x] = "R";//Black Rooks
                Board[7, x] = "r";//White Rooks
            }
            for (int x = 1; x < 24; x+=3)
            {
                Board[1, x] = "P";//Black Pawns
                Board[6, x] = "p";//White Pawns
            }
            //Empty squares
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 24; y++)
                {
                    if (Board[x, y] == null)
                    {
                        Board[x, y] = "-";
                    }
                }
            }
        }
        /// <summary>
        /// Reads in a column letter and row number to store it in an Array.
        /// </summary>
        /// <param name="column">Column letter from a chess board.</param>
        /// <param name="row">Row number from a chess board.</param>
        /// <returns>Returns an array with the column and row paremeters passed.</returns>
        public int[] GrabPiece(char column, char row)
        {
            int x = 0;//rows
            int y = 0;//columns
            switch (row)
            {
                case '8':
                    x = 0;
                    break;
                case '7':
                    x = 1;
                    break;
                case '6':
                    x = 2;
                    break;
                case '5':
                    x = 3;
                    break;
                case '4':
                    x = 4;
                    break;
                case '3':
                    x = 5;
                    break;
                case '2':
                    x = 6;
                    break;
                case '1':
                    x = 7;
                    break;
                default:
                    break;
            }
            switch (column)
            {
                case 'a':
                    y = 1;
                    break;
                case 'b':
                    y = 4;
                    break;
                case 'c':
                    y = 7;
                    break;
                case 'd':
                    y = 10;
                    break;
                case 'e':
                    y = 13;
                    break;
                case 'f':
                    y = 16;
                    break;
                case 'g':
                    y = 19;
                    break;
                case 'h':
                    y = 22;
                    break;
                default:
                    break;
            }
            int[] rowColumns = new int[] { x, y };
            return rowColumns;
        }
        #region Properties
        public string Piece { get {return _piece; } set {_piece = value; } }
        public string Color { get { return _color; } set { _color = value; } }
        public string Square1 { get { return _square[0]; } set { _square[0] = value; } }
        public string Square2 { get { return _square[1]; } set { _square[1] = value; } }
        public string Square3 { get { return _square[2]; } set { _square[2] = value; } }
        public string Square4 { get { return _square[3]; } set { _square[3] = value; } }
        public string[,] Board { get {return _boardSquares; } set {_boardSquares= value; } }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}
