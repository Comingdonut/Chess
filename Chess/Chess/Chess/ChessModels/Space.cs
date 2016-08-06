using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Space : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Space()
        {
            Color = ' ';
            Piece = " ";
            Symbol = ' ';
        }
        public bool CheckMovement(ChessPiece[,] board, int[] start, int[] end) { return false; }
        public bool CheckSquare(ChessPiece[,] square, int[] end) { return false; }
        public void MovePiece(ChessPiece[,] board, int[] start, int[] end) { }
        public List<int[]> IsAvailable(int[] start) { return new List<int[]>(); }
    }
}
