using Chess_Project.Models.Helper;
using Chess_Project.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Board
{
    internal class BoardSpace
    {
        internal bool IsEmpty { get; set; }
        internal ChessPiece Piece { get; set; }
        internal BoardSpace()
        {
            Piece = null;
        }
        internal BoardSpace(bool IsEmpty)
        {
            this.IsEmpty = IsEmpty;
        }
        internal BoardSpace(ChessPiece Piece)
        {
            this.Piece = Piece;
        }
        public override string ToString()
        {
            if(Piece == null)
            {
                return "   ";
            }
            return Piece.ToString();
        }
    }
}
