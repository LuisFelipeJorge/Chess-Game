using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameProject.gameBoard
{
    class GameBoard
    {
        public int NumberOfRows{ get; set; }
        public int NumberOfColumns{ get; set; }

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
    }
}
