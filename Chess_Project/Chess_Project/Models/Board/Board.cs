using Chess_Project.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Board
{
    internal class Board
    {
        internal BoardSpace[,] GameBoard { get; set; }
        internal Board()
        {
            GameBoard = new BoardSpace[8, 8];
        }
        internal void ResetBoard()
        {
            GameBoard = new BoardSpace[8, 8];
        }
    }
}
