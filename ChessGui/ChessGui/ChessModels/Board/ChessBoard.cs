using Chess.ChessControl;
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
            SetPieces();
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
        /// Sets the pieces on the board.
        /// </summary>
        public void SetPieces()
        {
            //Dark Pieces
            Squares[0, 0].Piece = new Rook(ChessColor.DARK);
            Squares[0, 1].Piece = new Knight(ChessColor.DARK);
            Squares[0, 2].Piece = new Bishop(ChessColor.DARK);
            Squares[0, 3].Piece = new Queen(ChessColor.DARK);
            Squares[0, 4].Piece = new King(ChessColor.DARK);
            Squares[0, 5].Piece = new Bishop(ChessColor.DARK);
            Squares[0, 6].Piece = new Knight(ChessColor.DARK);
            Squares[0, 7].Piece = new Rook(ChessColor.DARK);
            //White Pieces
            Squares[7, 0].Piece = new Rook(ChessColor.LIGHT);
            Squares[7, 1].Piece = new Knight(ChessColor.LIGHT);
            Squares[7, 2].Piece = new Bishop(ChessColor.LIGHT);
            Squares[7, 3].Piece = new Queen(ChessColor.LIGHT);
            Squares[7, 4].Piece = new King(ChessColor.LIGHT);
            Squares[7, 5].Piece = new Bishop(ChessColor.LIGHT);
            Squares[7, 6].Piece = new Knight(ChessColor.LIGHT);
            Squares[7, 7].Piece = new Rook(ChessColor.LIGHT);
            //Pawns for both colors
            for (int y = 0; y < 8; ++y)
            {
                Squares[1, y].Piece = new Pawn(ChessColor.DARK);
                Squares[6, y].Piece = new Pawn(ChessColor.LIGHT);
            }
            //Sets all empty squares to a space.
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if(Squares[x, y].Piece == null)
                    {
                        Squares[x, y].Piece = new Space();
                    }
                }
            }
        }
        #endregion
    }

}