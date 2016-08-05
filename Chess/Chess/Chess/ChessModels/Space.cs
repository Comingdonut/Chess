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
        public void CheckMovement() { }
        public void CheckSquare() { }
        public void MovePiece() { }
    }
}
