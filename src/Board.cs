using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
	class Board
	{
		public readonly int size;
		private Cell[,] table;

		public Cell[,] Table { get => table; set => table = value; }

		public Board(int size)
		{
			this.size = size;
			table = new Cell[size, size];
			FillTable();
		}

		public void FillTable()
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					table[i, j] = new();
				}
			}

			for (int i = 0; i < 2; i++) FillRandomCell();
		}

		public void FillRandomCell()
		{
			Random rand = new();
			
			int randRow = rand.Next(size);
			int randCol = rand.Next(size);

			while (table[randRow, randCol].Value != 0) 
			{
				randRow = rand.Next(size);
				randCol = rand.Next(size);
			}

			table[randRow, randCol].Value = rand.Next(2) == 0 ? 2 : 4;
		}

		public void SlideAndAddNew(char direction)
        {
			int[,] oldTable = new int[size, size];

			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					oldTable[i, j] = table[i, j].Value;

			SumAndSlide(direction);

			if (!IsTableFull() && DidTableChanged(oldTable))
				FillRandomCell();
		}

		public bool IsThereAnyDuplicate()
        {
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
                {
					if (i < size - 1)
						if (table[i, j].Value == table[i + 1, j].Value)
							return true;

					if (j < size - 1)
						if (table[i, j].Value == table[i, j + 1].Value)
							return true;
				}
			return false;
		}

		public bool IsTableFull()
        {
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					if (table[i, j].Value == 0)
						return false;
			return true;
		}

		public bool DidTableChanged(int [,] oldTable)
        {
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					if (oldTable[i, j] != table[i, j].Value)
						return true;
			return false;
        }

		public void SumAndSlide(char direction)
		{
			if(direction == 'u' || direction == 'd')
			{
				SlideAndSumVertically(direction);
			}
			else
			{
				SlideAndSumHorizontally(direction);
			}
		}

		public void SlideAndSumVertically(char direction)
		{
			SlideVertically(direction);
			SumVertically(direction);
			SlideVertically(direction);
		}

		public void SlideVertically(char direction)
        {
			int[] line = new int[size];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					line[j] = table[j, i].Value;
					table[j, i].Value = 0;
				}


				if (direction == 'd')
				{
					int emptyIndex = size - 1;

					for (int k = size - 1; k >= 0; k--)
					{
						if (line[k] != 0)
						{
							table[emptyIndex, i].Value = line[k];
							emptyIndex--;
						}
					}
				}
				else
				{
					int emptyIndex = 0;

					for (int k = 0; k < size; k++)
					{
						if (line[k] != 0)
						{
							table[emptyIndex, i].Value = line[k];
							emptyIndex++;
						}
					}
				}
			}
		}

		public void SumVertically(char direction)
		{
			for (int i = 0; i < size; i++)
			{
				if (direction == 'd')
				{
					for (int j = size - 1; j > 0; j--)
					{
						if (table[j, i].Value != 0)
							if (table[j, i].Value == table[j - 1, i].Value)
							{
								table[j, i].Value *= 2;
								table[j - 1, i].Value = 0;
								j--;
							}
					}
				}
				else
				{
					for (int j = 0; j < size - 1; j++)
					{
						if (table[j, i].Value != 0)
							if (table[j, i].Value == table[j + 1, i].Value)
							{
								table[j, i].Value *= 2;
								table[j + 1, i].Value = 0;
								j++;
							}
					}
				}
			}
		}

		public void SlideAndSumHorizontally(char direction)
		{
			SlideHorizontally(direction);
			SumHorizontally(direction);
			SlideHorizontally(direction);
		}

		public void SlideHorizontally(char direction)
		{
			int[] line = new int[size];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					line[j] = table[i, j].Value;
					table[i, j].Value = 0;
				}


				if (direction == 'r')
				{
					int emptyIndex = size - 1;

					for (int k = size - 1; k >= 0; k--)
					{
						if (line[k] != 0)
						{
							table[i, emptyIndex].Value = line[k];
							emptyIndex--;
						}
					}
				}
				else
				{
					int emptyIndex = 0;

					for (int k = 0; k < size; k++)
					{
						if (line[k] != 0)
						{
							table[i, emptyIndex].Value = line[k];
							emptyIndex++;
						}
					}
				}
			}
		}

		public void SumHorizontally(char direction)
		{
			for (int i = 0; i < size; i++)
			{
				if (direction == 'r')
				{
					for (int j = size - 1; j > 0; j--)
					{
						if (table[i, j].Value != 0)
							if (table[i, j].Value == table[i, j - 1].Value)
							{
								table[i, j].Value *= 2;
								table[i, j - 1].Value = 0;
								j--;
							}
					}
				}
				else
				{
					for (int j = 0; j < size - 1; j++)
					{
						if (table[i, j].Value != 0)
							if (table[i, j].Value == table[i, j + 1].Value)
							{
								table[i, j].Value *= 2;
								table[i, j + 1].Value = 0;
								j++;
							}
					}
				}
			}
		}

		public int MaxValueLength()
        {
			int maxSize = 0;
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					maxSize = Math.Max(maxSize, table[i, j].ToString().Length);
			return maxSize;
		}

		public override string ToString()
		{
			string result = "";

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					result += table[i, j].ToString();
					for (int k = 0; k < MaxValueLength() - table[i, j].ToString().Length; k++)
						result += " ";
				}
				result += "\n";
			}

			return result;
		}
	}
}
