
using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class Knight : Piece
    {

        public Knight(GameBoard gameboard, Color color) : base(gameboard, color)
        {
        }

        public override string ToString()
        {
            return "k";
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

            // --
            // |
            position.DefinePosition(Position.Row - 2, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }

            // --
            //  |
            position.DefinePosition(Position.Row - 2, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // __|
            position.DefinePosition(Position.Row - 1, Position.Column + 2);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // __
            //   |
            position.DefinePosition(Position.Row + 1, Position.Column + 2);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // |
            //  --
            position.DefinePosition(Position.Row + 2, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            //   |
            // --
            position.DefinePosition(Position.Row + 2, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // |__
            position.DefinePosition(Position.Row - 1, Position.Column - 2);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            //  __
            // |
            position.DefinePosition(Position.Row + 1, Position.Column - 2);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }

            return matrix;
        }
    }
}
