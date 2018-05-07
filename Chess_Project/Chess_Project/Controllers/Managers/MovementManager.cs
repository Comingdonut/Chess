using Chess_Project.Models.Board;
using Chess_Project.Models.Helper;
using Chess_Project.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess_Project.Controllers.Managers
{
    internal class MovementManager
    {
        private static MovementManager mManager;
        private int dir;
        private int piece_x_axis;
        private int piece_y_axis;
        private const int B_ZERO = 0; // Boundary 2d board
        private const int B_EIGHT = 8;
        private PieceManager pManager;
        private MovementManager()
        {
            pManager = PieceManager.GetInstance();
        }
        internal MovementManager GetInstance()
        {
            if(mManager == null)
            {
                mManager = new MovementManager();
            }
            return mManager;
        }
        internal void SetCoordinates(int p_x_axis, int p_y_axis)
        {
            piece_x_axis = p_x_axis;
            piece_y_axis = p_y_axis;
        }
        #region Determine Normal Available Movement
        internal List<BoardValuePair> DeterminePieceMovement(ChessPiece piece)
        {
            List<BoardValuePair> movement = new List<BoardValuePair>();
            SetDirection(piece.Paint);
            switch (piece.Type)
            {
                case Piece.Pawn:
                    if (!(piece as Pawn).HasMoved)
                        movement.Add(VerticalFoward(2));
                    else
                        movement.Add(VerticalFoward(1));
                    break;
                case Piece.Knight:
                    movement.Add(KnightMovement());
                    break;
                case Piece.Bishop:
                    movement.Add(DiagonalTopLeft(8));
                    movement.Add(DiagonalTopRight(8));
                    movement.Add(DiagonalBottomLeft(8));
                    movement.Add(DiagonalBottomRight(8));
                    break;
                case Piece.Rook:
                    movement.Add(VerticalFoward(8));
                    movement.Add(VerticalBackward(8));
                    movement.Add(HorizontalLeft(8));
                    movement.Add(HorizontalRight(8));
                    break;
                case Piece.Queen:
                    movement.Add(VerticalFoward(8));
                    movement.Add(VerticalBackward(8));
                    movement.Add(HorizontalLeft(8));
                    movement.Add(HorizontalRight(8));
                    movement.Add(DiagonalTopLeft(8));
                    movement.Add(DiagonalTopRight(8));
                    movement.Add(DiagonalBottomLeft(8));
                    movement.Add(DiagonalBottomRight(8));
                    break;
                case Piece.King:
                    movement.Add(VerticalFoward(1));
                    movement.Add(VerticalBackward(1));
                    movement.Add(HorizontalLeft(1));
                    movement.Add(HorizontalRight(1));
                    movement.Add(DiagonalTopLeft(1));
                    movement.Add(DiagonalTopRight(1));
                    movement.Add(DiagonalBottomLeft(1));
                    movement.Add(DiagonalBottomRight(1));
                    break;
                default:
                    // TODO: Handle if no correct type is given
                    break;
            }
            return movement;
        }
        private void SetDirection(Color paint)
        {
            if(paint == Color.white)
                dir = -1;
            dir = 1;
        }
        #endregion
        #region Normal Movement Availablity Checks
        private BoardValuePair VerticalFoward(int amount)
        {
            BoardValuePair vMovement = new BoardValuePair();
            for (int x = (piece_x_axis + dir); x < x + amount; x += dir)
            {
                if (x < B_EIGHT && x >= B_ZERO)
                    vMovement.AddPair(x, piece_y_axis);
                else
                    break;
            }
            return vMovement;
        }
        private BoardValuePair VerticalBackward(int amount)
        {
            BoardValuePair vMovement = new BoardValuePair();
            for (int x = piece_x_axis + (dir * -1); x < (x+amount) * -1; x += (dir * -1))
            {
                if (x < B_EIGHT && x >= B_ZERO)
                    vMovement.AddPair(x, piece_y_axis);
                else
                    break;
            }
            return vMovement;
        }
        private BoardValuePair HorizontalLeft(int amount)
        {
            BoardValuePair hMovement = new BoardValuePair();
            for (int y = (piece_y_axis + dir); y < y + amount; y += dir)
            {
                if (y < B_EIGHT && y >= B_ZERO)
                    hMovement.AddPair(piece_x_axis, y);
                else
                    break;
            }
            return hMovement;
        }
        private BoardValuePair HorizontalRight(int amount)
        {
            BoardValuePair hMovement = new BoardValuePair();
            for (int y = piece_y_axis + (dir * -1); y < y + amount; y += (dir * -1))
            {
                if (y < B_EIGHT && y >= B_ZERO)
                    hMovement.AddPair(piece_x_axis, y);
                else
                    break;
            }
            return hMovement;
        }
        //     \
        //      \
        //      | B |
        private BoardValuePair DiagonalTopLeft(int amount)
        {
            int column = 0;
            BoardValuePair tLMovement = new BoardValuePair();
            if (piece_y_axis + dir >= B_ZERO && piece_y_axis + dir < B_EIGHT)
                column = piece_y_axis + dir;

            for (int x = (piece_x_axis + dir); x < x + amount; x += dir)
            {
                if (column + dir >= B_ZERO && column + dir < B_EIGHT)
                {
                    column += dir;
                    if (x < B_EIGHT && x >= B_ZERO)
                        tLMovement.AddPair(x, column);
                    else
                        break;
                }
                else
                    break;
            }
            return tLMovement;
        }
        //  | B |
        //      \
        //       \
        private BoardValuePair DiagonalBottomRight(int amount)
        {
            int column = 0;
            BoardValuePair bRMovement = new BoardValuePair();
            if (piece_y_axis + (dir * -1) >= B_ZERO && piece_y_axis + (dir * -1) < B_EIGHT)
                column = piece_y_axis + (dir * -1);

            for (int x = piece_x_axis + (dir * -1); x < x + amount; x += (dir * -1))
            {
                if (column + (dir * -1) >= B_ZERO && column + (dir * -1) < B_EIGHT)
                {
                    column += (dir * -1);
                    if (x < B_EIGHT && x >= B_ZERO)
                        bRMovement.AddPair(x, column);
                    else
                        break;
                }
                else
                    break;
            }
            return bRMovement;
        }
        //       /
        //      /
        //  | B |
        private BoardValuePair DiagonalTopRight(int amount)
        {
            int column = 0;
            BoardValuePair tRMovement = new BoardValuePair();
            if (piece_y_axis - dir >= B_ZERO && piece_y_axis - dir < B_EIGHT)
            {
                column = piece_y_axis - dir;
            }
            for (int x = (piece_x_axis + dir); x < x + amount; x += dir)
            {
                if (column - dir >= B_ZERO && column - dir < B_EIGHT)
                {
                    column -= dir;
                    if (x < B_EIGHT && x >= B_ZERO)
                        tRMovement.AddPair(x, column);
                    else
                        break;
                }
                else
                    break;
            }
            return tRMovement;
        }
        //     | B |
        //     /
        //    /
        private BoardValuePair DiagonalBottomLeft(int amount)
        {
            int column = 0;
            BoardValuePair bLMovement = new BoardValuePair();
            if (piece_y_axis - (dir * -1) >= B_ZERO && piece_y_axis - (dir * -1) < B_EIGHT)
                column = piece_y_axis - (dir * -1);

            for (int x = piece_x_axis + (dir * -1); x < x + amount; x += (dir * -1))
            {
                if (column - (dir * -1) >= B_ZERO && column - (dir * -1) < B_EIGHT)
                {
                    column -= (dir * -1);
                    if (x < B_EIGHT && x >= B_ZERO)
                        bLMovement.AddPair(x, column);
                    else
                        break;
                }
                else
                    break;
            }
            return bLMovement;
        }
        private BoardValuePair KnightMovement()
        {
            BoardValuePair kMovement = new BoardValuePair();
            int x = piece_x_axis;
            int y = piece_y_axis;
            int moveX = 1;
            int moveY = 2;
            int moveX2 = -1;
            int moveY2 = 2;
            const int HALF_OF_MOVES = 4; // Knight available moves = 8
            for(int j = 0; j < HALF_OF_MOVES; j++)
            {
                if ((x + moveX >= B_ZERO && x + moveX < B_EIGHT) && (y + moveY >= B_ZERO && y + moveY < B_EIGHT))
                {
                    kMovement.AddPair(x + moveX, y + moveY);
                    moveX *= -1;
                    moveY *= -1;
                    if(j == 2)
                    {
                        moveX = 2;
                        moveY = 1;
                    }
                }
                if ((x + moveX2 >= B_ZERO && x + moveX2 < B_EIGHT) && (y + moveY2 >= B_ZERO && y + moveY2 < B_EIGHT))
                {
                    kMovement.AddPair(x + moveX2, y + moveY2);
                    moveX2 *= -1;
                    moveY2 *= -1;
                    if (j == 2)
                    {
                        moveX2 = -2;
                        moveY2 = 1;
                    }
                }
            }
            return kMovement;
        }
        #endregion
        #region Removing Movement
        internal void CheckForPiece(BoardSpace[,] board, List<BoardValuePair> movement, Color paint)
        {
            int currMovement = -1; // Current Movement Index and currentPair Index
            int currPair = -1;
            for(int m = 0; m < movement.Count(); m++)
            {
                for (int p = 0; p < movement[m].Count(); p++)
                {
                    int x = movement[m][p].Key;
                    int y = movement[m][p].Value;
                    if (!board[x,y].IsEmpty && (board[x, y].Piece.Paint == paint))
                    {
                        currMovement = m;
                        currPair = p;
                        break;
                    }
                    if(!board[x, y].IsEmpty && (board[x, y].Piece.Paint != paint))
                    {
                        currMovement = m;
                        currPair = p+1;
                        break;
                    }
                }
                if(currPair != -1)
                {
                    for(int p = currPair; p < movement[currMovement].Count(); p++)
                    {
                        movement[currMovement].RemoveAt(p);
                    }
                    currMovement = -1;
                    currPair = -1;
                }
            }
        }
        #endregion
        #region Determine Special Available Movement
        internal List<BoardValuePair> DetermineSpecialMovement(BoardSpace[,] board,  ChessPiece piece)
        {
            List<BoardValuePair> movement = new List<BoardValuePair>();
            SetDirection(piece.Paint);
            if (piece.Type == Piece.Rook && !(piece as Rook).HasMoved)
            {
                movement.Add(Castling(board, piece.Paint));
            }
            if (piece.Type == Piece.Pawn)
            {
                movement.Add(PawnCapture(board, piece.Paint));
                movement.Add(EnPassant(board, piece.Paint));
            }
            // Check for King, maybe turn this into a switch statement?
            return movement;
        }
        #endregion
        #region Special Movement Availablity Checks
        // TODO: Document that choosing to land on king will castle
        private BoardValuePair Castling(BoardSpace[,] board, Color paint)
        {
            List<BoardValuePair> sMovement = new List<BoardValuePair>();
            BoardValuePair sMove = new BoardValuePair();
            sMovement.Add(HorizontalLeft(8));
            sMovement.Add(HorizontalRight(8));
            for (int m = 0; m < sMovement.Count(); m++)
            {
                for (int p = 0; p < sMovement[m].Count(); p++)
                {
                    int x = sMovement[m][p].Key;
                    int y = sMovement[m][p].Value;
                    if (!board[x, y].IsEmpty && (board[x, y].Piece.Paint == paint))
                    {
                        if (board[x, y].Piece.Type == Piece.King && !(board[x, y].Piece as King).HasMoved)
                        {
                            sMove.AddPair(x, y);
                            break;
                        }
                    }
                }
            }
            return sMove;
        }
        private BoardValuePair PawnCapture(BoardSpace[,] board, Color paint)
        {
            List<BoardValuePair> sMovement = new List<BoardValuePair>();
            BoardValuePair sMove = new BoardValuePair();
            sMovement.Add(DiagonalTopLeft(1));
            sMovement.Add(DiagonalTopRight(1));
            for (int m = 0; m < sMovement.Count(); m++)
            {
                for (int p = 0; p < sMovement[m].Count(); p++)
                {
                    int x = sMovement[m][p].Key;
                    int y = sMovement[m][p].Value;
                    if (!board[x, y].IsEmpty && (board[x, y].Piece.Paint != paint))
                    {
                        sMove.AddPair(x, y);
                    }
                }
            }
            return sMove;
        }
        private BoardValuePair EnPassant(BoardSpace[,] board, Color paint)
        {
            List<BoardValuePair> sMovement = new List<BoardValuePair>();
            BoardValuePair sMove = new BoardValuePair();
            sMovement.Add(HorizontalLeft(1));
            sMovement.Add(HorizontalRight(1));
            for (int m = 0; m < sMovement.Count(); m++)
            {
                for (int p = 0; p < sMovement[m].Count(); p++)
                {
                    int x = sMovement[m][p].Key;
                    int y = sMovement[m][p].Value;
                    if (!board[x, y].IsEmpty && (board[x, y].Piece.Paint != paint))
                    {
                        if (board[x, y].Piece.Type == Piece.Pawn && (board[x, y].Piece as Pawn).MovedTwice) // Maybe this into its own method
                        {
                            sMove.AddPair(x + dir, y);
                            pManager.SetEnPassant(board[piece_x_axis, piece_y_axis].Piece);
                        }
                    }
                }
            }
            return sMove;
        }
        #endregion
        #region Checking Movement Validity and Moving Pieces
        internal bool CheckAvailablity(List<BoardValuePair> movement, int x, int y)
        {
            bool available = false;
            foreach(BoardValuePair pairList in movement)
            {
                foreach(KeyValuePair<int, int> pair in pairList)
                {
                    if(pair.Key == x && pair.Value == y)
                    {
                        available = true;
                    }
                }
            }
            return available;
        }
        internal void MovePiece(BoardSpace[,] board, int x, int y, int new_x, int new_y)
        {
            ChessPiece piece = board[x, y].Piece;
            pManager.FindKing(board, piece.Paint);
            pManager.ResetMovedTwice(board, piece.Paint);
            piece = pManager.HandlePiece(piece, x, new_x, new_y);
            if(piece.Type == Piece.Rook && (piece as Rook).CanCastle)
            {
                ChessPiece king = board[new_x, new_y].Piece;
                if (y == 0) // Move right for black, Move left for right
                {
                    board[new_x, new_y - 2].Piece = king;
                    board[new_x, new_y - 2].IsEmpty = false;
                    x--;
                }
                else if(y == 7) // Vise Versa
                {
                    board[new_x, new_y + 2].Piece = king;
                    board[new_x, new_y + 2].IsEmpty = false;
                    x++;
                }
                (piece as Rook).CanCastle = false;
            }
            else if(piece.Type == Piece.Pawn && (piece as Pawn).CanEnPassant)
            {
                board[new_x-dir, new_y].Piece = null;
                board[new_x-dir, new_y].IsEmpty = true;
                (piece as Pawn).CanEnPassant = false;
            }
            board[x, y].Piece = null;
            board[x, y].IsEmpty = true;
            board[new_x, new_y].Piece = piece;
            board[new_x, new_y].IsEmpty = false;
            if (pManager.ShouldPromote(piece, new_x))
            {
                // request option here
                // Check for null value
                // If null then prompt again
                // piece = pManager.PromotePawn(option, piece.Paint);
            }
        }
        #endregion
    }
}
