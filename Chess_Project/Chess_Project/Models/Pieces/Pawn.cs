using Chess_Project.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Pieces
{
    internal class Pawn : ChessPiece
    {
        internal override char Letter { get; }
        internal override int MoveAmount { get; set; }
        internal override Piece Type { get; set; }
        internal override Color Paint { get; set; }
        internal bool HasMoved { get; set; }
        internal bool MovedTwice { get; set; }
        internal bool CanEnPassant { get; set; }
        internal bool Promote { get; set; }
        internal Pawn() {  }
        internal Pawn(Color Paint)
        {
            Letter = 'P';
            MoveAmount = 2;
            Type = Piece.Pawn;
            this.Paint = Paint;
            HasMoved = false;
            MovedTwice = false;
            CanEnPassant = false;
            Promote = false;
        }
    }
}
