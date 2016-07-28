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
        /// Handles a char representing a piece
        /// </summary>
        char Piece
        {
            get;
            set;
        }
        /// <summary>
        /// Movement of a chess piece
        /// </summary>
        void Movement();
    }
}
