using System;

namespace Chess.ChessModels
{
    public class Pawn : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Pawn(char color)
        {
            Color = color;
            Piece = "Pawn";
            Symbol = 'P';
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
