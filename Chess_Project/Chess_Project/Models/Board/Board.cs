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
        private static Board b;
        internal BoardSpace[,] GameBoard { get; set; }
        private Board()
        {
            GameBoard = new BoardSpace[8, 8];
        }
        internal static void GetInstance()
        {
            if(b == null)
            {
                b = new Board();
            }
        }
        internal void ResetBoard()
        {
            GameBoard = new BoardSpace[8, 8]; // Maybe in GameManager
        }
    }
}
