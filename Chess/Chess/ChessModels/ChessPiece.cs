namespace Chess.ChessModels
{
    public interface ChessPiece
    {
        string Piece { get; set; }
        char Symbol { get; set; }
        char Color { get; set; }
        void MovePiece();
        void CheckSquare();
        void CheckMovement();
    }
}
