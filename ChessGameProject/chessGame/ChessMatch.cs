
using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class ChessMatch
    {
        public GameBoard GameBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool MatchEnded { get; private set; }

        public ChessMatch()
        {
            GameBoard = new GameBoard(8, 8);
            Turn = 1;
            MatchEnded = false;
            CurrentPlayer = Color.White;
            FillGameBoard();
        }

        public void DoMovement(Position origin, Position destiny)
        {
            Piece piece = GameBoard.RemovePiece(origin);
            piece.IncreaseNumberOfMovements();
            Piece captured = GameBoard.RemovePiece(destiny);
            GameBoard.PutPiece(piece, destiny);
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

        public void FillGameBoard()
        {
            GameBoard.PutPiece(new Rook(GameBoard, Color.White), new ChessPosition('a', 1).ToPosition());
            GameBoard.PutPiece(new Rook(GameBoard, Color.White), new ChessPosition('h', 1).ToPosition());
            GameBoard.PutPiece(new Rook(GameBoard, Color.Black), new ChessPosition('a', 8).ToPosition());
            GameBoard.PutPiece(new Rook(GameBoard, Color.Black), new ChessPosition('h', 8).ToPosition());

            GameBoard.PutPiece(new King(GameBoard, Color.White), new ChessPosition('e', 1).ToPosition());
            GameBoard.PutPiece(new King(GameBoard, Color.Black), new ChessPosition('e', 8).ToPosition());

        }
    }
}
