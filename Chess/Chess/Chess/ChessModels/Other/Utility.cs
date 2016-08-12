using System;
using System.Text.RegularExpressions;

namespace Chess.ChessModels
{
    public class Utility
    {
        #region Variables
        //command lengths
        private const int PLACE_PIECE = 4;
        private const int MOVE_PIECE = 5;
        private const int CAPTURE_PIECE = 6;
        private const int KING_SIDE_PIECE = 11;
        //Total squares on the board
        private const int PIECE_COUNT = 32;

        private string _piece;
        private string _color;
        private ChessBoard _board;
        private ChessPiece _pieceHolder;
        private int[] startMove;
        private int[] endMove;
        private int _turn = 1;
        private int _pieceCount = 0;
        private bool inCheck;
        #endregion
        public Utility()
        {
            _board = new ChessBoard();
            startMove = new int[2];
            endMove = new int[2];
        }
        #region Check Piece/Color
        /// <summary>
        /// Checks the parameter if it represents a board piece.
        /// If it does, then it sets _pieceHolder to a the board piece.
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
            string name;
            switch (piece)
            {
                case "K":
                    _pieceHolder = new King();
                    name = _pieceHolder.Piece;
                    break;
                case "Q":
                    _pieceHolder = new Queen();
                    name = _pieceHolder.Piece;
                    break;
                case "B":
                    _pieceHolder = new Bishop();
                    name = _pieceHolder.Piece;
                    break;
                case "N":
                    _pieceHolder = new Knight();
                    name = _pieceHolder.Piece;
                    break;
                case "R":
                    _pieceHolder = new Rook();
                    name = _pieceHolder.Piece;
                    break;
                case "P":
                    _pieceHolder = new Pawn();
                    name = _pieceHolder.Piece;
                    break;
                default:
                    Console.Error.WriteLine("Invalid Piece");
                    name = null;
                    break;
            }
            return name;
        }
        /// <summary>
        /// Checks the parameter if represents a color.
        /// If it does, then it sets _placeHolder's color to to the paremeter.
        /// </summary>
        /// <param name="color">
        /// l = "Light"
        /// d = "Dark"
        /// </param>
        /// <returns>Returns dark or light depending on the paremeter.</returns>
        public string CheckColor(string color)
        {
            ChessColor brush;
            switch (color)
            {
                case "l":
                    brush = ChessColor.LIGHT;
                    break;
                case "d":
                    brush = ChessColor.DARK;
                    break;
                default:
                    throw new Exception("Invalid color character...");
            }
            _pieceHolder.Color = brush;
            return brush.ToString();
        }
        #endregion

