using System;

namespace Chess.ChessModels
{
    public class King : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }
        public King(char color)
        {
            Color = color;
            Piece = "King";
            Symbol = 'K';
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
