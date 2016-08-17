using Chess.ChessControl;
using System;

namespace Chess
{
    public class Program
    {
        #region Variables
        private Commands read;
        #endregion
        public Program()
        {
            Console.Title = "Chess";
            read = new Commands();
        }
        /// <summary>
        /// Reads the game.
        /// </summary>
        public void Run()
        {
            read.Game();
        }
        static void Main(string[] args)
        {
            Program chess = new Program();
            chess.Run();
        }
        //-----------------------------------------------------------------------------------
    }
}