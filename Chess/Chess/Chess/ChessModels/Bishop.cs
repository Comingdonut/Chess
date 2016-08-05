using System;

namespace Chess.ChessModels
{
    public class Bishop : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Bishop(char color)
        {
            Color = color;
            Piece = "Bishop";
            Symbol = 'B';
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
