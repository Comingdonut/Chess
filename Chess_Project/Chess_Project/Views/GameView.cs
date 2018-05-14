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
        internal string PawnPromotion { get; private set; }
        internal string[] PromotionOptions { get; private set; }
        public GameView()
        {
            PromptName = "What is your name?";
            PromptPiece = new string[] { "What row is the piece you would like on?" ,"What column is the piece you would like on?" };
            PromptSpace = new string[] { "What row is the space you would like to move on?", "What space is the place you would like to move on?" };
            PawnPromotion = "Select a piece to promote too.";
            PromotionOptions = new string[] { "Knight", "Bishop", "Rook", "Queen" };
        }
    }
}
