using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
	class Game
	{
		private bool isGameEnded;
		private Board board;

        public bool IsGameEnded
		{
			get => isGameEnded;
			set
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Game state can't be changed manually");
				Console.ResetColor();
			}
		}

        public Game()
		{
			isGameEnded = false;
			SetBoardSize();
		}

		public void SetBoardSize()
		{
			Console.WriteLine("Please enter the size of your board between 4 and 8 (both included)");
			string size = Console.ReadLine();

			int result;

			while (!int.TryParse(size, out result) || result < 4 && result > 8)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Please enter an INTEGER number between 4 and 8 (both included)");
				Console.ResetColor();
				size = Console.ReadLine();
			}
			board = new(result);
		}

		public void AskForNewGame()
        {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Do you want to play again?(y/n)");
			Console.ResetColor();

			string answer = Console.ReadLine();

			while(!(answer.Equals("y") || answer.Equals("n")))
            {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Please give a valid anser.(y/n)");
				Console.ResetColor();
				answer = Console.ReadLine();
			}

			if (answer.Equals("n"))
				IsGameEnded = true;
			else
                ReStartGame();
		}

		public void ReStartGame()
        {
			SetBoardSize();
        }

		public void GetTurn()
        {
			Console.WriteLine("Please enter the direction you want to slide\n" +
				"(right/r, left/l, up/u, down/d)");

			string direction = Console.ReadLine();

			while(!(
				direction.Equals("right") || direction.Equals("r") ||
				direction.Equals("left") || direction.Equals("l") ||
				direction.Equals("up") || direction.Equals("u") ||
				direction.Equals("down") || direction.Equals("d")
				))
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Please enter an valid direction (right/r, left/l, up/u, down/d)");
				Console.ResetColor();
				direction = Console.ReadLine();
			}
			board.SlideAndAddNew(direction[0]);
		}

		public void ShowBoard()
        {
			Console.WriteLine(board);
        }

		public bool IsGameOver()
        {
			if (board.IsTableFull() && !board.IsThereAnyDuplicate())
				return true;
			else return false;
        }
	}
}
