﻿using Chess_Project.Models.Board;
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
        private const int B_ZERO = 0; // Bourd boundary
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
                    movement.Add(VerticalForward(piece.MoveAmount)); // TODO: set moveAmount is equal to 1
                    break;
                case Piece.Knight:
                    //movement.Add(KnightMovement());
                    break;
                case Piece.Bishop:
                    //movement.Add(DiagonalTopLeft(piece.MoveAmount));
                    //movement.Add(DiagonalTopRight(piece.MoveAmount));
                    //movement.Add(DiagonalBottomLeft(piece.MoveAmount));
                    //movement.Add(DiagonalBottomRight(piece.MoveAmount));
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
                    //movement.Add(DiagonalTopLeft(piece.MoveAmount));
                    //movement.Add(DiagonalTopRight(piece.MoveAmount));
                    //movement.Add(DiagonalBottomLeft(piece.MoveAmount));
                    //movement.Add(DiagonalBottomRight(piece.MoveAmount));
                    break;
                case Piece.King:
                    //movement.Add(VerticalForward(piece.MoveAmount));
                    //movement.Add(VerticalBackward(piece.MoveAmount));
                    //movement.Add(HorizontalLeft(piece.MoveAmount));
                    //movement.Add(HorizontalRight(piece.MoveAmount));
                    //movement.Add(DiagonalTopLeft(piece.MoveAmount));
                    //movement.Add(DiagonalTopRight(piece.MoveAmount));
                    //movement.Add(DiagonalBottomLeft(piece.MoveAmount));
                    //movement.Add(DiagonalBottomRight(piece.MoveAmount));
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
            int column = 0;
            BoardValuePair tLMovement = new BoardValuePair();
            if (CheckBoundary(piece_y_axis + dir))
                column = piece_y_axis + dir;

            for (int x = (piece_x_axis + dir); x < x + amount; x += dir)
            {
                if (CheckBoundary(column + dir))
                {
                    column += dir;
                    if (CheckBoundary(x))
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
            if (CheckBoundary(piece_y_axis + (dir * -1)))
                column = piece_y_axis + (dir * -1);

            for (int x = piece_x_axis + (dir * -1); x < x + amount; x += (dir * -1))
            {
                if (CheckBoundary(column + (dir * -1)))
                {
                    column += (dir * -1);
                    if (CheckBoundary(x))
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
            if (CheckBoundary(piece_y_axis - dir))
            {
                column = piece_y_axis - dir;
            }
            for (int x = (piece_x_axis + dir); x < x + amount; x += dir)
            {
                if (CheckBoundary(column - dir))
                {
                    column -= dir;
                    if (CheckBoundary(x))
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
            if (CheckBoundary(piece_y_axis - (dir * -1)))
                column = piece_y_axis - (dir * -1);

            for (int x = piece_x_axis + (dir * -1); x < x + amount; x += (dir * -1))
            {
                if (CheckBoundary(column - (dir * -1)))
                {
                    column -= (dir * -1);
                    if (CheckBoundary(x))
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
                if (CheckBoundary(x + moveX) && CheckBoundary(y + moveY))
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
                if (CheckBoundary(x + moveX2) && CheckBoundary(y + moveY2))
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
                    if (!CheckBoundary(x) || !CheckBoundary(y))
                    {
                        currMovement = m;
                        currPair = p;
                        break;
                    }
                    if (!board[x,y].IsEmpty && (board[x, y].Piece.Paint == paint))
                    {
                        currMovement = m;
                        currPair = p;
                        break;
                    }
                    if(!board[x, y].IsEmpty && (board[x, y].Piece.Paint != paint))
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
            pManager.FindKing(board, piece.Paint); // Here
            pManager.ResetMovedTwice(board, piece.Paint); // Here
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
