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
                ReadMove(line);
            }
            while (_con.HasWon == false)
            {
                _con.PrintMovablePieces(_con.Turn);
                string input = Console.ReadLine();
                ReadPiece(input);
                Console.WriteLine("------");
                input = Console.ReadLine();
                ReadMove(input);
            }
        }
        #endregion

        #region Read Commands
        /// <summary>
        /// Reads in a string that represents a piece.
        /// </summary>
        /// <param name="piece">A string that represents a piece.</param>
        public void ReadPiece(string piece)
        {
            if (piece.Length == _con.Piece_Length)
            {
                _con.ProcessPiece(piece, @"([a-h])([1-8])");
            }
        }
        /// <summary>
        /// Reads in commands from the user.
        /// </summary>
        /// <param name="move">A command from the user.</param>
        public void ReadMove(string move)
        {
            if (move.Length == _con.Place_Piece)
            {
                _con.ProcessMove(move, @"([KQBNRP])([ld])([a-h])([1-8])");
            }
            else if (move.Length == _con.Piece_Length)
            {
                _con.ProcessMove(move, @"([a-h])([1-8])");
                _con.Print();
            }
            else if (move.Length == _con.Capture_Piece)
            {
                _con.ProcessMove(move, @"([a-h])([1-8])([ ])([a-h])([1-8])([*])");
                _con.Print();
            }
            else if (move.Length == _con.King_Side_Piece)
            {
                _con.ProcessMove(move, @"([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])([ ])([a-h])([1-8])");
                _con.Print();
            }
            else
            {
                Console.WriteLine("Invalid command...");
            }
        }
        #endregion
        //-----------------------------------------------------------------------------------
    }
}
