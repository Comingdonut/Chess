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

        }
        public void CheckSquare(ChessPiece square)
        {

        }
        public void MovePiece()
        {

        }
    }
}
