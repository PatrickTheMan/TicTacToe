using System.Diagnostics;
using TicTacToe.Domain;
using TicTacToe.Handlers;
using TicTacToe.Singleton;
using TicTacToe.ViewModel;

namespace TicTacToe.View;

public partial class GamePage : ContentPage
{
	public GamePage(GamePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;


		HubConnectionHandlerSingleton.Instance.AddHappening("IdReceived",
				(string message) =>
				{
					Dispatcher.Dispatch(() => {
						vm.ConnectionIdString = message;
						vm.SetPlayerNum();

						if (vm.PlayerNum != 1)
						{
							Player player1 = vm.Player1;
							Player player2 = vm.Player2;

							vm.Player1 = player2;
							vm.Player2 = player1;
						}

						Debug.WriteLine("myID: " + message);
					});
				}
			);
		HubConnectionHandlerSingleton.Instance.AddHappening("PlayersReceived",
				(List<Player> players) =>
				{
					vm.Player1 = players[0];
					vm.Player2 = players[1];
				}
			);
		HubConnectionHandlerSingleton.Instance.AddHappening("BoardReceived",
				(Board board) =>
				{
					vm.SetupGameSpace(Dispatcher, gameBoardSpace, board);
					vm.GetTurn();
				}
			);
		HubConnectionHandlerSingleton.Instance.AddHappening("TurnReceived",
				(bool player1Turn) =>
				{
					Dispatcher.Dispatch(() => {
						vm.SetTurn(player1Turn);
					Debug.WriteLine("Player1Turn: " + player1Turn);
					});
				}
			);
		HubConnectionHandlerSingleton.Instance.AddHappening("MarkerSet",
				(Board board) =>
				{
					vm.SetupGameSpace(Dispatcher, gameBoardSpace, board);
					vm.GetTurn();
				}
			);

		HubConnectionHandlerSingleton.Instance.AddHappening("WinAdded",
				() =>
				{
					vm.GoToScoreboard();
				}
			);

		// Invoke
		HubConnectionHandlerSingleton.Instance.Invoke("GetPlayers", new object[] { });
		HubConnectionHandlerSingleton.Instance.Invoke("GetBoard", new object[] { });
		HubConnectionHandlerSingleton.Instance.Invoke("GetId", new object[] { });
	}
}