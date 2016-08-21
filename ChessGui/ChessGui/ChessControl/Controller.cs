using Chess.ChessModels;
using ChessGui.ChessView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess.ChessControl
{
    public class Controller
    {
        #region Variables
        //command lengths
        private const int PIECE_LENGTH = 2;
        //Total squares on the board
        private const int PIECE_COUNT = 32;
        //The Chess Board
        private ChessBoard _board;
        //Reprents the start location of a piece and the end location for a piece being moved.
        private Location _startLoc;
        private Location _endLoc;
        //Player's turn by defaults starts at 1.
        private int _turn;
        //A string that represents a selected piece for the user.
        private string _startSquare;
        private UniformGrid g;
        private UniformGrid g2;
        private Label l;
        private Square _sq;
        private ChessPiece newPiece;
        private ChessColor pawnColor;
        private int _promotion;
        private int[] square;
        #endregion
        public Controller()
        {
            _board = new ChessBoard();
            _startLoc = new Location();
            _endLoc = new Location();
            _turn = 1;
            _sq = null;
            _promotion = 0;
        }

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
        /// If the color of the desired piece is in line with the current player's turn,
        /// then it checks the movements of the piece to see if the new location for
        /// the piece is a legal move.
        /// 
        /// Moves a piece from it's start location to the desired location.
        /// Prints out where the piece moved too, in plain english.
        /// </summary>
        /// <param name="endSquare">A square on the board that the piece will move too.</param>
        public void MovePiece(UniformGrid grid, string endSquare)
        {
            _endLoc.X = GrabRow(endSquare[1]);
            _endLoc.Y = GrabColumn(endSquare[0]);
            if ((int)Board.Squares[_startLoc.X, _startLoc.Y].Piece.Color == Turn)
            {
                RemovePieceMovement(grid, _startLoc.X, _startLoc.Y);
                if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.CheckMovement(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y) == true)
                {
                    if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                    {
                        ChangeTurn();
                        IsInCheckMate(Turn);
                    }
                    else
                    {
                        ChangeTurn();
                    }
                    Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                    CheckKingSideCastle();
                    EnPassant(Board.Squares, _endLoc.X, _endLoc.Y);
                    l.Content = ("The piece at " + _startSquare + " moved to " + endSquare + ".");
                    Image img = (Image)((Square)grid.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic;
                    ((Square)grid.Children[(_startLoc.X * 8) + _startLoc.Y]).Panel.Children.Clear();
                    ((Square)grid.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic = null;
                    ((Square)grid.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Clear();
                    ((Square)grid.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Add(img);
                    ((Square)grid.Children[(_endLoc.X * 8) + _endLoc.Y]).Pic = img;
                    ResetSquares(_sq, _sq.loc.X, _sq.loc.Y);
                    _sq = null;
                    IsInCheck(Turn);
                    BeginPromotion();

                }
                else
                {
                    PrintPieceMovement(grid, _startLoc.X, _startLoc.Y);
                    MessageBox.Show("Invalid piece movement, please try again...");
                }
            }
            else
            {
                MessageBox.Show("Invalid movement, please try again.");
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
        public void CheckKingSideCastle()//Refactoring should be done later.
        {
            if (Turn == (int)ChessColor.LIGHT)
            {
                if (Board.Squares[7, 6].Piece.GetType() == typeof(King))//Right
                {
                    if (((King)Board.Squares[7, 6].Piece).moveAmount == 1)//Right
                    {
                        int startX = Board.Squares[7, 7].Loc.X;//Left
                        int startY = Board.Squares[7, 7].Loc.Y;
                        Board.Squares[7, 7].Piece.MovePiece(Board.Squares,startX, startY, startX, startY - 2);

                        Image img = (Image)((Square)g.Children[(startX * 8) + startY]).Pic;
                        ((Square)g.Children[(startX * 8) + startY]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + startY]).Pic = null;
                        ((Square)g.Children[(startX * 8) + (startY - 2)]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + (startY - 2)]).Panel.Children.Add(img);
                        ((Square)g.Children[(startX * 8) + (startY - 2)]).Pic = img;
                    }
                }
                else if (Board.Squares[7, 2].Piece.GetType() == typeof(King))//Left
                {
                    if (((King)Board.Squares[7, 2].Piece).moveAmount == 1)//Left
                    {
                        int startX = Board.Squares[7, 0].Loc.X;//Right
                        int startY = Board.Squares[7, 0].Loc.Y;
                        Board.Squares[7, 0].Piece.MovePiece(Board.Squares, startX, startY, startX, startY + 3);

                        Image img = (Image)((Square)g.Children[(startX * 8) + startY]).Pic;
                        ((Square)g.Children[(startX * 8) + startY]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + startY]).Pic = null;
                        ((Square)g.Children[(startX * 8) + (startY + 3)]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + (startY + 3)]).Panel.Children.Add(img);
                        ((Square)g.Children[(startX * 8) + (startY + 3)]).Pic = img;
                    }
                }
            }
            else if(Turn == (int)ChessColor.DARK)
            {
                if (Board.Squares[0, 6].Piece.GetType() == typeof(King))//Right
                {
                    if (((King)Board.Squares[0, 6].Piece).moveAmount == 1)//Right
                    {
                        int startX = Board.Squares[0, 7].Loc.X;//Left
                        int startY = Board.Squares[0, 7].Loc.Y;
                        Board.Squares[0, 7].Piece.MovePiece(Board.Squares, startX, startY, startX, startY - 2);

                        Image img = (Image)((Square)g.Children[(startX * 8) + startY]).Pic;
                        ((Square)g.Children[(startX * 8) + startY]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + startY]).Pic = null;
                        ((Square)g.Children[(startX * 8) + (startY - 2)]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + (startY - 2)]).Panel.Children.Add(img);
                        ((Square)g.Children[(startX * 8) + (startY - 2)]).Pic = img;
                    }
                }
                else if (Board.Squares[0, 2].Piece.GetType() == typeof(King))//Left
                {
                    if (((King)Board.Squares[0, 2].Piece).moveAmount == 1)//Left
                    {
                        int startX = Board.Squares[0, 0].Loc.X;//Right
                        int startY = Board.Squares[0, 0].Loc.Y;
                        Board.Squares[0, 0].Piece.MovePiece(Board.Squares, startX, startY, startX, startY + 3);

                        Image img = (Image)((Square)g.Children[(startX * 8) + startY]).Pic;
                        ((Square)g.Children[(startX * 8) + startY]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + startY]).Pic = null;
                        ((Square)g.Children[(startX * 8) + (startY + 3)]).Panel.Children.Clear();
                        ((Square)g.Children[(startX * 8) + (startY + 3)]).Panel.Children.Add(img);
                        ((Square)g.Children[(startX * 8) + (startY + 3)]).Pic = img;
                    }
                }
            }
        }
        #endregion

        #region Process
        /// <summary>
        /// Processes the desired piece to move.
        /// If the input is really a piece, then it stores the string and prints out all moves for that piece.
        /// </summary>
        /// <param name="input">A string that represents a piece.</param>
        /// <param name="pattern">A pattern to interpret the piece.</param>
        public void ProcessPiece(UniformGrid grid, string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (match.Length == PIECE_LENGTH)
                {
                    _startSquare = match.Groups[1].Value + match.Groups[2].Value;
                    _startLoc.X = GrabRow(match.Groups[2].Value[0]);
                    _startLoc.Y = GrabColumn(match.Groups[1].Value[0]);
                    PrintPieceMovement(grid, _startLoc.X, _startLoc.Y);
                }
            }
        }
        /// <summary>
        /// Reads the user's desired move for a selected piece and passes them to other methods 
        /// that will print them out in readable english.
        /// </summary>
        /// <param name="input">A string that represents a move for the selected piece.</param>
        /// <param name="pattern">A pattern to interpret the move.</param>
        public void ProcessMove(UniformGrid grid, string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (match.Length == PIECE_LENGTH)
                {
                    MovePiece(grid, (match.Groups[1].Value + "" + match.Groups[2].Value));
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

        #region PrintPieceMovement
        /// <summary>
        /// Prints out all legal movement of the selected piece.
        /// </summary>
        /// <param name="x">Row number from a 2-D Array.</param>
        /// <param name="y">Column number from a 2-D Array.</param>
        public void PrintPieceMovement(UniformGrid grid, int x, int y)
        {
            List<int[]> movement;
            //if (Board.Squares[x, y].Piece.GetType() != typeof(King))
            //{
                movement = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
            //}
            //else
            //{
            //    movement = ((King)Board.Squares[x, y].Piece).AvoidCheck(Board.Squares, x, y);
            //}
            string[] stringPiece = GrabPiece(x, y);
            for (int j = 0; j < movement.Count; ++j)
            {
                string[] stringMovement = GrabPiece(movement[j][0], movement[j][1]);//Word is equal to a move.
                if (Board.Squares[movement[j][0], movement[j][1]].Piece.Color == ChessColor.NONE)
                {
                    ((Square)grid.Children[(movement[j][0] * 8) + movement[j][1]]).Background = Brushes.LightGreen;
                }
                if (Board.Squares[movement[j][0], movement[j][1]].Piece.Color != ChessColor.NONE)
                {
                    if ((int)Board.Squares[movement[j][0], movement[j][1]].Piece.Color != Turn)
                    {
                        ((Square)grid.Children[(movement[j][0] * 8) + movement[j][1]]).Background = Brushes.LightSalmon;
                    }
                }
            }
            movement = null;
        }

        public void RemovePieceMovement(UniformGrid grid, int x, int y)
        {
            List<int[]> movement = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
            string[] stringPiece = GrabPiece(x, y);
            for (int j = 0; j < movement.Count; ++j)
            {
                string[] stringMovement = GrabPiece(movement[j][0], movement[j][1]);//Word is equal to a move.
                if (Board.Squares[movement[j][0], movement[j][1]].Color == ChessColor.DARK)
                {
                    ((Square)grid.Children[(movement[j][0] * 8) + movement[j][1]]).Background = Brushes.Gray;
                }
                else if (Board.Squares[movement[j][0], movement[j][1]].Color == ChessColor.LIGHT)
                {
                    ((Square)grid.Children[(movement[j][0] * 8) + movement[j][1]]).Background = Brushes.LightGray;
                }
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
                    MessageBox.Show("Light King is In danger!!!", "Check");
                    inCheck = true;
                }
                else if (movements[x][0] == dKing[0] && movements[x][1] == dKing[1])//If a move is the same as the dark king's location.
                {
                    MessageBox.Show("Dark King is In danger!!!", "Check");
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
        public bool CantBeSaved()
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
                    placeHold = Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Search(Board.Squares, Pieces[x][0], Pieces[x][1], lKing[0], lKing[1]);
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
                if (movements[x][0] == dKing[0] && movements[x][1] == dKing[1])//If an enemy movement is the same as the dark king's location.
                {
                    //Returns moves leading to the king's location.
                    placeHold = Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Search(Board.Squares, Pieces[x][0], Pieces[x][1], dKing[0], dKing[1]);
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
        public void IsInCheckMate(int turn)
        {
            List<int[]> kingsMoves = new List<int[]>();
            List<int[]> enemyMoves = new List<int[]>();
            bool inCheck = IsInCheck(turn);
            int[] king = new int[2];//The king(that is in check)'s location.
            if (inCheck == true)//If check has occurred.
            {
                inCheck = CantBeSaved();
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
                        PrintWinner();
                    }
                }
            }
        }
        /// <summary>
        /// Reads in a column int and row int to convert and store it to an Array.
        /// </summary>
        /// <param name="column">Column number from a 2-D Array.</param>
        /// <param name="row">Row number from a 2-D Array.</param>
        /// <returns>Returns an array with column and row converted to chess cordinates.</returns>
        public string[] GrabPiece(int row, int column)
        {
            char x = ' ';//Columns
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

            x = (char)(column + 97);
            string[] rowColumns = new string[] { x.ToString(), y };
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
        public void BeginPromotion()
        {
            bool containsPawn = false;
            square = new int[2];
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
                pawnColor = Board.Squares[square[0], square[1]].Piece.Color;
                SetUpPromotion();
            }
        }
        /// <summary>
        /// Asks the user what piece they would like the pawn to be.
        /// It will then set a type chess piece to the desired piece.
        /// </summary>
        /// <param name="pawnColor">Color of the pawn being promoted.</param>
        /// <returns>returns a piece for the pawn.</returns>
        public void SetUpPromotion()
        {
            g.IsEnabled = false;
            for (int x = 0; x < 5; ++x)
            {
                g2.Children[x].Visibility = Visibility.Visible;
            }
        }
        #endregion

        public void CreateBoard(UniformGrid grid, Label label)
        {
            g = grid;
            l = label;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    Square s = new Square(Board.Squares[x, y].Piece.ToString());
                    ResetSquares(s, x, y);
                    s.loc.X = x;
                    s.loc.Y = y;
                    s.Click += Button_PieceHandler;
                    s.MouseRightButtonDown += Button_RightClickHandler;
                    grid.Children.Add(s);
                }
            }
        }
        
        private void Button_PieceHandler(object sender, RoutedEventArgs e)
        {
            Square s = (Square)sender;
            
            if (_sq == null)
            {
                if (Board.Squares[s.loc.X, s.loc.Y].Piece.GetType() != typeof(Space))
                {
                    if ((int)Board.Squares[s.loc.X, s.loc.Y].Piece.Color == Turn)
                    {
                        string[] input = GrabPiece(s.loc.X, s.loc.Y);
                        s.Background = Brushes.CornflowerBlue;
                        ProcessPiece((UniformGrid)s.Parent, (input[0] + input[1]), @"([a-h])([1-8])");
                        _sq = s;
                    }
                    else
                    {
                        MessageBox.Show("Please select a piece with the correct color!!!");
                    }
                }
            }
            else
            {
                string[] input = GrabPiece(s.loc.X, s.loc.Y);
                ProcessMove((UniformGrid)s.Parent, (input[0] + input[1]), @"([a-h])([1-8])");
                ResetEnPassant();
                IfKingsExist();
            }
        }
        
        public void EnPassant(ChessSquare[,] board, int x1, int y1)
        {
            if (board[x1, y1].Piece.GetType() == typeof(Pawn))
            {
                if ((x1 + 1) < 8)
                {
                    if (board[x1, y1].Piece.Color == ChessColor.LIGHT)
                    {
                        if (board[x1 + 1, y1].Piece.GetType() == typeof(Pawn))
                        {
                            MoveEnPassant(g, Board.Squares, x1 + 1, y1);
                        }
                    }
                }
                if ((x1 - 1) >= 0)
                {
                    if (board[x1, y1].Piece.Color == ChessColor.DARK)
                    {
                        if (board[x1 - 1, y1].Piece.GetType() == typeof(Pawn))
                        {
                            MoveEnPassant(g, Board.Squares, x1 - 1, y1);
                        }
                    }
                }
            }
        }

        public void MoveEnPassant(UniformGrid grid, ChessSquare[,] board, int x, int y)
        {
            board[x, y].Piece = new Space();
            ((Square)grid.Children[(x * 8) + y]).Panel.Children.Clear();
            ((Square)grid.Children[(x * 8) + y]).Pic = null;
        }

        public void ResetEnPassant()
        {
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (Board.Squares[x, y].Piece.GetType() == typeof(Pawn))
                    {
                        if ((int)Board.Squares[x, y].Piece.Color == Turn)
                        {
                            if (((Pawn)Board.Squares[x, y].Piece).chance == true)
                            {
                                ((Pawn)Board.Squares[x, y].Piece).chance = false;
                            }
                        }
                    }
                }
            }
        }
        
        private void Button_RightClickHandler(object sender, RoutedEventArgs e)
        {
            Square s = (Square)sender;
            if (_sq != null)
            {
                ResetSquares(_sq, _sq.loc.X, _sq.loc.Y);
                RemovePieceMovement((UniformGrid)_sq.Parent, _sq.loc.X, _sq.loc.Y);
                _sq = null;
            }
        }

        public void ResetSquares(Square s, int x, int y)
        {
            if (Board.Squares[x, y].Color == ChessColor.DARK)
            {
                s.Background = Brushes.Gray;
            }
            else if (Board.Squares[x, y].Color == ChessColor.LIGHT)
            {
                s.Background = Brushes.LightGray;
            }
        }
        
        public void ResetGame()
        {
            _board = new ChessBoard();
            g.Children.Clear();
            CreateBoard(g, l);
            _startLoc = new Location();
            _endLoc = new Location();
            ChangeTurn();
            _startSquare = null;
            _sq = null;
        }

        public void PrintWinner()
        {
            if (Turn == 1)
            {
                MessageBox.Show((Turn + 1) + "'s player wins!!!", "Checkmate!!!");
            }
            else
            {
                MessageBox.Show((Turn - 1) + "'s player wins!!!", "Checkmate!!!");
            }
            //************
            MessageBoxResult msg;
            msg = MessageBox.Show("Would you like to play again?", "Chess", MessageBoxButton.YesNo);
            if (msg == MessageBoxResult.Yes)
            {
                ResetGame();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        public void IfKingsExist()
        {
            bool lightExist = false;
            bool darkExist = false;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (Board.Squares[x, y].Piece.GetType() == typeof(King))
                    {
                        if (Board.Squares[x, y].Piece.Color == ChessColor.DARK)
                        {
                            darkExist = true;
                        }
                        else
                        {
                            lightExist = true;
                        }
                    }
                }
            }
            if(lightExist == false)
            {
                PrintWinner();
            }
            else if (darkExist == false)
            {
                PrintWinner();
            }
        }

        public void SetButtons(UniformGrid grid)
        {
            g2 = grid;
            for (int x = 1; x < 5; ++x)
            {
                ((Button)g2.Children[x]).Click += Button_PromoteHandler;
            }
        }

        private void Button_PromoteHandler(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if(b.Name == "Queen")
            {
                _promotion = 1;
            }
            else if (b.Name == "Bishop")
            {
                _promotion = 2;
            }
            else if (b.Name == "Rook")
            {
                _promotion = 3;
            }
            else if (b.Name == "Knight")
            {
                _promotion = 4;
            }

            if (_promotion == 1)
            {
                newPiece = new Queen(pawnColor);
            }
            else if (_promotion == 2)
            {
                newPiece = new Bishop(pawnColor);
            }
            else if (_promotion == 3)
            {
                newPiece = new Rook(pawnColor);
            }
            else if (_promotion == 4)
            {
                newPiece = new Knight(pawnColor);
            }

            if (newPiece != null)
            {
                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.UriSource = new Uri("Images/" + newPiece.ToString(), UriKind.RelativeOrAbsolute);
                source.EndInit();

                Image img = new Image();
                img.Source = source;

                ((Square)g.Children[((square[0] * 8) + square[1])]).Panel.Children.Clear();
                ((Square)g.Children[((square[0] * 8) + square[1])]).Panel.Children.Add(img);
                ((Square)g.Children[((square[0] * 8) + square[1])]).Pic = img;
                Board.Squares[square[0], square[1]].Piece = newPiece;
            }
            for (int x = 0; x < 5; ++x)
            {
                g2.Children[x].Visibility = Visibility.Hidden;
            }
            g.IsEnabled = true;
        }
        
        #region Properties
        /// <summary>
        /// Command Length of 2.
        /// </summary>
        public int Piece_Length { get { return PIECE_LENGTH; } }
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