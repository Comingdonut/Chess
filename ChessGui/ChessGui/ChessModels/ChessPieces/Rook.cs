﻿using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Rook : ChessPiece
    {
        public int moveAmount;

        public Rook(ChessColor color)
        {
            Color = color;
            Init();
        }

        private void Init()
        {
            Symbol = 'R';
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
                    moveAmount++;
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
                if (startX + x < 8)//down
                {
                    isAvailable = IsAvailable(board, startX + x, startY, 0);
                    if (isAvailable == true)
                    {
                        if (canMove[0] == true)
                        {
                            available.Add(new int[] { startX + x, startY });
                        }
                    }
                }
            }
            collision = new int[8];
            for (int x = 1; x < 8; ++x)
            {
                if (startY + x < 8)//right
                {
                    isAvailable = IsAvailable(board, startX, startY + x, 1);
                    if (isAvailable == true)
                    {
                        if (canMove[1] == true)
                        {
                            available.Add(new int[] { startX, startY + x });
                        }
                    }
                }
            }
            collision = new int[8];
            for (int x = 1; x < 8; ++x)
            {
                if (startX - x >= 0)//up
                {
                    isAvailable = IsAvailable(board, startX - x, startY, 2);
                    if (isAvailable == true)
                    {
                        if (canMove[2] == true)
                        {
                            available.Add(new int[] { startX - x, startY });
                        }
                    }
                }
            }
            collision = new int[8];
            for (int x = 1; x < 8; ++x)
            {
                if (startY - x >= 0)//left
                {
                    isAvailable = IsAvailable(board, startX, startY - x, 3);
                    if (isAvailable == true)
                    {
                        if (canMove[3] == true)
                        {
                            available.Add(new int[] { startX, startY - x });
                        }
                    }
                }
            }
            return available;
        }
        public override bool IsAvailable(ChessSquare[,] board, int row, int column, int index)
        {
            bool canMove = true;
            if (board[row, column].Piece.Color == Color)
            {
                canMove = false;
            }
            else if (board[row, column].Piece.Color != ChessColor.NONE)
            {
                if (collision[index] >= 1)
                {
                    canMove = false;
                }
                collision[index]++;
            }
            else
            {
                for (int x = 0; x < 8; ++x)
                {
                    if (collision[x] > 0)
                    {
                        canMove = false;
                        break;
                    }
                }
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
            collision = new int[8];
        }
        public override List<int[]> Search(ChessSquare[,] board, int startX, int startY, int endX, int endY)
        {
            bool isMoveSet = false;
            List<int[]> available = new List<int[]>();
            for (int x = 1; x < 8; ++x)
            {
                if (startX + x < 8)//down
                {
                    available.Add(new int[] { startX + x, startY });
                    if (startX + x == endX && startY == endY)
                    {
                        isMoveSet = true;
                        break;
                    }
                }
            }
            ResetSearch(available, isMoveSet);
            if (isMoveSet == false)
            {
                for (int x = 1; x < 8; ++x)
                {
                    if (startY + x < 8)//right
                    {
                        available.Add(new int[] { startX, startY + x });
                        if (startX == endX && startY + x == endY)
                        {
                            isMoveSet = true;
                            break;
                        }
                    }
                }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                for (int x = 1; x < 8; ++x)
                {
                    if (startX - x >= 0)//up
                    {
                        available.Add(new int[] { startX - x, startY });
                        if (startX - x == endX && startY == endY)
                        {
                            isMoveSet = true;
                            break;
                        }
                    }
                }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                for (int x = 1; x < 8; ++x)
                {
                    if (startY - x >= 0)//left
                    {
                        available.Add(new int[] { startX, startY - x });
                        if (startX == endX && startY - x == endY)
                        {
                            isMoveSet = true;
                            break;
                        }
                    }
                }
                ResetSearch(available, isMoveSet);
            }
            return available;
        }

        public override void ResetSearch(List<int[]> available, bool isMoveSet)
        {
            if (isMoveSet == false)
            {
                available.Clear();
            }
        }

        public override string ToString()
        {
            string piece = Color.ToString() + "_ROOK.png";
            return piece;
        }
        /****************************/
    }
}
