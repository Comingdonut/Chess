using Chess_Project.Models.Board;
using Chess_Project.Models.Helper;
using Chess_Project.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess_Project.Controllers.Managers
{
    internal class MovementManager
    {
        private static MovementManager instance;
        private int dir;
        private int piece_x_axis;
        private int piece_y_axis;
        private const int B_ZERO = 0; // Board boundary
        private const int B_EIGHT = 8;
        private const int SWITCH_DIR = -1;
        private PieceManager pManager;
        private MovementManager()
        {
            pManager = PieceManager.GetInstance();
        }
        internal static MovementManager GetInstance()
        {
            if(instance == null)
                instance = new MovementManager();
            return instance;
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
                    movement.Add(VerticalForward(piece.MoveAmount));
                    break;
                case Piece.Knight:
                    movement.Add(KnightMovement());
                    break;
                case Piece.Bishop:
                    movement.Add(DiagonalTopLeft(piece.MoveAmount));
                    movement.Add(DiagonalTopRight(piece.MoveAmount));
                    movement.Add(DiagonalBottomLeft(piece.MoveAmount));
                    movement.Add(DiagonalBottomRight(piece.MoveAmount));
                    break;
                case Piece.Rook:
                    movement.Add(VerticalForward(piece.MoveAmount));
                    movement.Add(VerticalBackward(piece.MoveAmount));
                    movement.Add(HorizontalLeft(piece.MoveAmount));
                    movement.Add(HorizontalRight(piece.MoveAmount));
                    break;
                case Piece.Queen:
                    movement.Add(VerticalForward(piece.MoveAmount));
                    movement.Add(VerticalBackward(piece.MoveAmount));
                    movement.Add(HorizontalLeft(piece.MoveAmount));
                    movement.Add(HorizontalRight(piece.MoveAmount));
                    movement.Add(DiagonalTopLeft(piece.MoveAmount));
                    movement.Add(DiagonalTopRight(piece.MoveAmount));
                    movement.Add(DiagonalBottomLeft(piece.MoveAmount));
                    movement.Add(DiagonalBottomRight(piece.MoveAmount));
                    break;
                case Piece.King:
                    movement.Add(VerticalForward(piece.MoveAmount));
                    movement.Add(VerticalBackward(piece.MoveAmount));
                    movement.Add(HorizontalLeft(piece.MoveAmount));
                    movement.Add(HorizontalRight(piece.MoveAmount));
                    movement.Add(DiagonalTopLeft(piece.MoveAmount));
                    movement.Add(DiagonalTopRight(piece.MoveAmount));
                    movement.Add(DiagonalBottomLeft(piece.MoveAmount));
                    movement.Add(DiagonalBottomRight(piece.MoveAmount));
                    break;
                default:
                    // TODO: Handle if no correct type is given (Maybe prompt again if empty space)
                    break;
            }
            return movement;
        }
        private void SetDirection(Color paint)
        {
            dir = 1;
            if(paint == Color.white)
                dir = -1;
        }
        internal bool CheckBoundary(int row_column)
        {
            bool isValid = false;
            if (row_column >= B_ZERO && row_column < B_EIGHT)
            {
                isValid = true;
            }
            return isValid;
        }
        #endregion
        #region Normal Movement Availablity Checks
        private BoardValuePair VerticalForward(int amount)
        {
            BoardValuePair vMovement = new BoardValuePair();
            int moveCount = amount;
            int x = piece_x_axis;
            do
            {
                x += dir;
                vMovement.AddPair(x, piece_y_axis);
                --moveCount;
            } while (moveCount != 0);
            return vMovement;
        }
        private BoardValuePair VerticalBackward(int amount)
        {
            BoardValuePair vMovement = new BoardValuePair();
            int moveCount = amount;
            int x = piece_x_axis;
            do
            {
                x += dir * SWITCH_DIR;
                vMovement.AddPair(x, piece_y_axis);
                --moveCount;
            } while (moveCount != 0);
            return vMovement;
        }
        private BoardValuePair HorizontalLeft(int amount)
        {
            BoardValuePair hMovement = new BoardValuePair();
            int moveCount = amount;
            int y = piece_y_axis;
            do
            {
                y += dir;
                hMovement.AddPair(piece_x_axis, y);
                --moveCount;
            } while (moveCount != 0);
            return hMovement;
        }
        private BoardValuePair HorizontalRight(int amount)
        {
            BoardValuePair hMovement = new BoardValuePair();
            int moveCount = amount;
            int y = piece_y_axis;
            do
            {
                y += dir * SWITCH_DIR;
                hMovement.AddPair(piece_x_axis, y);
                --moveCount;
            } while (moveCount != 0);
            return hMovement;
        }
        //     \
        //      \
        //      | B |
        private BoardValuePair DiagonalTopLeft(int amount)
        {
            BoardValuePair movement = new BoardValuePair();
            int x = piece_x_axis;
            int y = piece_y_axis;
            int moveCount = amount;
            do
            {
                x += dir;
                y += dir;
                movement.AddPair(x, y);
                --moveCount;
            } while (moveCount != 0);
            return movement;
        }
        //  | B |
        //      \
        //       \
        private BoardValuePair DiagonalBottomRight(int amount)
        {
            BoardValuePair movement = new BoardValuePair();
            int x = piece_x_axis;
            int y = piece_y_axis;
            int moveCount = amount;
            do
            {
                x += dir * SWITCH_DIR;
                y += dir * SWITCH_DIR;
                movement.AddPair(x, y);
                --moveCount;
            } while (moveCount != 0);
            return movement;
        }
        //       /
        //      /
        //  | B |
        private BoardValuePair DiagonalTopRight(int amount)
        {
            BoardValuePair movement = new BoardValuePair();
            int x = piece_x_axis;
            int y = piece_y_axis;
            int moveCount = amount;
            do
            {
                x += dir;
                y += dir * SWITCH_DIR;
                movement.AddPair(x, y);
                --moveCount;
            } while (moveCount != 0);
            return movement;
        }
        //     | B |
        //     /
        //    /
        private BoardValuePair DiagonalBottomLeft(int amount)
        {
            BoardValuePair movement = new BoardValuePair();
            int x = piece_x_axis;
            int y = piece_y_axis;
            int moveCount = amount;
            do
            {
                x += dir * SWITCH_DIR;
                y += dir;
                movement.AddPair(x, y);
                --moveCount;
            } while (moveCount != 0);
            return movement;
        }
        private BoardValuePair KnightMovement()
        {
            BoardValuePair kMovement = new BoardValuePair();
            int x = piece_x_axis;
            int y = piece_y_axis;
            kMovement.AddPair(x - 1, y - 2);
            kMovement.AddPair(x - 2, y - 1);
            kMovement.AddPair(x - 2, y + 1);
            kMovement.AddPair(x - 1, y + 2);
            kMovement.AddPair(x + 1, y + 2);
            kMovement.AddPair(x + 2, y + 1);
            kMovement.AddPair(x + 2, y - 1);
            kMovement.AddPair(x + 1, y - 2);
            return kMovement;
        }
        #endregion
        #region Removing Movement
        internal void CheckForPiece(BoardSpace[,] board, List<BoardValuePair> movement, ChessPiece piece)
         {
            int currMovement = -1; // Current Movement Index and currentPair Index
            int currPair = -1;
            BoardValuePair knightRestrictions = new BoardValuePair();
            for(int m = 0; m < movement.Count(); m++)
            {
                for (int p = 0; p < movement[m].Count(); p++)
                {
                    int x = movement[m][p].Key;
                    int y = movement[m][p].Value;
                    if (piece.Type == Piece.Knight)
                    {
                        if(!CheckBoundary(x) || !CheckBoundary(y) || (!board[x, y].IsEmpty && board[x, y].Piece.Paint == piece.Paint))
                        {
                            knightRestrictions.AddPair(x, y);
                        }
                    }
                    else if (!CheckBoundary(x) || !CheckBoundary(y))
                    {
                        currMovement = m;
                        currPair = p;
                        break;
                    }
                    else if (!board[x,y].IsEmpty && (board[x, y].Piece.Paint == piece.Paint) ||
                        !board[x, y].IsEmpty && (board[x, y].Piece.Paint != piece.Paint) && piece.Type == Piece.Pawn) //Pawn can't move on enemy piece
                    {
                        currMovement = m;
                        currPair = p;
                        break;
                    }
                    else if(!board[x, y].IsEmpty && (board[x, y].Piece.Paint != piece.Paint))
                    {
                        currMovement = m;
                        currPair = p+dir;
                        break;
                    }
                }
                if(currPair != -1)
                {
                    int count = movement[currMovement].Count;
                    for (int p = currPair; p < count; p++)
                    {
                        movement[currMovement].RemoveAt(currPair);
                    }
                    currMovement = -1;
                    currPair = -1;
                }
                if (knightRestrictions.Count() != 0)
                {
                    for (int x = 0; x < knightRestrictions.Count(); x++)
                    {
                        int count = movement[m].Count-1;
                        for (int p = count; p > -1; p--)
                        {
                            if (knightRestrictions[x].Key == movement[m][p].Key && knightRestrictions[x].Value == movement[m][p].Value)
                            {
                                movement[m].RemoveAt(p);
                                break;
                            }
                        }
                    }
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
            else if (piece.Type == Piece.Pawn)
            {
                movement.Add(PawnCapture(board, piece.Paint)); 
                movement.Add(EnPassant(board, piece.Paint));
            }
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
                    if (!CheckBoundary(x) || !CheckBoundary(y))
                    {
                        break;
                    }
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
                    if (!CheckBoundary(x) || !CheckBoundary(y))
                    {
                        break;
                    }
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
                    if (!CheckBoundary(x) || !CheckBoundary(y))
                    {
                        break;
                    }
                    if (!board[x, y].IsEmpty && (board[x, y].Piece.Paint != paint))
                    {
                        if (board[x, y].Piece.Type == Piece.Pawn && (board[x, y].Piece as Pawn).MovedTwice) // Maybe this into its own method
                        {
                            sMove.AddPair(x + dir, y);
                            (board[piece_x_axis, piece_y_axis].Piece as Pawn).CanEnPassant = true;
                        }
                    }
                }
            }
            return sMove;
        }
        #endregion
        #region Check and Checkmate
        private void CheckForCheck(BoardSpace[,] board) // After Searching for a king but before selecting a piece
        {
            List<BoardValuePair> movement = new List<BoardValuePair>();
            BoardValuePair enemyChecks = new BoardValuePair();
            int kingX = pManager.CurrentKing.Key;
            int kingY = pManager.CurrentKing.Value;
            SetCoordinates(kingX, kingY);
            movement.Add(VerticalForward(8));
            movement.Add(VerticalBackward(8));
            movement.Add(HorizontalLeft(8));
            movement.Add(HorizontalRight(8));
            movement.Add(DiagonalTopLeft(8));
            movement.Add(DiagonalTopRight(8));
            movement.Add(DiagonalBottomLeft(8));
            movement.Add(DiagonalBottomRight(8));
            movement.Add(KnightMovement());
            foreach(BoardValuePair group in movement)
            {
                foreach(KeyValuePair<int, int> pair in group) // 1. Run a check for queen movement and knight movement on king.
                {
                    int enemyX = pair.Key;
                    int enemyY = pair.Value;
                    if (board[enemyX, enemyY].Piece.Paint != board[kingX, kingY].Piece.Paint) // 2. Check for enemy piece
                    {
                        List<BoardValuePair> eMovement = new List<BoardValuePair>();
                        SetCoordinates(enemyX, enemyY);
                        eMovement.AddRange(DeterminePieceMovement(board[enemyX, enemyY].Piece));
                        eMovement.AddRange(DetermineSpecialMovement(board, board[enemyX, enemyY].Piece));

                        foreach (BoardValuePair eGroup in movement)
                        {
                            foreach (KeyValuePair<int, int> ePair in group) // 3. See enemy piece movement
                            {
                                if(ePair.Key == kingX && ePair.Value == kingY) // 4. If enemy movement get king
                                {
                                    enemyChecks.Add(new KeyValuePair<int, int>(enemyX, enemyY)); // 5. Add enemy && put king in check
                                    (board[kingX, kingY].Piece as King).InCheck = true; 
                                }
                            }
                        }
                    }
                }
            }
        }
        // New method - 
        //      After Determining piece movement
        //          Start: Check if number of enemy check is one
        //              check if any movement removes king from check
        //                  End: remove any movement that doesn't help remove king from check
        #endregion
        #region Checking Movement Validity and Moving Pieces
        internal bool CheckAvailablity(List<BoardValuePair> movement, int x, int y)
        {
            bool available = false;
            foreach(BoardValuePair pairList in movement)
            {
                if (pairList.Count != 0)
                {
                    foreach (KeyValuePair<int, int> pair in pairList)
                    {
                        if (pair.Key == x && pair.Value == y)
                        {
                            available = true;
                        }
                    }
                }
            }
            return available;
        }
        internal void MovePiece(BoardSpace[,] board, int x, int y, int new_x, int new_y)
        {
            ChessPiece piece = board[x, y].Piece;
            pManager.ResetMovedTwice(board, piece.Paint);
            pManager.FindKing(board, piece.Paint);
            piece = pManager.HandlePiece(piece, x, new_x, new_y);
            if(piece.Type == Piece.Rook && (piece as Rook).CanCastle)
            {
                // Activate en passant by moving rook over ally king
                // Don't need to reset kings space because will be replaced by rook
                ChessPiece king = board[new_x, new_y].Piece;
                board[new_x, new_y] = new BoardSpace(true);
                if (y == 0) // Move right for black, Move left for white// TODO: Look at this logic again
                {
                    board[new_x, new_y - 2].Piece = king;
                    board[new_x, new_y - 2].IsEmpty = false;
                    new_y--;
                }
                else if(y == 7) // Vise Versa
                {
                    board[new_x, new_y + 2].Piece = king;
                    board[new_x, new_y + 2].IsEmpty = false;
                    new_y++;
                }
                (piece as Rook).CanCastle = false;
            }
            else if(piece.Type == Piece.Pawn && (piece as Pawn).CanEnPassant)
            {
                board[new_x - dir, new_y] = new BoardSpace(true);
                (piece as Pawn).CanEnPassant = false;
            }
            board[x, y] = new BoardSpace(true);
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
