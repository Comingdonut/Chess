using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Rook : ChessPiece
    {
        public Rook()
        {
            Init();
        }

        public Rook(ChessColor color)
        {
            Color = color;
            Init();
        }

        private void Init()
        {
            Piece = "Rook";
            Symbol = 'R';
            ResetMovement();
        }
        public override void MovePiece(ChessSquare[,] board, int[] start, int[] end)
        {
            board[end[0], end[1]].Piece = board[start[0], start[1]].Piece;
            board[start[0], start[1]].Piece = new Space();
        }
        public override bool CheckMovement(ChessSquare[,] board, int[] start, int[] end)
        {
            bool isValid = false;
            List<int[]> available = RestrictMovement(board, start);
            for (int x = 0; x < available.Count; ++x)
            {
                if (available[x][0] == end[0] && available[x][1] == end[1])
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        public override List<int[]> RestrictMovement(ChessSquare[,] board, int[] start)
        {
            ResetMovement();
            List<int[]> available = new List<int[]>();
            bool isAvailable = false;
            for (int x = 1; x < 8; ++x)
            {
                if (start[0] + x < 8)//down
                {
                    isAvailable = IsAvailable(board, start[0] + x, start[1], 0);
                    if (isAvailable == true)
                    {
                        if (canMove[0] == true)
                        {
                            available.Add(new int[] { start[0] + x, start[1] });
                        }
                    }
                }
                if (start[1] + x < 8)//right
                {
                    isAvailable = IsAvailable(board, start[0], start[1] + x, 1);
                    if (isAvailable == true)
                    {
                        if (canMove[1] == true)
                        {
                            available.Add(new int[] { start[0], start[1] + x });
                        }
                    }
                }
                if (start[0] - x >= 0)//up
                {
                    isAvailable = IsAvailable(board, start[0] - x, start[1], 2);
                    if (isAvailable == true)
                    {
                        if (canMove[2] == true)
                        {
                            available.Add(new int[] { start[0] - x, start[1] });
                        }
                    }
                }
                if (start[1] - x >= 0)//left
                {
                    isAvailable = IsAvailable(board, start[0], start[1] - x, 3);
                    if (isAvailable == true)
                    {
                        if (canMove[3] == true)
                        {
                            available.Add(new int[] { start[0], start[1] - x });
                        }
                    }
                }
            }
            return available;
        }
        public override bool IsAvailable(ChessSquare[,] board, int row, int column, int index)
        {
            bool canMove = true;
            if (board[row, column].Piece.Color == Color && board[row, column].Piece.Color != ChessColor.NONE)
            {
                canMove = false;
            }
            if (this.canMove[index] == true)
            {
                this.canMove[index] = canMove;
            }
            return canMove;
        }
        public override void ResetMovement()
        {
            canMove = new bool[] { true, true, true, true };
        }
        /****************************/
    }
}
