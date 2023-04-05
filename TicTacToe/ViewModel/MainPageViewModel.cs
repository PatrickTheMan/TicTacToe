using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TicTacToe.EventHandler;
using TicTacToe.Singleton;
using TicTacToe.View;

namespace TicTacToe.ViewModel
{
	public partial class MainPageViewModel : ObservableObject, IDisposable
	{

		[ObservableProperty]
		string startBtnString = "Connect First";

		[ObservableProperty]
		string connectBtnString = "Connect";

		[ObservableProperty]
		string connectionIdString = "";

		[ObservableProperty]
		string playerNameString = "Name";

		[RelayCommand]
		void Connect()
		{
			Debug.WriteLine("Connect");

			HubConnectionHandlerSingleton.Instance.Connect(Dispatcher.GetForCurrentThread());
		}

		[RelayCommand]
		async void Start(string connectionId)
		{
			Debug.WriteLine("Start");

			HubConnectionHandlerSingleton.Instance.Invoke("SetPlayer", args: new[] { connectionId, playerNameString });

			await Shell.Current.GoToAsync($"{nameof(GamePage)}?ConnectionPlayerId={connectionId}");
		}

		[RelayCommand]
		async void Scoreboard()
		{
			Debug.WriteLine("Scoreboard");

			await Shell.Current.GoToAsync(nameof(ScoreboardPage));
		}

		[RelayCommand]
		void Exit()
		{
			Debug.WriteLine("Exit");

			Application.Current.Quit();
		}

		public MainPageViewModel()
		{
			// Subscribe to IsConnected
			HubConnectionHandlerSingleton.Instance.IsConnected += connectionButtonStringUpdate;
			HubConnectionHandlerSingleton.Instance.IsConnected += connectionIdStringUpdate;
		}

		void connectionButtonStringUpdate(object sender, ConnectionStatusEventArgs ea)
		{
			if (ea.ConnectionStatus)
			{
				StartBtnString = "Start";
				ConnectBtnString = "Connected";
			}
			else
			{
				StartBtnString = "Connect First";
				ConnectBtnString = "Connect";
			}
		}

		void connectionIdStringUpdate(object sender, ConnectionStatusEventArgs ea)
		{
			if (ea.ConnectionStatus)
			{
				HubConnectionHandlerSingleton.Instance.Invoke("GetId", new object[] { });
				HubConnectionHandlerSingleton.Instance.Invoke("GetPlayerIds", new object[] { });
			}
		}

		public void Dispose()
		{
			// UnSubscribe to IsConnected
			HubConnectionHandlerSingleton.Instance.IsConnected -= connectionButtonStringUpdate;
			HubConnectionHandlerSingleton.Instance.IsConnected -= connectionIdStringUpdate;
		}

	}
}
