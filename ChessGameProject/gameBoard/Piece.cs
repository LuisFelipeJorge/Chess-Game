
namespace ChessGameProject.gameBoard
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color  { get; set; }
        public int NumberOfMovements { get; protected set; }
        public GameBoard GameBoard { get; set; }

        public Piece(GameBoard gameBoard, Color color)
        {
            Position = null;
            Color = color;
            GameBoard = gameBoard;
            NumberOfMovements = 0;
        }
    }
}
