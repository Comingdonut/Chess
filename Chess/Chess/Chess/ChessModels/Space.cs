using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Space : ChessPiece
    {
        public char Color { get; set; }
        public string Piece { get; set; }
        public char Symbol { get; set; }
        public bool[] canMove { get; set; }
        public Space()
        {
            Color = ' ';
            Piece = " ";
            Symbol = ' ';
            canMove = new bool[1];
        }
        public void MovePiece(ChessPiece[,] board, int[] start, int[] end) { }
        public bool CheckSquare(ChessPiece[,] square, int[] end) { return false; }
        public bool CheckMovement(ChessPiece[,] board, int[] start, int[] end) { return false; }
        public List<int[]> RestrictMovement(ChessPiece[,] board, int[] start) { return new List<int[]>(); }
        public bool IsAvailable(ChessPiece[,] board, int row, int column, int index) { return false; }
        public void ResetMovement() { }
        /****************************/
    }
}
