using Chess_Project.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Pieces
{
    internal class King : ChessPiece
    {
        internal override char Letter { get; }
        internal override int MoveAmount { get; set; }
        internal override Piece Type { get; set; }
        internal override Color Paint { get; set; }
        internal bool HasMoved { get; set; }
        internal bool InCheck { get; set; }
        internal King() { }
        internal King(Color Paint)
        {
            Letter = 'K';
            MoveAmount = 1;
            Type = Piece.King;
            this.Paint = Paint;
            InCheck = false;
            HasMoved = false;
        }
    }
}
