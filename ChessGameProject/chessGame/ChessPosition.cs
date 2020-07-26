

using ChessGameProject.gameBoard;

namespace ChessGameProject.chessGame
{
    class ChessPosition
    {
        public int Row { get; set; }
        public char Column { get; set; }

        public ChessPosition(char column, int row)
        {
            Row = row;
            Column = column;
        }

        public Position ToPosition()
        {
            // Converts the input chess position to a valid index in the matrix
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Column + Row;
        }
    }
}
