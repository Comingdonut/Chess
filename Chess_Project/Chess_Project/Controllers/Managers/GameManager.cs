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
        private Menu menu;
        private Rules r;

        private GameManager()
        {
            pManager = PlayerManager.GetInstance();
            pmtManager = PromptManager.GetInstance();
            menu = new Menu();
            r = new Rules();
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
            do
            {
                int result = pmtManager.PromptForOption(menu.AsciiMenu, menu.MenuOptions);
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
        
        }
        #region Da Rules
        internal void LookAtRules()
        {
            bool back = false;
            do
            {
                pmtManager.Break();
                int result = pmtManager.PromptForOption(r.Title, r.RuleOptions);
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
            Console.WriteLine(r.RuleOptions[0]);
            pmtManager.Break();
            Console.WriteLine(r.Objective);
            Console.WriteLine(r.MovesFirst);
            Console.WriteLine(r.TurnTaking);
            Console.WriteLine(r.Movement);
            Console.WriteLine(r.StaleMate);
            Console.WriteLine(r.Check);
            Console.WriteLine(r.CheckMate);
            pmtManager.Prompt();
        }
        internal void PieceMovement()
        {
            pmtManager.Break();
            Console.WriteLine(r.RuleOptions[1]);
            pmtManager.Break();
            Console.WriteLine(r.Pawn);
            Console.WriteLine(r.Knight);
            Console.WriteLine(r.Bishop);
            Console.WriteLine(r.Rook);
            Console.WriteLine(r.Queen);
            Console.WriteLine(r.King);
            pmtManager.Prompt();
        }
        internal void SpecialConditions()
        {
            pmtManager.Break();
            Console.WriteLine(r.RuleOptions[2]);
            pmtManager.Break();
            Console.WriteLine(r.EnPassant);
            Console.WriteLine(r.Castling);
            Console.WriteLine(r.PawnPromotion);
            pmtManager.Prompt();
        }
        #endregion
    }
}
