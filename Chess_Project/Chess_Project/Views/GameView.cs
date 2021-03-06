﻿using Chess_Project.Models.Board;
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
        internal string AlmostCheck { get; set; }
        internal string StillInCheck { get; set; }
        internal string InCheck { get; set; }
        public GameView()
        {
            PromptName = "What is your name?";
            PromptPiece = new string[] { "What row is the Piece you would like on?" ,"What column is the Piece you would like on?" };
            PromptSpace = new string[] { "What row is the space you would like to move on?", "What space is the place you would like to move on?" };
            PieceChoiceError = "Choose your own Piece!";
            PieceMovementError = "That space is not available...";
            PawnPromotion = "Select a Piece to promote too:";
            PromotionOptions = new string[] { "Knight", "Bishop", "Rook", "Queen" };
            AlmostCheck = "***Can't move piece, King will be in Check.***";
            StillInCheck = "****Take your King out of Check!****";
            InCheck = "*****King is in Check!!!*****";
        }
        internal void PrintBoard(BoardSpace[,] board)
        {
            Console.WriteLine("     0   1   2   3   4   5   6   7  \n"
                + "   +-------------------------------+");
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
        internal string PrintPlayerTurn(Player p)
        {
            return String.Format("It's {0} Turn!", p.Name);
        }
        internal string PrintWinner(string name)
        {
            return String.Format("*****CheckMate! A winner is you, {0}!!!*****", name);
        }
    }
}
