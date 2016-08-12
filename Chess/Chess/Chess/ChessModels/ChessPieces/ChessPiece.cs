using System.Collections.Generic;

namespace Chess.ChessModels
{
    public abstract class ChessPiece
    {
        public string Piece { get; set; }
        public char Symbol { get; set; }
        public ChessColor Color { get; set; }
        public bool[] canMove { get; set; }

        public ChessPiece()
        {

        }
        /// <summary>
        /// Moves a piece from a starting location to a desired location.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        public abstract void MovePiece(ChessSquare[,] board, int[] start, int[] end);
        /// <summary>
        /// Checks if the desired location is an empty square and a piece of a different color.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        /// <returns>
        /// True: If the desired square is empty or has an enemy piece.
        /// False: If the desired square already contains a friendly piece.
        /// </returns>
        public abstract bool CheckSquare(ChessSquare[,] board, int[] end);
        /// <summary>
        /// Checks if a piece's end point is a valid move.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        /// <returns>
        /// True: if the deisred location is a valid move for the piece.
        /// False: if the desired location is not a valid move for the piece.
        /// </returns>
        public abstract bool CheckMovement(ChessSquare[,] board, int[] start, int[] end);
        /// <summary>
        /// Checks if movement does not go off the board.
        /// </summary>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <returns>Returns a list of valid movements for the piece.</returns>
        public abstract List<int[]> RestrictMovement(ChessSquare[,] board, int[] start);
        /// <summary>
        /// Checks if a movement option for a piece is empty or has an enemy piece.
        /// If it has a ally piece, then it can not move pass the piece unless it is
        /// a knight.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="row">A int representing a row from the board.</param>
        /// <param name="column">A int representing a column number fromthe board.</param>
        /// <param name="index">A int representing a index number for the bool array.</param>
        /// <returns></returns>
        public abstract bool IsAvailable(ChessSquare[,] board, int row, int column, int index);
        /// <summary>
        /// Resets the possible movements.
        /// </summary>
        public abstract void ResetMovement();
    }
}
