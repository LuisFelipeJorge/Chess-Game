using ChessGameProject.gameBoard;
using System;

namespace ChessGameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p = new Position(3, 4);
            Console.WriteLine(p);

            GameBoard board = new GameBoard(8, 8);
            Screen.PrintGameBoard(board);
        }
    }
}
