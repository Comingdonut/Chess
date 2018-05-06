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
            int[,] test = new int[8, 8];
            for(int i = 0; i < test.GetLength(0); i++)
            {
                Console.Write("R" + (i + 1) + " ");
                for (int j = 0; j < test.GetLength(1); j++)
                {
                    Console.Write("C" + (j + 1) + " ");

                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
