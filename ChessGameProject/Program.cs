using ChessGameProject.chessGame;
using ChessGameProject.gameBoard;
using System;
using System.Security.Cryptography;
using System.Xml;

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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMacth(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.GameBoard.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintGameBoard(match.GameBoard, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.PerformMovement(origin, destiny);
                    }
                    catch (GameBoardExceptions e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (GameBoardExceptions e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
