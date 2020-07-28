using System.Collections.Generic;
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

        public ChessMatch()
        {
            GameBoard = new GameBoard(8, 8);
            Turn = 1;
            MatchEnded = false;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            FillGameBoard();
        }

        public void DoMovement(Position origin, Position destiny)
        {
            Piece piece = GameBoard.RemovePiece(origin);
            piece.IncreaseNumberOfMovements();
            Piece captured = GameBoard.RemovePiece(destiny);
            GameBoard.PutPiece(piece, destiny);
            if (captured != null)
            {
                Captured.Add(captured);
            }
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
            DoMovement(origin, destiny);
            Turn++;
            ChangePlayer();
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
            if (!GameBoard.Piece(origin).CanMoveTo(destiny))
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

        public void PlaceNewPiece(char column, int row, Piece piece)
        {
            GameBoard.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        public void FillGameBoard()
        {
            PlaceNewPiece('c', 1, new Rook(GameBoard, Color.White));
            PlaceNewPiece('c', 2, new Rook(GameBoard, Color.White));
            PlaceNewPiece('d', 2, new Rook(GameBoard, Color.White));
            PlaceNewPiece('e', 2, new Rook(GameBoard, Color.White));
            PlaceNewPiece('e', 1, new Rook(GameBoard, Color.White));
            PlaceNewPiece('d', 1, new King(GameBoard, Color.White));

            PlaceNewPiece('c', 7, new Rook(GameBoard, Color.Black));
            PlaceNewPiece('c', 8, new Rook(GameBoard, Color.Black));
            PlaceNewPiece('d', 7, new Rook(GameBoard, Color.Black));
            PlaceNewPiece('e', 7, new Rook(GameBoard, Color.Black));
            PlaceNewPiece('e', 8, new Rook(GameBoard, Color.Black));
            PlaceNewPiece('d', 8, new King(GameBoard, Color.Black));

        }
    }
}
