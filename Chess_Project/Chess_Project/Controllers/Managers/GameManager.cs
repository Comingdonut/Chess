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
        // Controllers
        private PlayerManager playerC;
        private PromptManager promptC;
        private MovementManager moveC;
        // Models
        private Board board;
        // Views
        private MenuView menuV;
        private GameView gameV;
        private RulesView rulesV;

        internal GameManager()
        {
            promptC = PromptManager.GetInstance();
            moveC = new MovementManager();
            board = new Board();
            menuV = new MenuView();
            gameV = new GameView();
            rulesV = new RulesView();
        }
        internal void StartGame()
        {
            bool endGame = false;
            playerC = new PlayerManager();
            do
            {
                int result = promptC.PromptForOption(menuV.AsciiMenu, menuV.MenuOptions);
                endGame = SelectOption(result);
            } while (!endGame);
        }
        internal bool SelectOption(int result)
        {
            bool quit = false;
            promptC.ClearConsole();
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
        #region Da Game
        internal void PlayGame()
        {
            bool gameEnd = false;
            playerC.Player1.Name = promptC.PromptForName(gameV.PromptName, 1);// Prompt player names
            playerC.Player2.Name = promptC.PromptForName(gameV.PromptName, 2);
            playerC.CurrentP = playerC.Player1;// Sets current player
            InitBoard(board.GameBoard);// initializes board
            do// Games Loop
            {
                bool isValid = false;
                bool inCheck = moveC.Check(board.GameBoard, playerC.CurrentP.Color);
                if (moveC.CheckMate(board.GameBoard, playerC.CurrentP.Color))
                {
                    playerC.SwitchPlayer();
                    gameEnd = true;
                    continue;
                }
                do
                {
                    gameV.PrintBoard(board.GameBoard);// Prints Board
                    if (inCheck)
                    {
                        Console.WriteLine(gameV.InCheck);
                    }
                    Console.WriteLine(gameV.PrintPlayerTurn(playerC.CurrentP));// Prompt Player Turn
                    BoardValuePair space = new BoardValuePair();
                    BoardSpace currentSpace = new BoardSpace();
                    do// Prompt Loop
                    {
                        space = promptC.PromptForMovement(gameV.PromptPiece[0], gameV.PromptPiece[1]);// Prompt for piece to move
                        currentSpace = board.GameBoard[space[0].Key, space[0].Value];
                        if (space[0].Key != -1)
                        {
                            if (!currentSpace.IsEmpty)// Check if not empty
                            {
                                if (currentSpace.Piece.Paint == playerC.CurrentP.Color)// Check if color matches player color
                                {
                                    isValid = true;
                                    continue;// Skip error print if valid
                                }
                            }
                        }
                        promptC.PrintError(gameV.PieceChoiceError);
                        gameV.PrintBoard(board.GameBoard);// Prints Board
                        if (inCheck)
                        {
                            Console.WriteLine(gameV.InCheck);
                        }
                    } while (!isValid);
                    moveC.SetCoordinates(space[0].Key, space[0].Value);// Stores space coordinates to movement manager
                    isValid = false; // Resets boolean
                    BoardValuePair newSpace = promptC.PromptForMovement(gameV.PromptSpace[0], gameV.PromptSpace[1]);// Prompt for space to move piece too
                    List<BoardValuePair> movement = moveC.DeterminePieceMovement(currentSpace.Piece);// Add normal movement
                    moveC.RemoveInvalidMovement(board.GameBoard, movement, currentSpace.Piece);// Checks for ally and enemy piece and boundaries. Restricts movement if space has ally or enemy or is beyond boundaries.
                    movement.AddRange(moveC.DetermineSpecialMovement(board.GameBoard, currentSpace.Piece));// Add special movement
                    if (moveC.CheckAvailablity(movement, newSpace[0].Key, newSpace[0].Value))// Checks if new Space is an movement option
                    {
                        if(!moveC.Check(board.GameBoard, playerC.CurrentP.Color, space[0].Key, space[0].Value, newSpace[0].Key, newSpace[0].Value)) // Check if moving piece puts king in check
                        {
                            isValid = true;
                            moveC.MovePiece(board.GameBoard, space[0].Key, space[0].Value, newSpace[0].Key, newSpace[0].Value);
                            continue;// Skip error print if valid
                        }
                        if (inCheck)
                        {
                            promptC.PrintError(gameV.StillInCheck);
                            continue;
                        }
                        promptC.PrintError(gameV.AlmostCheck);
                        continue;
                    }
                    promptC.PrintError(gameV.PieceMovementError);
                } while (!isValid);// If new Space is not Valid, do loop again
                promptC.ClearConsole();
                playerC.SwitchPlayer();// Switches player turns
            } while (!gameEnd);
            promptC.Break();
            Console.WriteLine(gameV.PrintWinner(playerC.CurrentP.Name));
            promptC.Break();
            promptC.Prompt();
            promptC.ClearConsole();
            board.ResetBoard();
        }
        internal void InitBoard(BoardSpace[,] board)
        {
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
        #endregion
        #region Da Rules
        internal void LookAtRules()
        {
            bool back = false;
            do
            {
                promptC.Break();
                int result = promptC.PromptForOption(rulesV.Title, rulesV.RuleOptions);
                back = SelectRules(result);
            } while (!back);
        }
        internal bool SelectRules(int result)
        {
            bool back = false;
            promptC.ClearConsole();
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
            promptC.Break();
            Console.WriteLine(rulesV.RuleOptions[0]);
            promptC.Break();
            Console.WriteLine(rulesV.Objective);
            Console.WriteLine(rulesV.MovesFirst);
            Console.WriteLine(rulesV.TurnTaking);
            Console.WriteLine(rulesV.Movement);
            Console.WriteLine(rulesV.StaleMate);
            Console.WriteLine(rulesV.Check);
            Console.WriteLine(rulesV.CheckMate);
            promptC.Prompt();
        }
        internal void PieceMovement()
        {
            promptC.Break();
            Console.WriteLine(rulesV.RuleOptions[1]);
            promptC.Break();
            Console.WriteLine(rulesV.Pawn);
            Console.WriteLine(rulesV.Knight);
            Console.WriteLine(rulesV.Bishop);
            Console.WriteLine(rulesV.Rook);
            Console.WriteLine(rulesV.Queen);
            Console.WriteLine(rulesV.King);
            promptC.Prompt();
        }
        internal void SpecialConditions()
        {
            promptC.Break();
            Console.WriteLine(rulesV.RuleOptions[2]);
            promptC.Break();
            Console.WriteLine(rulesV.EnPassant);
            Console.WriteLine(rulesV.Castling);
            Console.WriteLine(rulesV.PawnPromotion);
            promptC.Prompt();
        }
        #endregion
    }
}
