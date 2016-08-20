using Chess.ChessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    public class ChessSquare
    {
        public ChessSquare(int y, int x, ChessColor color)
        {
            Loc = new Location();
            Loc.Y = y;
            Loc.X = x;
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
        /// The location of the square on the board.
        /// </summary>
        public Location Loc { get; set; }
    }
}
