using System;

namespace Chess.ChessModels
{
    public class Rook : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Rook(char color)
        {
            Color = color;
            Piece = "Rook";
            Symbol = 'R';
        }
        public void CheckMovement()
        {
            throw new NotImplementedException();
        }
        public void CheckSquare()
        {
            throw new NotImplementedException();
        }

        public void MovePiece()
        {
            throw new NotImplementedException();
        }
    }
}
