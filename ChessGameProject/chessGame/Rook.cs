using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class Rook : Piece
    {
        public Rook(GameBoard gameboard, Color color) : base(gameboard, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
