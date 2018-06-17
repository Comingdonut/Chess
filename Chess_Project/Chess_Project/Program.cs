using Chess_Project.Controllers.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            GameManager chess = new GameManager();
            chess.StartGame();
        }
    }
}
