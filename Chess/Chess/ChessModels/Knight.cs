using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    class Knight : IChessPiece
    {
        #region variables
        private char _piece;
        private char _color;
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
                if (value == 'N')
                {
                    _piece = value;
                }
            }
        }
        public char Color
        {
            get
            {
                return _color;
            }

            set
            {
                if (value == 'l' || value == 'd')
                {
                    _color = value;
                }
            }
        }
        #endregion
    }
}
