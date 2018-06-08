using Chess_Project.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Pieces
{
    internal class Knight : ChessPiece
    {
        internal override char Letter { get; }
        internal override int MoveAmount { get; set; }
        internal override Piece Type { get; set; }
        internal override Color Paint { get; set; }
        internal Knight() { }
        internal Knight(Color Paint)
        {
            Letter = 'N';
            MoveAmount = 7;
            Type = Piece.Knight;
            this.Paint = Paint;
        }
    }
}
