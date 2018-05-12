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
        internal PromptManager GetInstance()
        {
            if(instance == null)
                instance = new PromptManager();
            return instance;
        }
        internal string Prompt()
        {
            return Console.ReadLine();
        }
        internal int promptForOption(string prompt, string[] options)
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
                }
                isValid = int.TryParse(Prompt(), out result);
                if (isValid)
                    if(result < 0 || result > options.Count())
                        isValid = false;
            } while (!isValid);
            return result;
        }
        internal BoardValuePair Prompt(string prompt1, string prompt2)
        {
            int x = 0;
            int y = 0;
            bool isValid = false;
            do
            {
                Console.WriteLine(prompt1);// "What row is the piece you would like on?" || "What row is the space you would like to move on?"
                isValid = int.TryParse(Prompt(), out x);
                if (isValid && (x >= 0 && x < 8))
                {
                    Console.WriteLine(prompt2);// "What column is the piece you would like on?" || "What space is the place you would like to move on?"
                    isValid = int.TryParse(Prompt(), out y);
                    if (y < 0 && y > 7)
                        isValid = false;
                }
            } while (!isValid);
            BoardValuePair pieceLocation = new BoardValuePair();
            pieceLocation.AddPair(x, y);
            return pieceLocation;
        }
    }
}
