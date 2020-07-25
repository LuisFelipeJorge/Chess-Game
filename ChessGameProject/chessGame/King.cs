using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class King : Piece
    {
        public King(GameBoard gameboard, Color color) : base(gameboard, color)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
