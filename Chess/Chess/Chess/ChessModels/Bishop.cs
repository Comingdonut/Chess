using System;
using System.Collections.Generic;

namespace Chess.ChessModels
{
    public class Bishop : ChessPiece
    {
        public char Color { get; set; }

        public string Piece { get; set; }

        public char Symbol { get; set; }

        public Bishop(char color)
        {
            Color = color;
            Piece = "Bishop";
            Symbol = 'B';
        }
        public bool CheckMovement(ChessPiece[,] board, int[] start, int[] end)
        {
            bool isValid = false;
            List<int[]> available = IsAvailable(start);
            for (int x = 0; x < available.Count; ++x)
            {
                if (available[x][0] == end[0] && available[x][1] == end[1])
                {
                    isValid = true;
                }
            }
            return isValid;
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
        public void MovePiece(ChessPiece[,] board, int[] start, int[] end)
        {
            board[end[0], end[1]] = board[start[0], start[1]];
            board[start[0], start[1]] = new Space();
        }
        public List<int[]> IsAvailable(int[] start)
        {
            List<int[]> available = new List<int[]>();
            for (int x = 0; x < 8; ++x)
            {
                if (start[0] + x < 8 && start[1] - x >= 0)//down Left 1
                {
                    available.Add(new int[] { start[0] + x, start[1] - x });
                }
                if (start[0] + x < 8 && start[1] + x < 8)//down right 1
                {
                    available.Add(new int[] { start[0] + x, start[1] + x });
                }
                if (start[0] - x >= 0 && start[1] - x >= 0)//up left 1
                {
                    available.Add(new int[] { start[0] - x, start[1] - x });
                }
                if (start[0] - x >= 0 && start[1] + x < 8)//up right 1
                {
                    available.Add(new int[] { start[0] - x, start[1] + x });
                }
            }
            return available;
        }
    }
}
