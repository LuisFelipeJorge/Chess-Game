using System.Collections.Generic;
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
        public Piece VulnerableToEnPassant { get; private set; }

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
            VulnerableToEnPassant = null;
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

            // Special Movement Castling
            // Short Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Piece rook = GameBoard.RemovePiece(rookOrigin);
                rook.IncreaseNumberOfMovements();
                GameBoard.PutPiece(rook, rookDestiny);
            }
            // Long Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Piece rook = GameBoard.RemovePiece(rookOrigin);
                rook.IncreaseNumberOfMovements();
                GameBoard.PutPiece(rook, rookDestiny);
            }

            // Special Movement En Passant
            if ((piece is Pawn) && (origin.Column != destiny.Column) && (captured == null))
            {
                Position pawnPos;
                if (piece.Color == Color.White)
                {
                    pawnPos = new Position(destiny.Row + 1, destiny.Column);
                }
                else
                {
                    pawnPos = new Position(destiny.Row - 1, destiny.Column);
                }
                captured = GameBoard.RemovePiece(pawnPos);
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

            // Special Movement Castling
            // Short Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Piece rook = GameBoard.RemovePiece(rookDestiny);
                rook.DecreaseNumberOfMovements();
                GameBoard.PutPiece(rook, rookOrigin);
            }
            // Long Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Piece rook = GameBoard.RemovePiece(rookDestiny);
                rook.DecreaseNumberOfMovements();
                GameBoard.PutPiece(rook, rookOrigin);
            }

            // Special Movement En Passant
            if ((piece is Pawn) && (origin.Column != destiny.Column) && (captured == VulnerableToEnPassant))
            {
                Piece pawn = GameBoard.RemovePiece(destiny);
                Position pawnPos;
                if (piece.Color == Color.White)
                {
                    pawnPos = new Position(3, destiny.Column);
                }
                else
                {
                    pawnPos = new Position(4, destiny.Column);
                }
                GameBoard.PutPiece(pawn, pawnPos);
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
            Piece captured = DoMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, captured);
                throw new GameBoardExceptions("You can't put yourself in check");
            }

            // Special Movement Promotion
            Piece piece = GameBoard.Piece(destiny);
            if ((piece is Pawn) && ((piece.Color == Color.White && destiny.Row == 0) || (piece.Color == Color.Black && destiny.Row == 7)))
            {
                piece = GameBoard.RemovePiece(destiny);
                Pieces.Remove(piece);
                Queen queen = new Queen(GameBoard, piece.Color);
                GameBoard.PutPiece(queen, destiny);
                Pieces.Add(queen);
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

            // Special Movement En Passant
            if ((piece is Pawn) && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VulnerableToEnPassant = piece;
            }
            else
            {
                VulnerableToEnPassant = null;
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
                        if (matrix[i, j])
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
                throw new GameBoardExceptions("There is no king of color " + color + " on the game board");
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
            PlaceNewPiece('e', 1, new King(GameBoard, Color.White, this));
            PlaceNewPiece('e', 8, new King(GameBoard, Color.Black, this));

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
            PlaceNewPiece('a', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(GameBoard, Color.White, this));
            PlaceNewPiece('a', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(GameBoard, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(GameBoard, Color.Black, this));


        }
    }
}
