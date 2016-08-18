using Chess.ChessModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chess.ChessModels
{
    public class Controller
    {
        #region Variables
        //command lengths
        private const int PIECE_LENGTH = 2;
        private const int PLACE_PIECE = 4;
        private const int MOVE_PIECE = 5;
        private const int CAPTURE_PIECE = 6;//Is this even neccessary?
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
        //A string that represents a selected piece for the user.
        private string _startSquare;
        #endregion
        public Controller()
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

        #region Grab Column/Row
        /// <summary>
        /// Reads in a row number to convert it to a X-axis for a 2-D array .
        /// </summary>
        /// <param name="row">Row number from a chess board or the X coordinate of a 2-D array.</param>
        /// <returns>Returns an int with the row paremeter converted for a 2-D array.</returns>
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
        /// Reads in a column number to convert it to a Y-axis for a 2-D array .
        /// </summary>
        /// <param name="column">Column letter from a chess board or the Y cordinate of the 2-D array.</param>
        /// <returns>Returns an array with the column paremeter converted for a 2-D array.</returns>v
        public int GrabColumn(char column)
        {
            int y = 0;
            y = column - 97;
            return y;
        }
        #endregion

        #region Place/Move/Capture Piece
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
                Print();
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
        /// <param name="endSquare">A square on the board that the piece will move too.</param>
        public void MovePiece(string endSquare)
        {
            _endLoc.X = GrabRow(endSquare[1]);
            _endLoc.Y = GrabColumn(endSquare[0]);
            if ((int)Board.Squares[_startLoc.X, _startLoc.Y].Piece.Color == Turn)
            {
                if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.CheckMovement(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y) == true)
                {
                    Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                    Console.WriteLine("The piece at " + _startSquare + " moved to " + endSquare + ".");
                    PromotePawn();
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
        public void CapturePiece(string square1, string square2)//Is this even neccessary?
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
        public void KingSideCastle(string square1, string square2, string square3, string square4)//Refactoring should be done later.
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

        #region Process
        /// <summary>
        /// Processes the desired piece to move.
        /// If the input is really a piece, then it stores the string and prints out all moves for that piece.
        /// </summary>
        /// <param name="input">A string that represents a piece.</param>
        /// <param name="pattern">A pattern to interpret the piece.</param>
        public void ProcessPiece(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (match.Length == PIECE_LENGTH)
                {
                    _startSquare = match.Groups[1].Value + match.Groups[2].Value;
                    _startLoc.X = GrabRow(match.Groups[2].Value[0]);
                    _startLoc.Y = GrabColumn(match.Groups[1].Value[0]);
                    PrintPieceMovement(_startLoc.X, _startLoc.Y);
                }
            }
        }
        /// <summary>
        /// Reads the user's desired move for a selected piece and passes them to other methods 
        /// that will print them out in readable english.
        /// </summary>
        /// <param name="input">A string that represents a move for the selected piece.</param>
        /// <param name="pattern">A pattern to interpret the move.</param>
        public void ProcessMove(string input, string pattern)
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
                else if (match.Length == PIECE_LENGTH)
                {
                    MovePiece((match.Groups[1].Value + "" + match.Groups[2].Value));
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

        #region Print Turn/IsInCheck
        /// <summary>
        /// Prints out who's turn it is, 2 lines used to make the current player's turn more readable,
        /// the king's status, and the board itself.
        /// </summary>
        public void Print()
        {
            Console.WriteLine("<------------------------------>");
            Console.WriteLine("It's Player " + Turn + "'s turn!");
            Console.WriteLine("<------------------------------>");
            HasWon = IsInCheckMate(Turn);
            Board.PrintBoard();
        }
        #endregion

        #region PrintPieceMovement
        /// <summary>
        /// Prints out all movable pieces for the current player.
        /// </summary>
        /// <param name="turn">The current player's turn.</param>
        public void PrintMovablePieces(int turn)
        {
            List<int[]> piece = new List<int[]>();
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if ((int)Board.Squares[x, y].Piece.Color == turn)
                    {
                        List<int[]> movement = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
                        if (movement.Count != 0)
                        {
                            piece.Add(new int[] { x, y });
                        }
                    }
                }
            }
            for (int j = 0; j < piece.Count; ++j)
            {
                string[] stringPiece = GrabPiece(piece[j][0], piece[j][1]);
                Console.WriteLine("{0} {1}'s ({2}) can move.",
                            Board.Squares[piece[j][0], piece[j][1]].Piece.Color.ToString(),
                            Board.Squares[piece[j][0], piece[j][1]].Piece.Name,
                            (stringPiece[0] + stringPiece[1]));
            }
        }
        /// <summary>
        /// Prints out all legal movement of the selected piece.
        /// </summary>
        /// <param name="x">Row number from a 2-D Array.</param>
        /// <param name="y">Column number from a 2-D Array.</param>
        public void PrintPieceMovement(int x, int y)
        {
            List<int[]> movement = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
            string[] stringPiece = GrabPiece(x, y);
            for (int j = 0; j < movement.Count; ++j)
            {
                string[] stringMovement = GrabPiece(movement[j][0], movement[j][1]);//Word is equal to a move.
                Console.WriteLine("{0} {1}'s ({2}) movement: {3}{4}",
                            Board.Squares[x, y].Piece.Color.ToString(),
                            Board.Squares[x, y].Piece.Name,
                            (stringPiece[0] + stringPiece[1]),
                            stringMovement[0],
                            stringMovement[1]);
            }
        }
        #endregion

        #region Checks
        /// <summary>
        /// Grabs all available legal moves from every piece and checks if an
        /// available legal move from that piece can kill the opposite colored  king.
        /// Prints out the kings status.
        /// </summary>
        ///<returns>
        ///True: If a piece can move to kill a king.
        ///False: If no move can capture a king.
        /// </returns>
        public bool IsInCheck(int turn)
        {
            List<int[]> movements = new List<int[]>();//A storage for all possible legal move for the pieces.
            List<int[]> Pieces = new List<int[]>();
            int[] lKing = new int[2];//Light king's location.
            int[] dKing = new int[2];//Dark king's location.
            bool inCheck = false;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                    pieceLoc.X = x;
                    pieceLoc.Y = y;
                    List<int[]> placeHold = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                    for (int j = 0; j < placeHold.Count; ++j)
                    {
                        movements.Add(placeHold[j]);//Adds all possible legal movements from the current piece being looped through.
                        Pieces.Add(new int[] { pieceLoc.X, pieceLoc.Y });//Contains all piece's that have legal moves.
                    }
                    if (Board.Squares[x, y].Piece.GetType() == typeof(King))
                    {
                        if (Board.Squares[x, y].Piece.Color == ChessColor.LIGHT)
                        {
                            lKing = new int[] { x, y };//Stores the light king's location.
                        }
                        else
                        {
                            dKing = new int[] { x, y };//Stores the dark king's location.
                        }
                    }
                }
            }
            for (int x = 0; x < movements.Count; ++x)//Loops through all possible legal moves.
            {
                string[] word = GrabPiece(movements[x][0], movements[x][1]);//Word is equal to a move.

                if (movements[x][0] == lKing[0] && movements[x][1] == lKing[1])//If a move is the same as the light king's location.
                {
                    Console.WriteLine("Light King's Status: In danger!!!\n{0} {1}'s movement to {2}{3}, will capture {4} {5}.",
                                Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(),//Color of piece
                                Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Name,//Name of piece
                                word[0], word[1],//Piece's movement
                                Board.Squares[lKing[0], lKing[1]].Piece.Color.ToString(),//King's color
                                Board.Squares[lKing[0], lKing[1]].Piece.Name);//King's name
                    inCheck = true;
                }
                else if (movements[x][0] == dKing[0] && movements[x][1] == dKing[1])//If a move is the same as the dark king's location.
                {
                    Console.WriteLine("Dark King's Status: In danger!!!\n{0} {1}'s movement to {2}{3}, will capture {4} {5}.",
                                Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(),//Color of piece
                                Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Name,//Name of piece
                                word[0], word[1],//Piece's movement
                                Board.Squares[dKing[0], dKing[1]].Piece.Color.ToString(),//King's color
                                Board.Squares[dKing[0], dKing[1]].Piece.Name);//King's name
                    inCheck = true;
                }
            }
            return inCheck;
        }
        /// <summary>
        /// Checks if a piece can save the the same colored king from checkmate.
        /// </summary>
        /// <returns>
        /// True: If no piece can take the king out of check.
        /// False: If the king can be taken out of check.
        /// </returns>
        public bool CanBeSaved()
        {
            List<int[]> movements = new List<int[]>();//A storage for all possible legal move for the pieces.
            List<int[]> Pieces = new List<int[]>();//a list
            List<int[]> placeHold = new List<int[]>();//A placeholder for a piece's movements
            List<int[]> placeHold2 = new List<int[]>();//A placeholder for a piece's movements
            int[] lKing = new int[2];//Light king's location.
            int[] dKing = new int[2];//Dark king's location.
            bool inCheck = true;

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                    pieceLoc.X = x;
                    pieceLoc.Y = y;
                    placeHold = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                    for (int j = 0; j < placeHold.Count; ++j)
                    {
                        movements.Add(placeHold[j]);//Adds all possible legal movements from the current piece being looped through.
                        Pieces.Add(new int[] { pieceLoc.X, pieceLoc.Y });//Contains all piece's that have legal moves.
                    }
                    if (Board.Squares[x, y].Piece.GetType() == typeof(King))
                    {
                        if (Board.Squares[x, y].Piece.Color == ChessColor.LIGHT)
                        {
                            lKing = new int[] { x, y };//Stores the light king's location.
                        }
                        else
                        {
                            dKing = new int[] { x, y };//Stores the dark king's location.
                        }
                    }
                }
            }
            placeHold = new List<int[]>();
            for (int x = 0; x < movements.Count; ++x)//Loops through all possible legal moves.
            {
                if (movements[x][0] == lKing[0] && movements[x][1] == lKing[1])//If an enemy movement is the same as the light king's location.
                {
                    //Returns moves leading to the king's location.
                    placeHold = Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Test(Board.Squares, Pieces[x][0], Pieces[x][1], lKing[0], lKing[1]);
                    for (int v = 0; v < movements.Count; ++v)//Loops through all possible legal moves.
                    {
                        //If the king is the same color as a piece.
                        if (Board.Squares[lKing[0], lKing[1]].Piece.Color == Board.Squares[movements[v][0], movements[v][1]].Piece.Color)
                        {
                            //Returns moves that can protect the king from check mate.
                            placeHold2 = Board.Squares[movements[x][0], movements[x][1]].Piece.RestrictMovement(Board.Squares, movements[x][0], movements[x][1]);
                        }
                        for (int y = 0; y < placeHold2.Count; ++y)
                        {
                            for (int z = 0; z < placeHold.Count; ++z)
                            {
                                if (placeHold[z] == placeHold2[y])
                                {
                                    inCheck = false;
                                }
                            }
                        }
                    }
                }
                if (movements[x][0] == dKing[0] && movements[x][1] == dKing[1])//If an enemy movement is the same as the light dark's location.
                {
                    //Returns moves leading to the king's location.
                    placeHold = Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Test(Board.Squares, Pieces[x][0], Pieces[x][1], dKing[0], dKing[1]);
                    for (int v = 0; v < movements.Count; ++v)//Loops through all possible legal moves.
                    {
                        //If the king is the same color as a piece.
                        if (Board.Squares[dKing[0], dKing[1]].Piece.Color == Board.Squares[movements[v][0], movements[v][1]].Piece.Color)
                        {
                            //Returns moves that can protect the king from check mate.
                            placeHold2 = Board.Squares[movements[x][0], movements[x][1]].Piece.RestrictMovement(Board.Squares, movements[x][0], movements[x][1]);
                        }
                        for (int y = 0; y < placeHold2.Count; ++y)
                        {
                            for (int z = 0; z < placeHold.Count; ++z)
                            {
                                if (placeHold[z][0] == placeHold2[y][0] && placeHold[z][1] == placeHold2[y][1])
                                {
                                    inCheck = false;
                                }
                            }
                        }
                    }
                }
            }
            return inCheck;
        }
        /// <summary>
        /// Checks if a player is in check.
        /// If they are, then it determines if the king has no way to get out of check.
        /// Prints out checkmate if the king can not get out of check.
        /// </summary>
        /// <param name="turn">An int representing a player's turn.</param>
        public bool IsInCheckMate(int turn)
        {
            List<int[]> kingsMoves = new List<int[]>();
            List<int[]> enemyMoves = new List<int[]>();
            bool inCheck = IsInCheck(turn);
            bool checkmate = false;
            int[] king = new int[2];//The king(that is in check)'s location.
            if (inCheck == true)//If check has occurred.
            {
                inCheck = CanBeSaved();
                if (inCheck == true)//If the piece could be saved.
                {
                    for (int x = 0; x < 8; ++x)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (Board.Squares[x, y].Piece.GetType() == typeof(King))//Loops through every piece searching for the king in check.
                            {
                                if ((int)Board.Squares[x, y].Piece.Color == turn)//Makes sure the color of the king is that of the current player's turn.
                                {
                                    king = new int[] { x, y };//Stores the king's location.
                                }
                            }
                        }
                    }//End of the for loop
                    kingsMoves = Board.Squares[king[0], king[1]].Piece.RestrictMovement(Board.Squares, king[0], king[1]);//Stores the kings movements.
                    for (int x = 0; x < 8; ++x)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if ((int)Board.Squares[x, y].Piece.Color != turn)
                            {
                                Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                                pieceLoc.X = x;
                                pieceLoc.Y = y;
                                //Gets all possible legal moves for current enemy piece.
                                List<int[]> placeHold = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, pieceLoc.X, pieceLoc.Y);
                                for (int j = 0; j < placeHold.Count; ++j)
                                {
                                    enemyMoves.Add(placeHold[j]);//Adds all possible legal movements from the enemy piece being looped through.
                                }
                            }
                        }
                    }//End of the for loop
                    bool[] cantMove = new bool[kingsMoves.Count];//Sets a bool array length to the length of kingsMoves.
                    for (int j = 0; j < enemyMoves.Count; ++j)
                    {
                        for (int k = 0; k < kingsMoves.Count; ++k)
                        {
                            //Checks if any of the kings movements will make the king go back in check
                            if (enemyMoves[j][0] == kingsMoves[k][0] && enemyMoves[j][1] == kingsMoves[k][1])
                            {
                                cantMove[k] = true;
                            }
                        }
                    }//End of the for loop
                    int num = 0;
                    for (int z = 0; z < cantMove.Count(); ++z)
                    {
                        //Checks if all of kings moves will make him captured
                        if (cantMove[z] == true)
                        {
                            ++num;
                        }
                    }//End of the for loop
                    if (num == cantMove.Length)//If all the kings movements will cause the king to get captures.
                    {
                        Console.WriteLine("Checkmate!!!");
                        checkmate = true;
                    }
                }
            }
            return checkmate;
        }
        /// <summary>
        /// Reads in a column int and row int to convert and store it to an Array.
        /// </summary>
        /// <param name="column">Column number from a 2-D Array.</param>
        /// <param name="row">Row number from a 2-D Array.</param>
        /// <returns>Returns an array with column and row converted to chess cordinates.</returns>
        public string[] GrabPiece(int row, int column)
        {
            string x = "";//Columns
            string y = "";//Rows
            switch (row)
            {
                case 0:
                    y = "8";
                    break;
                case 1:
                    y = "7";
                    break;
                case 2:
                    y = "6";
                    break;
                case 3:
                    y = "5";
                    break;
                case 4:
                    y = "4";
                    break;
                case 5:
                    y = "3";
                    break;
                case 6:
                    y = "2";
                    break;
                case 7:
                    y = "1";
                    break;
                default:
                    break;
            }
            switch (column)
            {
                case 0:
                    x = "a";
                    break;
                case 1:
                    x = "b";
                    break;
                case 2:
                    x = "c";
                    break;
                case 3:
                    x = "d";
                    break;
                case 4:
                    x = "e";
                    break;
                case 5:
                    x = "f";
                    break;
                case 6:
                    x = "g";
                    break;
                case 7:
                    x = "h";
                    break;
                default:
                    break;
            }
            string[] rowColumns = new string[] { x, y };
            return rowColumns;
        }
        #endregion

        #region PawnPromtion
        /// <summary>
        /// Checks if the current square has a pawn.
        /// </summary>
        /// <param name="x">Row number from a chess board or the X coordinate of a 2-D array.</param>
        /// <param name="y">Column number from a chess board or the Y coordinate of a 2-D array.</param>
        /// <returns>
        /// True: If the square does contain a pawn.
        /// False: If the square does not contain a pawn.
        /// </returns>
        public bool Contains(int x, int y)
        {
            bool containsPawn = false;
            if(Board.Squares[x, y].Piece.GetType() == typeof(Pawn))
            {
                containsPawn = true;
            }
            else if (Board.Squares[x, y].Piece.GetType() == typeof(Pawn))
            {
                containsPawn = true;
            }
            return containsPawn;
        }
        /// <summary>
        /// Checks certain squares for a pawn. If a square does have a pawn, it will prompt the user to promote the pawn
        /// to another piece.
        /// </summary>
        public void PromotePawn()
        {
            bool containsPawn = false;
            int[] square = new int[2];
            for (int y = 0; y < 8; ++y)
            {
                containsPawn = Contains(0, y);
                if (containsPawn == true)
                {
                    square = new int[] { 0, y };
                    break;
                }
                containsPawn = Contains(7, y);
                if (containsPawn == true)
                {
                    square = new int[] { 7, y };
                    break;
                }
            }
            if (containsPawn)
            {
                Console.WriteLine("What's this???");
                Console.ReadLine();
                Console.WriteLine("{0} {1} is evolving...!",
                    Board.Squares[square[0], square[1]].Piece.Color,//The color of the pawn.
                    Board.Squares[square[0], square[1]].Piece.Name);//The name of the pawn.
                Console.ReadLine();
                ChessPiece newPiece = Promotion(Board.Squares[square[0], square[1]].Piece.Color);
                Board.Squares[square[0], square[1]].Piece = newPiece;
            }
        }
        /// <summary>
        /// Asks the user what piece they would like the pawn to be.
        /// It will then set a type chess piece to the desired piece.
        /// </summary>
        /// <param name="pawnColor">Color of the pawn being promoted.</param>
        /// <returns>returns a piece for the pawn.</returns>
        public ChessPiece Promotion(ChessColor pawnColor)
        {
            int promotion = 0;
            bool isValid = false;
            ChessPiece newPiece;
            while (isValid = false || (1 > promotion && promotion < 4))
            {
                Console.WriteLine(" 1) Queen\n 2) Bishop\n 3) Rook\n 4) knight");
                string choice = Console.ReadLine();
                isValid = int.TryParse(choice, out promotion);
            }
            if (promotion == 1)
            {
                newPiece = new Queen(pawnColor);
            }
            else if (promotion == 2)
            {
                newPiece = new Bishop(pawnColor);
            }
            else if(promotion == 3)
            {
                newPiece = new Rook(pawnColor);
            }
            else
            {
                newPiece = new Knight(pawnColor);
            }
            return newPiece;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Command Length of 2.
        /// </summary>
        public int Piece_Length { get { return PIECE_LENGTH; } }
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
        public int Capture_Piece { get { return CAPTURE_PIECE; } }//Is this even neccessary?
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
        public bool HasWon { get; set; }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}