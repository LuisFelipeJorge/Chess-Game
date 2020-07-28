
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

        public bool PossibleMovementsExists()
        {
            bool[,] matrix = PossibleMovements();
            for (int i = 0; i < GameBoard.NumberOfRows; i++)
            {
                for (int j = 0; j < GameBoard.NumberOfColumns; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }
    }
}
