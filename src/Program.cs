using System;

namespace _2048
{
    class Program
    {
        static void Main()
        {
            Game game = new();
            while (!game.IsGameEnded)
            {
                while (!game.IsGameOver())
                {
                    game.ShowBoard();
                    game.GetTurn();
                }
                game.ShowBoard();

                game.AskForNewGame();
            }
        }
    }
}
