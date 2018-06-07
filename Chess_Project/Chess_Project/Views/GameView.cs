using Chess_Project.Models.Board;
using Chess_Project.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Views
{
    internal class GameView
    {
        internal string PromptName { get; set; }
        internal string[] PromptPiece { get; private set; }
        internal string[] PromptSpace { get; private set; }
        internal string PieceChoiceError { get; set; }
        internal string PieceMovementError { get; set; }
        internal string PawnPromotion { get; private set; }
        internal string[] PromotionOptions { get; private set; }
        public GameView()
        {
            PromptName = "What is your name?";
            PromptPiece = new string[] { "What row is the piece you would like on?" ,"What column is the piece you would like on?" };
            PromptSpace = new string[] { "What row is the space you would like to move on?", "What space is the place you would like to move on?" };
            PieceChoiceError = "Choose your own Piece!";
            PieceMovementError = "That space is not available...";
            PawnPromotion = "Select a piece to promote too.";
            PromotionOptions = new string[] { "Knight", "Bishop", "Rook", "Queen" };
        }

        internal void printBoard(BoardSpace[,] board)
        {
            Console.WriteLine("     0   1   2   3   4   5   6   7  ");
            Console.WriteLine("   +-------------------------------+");
            for (int x = 0; x < board.GetLength(0); x++)
            {
                Console.Write(" " + x + " |");
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    Console.Write(board[x, y] + "|");
                }
                Console.WriteLine();
                if (x != 7)
                    Console.WriteLine("   |---+---+---+---+---+---+---+---|");
            }
            Console.WriteLine("   +-------------------------------+");
        }

        internal void setBackgroundColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }
        internal string PromptPlayerTurn(Player p)
        {
            return String.Format("It's {0} Turn!", p.Name);
        }
    }
}
