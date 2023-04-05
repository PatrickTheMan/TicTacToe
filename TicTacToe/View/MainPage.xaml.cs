using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Data;
using System.Diagnostics;
using TicTacToe.EventHandler;
using TicTacToe.Singleton;
using TicTacToe.ViewModel;

namespace TicTacToe;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		HubConnectionHandlerSingleton.Instance.IsConnected += startButtonEnable;
		HubConnectionHandlerSingleton.Instance.IsConnected += scoreboardButtonEnable;
		HubConnectionHandlerSingleton.Instance.IsConnected += nameEntryEnable;
		HubConnectionHandlerSingleton.Instance.IsConnected += connectButtonDisable;

		


		HubConnectionHandlerSingleton.Instance.AddHappening("IdReceived",
				(string message) =>
				{
					Dispatcher.Dispatch(() => {
						connectionLabel.Text = message;
						vm.ConnectionIdString = message;
						Debug.WriteLine("myID: " + message);
					});
				}
			);

		HubConnectionHandlerSingleton.Instance.AddHappening("PlayerIdsReceived",
				(List<string> strings) => 
				{
					Dispatcher.Dispatch(() => {

						string completeString = strings[0];
						foreach (string s in strings.Skip(1))
						{
							completeString += ", " + s;
						}
						connectionsLabel.Text = completeString;
						Debug.WriteLine("Connections: " + completeString);
					});
				}
			);
	}

	void nameEntryEnable(object sender, ConnectionStatusEventArgs ea)
	{
		if (ea.ConnectionStatus)
		{
			Dispatcher.Dispatch(() => nameEntry.IsVisible = true);
			HubConnectionHandlerSingleton.Instance.IsConnected -= nameEntryEnable;
		}
	}

	void startButtonEnable(object sender, ConnectionStatusEventArgs ea)
	{
		if (ea.ConnectionStatus)
		{
			Dispatcher.Dispatch(() => startBtn.IsVisible = true);
			HubConnectionHandlerSingleton.Instance.IsConnected -= startButtonEnable;
		}
	}

	void scoreboardButtonEnable(object sender, ConnectionStatusEventArgs ea)
	{
		if (ea.ConnectionStatus)
		{
			Dispatcher.Dispatch(() => scoreboardBtn.IsVisible = true);
			HubConnectionHandlerSingleton.Instance.IsConnected -= scoreboardButtonEnable;
		}
	}

	void connectButtonDisable(object sender, ConnectionStatusEventArgs ea)
	{
		if (ea.ConnectionStatus)
		{
			Dispatcher.Dispatch(() => connectBtn.IsEnabled = false);
			HubConnectionHandlerSingleton.Instance.IsConnected -= connectButtonDisable;
		}
	}

}

