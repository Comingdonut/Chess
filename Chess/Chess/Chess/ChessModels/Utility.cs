using System;
using System.Text.RegularExpressions;

namespace Chess.ChessModels
{
    public class Utility
    {
        #region Variables
        private const int PLACE_PIECE = 4;
        private const int MOVE_PIECE = 5;
        private const int CAPTURE_PIECE = 6;
        private const int KING_SIDE_PIECE = 11;
        private string _piece;
        private string _color;
        private ChessPiece[,] _boardSquares;
        private ChessPiece _pieceHolder;
        private int[] startMove;
        private int[] endMove;
        #endregion
        public Utility()
        {
            _boardSquares = new ChessPiece[8,8];
            startMove = new int[2];
            endMove = new int[2];
            SetBoard();
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
                    _pieceHolder = new King(' ');
                    name = _pieceHolder.Piece;
                    break;
                case "Q":
                    _pieceHolder = new Queen(' ');
                    name = _pieceHolder.Piece;
                    break;
                case "B":
                    _pieceHolder = new Bishop(' ');
                    name = _pieceHolder.Piece;
                    break;
                case "N":
                    _pieceHolder = new Knight(' ');
                    name = _pieceHolder.Piece;
                    break;
                case "R":
                    _pieceHolder = new Rook(' ');
                    name = _pieceHolder.Piece;
                    break;
                case "P":
                    _pieceHolder = new Pawn(' ');
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
            string brush;
            switch (color)
            {
                case "l":
                    brush = "Light";
                    break;
                case "d":
                    brush = "Dark";
                    break;
                default:
                throw new Exception("Invalid color character...");
            }
            _pieceHolder.Color = color[0];
            return brush;
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
            switch (column)
            {
                case 'a':
                    y = 0;
                    break;
                case 'b':
                    y = 1;
                    break;
                case 'c':
                    y = 2;
                    break;
                case 'd':
                    y = 3;
                    break;
                case 'e':
                    y = 4;
                    break;
                case 'f':
                    y = 5;
                    break;
                case 'g':
                    y = 6;
                    break;
                case 'h':
                    y = 7;
                    break;
                default:
                    break;
            }
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
            Board[startMove[0], startMove[1]] = _pieceHolder;
            Console.WriteLine(Color + " " + _pieceHolder.Piece + " has been placed at " + square1 + ".");
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
            if(Board[startMove[0], startMove[1]].CheckMovement(Board, startMove, endMove) == true)
            {
                if (Board[startMove[0], startMove[1]].CheckSquare(Board, endMove))
                {
                    Board[startMove[0], startMove[1]].MovePiece(Board, startMove, endMove);
                    Console.WriteLine("The piece at " + square1 + " moved to " + square2 + ".");
                    PrintBoard(Board);
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
        /// <summary>
        /// Moves a piece from it's start location to the desired location and captures
        /// the piece.
        /// Prints out where the piece moved too, in plain english.
        /// </summary>
        /// <param name="square1">The starting point of the piece.</param>
        /// <param name="square2">The end point for the piece.</param>
        public void CapturePiece(string square1, string square2)
        {
            startMove = GrabPiece(square1[0], square1[1]);
            endMove = GrabPiece(square2[0], square2[1]);
            if (Board[startMove[0], startMove[1]].CheckMovement(Board, startMove, endMove) == true)
            {
                if (Board[startMove[0], startMove[1]].CheckSquare(Board, endMove))
                {
                    Board[startMove[0], startMove[1]].MovePiece(Board, startMove, endMove);
                    Console.WriteLine("The piece at " + square1 + " captured the piece at and moved to " + square2 + ".");
                    PrintBoard(Board);
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
            Board[endMove[0], endMove[1]] = Board[startMove[0], startMove[1]];
            Board[startMove[0], startMove[1]] = new Space();
            startMove = GrabPiece(square3[0], square3[1]);
            endMove = GrabPiece(square4[0], square4[1]);
            Board[endMove[0], endMove[1]] = Board[startMove[0], startMove[1]];
            Board[startMove[0], startMove[1]] = new Space();
            Console.WriteLine("King has moved from " + square1 + " to " + square2 + ", rook moved from " + square3 + " to " + square4 + ".");
        }
        #endregion
        
        #region SetBoard
        /// <summary>
        /// Creates a board of empty board squares and pieces to there rightful location and color.
        /// </summary>
        public void SetBoard()
        {
            //Sets Black & White Pieces
            Board[0, 4] = new King('d');//Kings
            Board[7, 4] = new King('l');//Kings
            Board[0, 3] = new Queen('d');//Queens
            Board[7, 3] = new Queen('l');//Queens
            for (int x = 2; x < 6; x+=3)
            {
                Board[0, x] = new Bishop('d');//Black Bishops
                Board[7, x] = new Bishop('l');//White Bishops
            }
            for (int x = 1; x < 7; x+=5)
            {
                Board[0, x] = new Knight('d');//Black Knight
                Board[7, x] = new Knight('l');//White Knight
            }
            for (int x = 0; x < 9; x+=7)
            {
                Board[0, x] = new Rook('d');//Black Rooks
                Board[7, x] = new Rook('l');//White Rooks
            }
            for (int x = 0; x < 8; x++)
            {
                Board[1, x] = new Pawn('d');//Black Pawns
                Board[6, x] = new Pawn('l');//White Pawns
            }
            //Empty squares
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x, y] == null)
                    {
                        Board[x, y] = new Space();
                    }
                }
            }
        }
        #endregion

        #region PrintBoard
        /// <summary>
        /// Prints out 8 array values before moving on to the next set of values.
        /// This will continue essentially creating a board.
        /// </summary>
        /// <param name="boardSqaures">Represents a board. Required size for the 2-D array is 8 rows and 8 columns.</param>
        public void PrintBoard(ChessPiece[,] boardSqaures)
        {
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (x % 2 == 0)
                    {
                        if (y % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                    }
                    else
                    {
                        if (y % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                    }
                    if (boardSqaures[x, y].Color == 'l')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (boardSqaures[x, y].Color == 'd')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write(" " + boardSqaures[x, y].Symbol + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
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

        #region Properties
        public int Place_Piece { get {return PLACE_PIECE; } }
        public int Move_Piece { get { return MOVE_PIECE; } }
        public int Capture_Piece { get { return CAPTURE_PIECE; } }
        public int King_Side_Piece { get { return KING_SIDE_PIECE; } }
        public string Piece { get {return _piece; } set {_piece = value; } }
        public string Color { get { return _color; } set { _color = value; } }
        public ChessPiece[,] Board { get {return _boardSquares; } set {_boardSquares= value; } }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}
