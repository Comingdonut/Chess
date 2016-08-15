using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Knight : ChessPiece
    {
        public Knight()
        {
            Init();
        }

        public Knight(ChessColor color)
        {
            Color = color;
            Init();
        }

        private void Init()
        {
            Piece = "Knight";
            Symbol = 'N';
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
            if (start[0] - 2 >= 0 && start[1] - 1 >= 0)//up 2 left 1
            {
                isAvailable = IsAvailable(board, start[0] - 2, start[1] - 1, 0);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] - 2, start[1] - 1 });
                }
            }
            if (start[0] - 2 >= 0 && start[1] + 1 < 8)//up 2 right 1
            {
                isAvailable = IsAvailable(board, start[0] - 2, start[1] + 1, 1);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] - 2, start[1] + 1 });
                }
            }
            if (start[0] - 1 >= 0 && start[1] - 2 >= 0)//up 1 left 2
                {
                    isAvailable = IsAvailable(board, start[0] - 1, start[1] - 2, 2);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] - 1, start[1] - 2 });
                }
            }
            if (start[0] - 1 >= 0 && start[1] + 2 < 8)//up 1 right 2
            {
                isAvailable = IsAvailable(board, start[0] - 1, start[1] + 2, 3);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] - 1, start[1] + 2 });
                }
            }
            if (start[0] + 2 < 8 && start[1] - 1 >= 0 )//down 2 Left 1
            {
                isAvailable = IsAvailable(board, start[0] + 2, start[1] - 1, 4);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] + 2, start[1] - 1 });
                }
            }
            if (start[0] + 2 < 8 && start[1] + 1 < 8)//down 2 right 1
            {
                isAvailable = IsAvailable(board, start[0] + 2, start[1] + 1, 5);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] + 2, start[1] + 1 });
                }
            }
            if (start[0] + 1 < 8 && start[1] - 2 >= 0)//down 1 Left 2
            {
                isAvailable = IsAvailable(board, start[0] + 1, start[1] - 2, 6);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] + 1, start[1] - 2 });
                }
            }
            if (start[0] + 1 < 8 && start[1] + 2 < 8)//down 1 right 2
            {
                isAvailable = IsAvailable(board, start[0] + 1, start[1] + 2, 7);
                if (isAvailable == true)
                {
                    available.Add(new int[] { start[0] + 1, start[1] + 2 });
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
            canMove = new bool[] { true, true, true, true, true, true, true, true };
        }
        /****************************/
    }
}
