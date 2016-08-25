using Chess.ChessModels;
using ChessGui.ChessView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        //Grid that represents the board.
        private UniformGrid g;
        //Grid that represents the promote spot.
        private UniformGrid g2;
        //Label that represents the movement of each peice
        private Label l;
        //Label that represents the turn of the current player.
        private Label l2;
        //A square on the board that was selected.
        private Square _selectedSquare;
        //
        private ChessPiece newPiece;
        private ChessColor pawnColor;
        private int _promotion;
        private int[] square;
        //If king is in check.
        private bool inCheck;
        List<int[]> a = new List<int[]>();
        #endregion
        public Controller()
        {
            _board = new ChessBoard();
            _startLoc = new Location();
            _endLoc = new Location();
            _turn = 1;
            _selectedSquare = null;
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

        #region Process Piece/Move
        /// <summary>
        /// Processes the desired piece to move.
        /// If the input is really a piece, then it stores the piece's location and highlights
        /// all moves for the current selected piece.
        /// </summary>
        /// <param name="board">A grid representing the board.</param>
        /// <param name="input">A string that represents a piece.</param>
        /// <param name="pattern">A pattern to interpret the piece.</param>
        public void ProcessPiece(UniformGrid board, string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (match.Length == PIECE_LENGTH)
                {
                    if (inCheck != true)
                    {
                        _startSquare = match.Groups[1].Value + match.Groups[2].Value;
                        _startLoc.X = GrabRow(match.Groups[2].Value[0]);
                        _startLoc.Y = GrabColumn(match.Groups[1].Value[0]);
                        PrintPieceMovement(board, _startLoc.X, _startLoc.Y);
                    }
                    else
                    {
                        _startSquare = match.Groups[1].Value + match.Groups[2].Value;
                        _startLoc.X = GrabRow(match.Groups[2].Value[0]);
                        _startLoc.Y = GrabColumn(match.Groups[1].Value[0]);
                        CanSaveKing(_startLoc.X, _startLoc.Y);
                    }
                }
            }
        }
        /// <summary>
        /// Reads the user's desired move for a selected piece.
        /// </summary>
        /// <param name="board">A grid representing the board.</param>
        /// <param name="input">A string that represents a move for the selected piece.</param>
        /// <param name="pattern">A pattern to interpret the move.</param>
        public void ProcessMove(UniformGrid board, string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (match.Length == PIECE_LENGTH)
                {
                    MovePiece(board, (match.Groups[1].Value + "" + match.Groups[2].Value));
                }
            }
        }
        #endregion

        #region Move/Castling
        /// <summary>
        /// Checks if the movements of the piece is a valid movement.
        /// 
        /// If it is Valid, it moves the piece to the desired location.
        /// It checks for castleing, and En Passant.
        /// Updates a label where everything describing the movement in readable english.
        /// Changes the image from the old location to the new location.
        /// Removes the highlight for the selected piece.
        /// Removes the highlighted squares that show where the piece can move.
        /// Deselects the selected piece.
        /// Checks for pawn promotion.
        /// Checks if the for checkmate if the selected piece not a king and changes turn.
        /// </summary>
        /// <param name="endSquare">A square on the board that the piece will move too.</param>
        public void MovePiece(UniformGrid board, string endSquare)
        {
            _endLoc.X = GrabRow(endSquare[1]);
            _endLoc.Y = GrabColumn(endSquare[0]);

            RemovePieceMovement(board, _startLoc.X, _startLoc.Y);

            if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.CheckMovement(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y) == true)
            {
                if (inCheck == false)//*****************************************************
                {
                    if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                    {
                        bool isValid = WillKingCheck(_startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                        if (isValid == false)
                        {
                            Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                            CheckKingSideCastle();
                            l.Content = ("The piece at " + _startSquare + " moved to " + endSquare + ".");
                            Image img = (Image)((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic;
                            ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Panel.Children.Clear();
                            ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic = null;
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Clear();
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Add(img);
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Pic = img;
                            SetDefaultBoardSquareColors(_selectedSquare, _selectedSquare.loc.X, _selectedSquare.loc.Y);
                            _selectedSquare = null;
                            CheckEnPassant(Board.Squares, _endLoc.X, _endLoc.Y);
                            CheckPawnPromotion();
                            if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                            {
                                ChangeTurn();
                                IsInCheckMate(Turn);
                            }
                            else
                            {
                                ChangeTurn();
                            }
                            MovablePieces();
                        }
                        else
                        {
                            PrintPieceMovement(board, _startLoc.X, _startLoc.Y);
                            MessageBox.Show("Don't put the king check!!!");
                        }
                    }
                    else
                    {
                        bool isValid = KingInCheck(_endLoc.X, _endLoc.Y);
                        if(isValid == true)
                        {
                            Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                            CheckKingSideCastle();
                            l.Content = ("The piece at " + _startSquare + " moved to " + endSquare + ".");
                            Image img = (Image)((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic;
                            ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Panel.Children.Clear();
                            ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic = null;
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Clear();
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Add(img);
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Pic = img;
                            SetDefaultBoardSquareColors(_selectedSquare, _selectedSquare.loc.X, _selectedSquare.loc.Y);
                            _selectedSquare = null;
                            if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                            {
                                ChangeTurn();
                                IsInCheckMate(Turn);
                            }
                            else
                            {
                                ChangeTurn();
                            }
                            MovablePieces();
                        }
                        else
                        {
                            PrintPieceMovement(board, _startLoc.X, _startLoc.Y);
                            MessageBox.Show("Don't go back in check!!!");
                        }
                    }
                }
                else//*****************************************************
                {
                    if (WillSaveTheKing(_startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y) == true)
                    {
                        if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                        {
                            Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                            l.Content = ("The piece at " + _startSquare + " moved to " + endSquare + ".");
                            Image img = (Image)((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic;
                            ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Panel.Children.Clear();
                            ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic = null;
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Clear();
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Add(img);
                            ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Pic = img;
                            SetDefaultBoardSquareColors(_selectedSquare, _selectedSquare.loc.X, _selectedSquare.loc.Y);
                            _selectedSquare = null;
                            CheckEnPassant(Board.Squares, _endLoc.X, _endLoc.Y);
                            CheckPawnPromotion();
                            if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                            {
                                ChangeTurn();
                                IsInCheckMate(Turn);
                            }
                            else
                            {
                                ChangeTurn();
                            }
                            MovablePieces();
                        }
                        else
                        {
                            bool isValid = KingInCheck(_endLoc.X, _endLoc.Y);
                            if (isValid == true)
                            {
                                Board.Squares[_startLoc.X, _startLoc.Y].Piece.MovePiece(Board.Squares, _startLoc.X, _startLoc.Y, _endLoc.X, _endLoc.Y);
                                CheckKingSideCastle();
                                l.Content = ("The piece at " + _startSquare + " moved to " + endSquare + ".");
                                Image img = (Image)((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic;
                                ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Panel.Children.Clear();
                                ((Square)board.Children[(_startLoc.X * 8) + _startLoc.Y]).Pic = null;
                                ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Clear();
                                ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Panel.Children.Add(img);
                                ((Square)board.Children[(_endLoc.X * 8) + _endLoc.Y]).Pic = img;
                                SetDefaultBoardSquareColors(_selectedSquare, _selectedSquare.loc.X, _selectedSquare.loc.Y);
                                _selectedSquare = null;
                                if (Board.Squares[_startLoc.X, _startLoc.Y].Piece.GetType() != typeof(King))
                                {
                                    ChangeTurn();
                                    IsInCheckMate(Turn);
                                }
                                else
                                {
                                    ChangeTurn();
                                }
                                MovablePieces();
                            }
                            else
                            {
                                PrintPieceMovement(board, _startLoc.X, _startLoc.Y);
                                MessageBox.Show("Don't go back in check!!!");
                            }
                        }
                    }
                    else
                    {
                        CanSaveKing(_startLoc.X, _startLoc.Y);
                        MessageBox.Show("Save your king!!!");
                        MovablePieces();
                    }
                }
            }
            else
            {
                PrintPieceMovement(board, _startLoc.X, _startLoc.Y);
                MessageBox.Show("Invalid piece movement, please try again...");
            }
        }
        /// <summary>
        /// Checks if the current turn is that of a specific color.
        /// If so, then checks if the king is in the position of doing left or right caslting.
        /// If so, it checks if the king has move once.
        /// If so, then it checks if a specific square contains a rook.
        /// If so, then move rook to perform castleing.
        /// </summary>
        public void CheckKingSideCastle()
        {
            if (Turn == (int)ChessColor.LIGHT)
            {
                if (Board.Squares[7, 6].Piece.GetType() == typeof(King))//Right
                {
                    if (((King)Board.Squares[7, 6].Piece).moveAmount == 1)//Right
                    {
                        if(Board.Squares[7, 7].Piece.GetType() == typeof(Rook))
                        {
                            int startX = Board.Squares[7, 7].Loc.X;//Left
                            int startY = Board.Squares[7, 7].Loc.Y;
                            Board.Squares[7, 7].Piece.MovePiece(Board.Squares, startX, startY, startX, startY - 2);

                            Image img = (Image)((Square)g.Children[(startX * 8) + startY]).Pic;
                            ((Square)g.Children[(startX * 8) + startY]).Panel.Children.Clear();
                            ((Square)g.Children[(startX * 8) + startY]).Pic = null;
                            ((Square)g.Children[(startX * 8) + (startY - 2)]).Panel.Children.Clear();
                            ((Square)g.Children[(startX * 8) + (startY - 2)]).Panel.Children.Add(img);
                            ((Square)g.Children[(startX * 8) + (startY - 2)]).Pic = img;
                        }
                    }
                }
                else if (Board.Squares[7, 2].Piece.GetType() == typeof(King))//Left
                {
                    if (((King)Board.Squares[7, 2].Piece).moveAmount == 1)//Left
                    {
                        if (Board.Squares[7, 0].Piece.GetType() == typeof(Rook))
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
            }
            else if(Turn == (int)ChessColor.DARK)
            {
                if (Board.Squares[0, 6].Piece.GetType() == typeof(King))//Right
                {
                    if (((King)Board.Squares[0, 6].Piece).moveAmount == 1)//Right
                    {
                        if (Board.Squares[0, 7].Piece.GetType() == typeof(Rook))
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
                }
                else if (Board.Squares[0, 2].Piece.GetType() == typeof(King))//Left
                {
                    if (((King)Board.Squares[0, 2].Piece).moveAmount == 1)//Left
                    {
                        if (Board.Squares[0, 0].Piece.GetType() == typeof(Rook))
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
        }
        /// <summary>
        /// Checks if the movement of a king will put it back in check.
        /// </summary>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns>
        /// True: If the movement of the king will not put the king in check.
        /// False: If the movement of the king will put the king in check
        /// </returns>
        public bool KingInCheck(int endX, int endY)
        {
            bool isValid = true;
            ChessPiece pieceHolder = Board.Squares[endX, endY].Piece;
            Board.Squares[endX, endY].Piece = new Space();
            List<int[]> enemyMovements = StoreAllEnemyMovements();//Get all enemy moves

            for (int x = 0; x < enemyMovements.Count; ++x)
            {
                if (endX == enemyMovements[x][0] && endY == enemyMovements[x][1])
                {
                    isValid = false;
                }
            }
            Board.Squares[endX, endY].Piece = pieceHolder;
            return isValid;
        }
        public bool WillKingCheck(int startX, int startY, int endX, int endY)
        {
            bool isValid = false;
            int[] king = new int[2];
            //********
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if((int)Board.Squares[x, y].Piece.Color == Turn)
                    {
                        if (Board.Squares[x, y].Piece is King)
                        {
                            king = new int[] { x, y };
                        }
                    }
                }
            }
            //********
            ChessPiece pieceHolder = Board.Squares[startX, startY].Piece;
            Board.Squares[endX, endY].Piece = Board.Squares[startX, startY].Piece;
            Board.Squares[startX, startY].Piece = new Space();

            List<int[]> enemyMovements = StoreAllEnemyMovements();//Get all enemy moves

            for (int x = 0; x < enemyMovements.Count; ++x)
            {
                if (king[0] == enemyMovements[x][0] && king[1] == enemyMovements[x][1])
                {
                    isValid = true;
                }
            }
            Board.Squares[startX, startY].Piece = pieceHolder;
            Board.Squares[endX, endY].Piece = new Space();
            return isValid;
        }
        #endregion

        #region SaveKingFromCheck
        /// <summary>
        /// If the piece contains a movement that can save the king, then the piece if selected,
        /// will have it's movements that will save the king highlighted.
        /// </summary>
        /// <param name="j"></param>
        /// <param name="k"></param>
        public void CanSaveKing(int j, int k)
        {
            List<int[]> enemyMovement;
            List<int[]> allyMovement = new List<int[]>();
            List<int[]> kingMovement = new List<int[]>();
            List<int[]> ally = new List<int[]>();
            List<int[]> movement = new List<int[]>();
            Location l = new Location();
            Location l2 = new Location();
            enemyMovement = StoreEnemyMovements();
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if ((int)Board.Squares[x, y].Piece.Color == Turn)
                    {
                        if (Board.Squares[x, y].Piece.GetType() != typeof(King))
                        {
                            List<int[]> temp = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
                            for (int z = 0; z < temp.Count; ++z)
                            {
                                for (int w = 0; w < enemyMovement.Count; ++w)
                                {
                                    if (temp[z][0] == enemyMovement[w][0] && temp[z][1] == enemyMovement[w][1])
                                    {
                                        ally.Add(new int[] { x, y });
                                        allyMovement.Add(temp[z]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<int[]> kingMoves = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
                            for (int z = 0; z < kingMoves.Count; ++z)
                            {
                                for (int w = 0; w < enemyMovement.Count; ++w)
                                {
                                    //if (kingMoves[z][0] == enemyMovement[w][0] && kingMoves[z][1] == enemyMovement[w][1])
                                    //{
                                    //    if (Board.Squares[enemyMovement[w][0], enemyMovement[w][1]].Piece.GetType() != typeof(Space))
                                    //    {
                                    //        ((Square)g.Children[(kingMoves[z][0] * 8) + kingMoves[z][1]]).Background = Brushes.LightYellow;
                                    //    }
                                    //}
                                    /*else*/ if (kingMoves[z][0] != enemyMovement[w][0] || kingMoves[z][1] != enemyMovement[w][1])
                                    {
                                        kingMovement.Add(kingMoves[z]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            a = ally;
            if (Board.Squares[j, k].Piece.GetType() != typeof(King))
            {
                movement = Board.Squares[j, k].Piece.RestrictMovement(Board.Squares, j, k);
                for (int x = 0; x < allyMovement.Count; ++x)
                {
                    for (int y = 0; y < movement.Count; ++y)
                    {
                        l.X = allyMovement[x][0];
                        l.Y = allyMovement[x][1];
                        l2.X = movement[y][0];
                        l2.Y = movement[y][1];
                        if (allyMovement[x][0] == movement[y][0] && allyMovement[x][1] == movement[y][1])
                        {
                            ((Square)g.Children[(allyMovement[x][0] * 8) + allyMovement[x][1]]).Background = Brushes.Plum;
                        }
                    }
                }
            }
            else
            {
                PrintKingMoves(j, k);
            }
        }
        /// <summary>
        /// Checks if the selected piece's desired location willl take the king out of check.
        /// </summary>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns>
        /// True: If the desired location for the piece will take the king out of chess.
        /// False: If the desired location for the piece will not take the king out of chess.
        /// </returns>
        public bool WillSaveTheKing(int startX, int startY, int endX, int endY)
        {
            List<int[]> enemyMovement;
            List<int[]> ally = new List<int[]>();
            List<int[]> movement = new List<int[]>();
            Location l = new Location();
            bool temp = true;
            enemyMovement = StoreEnemyMovements();
            bool isValid = false;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if ((int)Board.Squares[x, y].Piece.Color == Turn)
                    {
                        if (Board.Squares[x, y].Piece.GetType() != typeof(King))
                        {
                            for (int w = 0; w < enemyMovement.Count; ++w)
                            {
                                l.X = enemyMovement[w][0];
                                l.Y = enemyMovement[w][1];
                                if (endX == enemyMovement[w][0] && endY == enemyMovement[w][1])
                                {
                                    isValid = true;
                                }
                            }
                        }
                        else
                        {
                            if (Board.Squares[startX, startY].Piece.GetType() == typeof(King))
                            {
                                bool valid = WillKingCheck(startX, startY, endX, endY);
                                if (valid == true)
                                {
                                    for (int w = 0; w < enemyMovement.Count; ++w)
                                    {
                                        if (endX == enemyMovement[w][0] && endY == enemyMovement[w][1])
                                        {
                                            if (Board.Squares[enemyMovement[w][0], enemyMovement[w][1]].Piece.GetType() == typeof(Space))
                                            {
                                                temp = false;
                                            }
                                            else
                                            {
                                                isValid = true;
                                            }
                                        }
                                        else if (endX != enemyMovement[w][0] || endY != enemyMovement[w][1])
                                        {
                                            isValid = true;
                                        }
                                    }
                                }
                                else
                                {
                                    temp = false;
                                }
                            }
                        }
                    }
                }
            }
            if (temp == false)
            {
                isValid = temp;
            }
            return isValid;
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
                l2.Content = "Dark Player's turn.";
                Turn = 2;
            }
            else
            {
                l2.Content = "Light Player's turn.";
                Turn = 1;
            }
        }
        #endregion

        #region PrintPieceMovement
        /// <summary>
        /// Prints out all legal movement of the selected piece by highlighting(change button backgrounds)
        /// to a specific color.
        /// 
        /// Lightgreen: Legal move.
        /// light-Salmon: Legal move that will capture a piece.
        /// </summary>
        /// <param name="x">Row number from a 2-D Array.</param>
        /// <param name="y">Column number from a 2-D Array.</param>
        public void PrintPieceMovement(UniformGrid grid, int x, int y)
        {
            List<int[]> movement;
            if (Board.Squares[x, y].Piece.GetType() != typeof(King))
            {
                movement = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);

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
            }
            else
            {
                PrintKingMoves(x, y);
            }
        }
        /// <summary>
        /// Removes all legal movement of the selected piece and resets the color to their
        /// default color.
        /// </summary>
        /// <param name="x">Row number from a 2-D Array.</param>
        /// <param name="y">Column number from a 2-D Array.</param>
        public void RemovePieceMovement(UniformGrid grid, int x, int y)
        {
            List<int[]> movement = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
            string[] stringPiece = GrabPiece(x, y);
            for (int j = 0; j < movement.Count; ++j)
            {
                if (Board.Squares[movement[j][0], movement[j][1]].Color == ChessColor.DARK)
                {
                    ((Square)grid.Children[(movement[j][0] * 8) + movement[j][1]]).Background = Brushes.Gray;
                }
                else if (Board.Squares[movement[j][0], movement[j][1]].Color == ChessColor.LIGHT)
                {
                    ((Square)grid.Children[(movement[j][0] * 8) + movement[j][1]]).Background = Brushes.LightGray;
                }
            }
            if (inCheck == true)
            {
                if (((Square)g.Children[(x * 8) + y]).Background == Brushes.CornflowerBlue)
                {
                    for (int a = 0; a < 8; ++a)
                    {
                        for (int b = 0; b < 8; ++b)
                        {
                            if (Board.Squares[a, b].Color == ChessColor.DARK)
                            {
                                ((Square)g.Children[(a * 8) + b]).Background = Brushes.Gray;
                            }
                            else if (Board.Squares[x, y].Color == ChessColor.LIGHT)
                            {
                                ((Square)g.Children[(a * 8) + b]).Background = Brushes.LightGray;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// If the king is in check, then highlight all the pieces that can save the king.
        /// Otherwise set their color back to default.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MovablePieces()
        {
            if (inCheck == true)
            {
                PrintPieceMovement();
            }
            else
            {
                RemovePieceMovement();
            }
        }
        /// <summary>
        /// Gets enemy movement leading to the king and stores it.
        /// Loops through the ally pieces and chscks their movement to counter against the enemy's movement.
        /// If a piece's movement counters, then the piece is highlighted in plum
        /// 
        /// Plum: When a ally piece can save the king.
        /// </summary>
        public void PrintPieceMovement()
        {
            List<int[]> enemyMovement;
            List<int[]> ally = new List<int[]>();
            enemyMovement = StoreEnemyMovements();
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if ((int)Board.Squares[x, y].Piece.Color == Turn)
                    {
                        if (Board.Squares[x, y].Piece.GetType() != typeof(King))
                        {
                            List<int[]> temp = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
                            for (int z = 0; z < temp.Count; ++z)
                            {
                                for (int w = 0; w < enemyMovement.Count; ++w)
                                {
                                    if (temp[z][0] == enemyMovement[w][0] && temp[z][1] == enemyMovement[w][1])
                                    {
                                        ally.Add(new int[] { x, y });
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<int[]> temp = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, x, y);
                            for (int z = 0; z < temp.Count; ++z)
                            {
                                for (int w = 0; w < enemyMovement.Count; ++w)
                                {
                                    if (temp[z][0] == enemyMovement[w][0] && temp[z][1] == enemyMovement[w][1])
                                    {

                                    }
                                    else if (temp[z][0] != enemyMovement[w][0] || temp[z][1] != enemyMovement[w][1])
                                    {
                                        ally.Add(new int[] { x, y });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < ally.Count; ++x)
            {
                ((Square)g.Children[(ally[x][0] * 8) + ally[x][1]]).Background = Brushes.Plum;
            }
        }
        /// <summary>
        /// Removes all legal movement of the selected piece and resets the color to their
        /// default color.
        /// </summary>
        public void RemovePieceMovement()
        {
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (inCheck != true)
                    {
                        if (((Square)g.Children[(x * 8) + y]).Background == Brushes.Plum)
                        {
                            for (int j = 0; j < 64; ++j)
                            {
                                if (Board.Squares[x, y].Color == ChessColor.DARK)
                                {
                                    ((Square)g.Children[(x * 8) + y]).Background = Brushes.Gray;
                                }
                                else if (Board.Squares[x, y].Color == ChessColor.LIGHT)
                                {
                                    ((Square)g.Children[(x * 8) + y]).Background = Brushes.LightGray;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Highlights where the king shouldn't go to avoid check.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void PrintKingMoves(int startX, int startY)
        {
            List<int[]> enemyMovements = StoreAllEnemyMovements();
            List<int[]> kingMovement = Board.Squares[startX, startY].Piece.RestrictMovement(Board.Squares, startX, startY);
            if (inCheck == false)
            {
                for (int x = 0; x < enemyMovements.Count; ++x)
                {
                    for (int y = 0; y < kingMovement.Count; ++y)
                    {
                        if (kingMovement[y][0] == enemyMovements[x][0] && kingMovement[y][1] == enemyMovements[x][1])
                        {
                            ((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background = Brushes.LightYellow;
                        }
                        else if (((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background != Brushes.LightYellow)
                        {
                            if (kingMovement[y][0] != enemyMovements[x][0] && kingMovement[y][1] != enemyMovements[x][1])
                            {
                                ((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background = Brushes.LightGreen;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < enemyMovements.Count; ++x)
                {
                    for (int y = 0; y < kingMovement.Count; ++y)
                    {
                        if (kingMovement[y][0] == enemyMovements[x][0] && kingMovement[y][1] == enemyMovements[x][1])
                        {
                            ((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background = Brushes.LightYellow;
                        }
                        if (((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background != Brushes.LightYellow)
                        {
                            //if (Board.Squares[enemyMovements[x][0], enemyMovements[x][1]].Piece.GetType() != typeof(Space))
                            //{
                            //    ((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background = Brushes.LightYellow;
                            //}
                            if (kingMovement[y][0] != enemyMovements[x][0] && kingMovement[y][1] != enemyMovements[x][1])
                            {
                                ((Square)g.Children[(kingMovement[y][0] * 8) + kingMovement[y][1]]).Background = Brushes.Plum;
                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region Checks
        /// <summary>
        /// Grabs all available legal moves from every piece and checks if an
        /// available legal move from that piece can kill the opposite colored king.
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
            inCheck = false;
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
        /// Checks for a pawn in squares that need a pawn for pawn promotion. If a square does have a pawn,
        /// it will prompt the user to promote the pawn to another piece.
        /// </summary>
        public void CheckPawnPromotion()
        {
            bool containsPawn = false;
            square = new int[2];
            for (int y = 0; y < 8; ++y)
            {
                containsPawn = SquareContainsPawn(0, y);
                if (containsPawn == true)
                {
                    square = new int[] { 0, y };
                    break;
                }
                containsPawn = SquareContainsPawn(7, y);
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
        /// Checks if the current square has a pawn.
        /// </summary>
        /// <param name="x">Row number from a chess board or the X coordinate of a 2-D array.</param>
        /// <param name="y">Column number from a chess board or the Y coordinate of a 2-D array.</param>
        /// <returns>
        /// True: If the square does contain a pawn.
        /// False: If the square does not contain a pawn.
        /// </returns>
        public bool SquareContainsPawn(int x, int y)
        {
            bool containsPawn = false;
            if (Board.Squares[x, y].Piece.GetType() == typeof(Pawn))
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
        /// Makes a label and it's buttons visible, prompting the user if they would like
        /// to promote their pawn.
        /// Disables the board to force the player to pick a piece to promote the pawn to.
        /// </summary>
        public void SetUpPromotion()
        {
            g.IsEnabled = false;
            for (int x = 0; x < 5; ++x)
            {
                g2.Children[x].Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Adds the handlers to the buttons that will promote the pawn.
        /// </summary>
        /// <param name="grid"></param>
        public void SetPromoteButtons(UniformGrid grid)
        {
            g2 = grid;
            for (int x = 1; x < 5; ++x)
            {
                ((Button)g2.Children[x]).Click += Button_PromoteHandler;
            }
        }
        #endregion

        #region StoreEnemyMovements
        /// <summary>
        /// Loops through, getting enemy movements, and storing them.
        /// Looks for the king of the current player and stores his location.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Returns a list of movements thats are directed to the king.</returns>
        public List<int[]> StoreEnemyMovements()
        {
            List<int[]> enemyMovement = new List<int[]>();//A storage for all possible legal move for enemy pieces.
            List<int[]> allyMovement = new List<int[]>();//A storage for all possible legal move for ally pieces.
            List<int[]> enemy = new List<int[]>();//A placeholder for enemy movements
            List<int[]> Pieces = new List<int[]>();//a list
            int[] king = new int[2];//King's location.

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((int)Board.Squares[x, y].Piece.Color != Turn && Board.Squares[x, y].Piece.Color != ChessColor.NONE)
                    {
                        if (Board.Squares[x, y].Piece.GetType() != typeof(Pawn))
                        {
                            Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                            pieceLoc.X = x;
                            pieceLoc.Y = y;
                            enemy = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                            for (int j = 0; j < enemy.Count; ++j)
                            {
                                enemyMovement.Add(enemy[j]);//Adds all possible legal enemy movements from the current piece being looped through.
                                Pieces.Add(new int[] { pieceLoc.X, pieceLoc.Y });//Contains all piece's that have legal moves.
                            }
                        }
                        else
                        {
                            Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                            pieceLoc.X = x;
                            pieceLoc.Y = y;
                            enemy = ((Pawn)Board.Squares[x, y].Piece).Attacks(Board.Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                            for (int j = 0; j < enemy.Count; ++j)
                            {
                                enemyMovement.Add(enemy[j]);//Adds all possible legal enemy movements from the current piece being looped through.
                                Pieces.Add(new int[] { pieceLoc.X, pieceLoc.Y });//Contains all piece's that have legal moves.
                            }
                        }
                    }
                    else if ((int)Board.Squares[x, y].Piece.Color == Turn)
                    {
                        if (Board.Squares[x, y].Piece.GetType() == typeof(King))
                        {
                            king = new int[] { x, y };//Stores the king's location.
                        }
                    }
                }
            }
            for (int x = 0; x < enemyMovement.Count; ++x)//Loops through all possible legal moves.
            {
                if (enemyMovement[x][0] == king[0] && enemyMovement[x][1] == king[1])//If an enemy movement is the same as the king's location.
                {
                    //Returns moves leading to the king's location.
                    enemy = Board.Squares[Pieces[x][0], Pieces[x][1]].Piece.Search(Board.Squares, Pieces[x][0], Pieces[x][1], king[0], king[1]);
                    enemy.Add(new int[] { Pieces[x][0], Pieces[x][1] });
                    break;
                }
            }
            return enemy;
        }
        /// <summary>
        /// Gets all possible enemy moves.
        /// </summary>
        /// <returns></returns>
        public List<int[]> StoreAllEnemyMovements()
        {
            List<int[]> enemyMovement = new List<int[]>();//A storage for all possible legal move for enemy pieces.
            List<int[]> enemy = new List<int[]>();//A placeholder for enemy movements
            List<int[]> Pieces = new List<int[]>();//a list

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((int)Board.Squares[x, y].Piece.Color != Turn && Board.Squares[x, y].Piece.Color != ChessColor.NONE)
                    {
                        if (Board.Squares[x, y].Piece.GetType() != typeof(Pawn))
                        {
                            Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                            pieceLoc.X = x;
                            pieceLoc.Y = y;
                            enemy = Board.Squares[x, y].Piece.RestrictMovement(Board.Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                            for (int j = 0; j < enemy.Count; ++j)
                            {
                                enemyMovement.Add(enemy[j]);//Adds all possible legal enemy movements from the current piece being looped through.
                            }
                        }
                        else
                        {
                            Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                            pieceLoc.X = x;
                            pieceLoc.Y = y;
                            enemy = ((Pawn)Board.Squares[x, y].Piece).Attacks(Board.Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                            for (int j = 0; j < enemy.Count; ++j)
                            {
                                enemyMovement.Add(enemy[j]);//Adds all possible legal enemy movements from the current piece being looped through.
                            }
                        }
                    }
                }
            }
            return enemyMovement;
        }
        #endregion

        #region SetBoard
        /// <summary>
        /// Adds handlers to the buttons, while also adding the buttons to the board.
        /// </summary>
        /// <param name="board">A grid representing the board.</param>
        /// <param name="movement">A label that will display the movemnt for every moved piece.</param>
        /// <param name="playerTurns">A label that will display who's turn it is.</param>
        public void CreateBoard(UniformGrid board, Label movement, Label playerTurns)
        {
            g = board;
            l = movement;
            l2 = playerTurns;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    Square s = new Square(Board.Squares[x, y].Piece.ToString());
                    SetDefaultBoardSquareColors(s, x, y);
                    s.loc.X = x;
                    s.loc.Y = y;
                    s.Click += Button_PieceHandler;
                    s.MouseRightButtonDown += Button_RightClickHandler;
                    g.Children.Add(s);
                }
            }
        }

        public void SetDefaultBoardSquareColors(Square s, int x, int y)
        {
            if(inCheck == false)
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
            else
            {
                for(int z = 0; z < a.Count; ++z)
                {
                    if (a[z][0] == x && a[z][1] == y)
                    {
                        s.Background = Brushes.Plum;
                    }
                    else if (Board.Squares[x, y].Color == ChessColor.DARK)
                    {
                        s.Background = Brushes.Gray;
                    }
                    else if (Board.Squares[x, y].Color == ChessColor.LIGHT)
                    {
                        s.Background = Brushes.LightGray;
                    }
                }
            }
        }
        #endregion
        public void ResetGame()
        {
            _board = new ChessBoard();
            g.Children.Clear();
            inCheck = false;
            CreateBoard(g, l, l2);
            _startLoc = new Location();
            _endLoc = new Location();
            ChangeTurn();
            _startSquare = null;
            _selectedSquare = null;
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

        //public void IfKingsExist()
        //{
        //    bool lightExist = false;
        //    bool darkExist = false;
        //    for (int x = 0; x < 8; ++x)
        //    {
        //        for (int y = 0; y < 8; ++y)
        //        {
        //            if (Board.Squares[x, y].Piece.GetType() == typeof(King))
        //            {
        //                if (Board.Squares[x, y].Piece.Color == ChessColor.DARK)
        //                {
        //                    darkExist = true;
        //                }
        //                else
        //                {
        //                    lightExist = true;
        //                }
        //            }
        //        }
        //    }
        //    if(lightExist == false)
        //    {
        //        PrintWinner();
        //    }
        //    else if (darkExist == false)
        //    {
        //        PrintWinner();
        //    }
        //}

        #region ButtonHandlers
        private void Button_PieceHandler(object sender, RoutedEventArgs e)
        {
            Square s = (Square)sender;
            
            if (_selectedSquare == null)
            {
                if (Board.Squares[s.loc.X, s.loc.Y].Piece.GetType() != typeof(Space))
                {
                     if ((int)Board.Squares[s.loc.X, s.loc.Y].Piece.Color == Turn)
                     {
                         string[] input = GrabPiece(s.loc.X, s.loc.Y);
                         s.Background = Brushes.CornflowerBlue;
                         ProcessPiece((UniformGrid)s.Parent, (input[0] + input[1]), @"([a-h])([1-8])");
                         _selectedSquare = s;
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
            }
        }
        private void Button_RightClickHandler(object sender, RoutedEventArgs e)
        {
            Square s = (Square)sender;
            if (_selectedSquare != null)
            {
                SetDefaultBoardSquareColors(_selectedSquare, _selectedSquare.loc.X, _selectedSquare.loc.Y);
                RemovePieceMovement((UniformGrid)_selectedSquare.Parent, _selectedSquare.loc.X, _selectedSquare.loc.Y);
                MovablePieces();
                _selectedSquare = null;
            }
        }
        private void Button_PromoteHandler(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Name == "Queen")
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
        #endregion

        #region En Passant
        /// <summary>
        /// Checks if the current piece is a pawn.
        /// Checks if the sides of the piece is not the end of the board.
        /// Checks the color of the selected piece.
        /// Checks if the sides of the selected piece is a pawn.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="x1">The selected piece's x coordinate</param>
        /// <param name="y1">The selected piece's y coordinate</param>
        public void CheckEnPassant(ChessSquare[,] board, int x1, int y1)
        {
            if (board[x1, y1].Piece.GetType() == typeof(Pawn))
            {
                if ((x1 + 1) < 8)
                {
                    if (board[x1, y1].Piece.Color == ChessColor.LIGHT)
                    {
                        if (board[x1 + 1, y1].Piece.GetType() == typeof(Pawn))
                        {
                            EnPassant(g, Board.Squares, x1 + 1, y1);
                        }
                    }
                }
                if ((x1 - 1) >= 0)
                {
                    if (board[x1, y1].Piece.Color == ChessColor.DARK)
                    {
                        if (board[x1 - 1, y1].Piece.GetType() == typeof(Pawn))
                        {
                            EnPassant(g, Board.Squares, x1 - 1, y1);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Removes the pawn that got 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="board"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void EnPassant(UniformGrid grid, ChessSquare[,] board, int x, int y)
        {
            if ((int)board[x, y].Piece.Color != Turn)
            {
                if (((Pawn)board[x, y].Piece)._moveAmount == 1)
                {
                    board[x, y].Piece = new Space();
                    ((Square)grid.Children[(x * 8) + y]).Panel.Children.Clear();
                    ((Square)grid.Children[(x * 8) + y]).Pic = null;
                }
            }
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
        #endregion
        
        #region Properties
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