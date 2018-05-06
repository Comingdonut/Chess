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
        private GameManager()
        {

        }
        internal GameManager GetInstance()
        {
            if(gManager == null)
            {
                gManager = new GameManager();
            }
            return gManager;
        }
    }
}
