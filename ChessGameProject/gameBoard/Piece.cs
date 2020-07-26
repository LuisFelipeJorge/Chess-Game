
namespace ChessGameProject.gameBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color  { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public GameBoard GameBoard { get; protected set; }

        public Piece(GameBoard gameBoard, Color color)
        {
            Position = null;
            Color = color;
            GameBoard = gameBoard;
            NumberOfMovements = 0;
        }

        public void IncreaseNumberOfMovements()
        {
            NumberOfMovements += 1;
        }

        public abstract bool[,] PossibleMovements();
    }
}
