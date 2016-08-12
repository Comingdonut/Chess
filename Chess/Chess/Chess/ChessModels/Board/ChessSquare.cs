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
        public ChessColor Color { get; set; }
        public ChessPiece Piece { get; set; }
        public int Vertical { get; set; }
        public int Horizontal { get; set; }
    }
}
