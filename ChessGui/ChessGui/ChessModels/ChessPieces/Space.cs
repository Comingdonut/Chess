using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Space : ChessPiece
    {
        public Space()
        {
            Color = ChessColor.NONE;
        }
        public override void MovePiece(ChessSquare[,] board, int startX, int startY, int endX, int endY) { }
        public override bool CheckMovement(ChessSquare[,] board, int startX, int startY, int endX, int endY) { return false; }
        public override List<int[]> RestrictMovement(ChessSquare[,] board, int startX, int startY) { return new List<int[]>(); }
        public override bool IsAvailable(ChessSquare[,] board, int row, int column, int index) { return false; }
        public override void ResetMovement() { }
        public override List<int[]> Search(ChessSquare[,] board, int startX, int startY, int endX, int endY) { return new List<int[]>(); }
        public override void ResetSearch(List<int[]> available, bool isMoveSet) { }
        public override string ToString() { return ""; }
        /****************************/
    }
}
