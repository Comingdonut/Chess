using Chess.ChessModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    public class ChessBoard
    {
        //A 2-D Array of squares essentially creating a board.
        public ChessSquare[,] Squares { get; set; }
        public ChessBoard()
        {
            CreateSquares();
        }
        #region Board
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
                    //Spaces
                    if (Squares[x, y].Piece == null)
                    {
                        Squares[x, y].Piece = new Space();
                    }
                    color = ChangeColor(color);
                }
                color = ChangeColor(color);
            }
        }
        /// <summary>
        /// Changes color to it's opposite value.
        /// </summary>
        /// <param name="color">Represents a color that will be changed to it's opposites.</param>
        /// <returns>Returns the opposite color, then the color passed in the parameter.</returns>
        public ChessColor ChangeColor(ChessColor color)
        {
            ChessColor newColor = color == ChessColor.LIGHT ? ChessColor.DARK : ChessColor.LIGHT;
            return newColor;
        }
        /// <summary>
        /// Loops through the board, prints out 8 array values before moving on to the next set of values.
        /// This will continue, essentially printing out the board.
        /// 
        /// If the color of a board square is light, then it is set to gray.
        /// If it's dark, then it's set to dark gray.
        /// If there is no color, then its does nothing.
        /// 
        /// If the board square has a piece, then it sets the forground color the pieces
        /// respective color. So light gets a font solor of white and dark gets a font
        /// color of black. It prints out the pieces respective symbol.
        /// If the board square does not have a piece, it prints out 3 spaces representing a an empty
        /// square.
        /// 
        /// After all this, the backgroung and froeground color get reset to their defaults.
        /// </summary>
        public void PrintBoard()
        {
            Console.WriteLine("    A  B  C  D  E  F  G  H ");
            for (int x = 0; x < 8; ++x)//Loop for rows of 2-D Arr
            {
                Console.ResetColor();
                Console.Write(" " + ((8) - x) + " ");
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
        /// <summary>
        /// Grabs all available legal moves from every piece and checks if a avialable legal move from that piece can kill a king.
        /// Prints out the kings status.
        /// </summary>
        ///<returns>
        ///True: If a piece can move to kill a king.
        ///False: If no move can capture a king.
        /// </returns>
        public bool IsInCheck()
        {
            List<int[]> enemyMovements = new List<int[]>();//A storage for all possible legal move for the pieces.
            List<int[]> Pieces = new List<int[]>();
            int[] lKing = new int[2];//Light king's location.
            int[] dKing = new int[2];//Dark king's location.
            bool inCheck = false;
            string status = "";//The status of the king.
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                    pieceLoc.X = x;
                    pieceLoc.Y = y;
                    List<int[]> placeHold = Squares[x, y].Piece.RestrictMovement(Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                    for (int j = 0; j < placeHold.Count; ++j)
                    {
                        enemyMovements.Add(placeHold[j]);//Adds all possible legal movements from the current piece being looped through.
                        Pieces.Add(new int[] { pieceLoc.X, pieceLoc.Y });//Contains all piece's that have legal moves.
                    }
                    if (Squares[x, y].Piece.GetType() == typeof(King))
                    {
                        if (Squares[x, y].Piece.Color == ChessColor.LIGHT)
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
            for (int x = 0; x < enemyMovements.Count; ++x)//Loops through all possible legal moves.
            {
                string[] word = GrabPiece(enemyMovements[x][0], enemyMovements[x][1]);//Word is equal to a move.
                if (enemyMovements[x][0] == lKing[0] && enemyMovements[x][1] == lKing[1])//If a move is the same as the light king's location.
                {
                    Console.WriteLine("Light King's Status: In danger\n{0} {1}'s movement to {2}{3}, will capture {4} {5}.",
                                Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(), Squares[Pieces[x][0], Pieces[x][1]].Piece.Name,
                                word[0], word[1],
                                Squares[lKing[0], lKing[1]].Piece.Color.ToString(), Squares[lKing[0], lKing[1]].Piece.Name);
                    status = " ";
                    inCheck = true;
                }
                else if (enemyMovements[x][0] == dKing[0] && enemyMovements[x][1] == dKing[1])//If a move is the same as the dark king's location.
                {
                    Console.WriteLine("Dark King's Status: In danger\n{0} {1}'s movement to {2}{3}, will capture {4} {5}.",
                                Squares[Pieces[x][0], Pieces[x][1]].Piece.Color.ToString(), Squares[Pieces[x][0], Pieces[x][1]].Piece.Name,
                                word[0], word[1],
                                Squares[dKing[0], dKing[1]].Piece.Color.ToString(), Squares[dKing[0], dKing[1]].Piece.Name);
                    status = " ";
                    inCheck = true;
                }
                else//If status is a space the print out that the king is safe.
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
            if (status != " ")//If status is not a space the print out status
            {
                Console.WriteLine(status);
            }
            return inCheck;
        }
        public bool canBeSaved()
        {
            List<int[]> movements = new List<int[]>();//A storage for all possible legal move for the pieces.
            List<int[]> Pieces = new List<int[]>();
            int[] lKing = new int[2];//Light king's location.
            int[] dKing = new int[2];//Dark king's location.
            bool inCheck = true;
            List<int[]> placeHold3 = new List<int[]>();
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                    pieceLoc.X = x;
                    pieceLoc.Y = y;
                    placeHold3 = Squares[x, y].Piece.RestrictMovement(Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current piece.
                    for (int j = 0; j < placeHold3.Count; ++j)
                    {
                        movements.Add(placeHold3[j]);//Adds all possible legal movements from the current piece being looped through.
                        Pieces.Add(new int[] { pieceLoc.X, pieceLoc.Y });//Contains all piece's that have legal moves.
                    }
                    if (Squares[x, y].Piece.GetType() == typeof(King))
                    {
                        if (Squares[x, y].Piece.Color == ChessColor.LIGHT)
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
            List<int[]> placeHold = new List<int[]>();
            List<int[]> placeHold2 = new List<int[]>();
            for (int x = 0; x < movements.Count; ++x)//Loops through all possible legal moves.
            {
                if (movements[x][0] == lKing[0] && movements[x][1] == lKing[1])//If a move is the same as the light king's location.
                {
                    placeHold = Squares[Pieces[x][0], Pieces[x][1]].Piece.Test(Squares, Pieces[x][0], Pieces[x][1], lKing[0], lKing[1]);//returns moves leading to the king's location
                    for (int v = 0; v < movements.Count; ++v)//Loops through all possible legal moves.
                    {
                        if (Squares[lKing[0], lKing[1]].Piece.Color == Squares[movements[v][0], movements[v][1]].Piece.Color)//if king is the same color as a piece
                        {
                            placeHold2 = Squares[movements[x][0], movements[x][1]].Piece.RestrictMovement(Squares, movements[x][0], movements[x][1]);//returns moves that can protect the king
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
                if (movements[x][0] == dKing[0] && movements[x][1] == dKing[1])//If a move is the same as the light king's location.
                {
                    placeHold = Squares[Pieces[x][0], Pieces[x][1]].Piece.Test(Squares, Pieces[x][0], Pieces[x][1], dKing[0], dKing[1]);//returns moves leading to the king's location
                    for (int v = 0; v < movements.Count; ++v)//Loops through all possible legal moves.
                    {
                        if (Squares[dKing[0], dKing[1]].Piece.Color == Squares[movements[v][0], movements[v][1]].Piece.Color)
                        {
                            placeHold2 = Squares[movements[x][0], movements[x][1]].Piece.RestrictMovement(Squares, movements[x][0], movements[x][1]);//returns moves that can protect the king
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
        /// Prints out check mate if the king is surrounded.
        /// </summary>
        /// <param name="turn">An int representing a player's turn.</param>
        public void IsInCheckMate(int turn)
        {
            List<int[]> kingsMoves = new List<int[]>();
            List<int[]> enemyMoves = new List<int[]>();
            bool inCheck = IsInCheck();
            int[] king = new int[2];//The king(that is in check)'s location.
            if (inCheck == true)//If check has occurred.
            {
                inCheck = canBeSaved();
                if(inCheck == true)
                {
                    for (int x = 0; x < 8; ++x)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (Squares[x, y].Piece.GetType() == typeof(King))//Loops through every piece searching for the king in check.
                            {
                                if ((int)Squares[x, y].Piece.Color == turn)//Makes sure the color of the king is that of the current player's turn.
                                {
                                    king = new int[] { x, y };//Stores the king's location.
                                }
                            }
                        }
                    }//End of the for loop
                    kingsMoves = Squares[king[0], king[1]].Piece.RestrictMovement(Squares, king[0], king[1]);//Stores the kings movements.
                    for (int x = 0; x < 8; ++x)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if ((int)Squares[x, y].Piece.Color != turn)
                            {
                                Location pieceLoc = new Location();//Gets the current piece's location, which will eventually loop through all pieces.
                                pieceLoc.X = x;
                                pieceLoc.Y = y;
                                List<int[]> placeHold = Squares[x, y].Piece.RestrictMovement(Squares, pieceLoc.X, pieceLoc.Y);//Gets all possible legal moves for current enemy piece.
                                for (int j = 0; j < placeHold.Count; ++j)
                                {
                                    enemyMoves.Add(placeHold[j]);//Adds all possible legal movements from the enemy piece being looped through.
                                }
                            }
                        }
                    }//End of the for loop
                    bool[] cantMove = new bool[kingsMoves.Count];//Sets a bool array length to the kingsMoves length.
                    for (int j = 0; j < enemyMoves.Count; ++j)
                    {
                        for (int k = 0; k < kingsMoves.Count; ++k)
                        {
                            if (enemyMoves[j][0] == kingsMoves[k][0] && enemyMoves[j][1] == kingsMoves[k][1])//Checks if any of the kings movements will make the king go back in check
                            {
                                cantMove[k] = true;
                            }
                        }
                    }//End of the for loop
                    int num = 0;
                    for (int z = 0; z < cantMove.Count(); ++z)
                    {
                        if (cantMove[z] == true)//checks if all of kings moves will make him captured
                        {
                            ++num;
                        }
                    }//End of the for loop
                    if (num == cantMove.Length)//If all the kings movements will cause the king to get captures
                    {
                        Console.WriteLine("Checkmate!!!");
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
