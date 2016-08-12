using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    public class ChessBoard
    {
        public ChessSquare[,] Squares { get; set; }
        public ChessBoard()
        {
            CreateSquares();
            AddPieces();
        }
        #region SetBoard
        /// <summary>
        /// Creates a board of empty board squares.
        /// </summary>
        public void CreateSquares()
        {
            //Board
            Squares = new ChessSquare[8, 8];
            ChessColor color = ChessColor.LIGHT; 
            //Empty squares
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    Squares[x, y] = new ChessSquare(x, y, color);
                    color = ChangeColor(color);
                }
                color = ChangeColor(color);
            }
        }
        /// <summary>
        /// Add pieces to the board squares.
        /// </summary>
        public void AddPieces()
        {
            //Sets Black & White Pieces
            //Squares[0, 4].Piece = new King(ChessColor.DARK);//Kings
            //Squares[7, 4].Piece = new King(ChessColor.LIGHT);//Kings
            //Squares[0, 3].Piece = new Queen(ChessColor.DARK);//Queens
            //Squares[7, 3].Piece = new Queen(ChessColor.LIGHT);//Queens
            //for (int x = 2; x < 6; x += 3)
            //{
            //    Squares[0, x].Piece = new Bishop(ChessColor.DARK);//Black Bishops
            //    Squares[7, x].Piece = new Bishop(ChessColor.LIGHT);//White Bishops
            //}
            //for (int x = 1; x < 7; x += 5)
            //{
            //    Squares[0, x].Piece = new Knight(ChessColor.DARK);//Black Knight
            //    Squares[7, x].Piece = new Knight(ChessColor.LIGHT);//White Knight
            //}
            //for (int x = 0; x < 9; x += 7)
            //{
            //    Squares[0, x].Piece = new Rook(ChessColor.DARK);//Black Rooks
            //    Squares[7, x].Piece = new Rook(ChessColor.LIGHT);//White Rooks
            //}
            //for (int x = 0; x < 8; x++)
            //{
            //    Squares[1, x].Piece = new Pawn(ChessColor.DARK);//Black Pawns
            //    Squares[6, x].Piece = new Pawn(ChessColor.LIGHT);//White Pawns
            //}
            //Empty squares
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Squares[x, y].Piece == null)
                    {
                        Squares[x, y].Piece = new Space();
                    }
                }
            }
        }
        /// <summary>
        /// Changes color to it's opposite value.
        /// </summary>
        /// <param name="color">Returns the opposite value.</param>
        /// <returns></returns>
        public ChessColor ChangeColor(ChessColor color)
        {
            ChessColor newColor = color == ChessColor.LIGHT ? ChessColor.DARK : ChessColor.LIGHT;
            return newColor;
        }
        /// <summary>
        /// Prints out 8 array values before moving on to the next set of values.
        /// This will continue essentially creating a board.
        /// </summary>
        /// <param name="boardSqaures">Represents a board. Required size for the 2-D array is 8 rows and 8 columns.</param>
        public void PrintBoard()
        {
            for (int x = 0; x < 8; ++x)//Loop for rows of 2-D Arr
            {
                for (int y = 0; y < 8; y++)//Loop for columns of 2-D Arr
                {
                    ChessSquare square = Squares[x, y];//Grabs a square from the board
                    if (square.Color == ChessColor.LIGHT)//Square is light colored
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else if (square.Color == ChessColor.DARK)//Square is dark colored
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    if (square.Piece.GetType() != typeof(Space))//piece on the square is not a empty
                    {
                        Console.ForegroundColor = square.Piece.Color == ChessColor.LIGHT ? ConsoleColor.White : ConsoleColor.Black;
                        Console.Write(" " + square.Piece.Symbol + " ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        #endregion

        #region Checks
        
        public void IsInCheck()
        {
            List<int[]> available = new List<int[]>();
            List<int[]> Pieces = new List<int[]>();
            int[] lKing = new int[2];
            int[] dKing = new int[2];
            string status = "";
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    int[] pieceLocation = new int[] { x, y };//Current piece location, will eventually loopthrough all pieces
                    List<int[]> placeHold = Squares[x, y].Piece.RestrictMovement(Squares, pieceLocation);//Gets all possible moves for current piece
                    for (int j = 0; j < placeHold.Count; ++j)
                    {
                        available.Add(placeHold[j]);//Adds all possible legal movements for the current piece
                        Pieces.Add(pieceLocation);//Contains all pieces with legal moves
                    }
                    if (Squares[x, y].Piece.GetType() == typeof(King))
                    {
                        if (Squares[x, y].Piece.Color == ChessColor.LIGHT)
                        {
                            lKing = new int[] { x, y };//stores the king in a special place ;)
                        }
                        else
                        {
                            dKing = new int[] { x, y };//stores the king in a special place ;)
                        }
                    }
                }
            }
            for (int x = 0; x < available.Count; ++x)
            {
                string[] word = GrabPiece(available[x][0], available[x][1]);
                if (available[x][0] == lKing[0] && available[x][1] == lKing[1])
                {
                    Console.WriteLine("Light King's Status: In danger\n{0} {1}'s movement to {2}{3}, will capture {4} {5}.",
                                Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(), Squares[Pieces[x][0], Pieces[x][1]].Piece.Piece,
                                word[0], word[1],
                                Squares[lKing[0], lKing[1]].Piece.Color.ToString(), Squares[lKing[0], lKing[1]].Piece.Piece);
                    status = " ";
                }
                else if (available[x][0] == dKing[0] && available[x][1] == dKing[1])
                {
                    Console.WriteLine("Dark King's Status: In danger\n{0} {1}'s movement to {2}{3}, will capture {4} {5}.",
                                Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(), Squares[Pieces[x][0], Pieces[x][1]].Piece.Piece,
                                word[0], word[1],
                                Squares[dKing[0], dKing[1]].Piece.Color.ToString(), Squares[dKing[0], dKing[1]].Piece.Piece);
                    status = " ";
                }
                else
                {
                    if(status != " ")
                    {
                        status = "King's Status: Safe.";
                    }
                }
                //else
                //{
                //Console.WriteLine("{0} {1}'s movement: {2}{3}",
                //            Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(), Squares[Pieces[x][0], Pieces[x][1]].Piece.Piece, word[0], word[1]);
                //}
            }
            if (status != " ")
            {
                Console.WriteLine(status);
            }
        }
        /// <summary>
        /// Reads in a column int and row int to convert and store it in an Array.
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
                    x = "d";
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

    }
}
