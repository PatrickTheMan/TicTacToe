using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.EventHandler
{
	public class GameFinishedEventArgs : EventArgs
	{
		public string Winner { get; }

		public GameFinishedEventArgs(string winner)
		{
			Winner = winner;
		}
	}
}
