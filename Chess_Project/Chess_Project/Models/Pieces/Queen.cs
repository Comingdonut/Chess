using Chess_Project.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Pieces
{
    internal class Queen : ChessPiece
    {
        internal override char Letter { get; }
        internal override Piece Type { get; set; }
        internal override Color Paint { get; set; }
        internal Queen() {  }
        internal Queen(Color Paint)
        {
            Letter = 'Q';
            Type = Piece.Queen;
            this.Paint = Paint;
        }
    }
}
