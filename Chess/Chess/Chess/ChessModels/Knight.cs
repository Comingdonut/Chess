using System;

namespace Chess.ChessModels
{
    public class Knight : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Knight(char color)
        {
            Color = color;
            Piece = "Knight";
            Symbol = 'N';
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
