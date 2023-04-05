using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain;
using TicTacToe.Handlers;
using TicTacToe.Singleton;

namespace TicTacToe.ViewModel
{
	[QueryProperty("ConnectionPlayerId", "ConnectionPlayerId")]
	public partial class GamePageViewModel : ObservableObject, IDisposable
	{

		[ObservableProperty]
		Player player1;

		[ObservableProperty]
		Player player2;

		[ObservableProperty]
		int playerNum;

		[ObservableProperty]
		string connectionIdString;

		[ObservableProperty]
		bool yourTurn = true;

		[ObservableProperty]
		bool opponentsTurn = false;

		[RelayCommand]
		async Task GoBack()
		{
			// Go back to last shell
			await Shell.Current.GoToAsync("..");
		}

		public GamePageViewModel() 
		{
			
		}

		public void SetPlayerNum()
		{
			if (ConnectionIdString.Equals(Player1.Id))
			{
				PlayerNum = 1;
			} 
			else
			{
				PlayerNum = 2;
			}

			Debug.WriteLine("PN: "+ PlayerNum);
		}

		public void GetTurn()
		{
			HubConnectionHandlerSingleton.Instance.Invoke("GetTurn", new object[] { });
		}

		public void SetTurn(bool player1Turn)
		{
			if (player1Turn)
			{
				YourTurn = PlayerNum == 1;
				OpponentsTurn = !YourTurn;
			} else
			{
				YourTurn = PlayerNum == 2;
				OpponentsTurn = !YourTurn;
			}

			Debug.WriteLine("YourTurn: " + YourTurn);
		}

		public void SetupGameSpace(IDispatcher dispatcher, Grid grid, Board board)
		{
			dispatcher.Dispatch(() =>
			{
				grid.Children.Clear();
				foreach (var item in board.Fields)
				{
					PlayField playField = new PlayField(item);
					grid.Children.Add(playField);
				}
			});
		}

		public async void GoToScoreboard()
		{
			// Go back to last shell, then to scoreboard shell
			await Shell.Current.GoToAsync("../Scoreboard");
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
