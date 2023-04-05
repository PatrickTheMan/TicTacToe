using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Domain
{
	public class ScoreboardEntry
	{
		public string Name { get; set; }
		public int Score { get; set; }
		public ScoreboardEntry(string name, int score) 
		{ 
			this.Name = name;
			this.Score = score;
		}
	}
}
