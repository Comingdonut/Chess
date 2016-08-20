using Chess.ChessModels;
using System;

namespace Chess.ChessControl
{
    public class Commands
    {
        #region Variables
        private Controller _con;
        #endregion
        public Commands()
        {
            _con = new Controller();
        }
        #region Game
        /// <summary>
        /// Starts the game by placing all the pieces.
        /// It then prints out all movable pieces for the current player.
        /// When the player chooses a piece, it then prints out all locations the piece can move to.
        /// The player then chooses where the piece should go.
        /// As long as it's a legal move, the piece will move to the desired location.
        /// </summary>
        public void Game()
        {
            string[] lines = System.IO.File.ReadAllLines(@"ChessCommands.txt");

            foreach (string line in lines)
            {
                //ReadMove(line);
            }
            while (_con.HasWon == false)
            {
                _con.PrintMovablePieces(_con.Turn);
                string input = Console.ReadLine();
                //ReadPiece(input);
                Console.WriteLine("------");
                input = Console.ReadLine();
                //ReadMove(input);
            }
        }
        #endregion

        
        //-----------------------------------------------------------------------------------
    }
}