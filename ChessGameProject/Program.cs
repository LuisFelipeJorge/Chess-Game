using ChessGameProject.chessGame;
using ChessGameProject.gameBoard;
using System;
using System.Security.Cryptography;

namespace ChessGameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.MatchEnded)
                {
                    Console.Clear();
                    Screen.PrintGameBoard(match.GameBoard);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();

                    bool[,] possiblePositions = match.GameBoard.Piece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintGameBoard(match.GameBoard, possiblePositions);

                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadPosition().ToPosition();

                    match.DoMovement(origin, destiny);

                }

            }
            catch (GameBoardExceptions e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
