using Chess_Project.Models.Board;
using Chess_Project.Models.Helper;
using Chess_Project.Models.Pieces;
using Chess_Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Controllers.Managers
{
    internal class GameManager
    {
        // TODO: Initialize board
        // TODO: Players move opponent's piece
        //      But they could click on a opponent's piece to show their movement.
        private static GameManager instance;
        // Controllers
        private PlayerManager pManager;
        private PromptManager pmtManager;
        private MovementManager mManager;
        // Models
        private Board b;
        // Views
        private MenuView mView;
        private GameView gView;
        private RulesView rView;

        private GameManager()
        {
            pmtManager = PromptManager.GetInstance();
            mManager = MovementManager.GetInstance();
            b = Board.GetInstance();
            mView = new MenuView();
            gView = new GameView();
            rView = new RulesView();
        }
        internal static GameManager GetInstance()
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
        internal void StartGame()
        {
            bool endGame = false;
            pManager = new PlayerManager();
            do
            {
                int result = pmtManager.PromptForOption(mView.AsciiMenu, mView.MenuOptions);
                endGame = SelectOption(result);
            } while (!endGame);
            /// TODO: Start
            /// - Ask for 1st player name
            /// - Ask for 2nd player name
            /// - Initialize Board***
            ///   * Start Loop Game
            ///     - PrintBoard
            //     - Reset pawn moved twice method
            //     - Get Player's King position
            //     - CheckForCheck()
            ///     - Prompt player for movement
            ///       * If space not available then prompt again
            //     - CheckAvailability()
            //       * If not available then prompt again
            //     - Move piece
            //       * Check for pawn promotion
            //         - Prompt for promotion
            //           * If null then prompt again
            ///     - Switch Player turn
            // - At end of game
        }
        internal bool SelectOption(int result)
        {
            bool quit = false;
            pmtManager.ClearConsole();
            switch (result)
            {
                case 1:
                    PlayGame();
                    break;
                case 2:
                    LookAtRules();
                    break;
                case 3:
                    quit = true;
                    break;
                default:
                    break;
            }
            return quit;
        }
        internal void PlayGame()
        {
            bool gameEnd = false;
            pManager.Player1.Name = pmtManager.PromptForName(gView.PromptName, 1);// Prompt player names
            pManager.Player2.Name = pmtManager.PromptForName(gView.PromptName, 2);
            pManager.CurrentPlayer = pManager.Player1;// Sets current player
            InitBoard(b.GameBoard);// initializes board
            do// Games Loop
            {
                bool isValid = false;
                do
                {
                    gView.printBoard(b.GameBoard);// Prints Board
                    Console.WriteLine(gView.PromptPlayerTurn(pManager.CurrentPlayer));// Prompt Player Turn
                    BoardValuePair space = new BoardValuePair();
                    do// Prompt Loop
                    {
                        space = pmtManager.PromptForMovement(gView.PromptPiece[0], gView.PromptPiece[1]);// Prompt for piece to move
                        if(space[0].Key != -1)
                            if (!b.GameBoard[space[0].Key, space[0].Value].IsEmpty)// Check if not empty TODO: Refactor to space.empty
                                if (b.GameBoard[space[0].Key, space[0].Value].Piece.Paint == pManager.CurrentPlayer.Color)// Check if color matches player color TODO: Refactor to space.paint
                                {
                                    isValid = true;
                                    continue;// Skip error print if valid
                                }
                        pmtManager.PrintError(gView.PieceChoiceError);
                        gView.printBoard(b.GameBoard);// Prints Board
                    } while (!isValid);
                    mManager.SetCoordinates(space.First().Key, space.First().Value);// Stores space coordinates to movement manager
                    isValid = false;// Resets isvalid
                    BoardValuePair newSpace = pmtManager.PromptForMovement(gView.PromptSpace[0], gView.PromptSpace[1]);// Prompt for space to move piece too
                    List<BoardValuePair> movement = mManager.DeterminePieceMovement(b.GameBoard[space[0].Key, space[0].Value].Piece);
                    mManager.CheckForPiece(b.GameBoard, movement, pManager.CurrentPlayer.Color);// Checks for ally and enemy piece and boundaries. Restricts movement if space has ally or enemy or is beyond boundaries.
                    if (mManager.CheckAvailablity(movement, newSpace[0].Key, newSpace[0].Value))// Checks if new Space is an movement option
                    {
                        isValid = true;
                        continue;// Skip error print if valid
                    }
                    pmtManager.PrintError(gView.PieceMovementError);
                } while (!isValid);// If new Space is not Valid, do loop again

                pmtManager.Prompt(); // Replace this with moving pieces

                pManager.SwitchPlayer();// Switches player turns
            } while (!gameEnd);
        }
        internal void InitBoard(BoardSpace[,] board)
        {
            // TODO: Optional(Replace initalization with a text file?)
            board[0, 0] = new BoardSpace(new Rook(Color.black));
            board[0, 1] = new BoardSpace(new Knight(Color.black));
            board[0, 2] = new BoardSpace(new Bishop(Color.black));
            board[0, 3] = new BoardSpace(new King(Color.black));
            board[0, 4] = new BoardSpace(new Queen(Color.black));
            board[0, 5] = new BoardSpace(new Bishop(Color.black));
            board[0, 6] = new BoardSpace(new Knight(Color.black));
            board[0, 7] = new BoardSpace(new Rook(Color.black));
            
            board[1, 0] = new BoardSpace(new Pawn(Color.black));
            board[1, 1] = new BoardSpace(new Pawn(Color.black));
            board[1, 2] = new BoardSpace(new Pawn(Color.black));
            board[1, 3] = new BoardSpace(new Pawn(Color.black));
            board[1, 4] = new BoardSpace(new Pawn(Color.black));
            board[1, 5] = new BoardSpace(new Pawn(Color.black));
            board[1, 6] = new BoardSpace(new Pawn(Color.black));
            board[1, 7] = new BoardSpace(new Pawn(Color.black));
            
            for(int j = 2; j < 6; j++)
            {
                for(int k = 0; k < 8; k++)
                {
                    board[j, k] = new BoardSpace(true);
                }
            }

            board[7, 0] = new BoardSpace(new Rook(Color.white));
            board[7, 1] = new BoardSpace(new Knight(Color.white));
            board[7, 2] = new BoardSpace(new Bishop(Color.white));
            board[7, 3] = new BoardSpace(new King(Color.white));
            board[7, 4] = new BoardSpace(new Queen(Color.white));
            board[7, 5] = new BoardSpace(new Bishop(Color.white));
            board[7, 6] = new BoardSpace(new Knight(Color.white));
            board[7, 7] = new BoardSpace(new Rook(Color.white));
            
            board[6, 0] = new BoardSpace(new Pawn(Color.white));
            board[6, 1] = new BoardSpace(new Pawn(Color.white));
            board[6, 2] = new BoardSpace(new Pawn(Color.white));
            board[6, 3] = new BoardSpace(new Pawn(Color.white));
            board[6, 4] = new BoardSpace(new Pawn(Color.white));
            board[6, 5] = new BoardSpace(new Pawn(Color.white));
            board[6, 6] = new BoardSpace(new Pawn(Color.white));
            board[6, 7] = new BoardSpace(new Pawn(Color.white));
        }
        
        #region Da Rules
        internal void LookAtRules()
        {
            bool back = false;
            do
            {
                pmtManager.Break();
                int result = pmtManager.PromptForOption(rView.Title, rView.RuleOptions);
                back = SelectRules(result);
            } while (!back);
        }
        internal bool SelectRules(int result)
        {
            bool back = false;
            pmtManager.ClearConsole();
            switch (result)
            {
                case 1:
                    BasicRules();
                    break;
                case 2:
                    PieceMovement();
                    break;
                case 3:
                    SpecialConditions();
                    break;
                case 4:
                    back = true;
                    break;
            }
            return back;
        }
        internal void BasicRules()
        {
            pmtManager.Break();
            Console.WriteLine(rView.RuleOptions[0]);
            pmtManager.Break();
            Console.WriteLine(rView.Objective);
            Console.WriteLine(rView.MovesFirst);
            Console.WriteLine(rView.TurnTaking);
            Console.WriteLine(rView.Movement);
            Console.WriteLine(rView.StaleMate);
            Console.WriteLine(rView.Check);
            Console.WriteLine(rView.CheckMate);
            pmtManager.Prompt();
        }
        internal void PieceMovement()
        {
            pmtManager.Break();
            Console.WriteLine(rView.RuleOptions[1]);
            pmtManager.Break();
            Console.WriteLine(rView.Pawn);
            Console.WriteLine(rView.Knight);
            Console.WriteLine(rView.Bishop);
            Console.WriteLine(rView.Rook);
            Console.WriteLine(rView.Queen);
            Console.WriteLine(rView.King);
            pmtManager.Prompt();
        }
        internal void SpecialConditions()
        {
            pmtManager.Break();
            Console.WriteLine(rView.RuleOptions[2]);
            pmtManager.Break();
            Console.WriteLine(rView.EnPassant);
            Console.WriteLine(rView.Castling);
            Console.WriteLine(rView.PawnPromotion);
            pmtManager.Prompt();
        }
        #endregion
    }
}
