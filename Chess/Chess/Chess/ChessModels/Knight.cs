using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Knight : ChessPiece
    {
        public char Color { get; set; }
        public string Piece { get; set; }
        public char Symbol { get; set; }
        public bool[] canMove { get; set; }
        public Knight(char color)
        {
            Color = color;
            Piece = "Knight";
            Symbol = 'N';
            ResetMovement();
        }
        public void MovePiece(ChessPiece[,] board, int[] start, int[] end)
        {
            board[end[0], end[1]] = board[start[0], start[1]];
            board[start[0], start[1]] = new Space();
        }
        public bool CheckSquare(ChessPiece[,] board, int[] end)
        {
            bool isValid = false;
            if (board[end[0], end[1]].GetType() == typeof(Space))
            {
                isValid = true;
            }
            else if (board[end[0], end[1]].Color != Color)
            {
                isValid = true;
            }
            return isValid;
        }
        public bool CheckMovement(ChessPiece[,] board, int[] start, int[] end)
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
        public List<int[]> RestrictMovement(ChessPiece[,] board, int[] start)
        {
            List<int[]> available = new List<int[]>();
            
            if (start[0] - 2 >= 0 && start[1] - 1 >= 0)//up 2 left 1
            {
                available.Add(new int[] { start[0] - 2, start[1] - 1 });
            }
            if (start[0] - 2 >= 0 && start[1] + 1 < 8)//up 2 right 1
            {
                available.Add(new int[] { start[0] - 2, start[1] + 1 });
            }
            if (start[0] - 1 >= 0 && start[1] - 2 >= 0)//up 1 left 2
            {
                available.Add(new int[] { start[0] - 1, start[1] - 2 });
            }
            if (start[0] - 1 >= 0 && start[1] + 2 < 8)//up 1 right 2
            {
                available.Add(new int[] { start[0] - 1, start[1] + 2 });
            }
            if (start[0] + 2 < 8 && start[1] - 1 >= 0 )//down 2 Left 1
            {
                available.Add(new int[] { start[0] + 2, start[1] - 1 });
            }
            if (start[0] + 2 < 8 && start[1] + 1 < 8)//down 2 right 1
            {
                available.Add(new int[] { start[0] + 2, start[1] + 1 });
            }
            if (start[0] + 1 < 8 && start[1] - 2 >= 0)//down 1 Left 2
            {
                available.Add(new int[] { start[0] + 1, start[1] - 2 });
            }
            if (start[0] + 1 < 8 && start[1] + 2 < 8)//down 1 right 2
            {
                available.Add(new int[] { start[0] + 1, start[1] + 2 });
            }
            return available;
        }
        public bool IsAvailable(ChessPiece[,] board, int row, int column, int index)
        {
            bool canMove = true;
            if (board[row, column].Color == Color)
            {
                canMove = false;
            }
            if (this.canMove[index] == true)
            {
                this.canMove[index] = canMove;
            }
            return canMove;
        }
        public void ResetMovement() { }
        /****************************/
    }
}
