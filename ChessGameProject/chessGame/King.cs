using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class King : Piece
    {
        private ChessMatch Match;
        public King(GameBoard gameboard, Color color, ChessMatch match) : base(gameboard, color)
        {
            Match = match;
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

        private bool TestRookForCastling(Position position)
        {
            Piece piece = GameBoard.Piece(position);
            return ((piece != null) && (piece is Rook) && (piece.Color == Color) && (piece.NumberOfMovements == 0));
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
            }
            // Right
            position.DefinePosition(Position.Row, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // Southeast
            position.DefinePosition(Position.Row + 1, Position.Column + 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // South
            position.DefinePosition(Position.Row + 1, Position.Column);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // South-west
            position.DefinePosition(Position.Row + 1, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // Left
            position.DefinePosition(Position.Row, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }
            // Northwest 
            position.DefinePosition(Position.Row - 1, Position.Column - 1);
            if ((GameBoard.IsPositionValid(position)) && (CanMove(position)))
            {
                matrix[position.Row, position.Column] = true;
            }

            //  Special Movement Castling
            //      Castling may only be done if the king has never moved, 
            //      the rook involved has never moved, the squares between the king and the rook involved are unoccupied, 
            //      the king is not in check, and the king does not cross over or end on a square attacked by an enemy piece.

            if (NumberOfMovements == 0 && !Match.InCheck)
            {
                // Short Castling or Kingside castling 
                Position rookPos1 = new Position(Position.Row, Position.Column + 3);
                if (TestRookForCastling(rookPos1))
                {
                    Position pos1 = new Position(Position.Row, Position.Column + 1);
                    Position pos2 = new Position(Position.Row, Position.Column + 2);
                    if (GameBoard.Piece(pos1) == null && GameBoard.Piece(pos2) == null)
                    {
                        matrix[Position.Row, Position.Column + 2] = true;
                    }
                }
                // Lont Castling or Queenside castling
                Position rookPos2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookForCastling(rookPos1))
                {
                    Position pos1 = new Position(Position.Row, Position.Column - 1);
                    Position pos2 = new Position(Position.Row, Position.Column - 2);
                    Position pos3 = new Position(Position.Row, Position.Column - 3);

                    if (GameBoard.Piece(pos1) == null && GameBoard.Piece(pos2) == null && GameBoard.Piece(pos3) == null)
                    {
                        matrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }


    }
}
