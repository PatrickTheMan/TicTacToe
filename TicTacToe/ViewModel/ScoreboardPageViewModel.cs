using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain;
using TicTacToe.Singleton;

namespace TicTacToe.ViewModel
{
	public partial class ScoreboardPageViewModel : ObservableObject, IDisposable
	{

		[ObservableProperty]
		List<ScoreboardEntry> scoreboardEntries;

		public ScoreboardPageViewModel() 
		{
			HubConnectionHandlerSingleton.Instance.Invoke("GetScoreboard", new object[] { });
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
