﻿using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class ChessMatch
    {
        public GameBoard GameBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool MatchEnded { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool InCheck { get; private set; }

        public ChessMatch()
        {
            GameBoard = new GameBoard(8, 8);
            Turn = 1;
            MatchEnded = false;
            InCheck = false;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            FillGameBoard();
        }

        public Piece DoMovement(Position origin, Position destiny)
        {
            Piece piece = GameBoard.RemovePiece(origin);
            piece.IncreaseNumberOfMovements();
            Piece captured = GameBoard.RemovePiece(destiny);
            GameBoard.PutPiece(piece, destiny);
            if (captured != null)
            {
                Captured.Add(captured);
            }
            return captured;
        }

        public void UndoMovement(Position origin, Position destiny, Piece captured)
        {
            Piece piece = GameBoard.RemovePiece(destiny);
            piece.DecreaseNumberOfMovements();

            if (captured != null)
            {
                GameBoard.PutPiece(captured, destiny);
                Captured.Remove(captured);
            }
            GameBoard.PutPiece(piece, origin);
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }
        public void PerformMovement(Position origin, Position destiny)
        {
            Piece captured = DoMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, captured);
                throw new GameBoardExceptions("You can't put yourself in check");
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                InCheck = true;
            }
            else
            {
                InCheck = false;
            }

            if (TestCheckMate(Adversary(CurrentPlayer)))
            {
                MatchEnded = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }            
        }

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            // Analyze all possible movements for each piece to see if there is a movement that does not result in a Check
            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] matrix = piece.PossibleMovements();
                for (int i = 0; i < GameBoard.NumberOfRows; i++)
                {
                    for (int j = 0; j < GameBoard.NumberOfColumns; j++)
                    {
                        if (matrix[i,j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece captured = DoMovement(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, destiny, captured);
                            if (!testCheck)
                            {// there are still possible moves
                                return false;
                            }
                        }                        
                    }
                }
            }
            return true;
        }

        public void ValidateOriginPosition(Position position)
        {
            if (GameBoard.Piece(position) == null)
            {
                throw new GameBoardExceptions("There is no piece in this chosen position");
            }
            if (CurrentPlayer != GameBoard.Piece(position).Color)
            {
                throw new GameBoardExceptions("The piece in this position is not yours!");
            }
            if (!GameBoard.Piece(position).PossibleMovementsExists())
            {
                throw new GameBoardExceptions("There is no possible movements for the piece in that position");
             }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!GameBoard.Piece(origin).PossibleMovement(destiny))
            {
                throw new GameBoardExceptions("Invalid destiny position");
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(CapturedPieces(color)); 
            return aux;
        }

        private Color Adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece GetKing(Color color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = GetKing(color);
            if (king == null)
            {
                throw new GameBoardExceptions("There is no king of color "+ color + " on the game board");
            }
            foreach (Piece piece in PiecesInPlay(Adversary(color)))
            {
                bool[,] matrix = piece.PossibleMovements();
                if (matrix[king.Position.Row, king.Position.Column])
                {
                    // an adversary piece can take the king, therefore
                    // is in checkmate
                    return true;
                }
            }
            return false;
        }

        public void PlaceNewPiece(char column, int row, Piece piece)
        {
            GameBoard.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        public void FillGameBoard()
        {
            // King
            PlaceNewPiece('e', 1, new King(GameBoard, Color.White));
            PlaceNewPiece('e', 8, new King(GameBoard, Color.Black));

            // Queen
            PlaceNewPiece('d', 1, new Queen(GameBoard, Color.White));
            PlaceNewPiece('d', 8, new Queen(GameBoard, Color.Black));

            // Rook
            PlaceNewPiece('a', 1, new Rook(GameBoard, Color.White));
            PlaceNewPiece('h', 1, new Rook(GameBoard, Color.White));
            PlaceNewPiece('a', 8, new Rook(GameBoard, Color.Black));
            PlaceNewPiece('h', 8, new Rook(GameBoard, Color.Black));

            // Bishop
            PlaceNewPiece('c', 1, new Bishop(GameBoard, Color.White));
            PlaceNewPiece('f', 1, new Bishop(GameBoard, Color.White));
            PlaceNewPiece('c', 8, new Bishop(GameBoard, Color.Black));
            PlaceNewPiece('f', 8, new Bishop(GameBoard, Color.Black));

            // Knight
            PlaceNewPiece('b', 1, new Knight(GameBoard, Color.White));
            PlaceNewPiece('g', 1, new Knight(GameBoard, Color.White));
            PlaceNewPiece('b', 8, new Knight(GameBoard, Color.Black));
            PlaceNewPiece('g', 8, new Knight(GameBoard, Color.Black));

            // Pawn
            PlaceNewPiece('a', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('b', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('c', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('d', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('e', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('f', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('g', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('h', 2, new Pawn(GameBoard, Color.White));
            PlaceNewPiece('a', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('b', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('c', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('d', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('e', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('f', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('g', 7, new Pawn(GameBoard, Color.Black));
            PlaceNewPiece('h', 7, new Pawn(GameBoard, Color.Black));


        }
    }
}
