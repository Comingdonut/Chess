using Chess.ChessControl;
using Chess.ChessModels;
using Chess.ChessView;
using System.IO;

namespace Chess
{
    class Program
    {
        #region Variables
        private ReadCommands read;
        #endregion
        public Program()
        {
            read = new ReadCommands();
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
