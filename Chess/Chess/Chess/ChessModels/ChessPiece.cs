using System.Collections.Generic;

namespace Chess.ChessModels
{
    public interface ChessPiece
    {
        string Piece { get; set; }
        char Symbol { get; set; }
        char Color { get; set; }
        bool[] canMove { get; set; }
        /// <summary>
        /// Moves a piece from a starting location to a desired location.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        void MovePiece(ChessPiece[,] board, int[] start, int[] end);
        /// <summary>
        /// Checks if the desired location is an empty square and a piece of a different color.
        /// </summary>
        /// <param name="board">A 2-Dimesional array of ints representing the board.</param>
        /// <param name="end">A int array containg the end position for the piece.</param>
        /// <returns>
        /// True: If the desired square is empty or has an enemy piece.
        /// False: If the desired square already contains a friendly piece.
        /// </returns>
        bool CheckSquare(ChessPiece[,] board, int[] end);
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
        bool CheckMovement(ChessPiece[,] board, int[] start, int[] end);
        /// <summary>
        /// Checks if movement does not go off the board.
        /// </summary>
        /// <param name="start">A int array containing the starting postion for the piece.</param>
        /// <returns>Returns a list of valid movements for the piece.</returns>
        List<int[]> RestrictMovement(ChessPiece[,] board, int[] start);
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
        bool IsAvailable(ChessPiece[,] board, int row, int column, int index);
        /// <summary>
        /// Resets the possible movements.
        /// </summary>
        void ResetMovement();
    }
}
