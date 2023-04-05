using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Handlers;

namespace TicTacToe.Singleton
{
    public class HubConnectionHandlerSingleton : HubConnectionHandler
	{
		private HubConnectionHandlerSingleton() { }
		private static HubConnectionHandlerSingleton instance = null;
		/// <summary>
		/// Gets a already existing instance or creates a new instance of the HubConnectionHandler
		/// </summary>
		public static HubConnectionHandlerSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new HubConnectionHandlerSingleton();
				}
				return instance;
			}
		}
	}
}
