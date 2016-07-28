using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    class Pawn : IChessPiece
    {
        #region variables
        private char _piece;
        #endregion
        public void Movement()
        {
            throw new NotImplementedException();
        }
        #region Properties
        public char Piece
        {
            get
            {
                return _piece;
            }

            set
            {
                if (value == 'P')
                {
                    _piece = value;
                }
            }
        }
        #endregion
    }
}
