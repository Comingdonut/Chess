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
            Piece = "King";
            Symbol = 'K';
            ResetMovement();
        }
        public override void MovePiece(ChessSquare[,] board, int[] start, int[] end)
        {
            board[end[0], end[1]].Piece = board[start[0], start[1]].Piece;
            board[start[0], start[1]].Piece = new Space();
        }
        public override bool CheckSquare(ChessSquare[,] board, int[] end)
        {
            bool isValid = false;
            if (board[end[0], end[1]].Piece.GetType() == typeof(Space))
            {
                isValid = true;
            }
            else if(board[end[0], end[1]].Piece.Color != Color)
            {
                isValid = true;
            }
            return isValid;
        }
        public override bool CheckMovement(ChessSquare[,] board, int[] start, int[] end)
        {
            bool isValid = false;
            List<int[]> available = RestrictMovement(board, start);
            for (int x = 0; x < available.Count; ++x )
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
            if (start[0] + 1 < 8)//down 1
            {
                isAvailable = IsAvailable(board, start[0] + 1, start[1], 0);
                if (isAvailable == true)
                {
                    if(canMove[0] == true)
                    {
                        available.Add(new int[] { start[0] + 1, start[1] });
                    }
                }
            }
            if (start[0] + 1 < 8 && start[1] - 1 >= 0)//down Left 1
            {
                isAvailable = IsAvailable(board, start[0] + 1, start[1] - 1, 1);
                if (isAvailable == true)
                {
                    if (canMove[1] == true)
                    {
                        available.Add(new int[] { start[0] + 1, start[1] - 1 });
                    }
                }
            }
            if (start[0] + 1 < 8 && start[1] + 1 < 8)//down right 1
            {
                isAvailable = IsAvailable(board, start[0] + 1, start[1] + 1, 2);
                if (isAvailable == true)
                {
                    if (canMove[2] == true)
                    {
                        available.Add(new int[] { start[0] + 1, start[1] + 1 });
                    }
                }
            }
            if (start[1] + 1 < 8)//right 1
            {
                isAvailable = IsAvailable(board, start[0], start[1] + 1, 3);
                if (isAvailable == true)
                {
                    if (canMove[3] == true)
                    {
                        available.Add(new int[] { start[0], start[1] + 1 });
                    }
                }
            }
            if (start[0] - 1 >= 0)//up 1
            {
                isAvailable = IsAvailable(board, start[0] - 1, start[1], 4);
                if (isAvailable == true)
                {
                    if (canMove[4] == true)
                    {
                        available.Add(new int[] { start[0] - 1, start[1] });
                    }
                }
            }
            if (start[0] - 1  >= 0 && start[1] - 1 >= 0)//up left 1
            {
                isAvailable = IsAvailable(board, start[0] - 1, start[1] - 1, 5);
                if (isAvailable == true)
                {
                    if (canMove[5] == true)
                    {
                        available.Add(new int[] { start[0] - 1, start[1] - 1 });
                    }
                }
            }
            if (start[0] - 1 >= 0 && start[1] + 1 < 8)//up right 1
            {
                isAvailable = IsAvailable(board, start[0] - 1, start[1] + 1, 6);
                if (isAvailable == true)
                {
                    if (canMove[6] == true)
                    {
                        available.Add(new int[] { start[0] - 1, start[1] + 1 });
                    }
                }
            }
            if (start[1] - 1 >= 0)//left 1
            {
                isAvailable = IsAvailable(board, start[0], start[1] - 1, 7);
                if (isAvailable == true)
                {
                    if (canMove[7] == true)
                    {
                        available.Add(new int[] { start[0], start[1] - 1 });
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