        #region Grab/Place/Move/Capture Piece
        /// <summary>
        /// Reads in a column letter and row number to convert and store it in an Array.
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
            y = column - 97;
            int[] rowColumns = new int[] { x, y };
            return rowColumns;
        }
        /// <summary>
        /// Places the desired colored piece to the desired space on the board.
        /// Prints out a sentence regarding where the piece has been placed.
        /// </summary>
        /// <param name="square">Represents a square that will be holding the new piece.</param>
        public void PlacePiece(string square1)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            Board.Squares[startMove[0], startMove[1]].Piece = _pieceHolder;
            Console.WriteLine(Color + " " + _pieceHolder.Piece + " has been placed at " + square1 + ".");
            ++_pieceCount;
            if (_pieceCount == PIECE_COUNT)
            {
                Board.PrintBoard();
            }
        }
        /// <summary>
        /// Moves a piece from it's start location to the desired location.
        /// Prints out where the piece moved too, in plain english.
        /// </summary>
        /// <param name="square1">The starting point of the piece.</param>
        /// <param name="square2">The end point for the piece.</param>
        public void MovePiece(string square1, string square2)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            if ((int)Board.Squares[startMove[0], startMove[1]].Piece.Color == Turn)
            {
                if (Board.Squares[startMove[0], startMove[1]].Piece.CheckMovement(Board.Squares, startMove, endMove) == true)
                {
                    if (Board.Squares[startMove[0], startMove[1]].Piece.CheckSquare(Board.Squares, endMove))
                    {
                        Board.Squares[startMove[0], startMove[1]].Piece.MovePiece(Board.Squares, startMove, endMove);
                        Console.WriteLine("The piece at " + square1 + " moved to " + square2 + ".");
                        Board.PrintBoard();
                        ChangeTurn();
                    }
                    else
                    {
                        Console.Error.WriteLine("You can not capture your own piece, please try again...");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Invalid piece movement, please try again...");
                }
            }
            else
            {
                Console.Error.WriteLine("Invalid movement, please try again.");
            }
        }
        /// <summary>
        /// Moves a piece from it's start location to the desired location and captures
        /// the piece.
        /// Prints out where the piece moved too, in plain english.
        /// </summary>
        /// <param name="square1">The starting point of the piece.</param>
        /// <param name="square2">The end point for the piece.</param>
        public void CapturePiece(string square1, string square2)
        {
            Board.PrintBoard();
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            if ((int)Board.Squares[startMove[0], startMove[1]].Piece.Color == Turn)
            {
                if (Board.Squares[startMove[0], startMove[1]].Piece.CheckMovement(Board.Squares, startMove, endMove) == true)
                {
                    if (Board.Squares[startMove[0], startMove[1]].Piece.CheckSquare(Board.Squares, endMove))
                    {
                        Board.Squares[startMove[0], startMove[1]].Piece.MovePiece(Board.Squares, startMove, endMove);
                        Console.WriteLine("The piece at " + square1 + " captured the piece at and moved to " + square2 + ".");
                        Board.PrintBoard();
                        ChangeTurn();
                    }
                    else
                    {
                        Console.Error.WriteLine("You can not capture your own piece, please try again...");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Invalid piece movement, please try again...");
                }
            }
            else
            {
                Console.Error.WriteLine("Invalid movement, please try again.");
            }
        }
        /// <summary>
        /// Moves a King left/right 2 squares, while moving a Rook to the right/left of the King.
        /// Describes the actions of king-side-castle.
        /// Print out where the pieces moved too, in plain english.
        /// </summary>
        /// <param name="square1">The starting point of the king</param>
        /// <param name="square2">The end point for the king.</param>
        /// <param name="square3">The starting point of the Rook.</param>
        /// <param name="square4">The end point for the Rook.</param>
        public void KingSideCastle(string square1, string square2, string square3, string square4)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            Board.Squares[endMove[0], endMove[1]].Piece = Board.Squares[startMove[0], startMove[1]].Piece;
            Board.Squares[startMove[0], startMove[1]].Piece = new Space();
            startMove = GrabPiece(square3[0], square3[1]);
            endMove = GrabPiece(square4[0], square4[1]);
            Board.Squares[endMove[0], endMove[1]].Piece = Board.Squares[startMove[0], startMove[1]].Piece;
            Board.Squares[startMove[0], startMove[1]].Piece = new Space();
            Console.WriteLine("King has moved from " + square1 + " to " + square2 + ", rook moved from " + square3 + " to " + square4 + ".");
            ChangeTurn();
        }
        #endregion

        #region ProcessLine
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
                if (match.Length == PLACE_PIECE)
                {
                    Piece = CheckPiece(match.Groups[1].Value);
                    Color = CheckColor(match.Groups[2].Value);
                    PlacePiece(match.Groups[3].Value + "" + match.Groups[4].Value);
                }
                else if (match.Length == MOVE_PIECE)
                {
                    MovePiece((match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value));
                }
                else if (match.Length == CAPTURE_PIECE)
                {
                    CapturePiece((match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value));
                }
                else if (match.Length == KING_SIDE_PIECE)
                {
                    KingSideCastle((match.Groups[1].Value + "" + match.Groups[2].Value), (match.Groups[4].Value + "" + match.Groups[5].Value), (match.Groups[7].Value + "" + match.Groups[8].Value), (match.Groups[10].Value + "" + match.Groups[11].Value));
                }
            }
        }
        #endregion

        #region Turns
        /// <summary>
        /// Changes the players turn respectively.
        /// </summary>
        public void ChangeTurn()
        {
            if(Turn == 1)
            {
                Turn = 2;
            }
            else
            {
                Turn = 1;
            }
        }
        #endregion

        #region Properties
        public int Place_Piece { get { return PLACE_PIECE; } }
        public int Move_Piece { get { return MOVE_PIECE; } }
        public int Capture_Piece { get { return CAPTURE_PIECE; } }
        public int King_Side_Piece { get { return KING_SIDE_PIECE; } }
        public string Piece { get { return _piece; } private set { _piece = value; } }
        public string Color { get { return _color; } private set { _color = value; } }
        public ChessBoard Board { get { return _board; } }
        public int Turn { get {return _turn; } private set {_turn = value; } }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}
