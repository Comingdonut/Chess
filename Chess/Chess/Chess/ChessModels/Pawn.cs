using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Pawn : ChessPiece
    {
        public char Color { get; set; }
        public string Piece { get; set; }
        public char Symbol { get; set; }
        public bool[] canMove { get; set; }
        public Pawn(char color)
        {
            Color = color;
            Piece = "Pawn";
            Symbol = 'P';
            ResetMovement();
        }
        public void MovePiece(ChessPiece[,] board, int[] start, int[] end)
        {
            board[end[0], end[1]] = board[start[0], start[1]];
            board[start[0], start[1]] = new Space();
        }
        public bool CheckSquare(ChessPiece[,] board, int[] end)
        {
            return false;
        }
        public bool CheckMovement(ChessPiece[,] board, int[] start, int[] end)
        {
            ResetMovement();
            return false;
        }
        public List<int[]> RestrictMovement(ChessPiece[,] board, int[] start)
        {
            List<int[]> available = new List<int[]>();

            return available;
        }
        public bool IsAvailable(ChessPiece[,] board, int row, int column, int index)
        {
            bool canMove = true;
            if (board[row, column].Color == Color)
            {
                canMove = false;
            }
            if (this.canMove[index] == true)
            {
                this.canMove[index] = canMove;
            }
            return canMove;
        }
        public void ResetMovement()
        {
            canMove = new bool[] { true, true, true, true, true, true, true, true };
        }
        /****************************/
    }
}
