using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    public class ChessSquare
    {
        public ChessSquare(int vertical, int horizontal, ChessColor color)
        {
           Vertical = vertical;
           Horizontal = horizontal;
           Color = color;
        }
        /// <summary>
        /// The color of the square.
        /// </summary>
        public ChessColor Color { get; set; }
        /// <summary>
        /// The piece the square contains.
        /// </summary>
        public ChessPiece Piece { get; set; }
        /// <summary>
        /// The column location of the square on the board.
        /// </summary>
        public int Vertical { get; set; }
        /// <summary>
        /// The row location of the square on the board.
        /// </summary>
        public int Horizontal { get; set; }
    }
}
