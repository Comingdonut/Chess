using System.Collections.Generic;

namespace Chess.ChessModels
{
    public interface ChessPiece
    {
        string Piece { get; set; }
        char Symbol { get; set; }
        char Color { get; set; }
        /// <summary>
        /// Moves the piece.
        /// </summary>
        void MovePiece(ChessPiece[,] board, int[] start, int[] end);
        /// <summary>
        /// Checks if a square is available.
        /// </summary>
        bool CheckSquare(ChessPiece[,] board, int[] end);
        /// <summary>
        /// Checks where a piece can go.
        /// </summary>
        bool CheckMovement(ChessPiece[,] board, int[] start, int[] end);
        /// <summary>
        /// Checks if movement does not go off board
        /// </summary>
        List<int[]> IsAvailable(int[] start);
    }
}
