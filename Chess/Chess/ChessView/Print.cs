using Chess.ChessModels;
using System;

namespace Chess.ChessView
{
    public class Print
    {
        /// <summary>
        /// Prints the parameter out to the console.
        /// </summary>
        /// <param name="command">Where the piece at a specific location has moved to.</param>
        public void PrintCommand(string command)
        {
            Console.WriteLine(command);
        }
        /// <summary>
        /// Prints out 24 array values before moving on to the next set of 24 values.
        /// </summary>
        /// <param name="boardSqaures">Represents a board. Required size for the 2-D array is 8 rows and 24 columns.</param>
        public void PrintBoard(ChessPiece[,] boardSqaures)
        {
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (x % 2 == 0)
                    {
                        if (y % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                    }
                    else
                    {
                        if (y % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                    }
                    if (boardSqaures[x, y].Color == 'l')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if(boardSqaures[x, y].Color == 'd')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write(" " + boardSqaures[x, y].Symbol + " ");
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
    //-----------------------------------------------------------------------------------
}
