using ChessGameProject.chessGame;
using ChessGameProject.gameBoard;
using System;

namespace ChessGameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Position p = new Position(3, 4);
                Console.WriteLine(p);

                GameBoard board = new GameBoard(8, 8);
                board.PutPiece(new King(board, Color.Black), new Position(1, 1));
                board.PutPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.PutPiece(new Rook(board, Color.Black), new Position(0, 2));

                board.PutPiece(new King(board, Color.White), new Position(1, 2));
                board.PutPiece(new King(board, Color.White), new Position(2, 2));

                Screen.PrintGameBoard(board);
            }
            catch (GameBoardExceptions e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
