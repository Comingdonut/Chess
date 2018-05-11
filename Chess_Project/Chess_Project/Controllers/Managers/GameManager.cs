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
        private static GameManager gManager;

        private PlayerManager pManager;
        private GameManager()
        {
            pManager = PlayerManager.GetInstance();
        }
        internal GameManager GetInstance()
        {
            if(gManager == null)
            {
                gManager = new GameManager();
            }
            return gManager;
        }
        internal void StartGame()
        {
            // TODO: Main Menu Title
            // Main Menu options:
            //  - Start Game
            //  - Rules
            //  - Quit
            // TODO: Start
            // - Ask for 1st player name
            // - Ask for 2nd player name
            // - Initialize Board
            //   * Start Loop Game
            //     - PrintBoard
            //     - Get Player's King position
            //     - CheckForCheck()
            //     - Reset pawn moved twice method
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
    }
}
