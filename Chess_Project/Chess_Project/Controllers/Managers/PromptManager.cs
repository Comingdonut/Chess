using Chess_Project.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Controllers.Managers
{
    internal class PromptManager
    {
        private static PromptManager instance;
        private PromptManager() { }
        internal static PromptManager GetInstance()
        {
            if(instance == null)
                instance = new PromptManager();
            return instance;
        }
        internal string Prompt()
        {
            return Console.ReadLine();
        }
        internal int PromptForOption(string prompt, string[] options)
        {
            bool isValid = false;
            int result = 0;
            do
            {
                int optionNumber = 1;
                Console.WriteLine(prompt);
                foreach (string opt in options)
                {
                    Console.WriteLine(string.Format("{0}) {1}", optionNumber, opt));
                    optionNumber++;
                }
                isValid = int.TryParse(Prompt(), out result);
                if (isValid)
                    if(result < 1 || result > options.Count())
                        isValid = false;
                if (!isValid)
                    PrintError("Invalid Option");
            } while (!isValid);
            return result;
        }
        internal string PromptForName(string prompt, int playerNum)
        {
            Console.WriteLine(prompt);
            string name = Prompt();
            if (string.IsNullOrWhiteSpace(name))
                name = "Player " + playerNum;
            return name;
        }
        internal BoardValuePair PromptForMovement(string prompt1, string prompt2)
        {
            int x = 0;
            int y = 0;
            bool isValid = false;
            do
            {
                Console.WriteLine(prompt1);
                isValid = int.TryParse(Prompt(), out x);
                if (isValid && (x >= 0 && x < 8))
                {
                    Console.WriteLine(prompt2);
                    isValid = int.TryParse(Prompt(), out y);
                    if (y < 0 || y > 7)
                    {
                        isValid = false;
                    }
                    else if (!isValid)
                    {
                        x = -1;
                        isValid = true;
                    }
                }
                else if (!isValid)
                {
                    x = -1;
                    isValid = true;
                }
                else
                    isValid = false;
                if (!isValid)
                    PrintError("Invalid Option");
            } while (!isValid);
            BoardValuePair pieceLocation = new BoardValuePair();
            pieceLocation.AddPair(x, y);
            return pieceLocation;
        }
        internal void ClearConsole()
        {
            Console.Clear();
        }
        internal void Break()
        {
            Console.WriteLine("*-----------------------------------------------*");
        }
        internal void PrintError(string error)
        {
            ClearConsole();
            Break();
            Console.WriteLine(error);
            Break();
        }
    }
}
