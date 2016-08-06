using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Pawn : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Pawn(char color)
        {
            Color = color;
            Piece = "Pawn";
            Symbol = 'P';
        }
        public bool CheckMovement(ChessPiece[,] board, int[] start, int[] end)
        {
            return false;
        }
        public bool CheckSquare(ChessPiece[,] board, int[] end)
        {
            return false;
        }
        public void MovePiece(ChessPiece[,] board, int[] start, int[] end)
        {
            board[end[0], end[1]] = board[start[0], start[1]];
            board[start[0], start[1]] = new Space();
        }
        public List<int[]> IsAvailable(int[] start)
        {
            List<int[]> available = new List<int[]>();

            return available;
        }
    }
}
