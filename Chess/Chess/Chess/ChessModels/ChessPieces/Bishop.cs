using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Bishop : ChessPiece
    {
        public Bishop()
        {
            Init();
        }

        public Bishop(ChessColor color)
        {
            Color = color;
            Init();
        }

        private void Init()
        {
            Name = "Bishop";
            Symbol = 'B';
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
            for (int x = 1; x < 8; ++x)
            {
                if (startX + x < 8 && startY - x >= 0)//down Left
                {
                    isAvailable = IsAvailable(board, startX + x, startY - x, 0);
                    if (isAvailable == true)
                    {
                        if (canMove[0] == true)
                        {
                            available.Add(new int[] { startX + x, startY - x });
                        }
                    }
                }
                if (startX + x < 8 && startY + x < 8)//down right
                {
                    isAvailable = IsAvailable(board, startX + x, startY + x, 1);
                    if (isAvailable == true)
                    {
                        if (canMove[1] == true)
                        {
                            available.Add(new int[] { startX + x, startY + x });
                        }
                    }
                }
                if (startX - x >= 0 && startY - x >= 0)//up left
                {
                    isAvailable = IsAvailable(board, startX - x, startY - x, 2);
                    if (isAvailable == true)
                    {
                        if (canMove[2] == true)
                        {
                            available.Add(new int[] { startX - x, startY - x });
                        }
                    }
                }
                if (startX - x >= 0 && startY + x < 8)//up right
                {
                    isAvailable = IsAvailable(board, startX - x, startY + x, 3);
                    if (isAvailable == true)
                    {
                        if (canMove[3] == true)
                        {
                            available.Add(new int[] { startX - x, startY + x });
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
            canMove = new bool[] { true, true, true, true};
        }
        public override List<int[]> Test(ChessSquare[,] board, int startX, int startY, int endX, int endY)
        {
            bool isMoveSet = false;
            List<int[]> available = new List<int[]>();
            Test2(available, isMoveSet);
            for (int x = 1; x < 8; ++x)
            {
                if (startX + x < 8 && startY - x >= 0)//down Left
                {
                    available.Add(new int[] { startX + x, startY - x });
                    if (startX + x == endX && startY - x == endY)
                    {
                        isMoveSet = true;
                        break;
                    }
                }
            }
            Test2(available, isMoveSet);
            if (isMoveSet == false)
            {
                for (int x = 1; x < 8; ++x)
                {
                    if (startX + x < 8 && startY + x < 8)//down right
                    {
                        available.Add(new int[] { startX + x, startY + x });
                        if (startX + x == endX && startY + x == endY)
                        {
                            isMoveSet = true;
                            break;
                        }
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                for (int x = 1; x < 8; ++x)
                {
                    if (startX - x >= 0 && startY - x >= 0)//up left
                    {
                        available.Add(new int[] { startX - x, startY - x });
                        if (startX - x == endX && startY - x == endY)
                        {
                            isMoveSet = true;
                            break;
                        }
                    }
                }
                Test2(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                for (int x = 1; x < 8; ++x)
                {
                    if (startX - x >= 0 && startY + x < 8)//up right
                    {
                        available.Add(new int[] { startX - x, startY + x });
                        if (startX - x == endX && startY + x == endY)
                        {
                            isMoveSet = true;
                            break;
                        }
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
