using System;

namespace ChessGameProject.gameBoard
{
    class GameBoardExceptions : Exception
    {
        public GameBoardExceptions(string msg) : base(msg)
        {
        }
    }
}
