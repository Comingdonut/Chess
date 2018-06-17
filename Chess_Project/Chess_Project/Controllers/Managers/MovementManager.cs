using Chess_Project.Models.Board;
using Chess_Project.Models.Helper;
using Chess_Project.Models.Pieces;
using Chess_Project.Views;
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
        #region Removing Invalid Movement
        internal void RemoveInvalidMovement(BoardSpace[,] board, List<BoardValuePair> movement, ChessPiece piece)
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
                        currPair = ++p;
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
                movement.Add(Castling(board, piece.Paint)); // Test this again
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
                        }
                        break;
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
                        if (board[x, y].Piece.Type == Piece.Pawn && (board[x, y].Piece as Pawn).MovedTwice)
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
        internal bool Check(BoardSpace[,] board, Color paint)
        {
            pManager.FindKing(board, paint);
            if (pManager.CurrentKing.Key != -1)
            {
                List<BoardValuePair> movement = new List<BoardValuePair>();
                BoardValuePair enemyChecks = new BoardValuePair();
                int kingX = pManager.CurrentKing.Key;
                int kingY = pManager.CurrentKing.Value;
                ChessPiece kingPiece = board[kingX, kingY].Piece;

                SetCoordinates(kingX, kingY);
                SetDirection(paint);
                movement.Add(VerticalForward(7));
                movement.Add(VerticalBackward(7));
                movement.Add(HorizontalLeft(7));
                movement.Add(HorizontalRight(7));
                movement.Add(DiagonalTopLeft(7));
                movement.Add(DiagonalTopRight(7));
                movement.Add(DiagonalBottomLeft(7));
                movement.Add(DiagonalBottomRight(7));
                movement.Add(KnightMovement());
                RemoveInvalidMovement(board, movement, kingPiece);

                foreach (BoardValuePair group in movement)
                {
                    foreach (KeyValuePair<int, int> pair in group) // 1. Loop through movement
                    {
                        int enemyX = pair.Key;
                        int enemyY = pair.Value;
                        ChessPiece enemyPiece = board[enemyX, enemyY].Piece;
                        if (!board[enemyX, enemyY].IsEmpty && enemyPiece.Paint != kingPiece.Paint) // 2. Check for enemy piece
                        {
                            List<BoardValuePair> eMovement = new List<BoardValuePair>();
                            SetCoordinates(enemyX, enemyY);
                            eMovement.AddRange(DeterminePieceMovement(enemyPiece));
                            eMovement.Add(PawnCapture(board, enemyPiece.Paint));
                            RemoveInvalidMovement(board, eMovement, enemyPiece);
                            foreach (BoardValuePair eGroup in eMovement)
                            {
                                foreach (KeyValuePair<int, int> ePair in eGroup) // 3. Loop through enemy movement
                                {
                                    if (ePair.Key == kingX && ePair.Value == kingY) // 4. If enemy movement can capture king
                                    {
                                        enemyChecks.AddPair(enemyX, enemyY); // 5. Add enemy && put king in check
                                        (kingPiece as King).InCheck = true;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool Check(BoardSpace[,] board, Color paint, int x, int y, int new_x, int new_y)
        {
            ChessPiece piece = board[x, y].Piece;
            ChessPiece newPiece = null;
            if (!board[new_x, new_y].IsEmpty)
            {
                newPiece = board[new_x, new_y].Piece;
            }
            board[x, y] = new BoardSpace(true);
            board[new_x, new_y].Piece = piece;
            board[new_x, new_y].IsEmpty = false;

            bool inCheck = Check(board, paint);

            board[x, y].Piece = piece;
            board[x, y].IsEmpty = false;
            if (newPiece != null)
            {
                board[new_x, new_y].Piece = newPiece;
                board[new_x, new_y].IsEmpty = false;
            }
            else
            {
                board[new_x, new_y] = new BoardSpace(true);
            }

            pManager.FindKing(board, paint);

            return inCheck;
        }
        public bool CheckMate(BoardSpace[,] board, Color playerPaint)
        {
            // Check all legal movement and if King stays in check or goes in check no matter what then CheckMate.
            bool checkMate = true;
            List<BoardValuePair> movement = new List<BoardValuePair>();
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    ChessPiece piece = board[x, y].Piece;
                    if (piece != null && piece.Paint == playerPaint)
                    {
                        SetCoordinates(x, y);
                        movement.AddRange(DeterminePieceMovement(piece));
                        RemoveInvalidMovement(board, movement, piece);
                        movement.AddRange(DetermineSpecialMovement(board, piece));
                        for(int z = 0; z < movement.Count(); z++)
                        {
                            if (movement[z].Count() > 0 && !Check(board, piece.Paint, x, y, movement[z][0].Key, movement[z][0].Value))
                            {
                                return checkMate = false;
                            }
                        }
                        movement = new List<BoardValuePair>();
                    }
                }
            }
            return checkMate;
        }
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
            piece = pManager.HandlePiece(piece, x, new_x, new_y);
            if(piece.Type == Piece.Rook && (piece as Rook).CanCastle)
            {
                // Activate castling by moving rook over ally king
                ChessPiece king = board[new_x, new_y].Piece;
                board[new_x, new_y] = new BoardSpace(true);
                if (y == 0) // Move right for black, Move left for white
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
            else if(piece.Type == Piece.Pawn)
            {
                if((piece as Pawn).CanEnPassant)
                {
                    board[new_x - dir, new_y] = new BoardSpace(true);
                    (piece as Pawn).CanEnPassant = false;
                }
                else if ((piece as Pawn).Promote)
                {
                    PromptManager pmtManager = PromptManager.GetInstance();
                    GameView gv = new GameView();
                    int option = pmtManager.PromptForOption(gv.PawnPromotion, gv.PromotionOptions);
                    piece = pManager.PromotePawn(option, piece.Paint);
                }
            }
            board[x, y] = new BoardSpace(true);
            board[new_x, new_y].Piece = piece;
            board[new_x, new_y].IsEmpty = false;
        }
        #endregion
    }
}
