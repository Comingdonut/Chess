using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Space : ChessPiece
    {
        public Space()
        {
            Color = ChessColor.NONE;
        }
        public override void MovePiece(ChessSquare[,] board, int[] start, int[] end) { }
        public override bool CheckSquare(ChessSquare[,] square, int[] end) { return false; }
        public override bool CheckMovement(ChessSquare[,] board, int[] start, int[] end) { return false; }
        public override List<int[]> RestrictMovement(ChessSquare[,] board, int[] start) { return new List<int[]>(); }
        public override bool IsAvailable(ChessSquare[,] board, int row, int column, int index) { return false; }
        public override void ResetMovement() { }
        /****************************/
    }
}
