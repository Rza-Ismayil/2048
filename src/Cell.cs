using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
	class Cell
	{
		private int value;

		public int Value { get => value; set => this.value = value; }


		public Cell(int value)
		{
			this.value = value;
		}

		public Cell()
        {
			value = 0;
        }

		public override string ToString()
		{
			return " " + (value != 0 ? value : "+") + " ";   
		}
	}
}
