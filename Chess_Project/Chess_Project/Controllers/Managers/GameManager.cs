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

        private GameManager()
        {
            pManager = PlayerManager.GetInstance();
            pmtManager = PromptManager.GetInstance();
            menu = new Menu();
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
            // TODO: Rules
            // - Basic Rules
            //   * Objective
            //   * Moves first
            //   * Check
            //   * Checkmate
            // - PieceMovement
            //   * Pawn / Knight / Bishop / Rook / Queen / King
            // - Special Conditions
            //   * En Passant / Castling / Pawn Promotion
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
            }
            return quit;
        }
        internal void PlayGame()
        {
        
        }
        internal void LookAtRules()
        {

        }
    }
}
