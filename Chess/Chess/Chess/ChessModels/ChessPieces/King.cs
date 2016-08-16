using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class King : ChessPiece
    {
        public King()
        {
            Init();
        }

        public King(ChessColor color)
        {
            Color = color;
            Init();
        }

        private void Init()
        {
            Name = "King";
            Symbol = 'K';
            ResetMovement();
        }
        public override void MovePiece(ChessSquare[,] board, int startX, int startY, int endX, int endY)
        {
            board[endX, endY].Piece = board[startX, startY].Piece;
            board[startX, startY].Piece = new Space();
        }
        public override bool CheckMovement(ChessSquare[,] board, int startX, int startY, int endX, int endY)
        {
            bool isValid = false;
            List<int[]> available = RestrictMovement(board, startX, startY);
            for (int x = 0; x < available.Count; ++x )
            {
                if (available[x][0] == endX && available[x][1] == endY)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        public override List<int[]> RestrictMovement(ChessSquare[,] board, int startX, int startY)
        {
            ResetMovement();
            List<int[]> available = new List<int[]>();
            bool isAvailable = false;
            if (startX + 1 < 8)//down 1
            {
                isAvailable = IsAvailable(board, startX + 1, startY, 0);
                if (isAvailable == true)
                {
                    if(canMove[0] == true)
                    {
                        available.Add(new int[] { startX + 1, startY });
                    }
                }
            }
            if (startX + 1 < 8 && startY - 1 >= 0)//down Left 1
            {
                isAvailable = IsAvailable(board, startX + 1, startY - 1, 1);
                if (isAvailable == true)
                {
                    if (canMove[1] == true)
                    {
                        available.Add(new int[] { startX + 1, startY - 1 });
                    }
                }
            }
            if (startX + 1 < 8 && startY + 1 < 8)//down right 1
            {
                isAvailable = IsAvailable(board, startX + 1, startY + 1, 2);
                if (isAvailable == true)
                {
                    if (canMove[2] == true)
                    {
                        available.Add(new int[] { startX + 1, startY + 1 });
                    }
                }
            }
            if (startY + 1 < 8)//right 1
            {
                isAvailable = IsAvailable(board, startX, startY + 1, 3);
                if (isAvailable == true)
                {
                    if (canMove[3] == true)
                    {
                        available.Add(new int[] { startX, startY + 1 });
                    }
                }
            }
            if (startX - 1 >= 0)//up 1
            {
                isAvailable = IsAvailable(board, startX - 1, startY, 4);
                if (isAvailable == true)
                {
                    if (canMove[4] == true)
                    {
                        available.Add(new int[] { startX - 1, startY });
                    }
                }
            }
            if (startX - 1  >= 0 && startY - 1 >= 0)//up left 1
            {
                isAvailable = IsAvailable(board, startX - 1, startY - 1, 5);
                if (isAvailable == true)
                {
                    if (canMove[5] == true)
                    {
                        available.Add(new int[] { startX - 1, startY - 1 });
                    }
                }
            }
            if (startX - 1 >= 0 && startY + 1 < 8)//up right 1
            {
                isAvailable = IsAvailable(board, startX - 1, startY + 1, 6);
                if (isAvailable == true)
                {
                    if (canMove[6] == true)
                    {
                        available.Add(new int[] { startX - 1, startY + 1 });
                    }
                }
            }
            if (startY - 1 >= 0)//left 1
            {
                isAvailable = IsAvailable(board, startX, startY - 1, 7);
                if (isAvailable == true)
                {
                    if (canMove[7] == true)
                    {
                        available.Add(new int[] { startX, startY - 1 });
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
            if (this.canMove[index])
            {
                this.canMove[index] = canMove;
            }
            return canMove;
        }
        public override void ResetMovement()
        {
            canMove = new bool[] { true, true, true, true, true, true, true, true };
        }
        /****************************/
    }
}
