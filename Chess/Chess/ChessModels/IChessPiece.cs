using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessModels
{
    interface IChessPiece
    {
        /// <summary>
        /// Handles a char representing a piece.
        /// K = king, Q = Queen, B = Bishop, N = Knight, R = Rook, and P = Pawn
        /// </summary>
        char Piece
        {
            get;
            set;
        }
        /// <summary>
        /// Handles a char representing the colorr of the piece.
        /// l = Light, d = Dark.
        /// </summary>
        char Color
        {
            get;
            set;
        }
        /// <summary>
        /// Movement of a chess piece.
        /// </summary>
        void Movement();
    }
}
