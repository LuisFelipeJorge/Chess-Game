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

            // North
            position.DefinePosition(Position.Row - 1, Position.Column);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // northeast
            position.DefinePosition(Position.Row - 1, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }// Right
            position.DefinePosition(Position.Row, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }// Southeast
            position.DefinePosition(Position.Row + 1, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }// South
            position.DefinePosition(Position.Row + 1, Position.Column);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }// South-west
            position.DefinePosition(Position.Row + 1, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }// Left
            position.DefinePosition(Position.Row, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }// Northwest 
            position.DefinePosition(Position.Row - 1, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }

            return matrix;
        }
    }
}
