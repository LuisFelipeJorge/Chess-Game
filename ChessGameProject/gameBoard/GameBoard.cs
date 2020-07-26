using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameProject.gameBoard
{
    class GameBoard
    {
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }

        private Piece[,] pieces;

        public GameBoard(int numberOfRows, int numberOfColumns)
        {
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
            pieces = new Piece[NumberOfRows, NumberOfColumns];
        }

        public Piece Piece(int r, int c)
        {
            return pieces[r, c];
        }

        public Piece Piece(Position position)
        {
            return pieces[position.Row, position.Column];
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (ExistsPiece(position))
            {
                throw new GameBoardExceptions("There is already a piece here !");
            }
            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece aux = Piece(position);
            aux.Position = null;
            pieces[position.Row, position.Column] = null;
            return aux;
        }

        public bool ExistsPiece(Position position)
        {
            TovalidatePosition(position);
            return Piece(position) != null;
        }

        public bool IsPositionValid(Position position)
        {
            if ((position.Row < 0) || (position.Row >= NumberOfRows) || (position.Column < 0) || (position.Column >= NumberOfColumns))
            {
                return false;
            }
            return true;
        }

        public void TovalidatePosition(Position position)
        {
            if (!IsPositionValid(position))
            {
                throw new GameBoardExceptions("Invalid Position !");
            }
        }

    }
}
