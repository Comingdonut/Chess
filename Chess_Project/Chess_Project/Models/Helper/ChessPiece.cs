
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Helper
{
    internal abstract class ChessPiece
    {
        internal abstract char Letter { get; }
        internal abstract Piece Type { get; set; }
        internal abstract Color Paint { get; set; } 
        public override string ToString()
        {
            return (" " + Letter + " ");
        }
    }
}
