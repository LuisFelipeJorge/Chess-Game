using ChessGameProject.chessGame;
using ChessGameProject.gameBoard;
using System;

namespace ChessGameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('a', 1);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());
        }
    }
}
