using Chess.ChessModels.Other;
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
        //The Chess Board
        private ChessBoard _board;
        //A chess piece that can be set to any piece and color.
        private ChessPiece _pieceHolder;
        //Reprents the start location of a piece and the end location for a piece being moved.
        private Location _startLoc;
        private Location _endLoc;
        //Player's turn by defaults starts at 1.
        private int _turn = 1;
        //A count representing the number of pieces on the board when placing pieces.
        //Will never go down.
        private int _pieceCount = 0;
        #endregion
        public Utility()
        {
            _board = new ChessBoard();
            _startLoc = new Location();
            _endLoc = new Location();
        }
        #region Check Piece/Color
        /// <summary>
        /// Checks the parameter if it represents a board piece.
        /// If it does, then it sets _pieceHolder to the board piece.
        /// </summary>
        /// <param name="piece">
        /// K = King
        /// Q = Queen
        /// B = Bishop
        /// N = Knight
        /// R = Rook
        /// P = Pawn
        /// </param>
        public void CheckPiece(string piece)
        {
            switch (piece)
            {
                case "K":
                    _pieceHolder = new King();
                    break;
                case "Q":
                    _pieceHolder = new Queen();
                    break;
                case "B":
                    _pieceHolder = new Bishop();
                    break;
                case "N":
                    _pieceHolder = new Knight();
                    break;
                case "R":
                    _pieceHolder = new Rook();
                    break;
                case "P":
                    _pieceHolder = new Pawn();
                    break;
                default:
                    Console.Error.WriteLine("Invalid Piece");
                    break;
            }
        }
        /// <summary>
        /// Checks the parameter if represents a color.
        /// If it does, then it sets _placeHolder's color to the paremeter.
        /// </summary>
        /// <param name="color">
        /// l = "Light",
        /// d = "Dark"
        /// </param>
        public void CheckColor(string color)
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
        }
        #endregion

        #region Grab/Place/Move/Capture Piece
        /// <summary>
        /// Reads in a row number to convert it to a 2-D array
        /// X-axis and stores it in an Array.
        /// </summary>
        /// <param name="row">Row number from a chess board or the X coordinate of a 2-D array.</param>
        /// <returns>Returns an array with the column and row paremeters passed.</returns>
        public int GrabRow(char row)
        {
            int x = 0;
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
            return x;
        }
        /// <summary>
        /// Reads in a column letter to convert it to a 2-D array
        /// y-axis and stores it in an Array.
        /// </summary>
        /// <param name="column">Column letter from a chess board or the Y cordinate of the 2-D array.</param>
        /// <returns>Returns an array with the column and row paremeters passed.</returns>v
        public int GrabColumn(int column)
        {
            int y = 0;
            y = column - 97;
            return y;
        }
        /// <summary>
        /// Places the desired colored piece to the desired space on the board.
        /// Prints out a sentence regarding where the piece has been placed.
        /// prints out the board when all pieces have been placed.
        /// </summary>
        /// <param name="square">
        /// Represents a square that will be holding the new piece.
        /// Requried size of the string is 2 characters.
        /// </param>
        public void PlacePiece(string square)
        {
            _startLoc.X = GrabRow(square[1]);
            _startLoc.Y = GrabColumn(square[0]);
            Board.Squares[_startLoc.X, _startLoc.Y].Piece = _pieceHolder;
            Console.WriteLine(_pieceHolder.Color.ToString() + " " + _pieceHolder.Name + " has been placed at " + square + ".");
            ++_pieceCount;
            if (_pieceCount == PIECE_COUNT)
            {
                Board.PrintBoard();
            }
        }
        /// <summary>
        /// If the color of the desired piece is in line with the current player's turn,
        /// then it checks the movements of the piece to see if the new location for
        /// the piece is a legal move.
        /// 
        /// Moves a piece from it's start location to the desired location.
        /// Prints out where the piece moved too, in plain english.
        /// </summary>
        /// <param name="square1">The starting point of the piece.</param>
        /// <param name="square2">The end point for the piece.</param>
        public void MovePiece(string square1, string square2)
        {
            _startLoc.X = GrabRow(square1[1]);
            _startLoc.Y = GrabColumn(square1[0]);
            _endLoc.X = GrabRow(square2[1]);
            _endLoc.Y = GrabColumn(square2[0]);
            if ((int)Board.Squares[_startLoc.X, _startLoc.Y].Piece.Color == Turn)
            {
                if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.CheckMovement(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y) == true)
                {
                    Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                    Console.WriteLine("The piece at " + square1 + " moved to " + square2 + ".");
                    Board.PrintBoard();
                    ChangeTurn();
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
        /// If the color of the desired piece is in line with the current player's turn,
        /// then it checks the movements of the piece to see if the new location for
        /// the piece is a legal move.
        /// 
        /// Moves a piece from it's start location to the desired location and captures
        /// the piece.
        /// Prints out where the piece moved too, in plain english.
        /// </summary>
        /// <param name="square1">The starting point of the piece.</param>
        /// <param name="square2">The end point for the piece.</param>
        public void CapturePiece(string square1, string square2)
        {
            Board.PrintBoard();
            _startLoc.X = GrabRow(square1[1]);
            _startLoc.Y = GrabColumn(square1[0]);
            _endLoc.X = GrabRow(square2[1]);
            _endLoc.Y = GrabColumn(square2[0]);
            if ((int)Board.Squares[_startLoc.X, _startLoc.Y].Piece.Color == Turn)
            {
                if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.CheckMovement(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y) == true)
                {
                    Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                    Console.WriteLine("The piece at " + square1 + " captured the piece at and moved to " + square2 + ".");
                    Board.PrintBoard();
                    ChangeTurn();
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

            _startLoc.X = GrabRow(square1[1]);
            _startLoc.Y = GrabColumn(square1[0]);
            _endLoc.X = GrabRow(square2[1]);
            _endLoc.Y = GrabColumn(square2[0]);
            Board.Squares[_endLoc.X, _endLoc.Y].Piece = Board.Squares[_startLoc.X, _startLoc.Y].Piece;
            Board.Squares[_startLoc.X, _startLoc.Y].Piece = new Space();
            _startLoc.X = GrabRow(square3[1]);
            _startLoc.Y = GrabColumn(square3[0]);
            _endLoc.X = GrabRow(square4[1]);
            _endLoc.Y = GrabColumn(square4[0]);
            Board.Squares[_endLoc.X, _endLoc.Y].Piece = Board.Squares[_startLoc.X, _startLoc.Y].Piece;
            Board.Squares[_startLoc.X, _startLoc.Y].Piece = new Space();
            Console.WriteLine("King has moved from " + square1 + " to " + square2 + ", rook moved from " + square3 + " to " + square4 + ".");
            ChangeTurn();
        }
        #endregion

        #region ProcessLine
        /// <summary>
        /// Reads a line that contains commands for chess and passes them
        /// to other methods that will print them out in readable english.
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
                    CheckPiece(match.Groups[1].Value);
                    CheckColor(match.Groups[2].Value);
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
        /// <summary>
        /// Command Length of 4.
        /// </summary>
        public int Place_Piece { get { return PLACE_PIECE; } }
        /// <summary>
        /// Command Length of 5.
        /// </summary>
        public int Move_Piece { get { return MOVE_PIECE; } }
        /// <summary>
        /// Command Length of 6.
        /// </summary>
        public int Capture_Piece { get { return CAPTURE_PIECE; } }
        /// <summary>
        /// Command Length of 11.
        /// </summary>
        public int King_Side_Piece { get { return KING_SIDE_PIECE; } }
        /// <summary>
        /// Represents the chess board.
        /// </summary>
        public ChessBoard Board { get { return _board; } }
        /// <summary>
        /// Represents the current Player's turn.
        /// </summary>
        public int Turn { get {return _turn; } private set {_turn = value; } }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}
