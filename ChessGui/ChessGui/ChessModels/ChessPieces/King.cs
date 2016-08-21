using System;
using System.Collections.Generic;
using System.Threading;

namespace Chess.ChessModels
{
    public class King : ChessPiece
    {
        public int moveAmount;

        public King(ChessColor color)
        {
            Color = color;
            Init();
        }

        private void Init()
        {
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
            bool isCastleing = false;
            bool isLeft = false;
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
                isCastleing = IsPossibleToCastleing(board, startX, isLeft);
                if (isCastleing == true)
                {
                    available.Add(new int[] { startX, startY + 2 });
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
                isLeft = true;
                isCastleing = IsPossibleToCastleing(board, startX, isLeft);
                if (isCastleing == true)
                {
                    available.Add(new int[] { startX, startY - 2 });
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
        public bool IsPossibleToCastleing(ChessSquare[,] board, int x, bool isLeft)
        {
            bool isPossible = false;
            bool leftCastle = false;
            bool rightCastle = false;
            int caslteLeft = 0;
            int caslteRight = 0;
            for (int y = 0; y < 4; ++y)
            {
                if(4 - y >= 0)
                {
                    if (board[x, 4 - y].Piece.Color == ChessColor.NONE)
                    {
                        caslteLeft++;
                    }
                }
                if (4 + y < 8)
                {
                    if (y != 3)
                    {
                        if (board[x, 4 + y].Piece.Color == ChessColor.NONE)
                        {
                            caslteRight++;
                        }
                    }
                }
            }
            if(isLeft == true)
            {
                if (caslteLeft == 3)
                {
                    leftCastle = CastleingLeftPosition(board, x);
                    if (leftCastle == true)
                    {
                        isPossible = true;
                    }
                }
            }
            else
            {
                if (caslteRight == 2)
                {
                    rightCastle = CastleingRightPosition(board, x);
                    if (rightCastle == true)
                    {
                        isPossible = true;
                    }
                }
            }
            return isPossible;
        }
        public bool CastleingLeftPosition(ChessSquare[,] board, int x)
        {
            bool inPosition = false;
            int position = 0;
            if (board[x, 4].Piece.GetType() == typeof(King))
            {
                if (((King)board[x, 4].Piece).moveAmount == 0)
                {
                    position++;
                }
            }
            if (board[x, 0].Piece.GetType() == typeof(Rook))
            {
                if (((Rook)board[x, 0].Piece).moveAmount == 0)
                {
                    position++;
                }
            }
            if (position == 2)
            {
                inPosition = true;
            }
            return inPosition;
        }
        public bool CastleingRightPosition(ChessSquare[,] board, int x)
        {
            bool inPosition = false;
            int position = 0;
            if (board[x, 4].Piece.GetType() == typeof(King))
            {
                if (((King)board[x, 4].Piece).moveAmount == 0)
                {
                    position++;
                }
            }
            if (board[x, 7].Piece.GetType() == typeof(Rook))
            {
                if (((Rook)board[x, 7].Piece).moveAmount == 0)
                {
                    position++;
                }
            }
            if (position == 2)
            {
                inPosition = true;
            }
            return inPosition;
        }
        //public List<int[]> AvoidCheck(ChessSquare[,] board, int startX, int startY)
        //{
        //    List<int[]> kingsMoves = new List<int[]>();
        //    List<int[]> enemyMoves = new List<int[]>();
        //    bool isDangerous = false;
        //    kingsMoves = board[startX, startY].Piece.RestrictMovement(board, startX, startY);//Stores the kings movements.
        //    for (int x = 0; x < 8; ++x)
        //    {
        //        for (int y = 0; y < 8; y++)
        //        {
        //            if (board[x, y].Piece.Color != Color && board[x, y].Piece.Color != ChessColor.NONE)
        //            {
        //                //Gets all possible legal moves for current enemy piece.
        //                int[] placeHold = new int[] {x, y };
        //                enemyMoves.Add(placeHold);//Adds all enemy pieces.
        //            }
        //        }
        //    }//End of the for loop
        //    for (int j = 0; j < enemyMoves.Count; ++j)
        //    {
        //        for (int k = 0; k < kingsMoves.Count; ++k)
        //        {
        //            //Checks if any of the kings movements will make the king go back in check
        //            if(board[enemyMoves[j][0], enemyMoves[j][1]].Piece.GetType() != typeof(King))
        //            {
        //                isDangerous = board[enemyMoves[j][0], enemyMoves[j][1]].Piece.CheckMovement(board, enemyMoves[j][0], enemyMoves[j][1], kingsMoves[k][0], kingsMoves[k][1]);
        //            }
        //            else
        //            {
        //                List<int[]> hold = board[enemyMoves[j][0], enemyMoves[j][1]].Piece.RestrictMovement(board, enemyMoves[j][0], enemyMoves[j][1]);
        //                for(int l = 0; l < hold.Count; ++l)
        //                {
        //                    for (int m = 0; m < kingsMoves.Count; ++m)
        //                    {
        //                        if (kingsMoves[m][0] == hold[l][0] && kingsMoves[m][1] == hold[l][1])
        //                        {
        //                            isDangerous = true;
        //                        }
        //                    }
        //                }
        //            }
        //            if (isDangerous == true)
        //            {
        //                kingsMoves.RemoveAt(k);
        //            }
        //        }
        //    }//End of the for loop
        //    return kingsMoves;
        //}
        public override void ResetMovement()
        {
            canMove = new bool[] { true, true, true, true, true, true, true, true };
        }
        public override List<int[]> Search(ChessSquare[,] board, int startX, int startY, int endX, int endY)
        {
            bool isMoveSet = false;
            List<int[]> available = new List<int[]>();
                if (startX + 1 < 8)//down
                {
                    available.Add(new int[] { startX + 1, startY });
                    if (startX + 1 == endX && startY == endY)
                    {
                        isMoveSet = true;
                    }
                }
            ResetSearch(available, isMoveSet);
            if (isMoveSet == false)
            {
                    if (startX + 1 < 8 && startY - 1 >= 0)//down Left
                    {
                        available.Add(new int[] { startX + 1, startY - 1 });
                        if (startX + 1 == endX && startY - 1 == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startX + 1 < 8 && startY - 1 >= 0)//down Left
                    {
                        available.Add(new int[] { startX + 1, startY - 1 });
                        if (startX + 1 == endX && startY - 1 == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startX + 1 < 8 && startY + 1 < 8)//down right
                    {
                        available.Add(new int[] { startX + 1, startY + 1 });
                        if (startX + 1 == endX && startY + 1 == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startY + 1 < 8)//right
                    {
                        available.Add(new int[] { startX, startY + 1 });
                        if (startX == endX && startY + 1 == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startX - 1 >= 0)//up
                    {
                        available.Add(new int[] { startX - 1, startY });
                        if (startX - 1 == endX && startY == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startX - 1 >= 0 && startY - 1 >= 0)//up left
                    {
                        available.Add(new int[] { startX - 1, startY - 1 });
                        if (startX - 1 == endX && startY - 1 == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startX - 1 >= 0 && startY + 1 < 8)//up right
                    {
                        available.Add(new int[] { startX - 1, startY + 1 });
                        if (startX - 1 == endX && startY + 1 == endY)
                        {
                            isMoveSet = true;
                        }
                    }
                ResetSearch(available, isMoveSet);
            }
            if (isMoveSet == false)
            {
                    if (startY - 1 >= 0)//left
                    {
                        available.Add(new int[] { startX, startY - 1 });
                        if (startX == endX && startY - 1 == endY)
                        {
                            isMoveSet = true;
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
            string piece = Color.ToString() + "_KING.png";
            return piece;
        }
        /****************************/
    }
}
