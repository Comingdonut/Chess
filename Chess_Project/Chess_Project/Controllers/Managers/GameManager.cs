using Chess_Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Controllers.Managers
{
    internal class GameManager
    {
        // TODO: Initialize board
        // TODO: Players move opponent's piece
        //      But they could click on a opponent's piece to show their movement.
        private static GameManager instance;

        private PlayerManager pManager;
        private PromptManager pmtManager;
        private MenuView mView;
        private GameView gView;
        private RulesView rView;

        private GameManager()
        {
            pmtManager = PromptManager.GetInstance();
            mView = new MenuView();
            gView = new GameView();
            rView = new RulesView();
        }
        internal static GameManager GetInstance()
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
        internal void StartGame()
        {
            bool endGame = false;
            pManager = new PlayerManager();
            do
            {
                int result = pmtManager.PromptForOption(mView.AsciiMenu, mView.MenuOptions);
                endGame = SelectOption(result);
            } while (!endGame);
            // TODO: Start
            // - Ask for 1st player name
            // - Ask for 2nd player name
            // - Initialize Board
            //   * Start Loop Game
            //     - PrintBoard
            //     - Reset pawn moved twice method
            //     - Get Player's King position
            //     - CheckForCheck()
            //     - Prompt player for movement
            //       * If space not available then prompt again
            //     - CheckAvailability()
            //       * If not available then prompt again
            //     - Move piece
            //       * Check for pawn promotion
            //         - Prompt for promotion
            //           * If null then prompt again
            //     - Switch Player turn
            // - At end of game
        }
        internal bool SelectOption(int result)
        {
            bool quit = false;
            pmtManager.ClearConsole();
            switch (result)
            {
                case 1:
                    PlayGame();
                    break;
                case 2:
                    LookAtRules();
                    break;
                case 3:
                    quit = true;
                    break;
                default:
                    break;
            }
            return quit;
        }
        internal void PlayGame()
        {
            bool gameEnd = false;
            do
            {
                pManager.Player1.Name = pmtManager.PromptForName(gView.PromptName, 1);
                pManager.Player2.Name = pmtManager.PromptForName(gView.PromptName, 2);
            } while (!gameEnd);
        }
        #region Da Rules
        internal void LookAtRules()
        {
            bool back = false;
            do
            {
                pmtManager.Break();
                int result = pmtManager.PromptForOption(rView.Title, rView.RuleOptions);
                back = SelectRules(result);
            } while (!back);
        }
        internal bool SelectRules(int result)
        {
            bool back = false;
            pmtManager.ClearConsole();
            switch (result)
            {
                case 1:
                    BasicRules();
                    break;
                case 2:
                    PieceMovement();
                    break;
                case 3:
                    SpecialConditions();
                    break;
                case 4:
                    back = true;
                    break;
            }
            return back;
        }
        internal void BasicRules()
        {
            pmtManager.Break();
            Console.WriteLine(rView.RuleOptions[0]);
            pmtManager.Break();
            Console.WriteLine(rView.Objective);
            Console.WriteLine(rView.MovesFirst);
            Console.WriteLine(rView.TurnTaking);
            Console.WriteLine(rView.Movement);
            Console.WriteLine(rView.StaleMate);
            Console.WriteLine(rView.Check);
            Console.WriteLine(rView.CheckMate);
            pmtManager.Prompt();
        }
        internal void PieceMovement()
        {
            pmtManager.Break();
            Console.WriteLine(rView.RuleOptions[1]);
            pmtManager.Break();
            Console.WriteLine(rView.Pawn);
            Console.WriteLine(rView.Knight);
            Console.WriteLine(rView.Bishop);
            Console.WriteLine(rView.Rook);
            Console.WriteLine(rView.Queen);
            Console.WriteLine(rView.King);
            pmtManager.Prompt();
        }
        internal void SpecialConditions()
        {
            pmtManager.Break();
            Console.WriteLine(rView.RuleOptions[2]);
            pmtManager.Break();
            Console.WriteLine(rView.EnPassant);
            Console.WriteLine(rView.Castling);
            Console.WriteLine(rView.PawnPromotion);
            pmtManager.Prompt();
        }
        #endregion
    }
}
