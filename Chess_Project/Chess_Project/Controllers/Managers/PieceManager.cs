using Chess_Project.Models.Board;
using Chess_Project.Models.Helper;
using Chess_Project.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Controllers.Managers
{
    internal class PieceManager
    {
        private static PieceManager instance = null;
        internal KeyValuePair<int, int> CurrentKing { get; set; }
        private PieceManager()
        {
            CurrentKing = new KeyValuePair<int, int>();
        }
        internal static PieceManager GetInstance()
        {
            if(instance == null)
            {
                instance = new PieceManager();
            }
            return instance;
        }
        internal ChessPiece HandlePiece(ChessPiece piece, int x, int new_x, int new_y)
        {
            switch (piece.Type)
            {
                case Piece.Pawn:
                    if (!(piece as Pawn).HasMoved)
                    {
                        piece.MoveAmount = 1;
                        (piece as Pawn).HasMoved = true;
                    }
                    if (PawnPromotion(piece.Paint, new_x))
                    {
                        (piece as Pawn).Promote = true;
                    }
                    if (PawnMovedTwice(x, new_x))
                    {
                        (piece as Pawn).MovedTwice = true;
                    }
                    break;
                case Piece.Rook:
                    (piece as Rook).HasMoved = true;
                    if (IsCastling(new_x, new_y))
                    {
                        (piece as Rook).CanCastle = true;
                    }
                    break;
                case Piece.King:
                    (piece as King).HasMoved = true;
                    // Check for check || Checkmate
                    // Check for new space or current space to remove king from check
                    //      Move king
                    //      Capture Enemy with king
                    //      Move Piece in front of king
                    //  `   Capture Enemy ally piece
                    break;
                default: // Do nothing
                    break;
            }
            return piece;
        }
        private bool PawnMovedTwice(int x, int new_x)
        {
            return Math.Abs(new_x - x) == 2;
        }
        private bool PawnPromotion(Color paint, int new_x)
        {
            return (paint == Color.black && new_x == 7) || (paint == Color.white && new_x == 0);
        }
        private bool IsCastling(int new_x, int new_y)
        {
            return new_x == CurrentKing.Key && new_y == CurrentKing.Value;
        }
        internal void FindKing(BoardSpace[,] board, Color paint)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if(board[x, y].Piece != null)
                    {
                        if (board[x, y].Piece.Paint == paint && board[x, y].Piece.Type == Piece.King)
                        {
                            CurrentKing = new KeyValuePair<int, int>(x, y);
                            return;
                        }
                    }
                }
            }
            CurrentKing = new KeyValuePair<int, int>(-1, -1);
        }
        internal void ResetMovedTwice(BoardSpace[,] board, Color paint)
        {
            foreach(BoardSpace space in board)
            {
                if (!space.IsEmpty && space.Piece.Paint == paint)
                {
                    if (space.Piece.Type == Piece.Pawn && (space.Piece as Pawn).MovedTwice)
                    {
                        (space.Piece as Pawn).MovedTwice = false;
                    }
                }
            }
        }
        internal ChessPiece PromotePawn(int option, Color Paint)
        {
            ChessPiece piece = null;
            switch (option)
            {
                case 1:
                    piece = new Knight(Paint);
                    break;
                case 2:
                    piece = new Bishop(Paint);
                    break;
                case 3:
                    piece = new Rook(Paint);
                    break;
                case 4:
                    piece = new Queen(Paint);
                    break;
                default:
                    break;
            }
            return piece;
        }
    }
}
