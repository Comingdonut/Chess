using System;

namespace Chess.ChessModels
{
    public class Queen : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Queen(char color)
        {
            Color = color;
            Piece = "Queen";
            Symbol = 'Q';
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
