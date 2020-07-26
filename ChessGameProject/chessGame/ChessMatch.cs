
using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class ChessMatch
    {
        public GameBoard GameBoard { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
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
