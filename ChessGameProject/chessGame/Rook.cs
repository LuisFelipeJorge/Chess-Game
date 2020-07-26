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

        private bool CanMove(Position position)
        {
            // the piece can move when the target position is empty (null) or
            // when there is an adversary piece in that location.
            Piece piece = GameBoard.Piece(position);
            return (piece == null || piece.Color != Color);
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] matrix = new bool[GameBoard.NumberOfRows, GameBoard.NumberOfColumns];

            Position position = new Position(0, 0);

            // North
            position.DefinePosition(Position.Row - 1, Position.Column);
            while(GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if(GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row -= 1;
            }

            // South
            position.DefinePosition(Position.Row + 1, Position.Column);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row += 1;
            }

            // Right
            position.DefinePosition(Position.Row, Position.Column + 1);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column += 1;
            }

            // Left
            position.DefinePosition(Position.Row, Position.Column - 1);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column -= 1;
            }

            return matrix;
        }
    }
}
