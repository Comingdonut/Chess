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
        #region Variables
        //A 2-D Array of squares essentially creating a board.
        public ChessSquare[,] Squares { get; set; }
        #endregion
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
            Console.WriteLine("    A  B  C  D  E  F  G  H ");
        }
        #endregion

    }
}
