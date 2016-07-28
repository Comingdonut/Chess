using Chess.ChessControl;
using Chess.ChessModels;
using Chess.ChessView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        #region Variables
        private Utility util;
        private ReadCommands read;
        private Print print;
        #endregion
        public void Run()
        {
            util = new Utility();
            read = new ReadCommands(util);
            print = new Print();
            read.ReadFile(print);
        }
        static void Main(string[] args)
        {
            Program chess = new Program();
            chess.Run();
        }
        //-----------------------------------------------------------------------------------
    }
}
