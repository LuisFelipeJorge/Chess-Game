
using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class Pawn : Piece
    {
        private ChessMatch Match;
        public Pawn(GameBoard gameBoard, Color color, ChessMatch match) : base(gameBoard, color)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ThereIsOpponent(Position position)
        {
            Piece piece = GameBoard.Piece(position);
            return (piece != null && piece.Color != Color);
        }

        private bool IsPositionFree(Position position)
        {
            return (GameBoard.Piece(position) == null);
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrix = new bool[GameBoard.NumberOfRows, GameBoard.NumberOfColumns];

            Position position = new Position(0, 0);

            // White pawns just move up
            if (Color == Color.White)
            {
                position.DefinePosition(Position.Row - 1, Position.Column);
                if (GameBoard.IsPositionValid(position) && IsPositionFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // In the first move a pawn can move two positions on the game board
                position.DefinePosition(Position.Row - 2, Position.Column);
                if (GameBoard.IsPositionValid(position) && IsPositionFree(position) && NumberOfMovements == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                // if the pawn is close to an enemy, it can move diagonally
                position.DefinePosition(Position.Row - 1, Position.Column + 1);
                if (GameBoard.IsPositionValid(position) && ThereIsOpponent(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.DefinePosition(Position.Row - 1, Position.Column - 1);
                if (GameBoard.IsPositionValid(position) && ThereIsOpponent(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Special Movement En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (GameBoard.IsPositionValid(left) && ThereIsOpponent(left) && GameBoard.Piece(left) == Match.VulnerableToEnPassant)
                    {
                        matrix[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (GameBoard.IsPositionValid(right) && ThereIsOpponent(right) && GameBoard.Piece(right) == Match.VulnerableToEnPassant)
                    {
                        matrix[right.Row - 1, right.Column] = true;
                    }
                }
            }
            // Black pawns just move down
            else
            {
                position.DefinePosition(Position.Row + 1, Position.Column);
                if (GameBoard.IsPositionValid(position) && IsPositionFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.DefinePosition(Position.Row + 2, Position.Column);
                // In the first move a pawn can move two positions on the game board
                if (GameBoard.IsPositionValid(position) && IsPositionFree(position) && NumberOfMovements == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                // if the pawn is close to an enemy, it can move diagonally
                position.DefinePosition(Position.Row + 1, Position.Column + 1);
                if (GameBoard.IsPositionValid(position) && ThereIsOpponent(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.DefinePosition(Position.Row + 1, Position.Column - 1);
                if (GameBoard.IsPositionValid(position) && ThereIsOpponent(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Special Movement En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (GameBoard.IsPositionValid(left) && ThereIsOpponent(left) && GameBoard.Piece(left) == Match.VulnerableToEnPassant)
                    {
                        matrix[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (GameBoard.IsPositionValid(right) && ThereIsOpponent(right) && GameBoard.Piece(right) == Match.VulnerableToEnPassant)
                    {
                        matrix[right.Row + 1, right.Column] = true;
                    }
                }
            }
            return matrix;
        }
    }
}
