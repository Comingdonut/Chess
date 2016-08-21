using System.Collections.Generic;

namespace Chess.ChessModels
{
    public abstract class ChessPiece
    {
        #region Variables
        /// <summary>
        /// A symbol that contains the Abbreviation of the piece.
        /// </summary>
        public char Symbol { get; set; }
        /// <summary>
        /// The color of the piece.
        /// </summary>
        public ChessColor Color { get; set; }
        /// <summary>
        /// A bool array that has the value turn true if a specific move for the piece is possible.
        /// The length of the array is set to the amount of all possible move directions for the piece.
        /// </summary>
        public bool[] canMove { get; set; }
        /// <summary>
        /// A int array that keeps track the amount of times a movement goes over the enemy piece.
        /// </summary>
        public int[] collision;
        #endregion
        #region PieceMovement
        /// <summary>
        /// Moves a piece from a starting location to a desired location.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        public abstract void MovePiece(ChessSquare[,] board, int startX, int startY, int endX, int endY);
        /// <summary>
        /// Checks if the desired location is an empty square and a piece of a different color.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        /// <returns>
        /// True: If the desired square is empty or has an enemy piece.
        /// False: If the desired square already contains a friendly piece.
        /// </returns>
        public abstract bool CheckMovement(ChessSquare[,] board, int startX, int startY, int endX, int endY);
        /// <summary>
        /// Checks if movement does not go off the board.
        /// </summary>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <returns>Returns a list of valid movements for the piece.</returns>
        public abstract List<int[]> RestrictMovement(ChessSquare[,] board, int startX, int startY);
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
        /// Resets the bool array, so all moves are possible.
        /// </summary>
        public abstract void ResetMovement();
        public abstract List<int[]> Search(ChessSquare[,] board, int startX, int startY, int endX, int endY);
        public abstract void ResetSearch(List<int[]> available, bool isMoveSet);
        #endregion
    }
}
