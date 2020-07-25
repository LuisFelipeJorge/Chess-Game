
using ChessGameProject.gameBoard;
using System;

namespace ChessGameProject
{
    class Screen
    {
        public static void PrintGameBoard(GameBoard board)
        {
            for (int i = 0; i < board.NumberOfRows; i++)
            {
                for (int j = 0; j < board.NumberOfColumns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.Piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
