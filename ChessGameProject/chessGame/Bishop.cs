
using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class Bishop : Piece
    {
        public Bishop(GameBoard gameBoard, Color color): base(gameBoard, color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position position)
        {
            // the piece can move when the target position is empty (null) or
            // when there is an adversary piece in that location.
            Piece piece = GameBoard.Piece(position);
            return ((piece == null) || (piece.Color != Color));
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrix = new bool[GameBoard.NumberOfRows, GameBoard.NumberOfColumns];

            Position position = new Position(0, 0);

            // Northeast
            position.DefinePosition(Position.Row - 1, Position.Column + 1);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row -= 1;
                position.Column += 1;
            }

            // Northwest
            position.DefinePosition(Position.Row - 1, Position.Column - 1);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row -= 1;
                position.Column -= 1;
            }

            // Southeast
            position.DefinePosition(Position.Row + 1, Position.Column + 1);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row += 1;
                position.Column += 1;
            }

            // South-west
            position.DefinePosition(Position.Row + 1, Position.Column - 1);
            while (GameBoard.IsPositionValid(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (GameBoard.Piece(position) != null && GameBoard.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row += 1;
                position.Column -= 1;
            }

            return matrix;
        }
    }
}
