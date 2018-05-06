using Chess_Project.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Pieces
{
    internal class Rook : ChessPiece
    {
        internal override char Letter { get; }
        internal override Piece Type { get; set; }
        internal override Color Paint { get; set; }
        internal bool HasMoved { get; set; }
        internal bool CanCastle { get; set; }
        internal Rook() { }
        internal Rook(Color Paint)
        {
            Letter = 'R';
            Type = Piece.Rook;
            this.Paint = Paint;
            HasMoved = false;
            CanCastle = false;
        }
    }
}
