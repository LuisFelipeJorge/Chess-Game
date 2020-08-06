
using ChessGameProject.chessGame;
using ChessGameProject.gameBoard;
using System;
using System.Collections.Generic;

namespace ChessGameProject
{
    class Screen
    {
        public static void PrintGameBoard(GameBoard board)
        {
            for (int i = 0; i < board.NumberOfRows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.NumberOfColumns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintGameBoard(GameBoard board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.NumberOfRows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.NumberOfColumns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }


            
        }

        public static ChessPosition ReadPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void DisplaySet(HashSet<Piece> pieces)
        {
            Console.Write("{");
            foreach (Piece p in pieces)
            {
                Console.Write(p + " ");
            }
            Console.WriteLine(" }");
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("Whites: ");
            DisplaySet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            DisplaySet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;  
            Console.WriteLine();
        }

        public static void PrintMacth(ChessMatch match)
        {
            PrintGameBoard(match.GameBoard);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine("Waiting for movement: " + match.CurrentPlayer);
            if (match.IsInCheck)
            {
                Console.WriteLine("CHECK!!");
            }
        }
    }
}
