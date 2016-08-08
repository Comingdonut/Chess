using Chess.ChessControl;
using System;

namespace Chess
{
    class Program
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
        /// Reads all lines from a file.
        /// </summary>
        public void Run()
        {
            string[] lines = System.IO.File.ReadAllLines(@"ChessCommands.txt");
            
            foreach (string line in lines)
            {
                read.ReadLine(line);
            }
        }
        static void Main(string[] args)
        {
            Program chess = new Program();
            chess.Run();
        }
        //-----------------------------------------------------------------------------------
    }
}
