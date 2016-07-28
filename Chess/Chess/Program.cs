using Chess.ChessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        private string _piece;
        private string _color;
        private string _square;
        private Utility util;
        private void StorePiece(char piece, char color, string square)
        {
            _piece = util.CheckPiece(piece);
            _color = util.CheckColor(color);
            _square = util.PlacePiece(square);
        }
        public void ReadFile()
        {
            int x = 0;
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines("C:/text.txt");
            // Get the file's content by using a foreach loop.
            foreach (string line in lines)
            {
                char[] commands = new char[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    commands[j] = line.ElementAt(j);
                }
                //Writes out commands in readable english
                if (commands.Length >= 4) {
                    if (commands[0] <= 90 && commands[0] >= 65)
                    {
                        StorePiece(commands[0], commands[1], (commands[2] + "" + commands[3]));
                        util.PrintCommand(_color, _piece, _square);
                    }
                }
                x++;
            }
        }
        public void Run()
        {
            util = new Utility();
            ReadFile();
        }
        static void Main(string[] args)
        {
            Program chess = new Program();
            chess.Run();
        }
    }
}
