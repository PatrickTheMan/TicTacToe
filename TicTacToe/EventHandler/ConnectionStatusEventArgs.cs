using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.EventHandler
{
	public class ConnectionStatusEventArgs : EventArgs
	{
		public bool ConnectionStatus { get; }

		public ConnectionStatusEventArgs(bool connectionStatus)
		{
			ConnectionStatus = connectionStatus;
		}
	}
}
