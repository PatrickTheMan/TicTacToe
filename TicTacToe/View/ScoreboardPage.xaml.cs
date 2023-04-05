using System.Diagnostics;
using TicTacToe.Domain;
using TicTacToe.Singleton;
using TicTacToe.ViewModel;

namespace TicTacToe.View;

public partial class ScoreboardPage : ContentPage
{
	public ScoreboardPage(ScoreboardPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		HubConnectionHandlerSingleton.Instance.AddHappening("ScoreboardReceived",
				(Dictionary<string, int> scoreboard) =>
				{
					vm.ScoreboardEntries = new List<ScoreboardEntry>();
					foreach (var item in scoreboard)
					{
						vm.ScoreboardEntries.Add(new ScoreboardEntry(item.Key, item.Value));
					}

					Debug.WriteLine(vm.ScoreboardEntries.Count);
				}
			);

		// Invoke
		HubConnectionHandlerSingleton.Instance.Invoke("GetScoreboard", new object[] { });
	}
}