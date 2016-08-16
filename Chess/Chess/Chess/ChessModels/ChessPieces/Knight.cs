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
            Name = "Knight";
            Symbol = 'N';
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
            for (int x = 0; x < available.Count; ++x)
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
            if (startX - 2 >= 0 && startY - 1 >= 0)//up 2 left 1
            {
                isAvailable = IsAvailable(board, startX - 2, startY - 1, 0);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX - 2, startY - 1 });
                }
            }
            if (startX - 2 >= 0 && startY + 1 < 8)//up 2 right 1
            {
                isAvailable = IsAvailable(board, startX - 2, startY + 1, 1);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX - 2, startY + 1 });
                }
            }
            if (startX - 1 >= 0 && startY - 2 >= 0)//up 1 left 2
                {
                    isAvailable = IsAvailable(board, startX - 1, startY - 2, 2);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX - 1, startY - 2 });
                }
            }
            if (startX - 1 >= 0 && startY + 2 < 8)//up 1 right 2
            {
                isAvailable = IsAvailable(board, startX - 1, startY + 2, 3);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX - 1, startY + 2 });
                }
            }
            if (startX + 2 < 8 && startY - 1 >= 0 )//down 2 Left 1
            {
                isAvailable = IsAvailable(board, startX + 2, startY - 1, 4);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX + 2, startY - 1 });
                }
            }
            if (startX + 2 < 8 && startY + 1 < 8)//down 2 right 1
            {
                isAvailable = IsAvailable(board, startX + 2, startY + 1, 5);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX + 2, startY + 1 });
                }
            }
            if (startX + 1 < 8 && startY - 2 >= 0)//down 1 Left 2
            {
                isAvailable = IsAvailable(board, startX + 1, startY - 2, 6);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX + 1, startY - 2 });
                }
            }
            if (startX + 1 < 8 && startY + 2 < 8)//down 1 right 2
            {
                isAvailable = IsAvailable(board, startX + 1, startY + 2, 7);
                if (isAvailable == true)
                {
                    available.Add(new int[] { startX + 1, startY + 2 });
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
        public override List<int[]> Test(ChessSquare[,] board, int startX, int startY, int endX, int endY)
        {
            bool isMoveSet = false;
            List<int[]> available = new List<int[]>();
                if (startX - 2 >= 0 && startY - 1 >= 0)//up 2 left 1
                {
                    available.Add(new int[] { startX - 2, startY - 1 });
                    if (startX - 2 == endX && startY - 1 == endY)
                    {
                        isMoveSet = true;
                    }
                }
            Test2(available, isMoveSet);
            if (isMoveSet == false)
            {
                if (startX - 2 >= 0 && startY + 1 < 8)//up 2 right 1
                {
                    available.Add(new int[] { startX - 2, startY + 1 });
                    if (startX - 2 == endX && startY + 1 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                if (startX - 1 >= 0 && startY - 2 >= 0)//up 1 left 2
                {
                    available.Add(new int[] { startX - 1, startY - 2 });
                    if (startX - 1 == endX && startY - 2 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                if (startX - 1 >= 0 && startY + 2 < 8)//up 1 right 2
                {
                    available.Add(new int[] { startX - 1, startY + 2 });
                    if (startX - 1 == endX && startY + 2 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                if (startX + 2 < 8 && startY - 1 >= 0)//down 2 left 1
                {
                    available.Add(new int[] { startX + 2, startY - 1 });
                    if (startX + 2 == endX && startY - 1 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                if (startX + 2 < 8 && startY + 1 < 8)//down 2 right 1
                {
                    available.Add(new int[] { startX + 2, startY + 1 });
                    if (startX + 2 == endX && startY + 1 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                if (startX + 1 < 8 && startY - 2 >= 0)//down 1 left 2
                {
                    available.Add(new int[] { startX + 1, startY - 2 });
                    if (startX + 1 == endX && startY - 2 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                if (startX + 1 < 8 && startY + 2 < 8)//down 1 right 2
                {
                    available.Add(new int[] { startX + 1, startY + 2 });
                    if (startX + 1 == endX && startY + 2 == endY)
                    {
                        isMoveSet = true;
                    }
                }
                Test2(available, isMoveSet);
            }
            return available;
        }

        public override void Test2(List<int[]> available, bool isMoveSet)
        {
            if (isMoveSet == false)
            {
                available.Clear();
            }
        }
        /****************************/
    }
}
