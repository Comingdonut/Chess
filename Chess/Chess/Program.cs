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
        /// <summary>
        /// Reads all lines from the file.
        /// </summary>
        public void Run()
        {
            read = new ReadCommands();
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
