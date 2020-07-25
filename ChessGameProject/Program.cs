using ChessGameProject.chessGame;
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
            board.PutPiece(new King(board, Color.Black), new Position(1, 1));
            board.PutPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.PutPiece(new Rook(board, Color.Black), new Position(2, 4));


            Screen.PrintGameBoard(board);
        }
    }
}
