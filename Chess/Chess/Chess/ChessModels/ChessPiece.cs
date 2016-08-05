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
        void MovePiece();
        /// <summary>
        /// Checks if a square is available.
        /// </summary>
        void CheckSquare(ChessPiece square);
        /// <summary>
        /// Checks where a piece can go.
        /// </summary>
        void CheckMovement();
    }
}
