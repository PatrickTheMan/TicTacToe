using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using TicTacToe.Server.Domain;

namespace TicTacToe.Server
{

    public static class UserHandler
	{
		public static List<string> ConnectedIds = new List<string>();

		public static Dictionary<string, int> Scoreboard = new Dictionary<string, int>()
		{
			{ "Patrick", 2 },
			{ "Allan", 1 },
			{ "Jette", 5 },
		};

		public static Player Player1 = new Player();
		public static Player Player2 = new Player();

		public static Board Board = new Board();

		public static bool player1Turn = true;
	}

	public class GameHub : Hub
	{

		#region On Connect/Disconnect

		public override Task OnConnectedAsync()
		{
			UserHandler.ConnectedIds.Add(Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			UserHandler.ConnectedIds.Remove(Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}

		#endregion

		public async Task SendMessage(string message)
		{
			Console.WriteLine(message);
			await Clients.All.SendAsync("MessageReceived", message);
		}

		public async Task GetId()
		{
			Console.WriteLine("GetId");
			await Clients.Caller.SendAsync("IdReceived", Context.ConnectionId);
		}

		public async Task GetPlayerIds()
		{
			Console.WriteLine("GetPlayerIds");

			await Clients.All.SendAsync("PlayerIdsReceived", UserHandler.ConnectedIds);
		}

		public async Task GetPlayers()
		{
			Console.WriteLine("GetPlayers");

			List<Player> players = new List<Player>()
			{
				UserHandler.Player1,
				UserHandler.Player2
			};

			await Clients.All.SendAsync("PlayersReceived", players);
		}

		public async Task GetBoard()
		{
			Console.WriteLine("GetBoard");
			await Clients.Caller.SendAsync("BoardReceived", UserHandler.Board);
		}

		public async Task GetScoreboard()
		{
			Console.WriteLine("GetScoreboard");
			await Clients.Caller.SendAsync("ScoreboardReceived", UserHandler.Scoreboard);
		}

		public async Task GetTurn()
		{
			Console.WriteLine("GetTurn");
			await Clients.Caller.SendAsync("TurnReceived", UserHandler.player1Turn);
		}

		public async Task SetPlayer(string id, string name)
		{
			Console.WriteLine("SetPlayer");

			if (id.Equals(UserHandler.Player1.Id) && UserHandler.ConnectedIds.Contains(UserHandler.Player1.Id) || id.Equals(UserHandler.Player2.Id) && UserHandler.ConnectedIds.Contains(UserHandler.Player2.Id))
			{
				Console.WriteLine("Already a player");
				return;
			}

			if (UserHandler.Player1.Id == null || !UserHandler.ConnectedIds.Contains(UserHandler.Player1.Id))
			{
				UserHandler.Player1 = new Player() { Id = id, Name = name, type = "X" };
			}
			else if (UserHandler.Player2.Id == null || !UserHandler.ConnectedIds.Contains(UserHandler.Player2.Id))
			{
				UserHandler.Player2 = new Player() { Id = id, Name = name, type = "O" };
			}

			if (UserHandler.Player1 != null && UserHandler.Player1.Name != null && UserHandler.Player1.Id != null)
			{
				Console.WriteLine($"\n\nPlayer1:\nPlayerName: {UserHandler.Player1.Name}\nPlayerId: {UserHandler.Player1.Id}\n\n");
			}
			if (UserHandler.Player2 != null && UserHandler.Player2.Name != null && UserHandler.Player2.Id != null)
			{
				Console.WriteLine($"\n\nPlayer2:\nPlayerName: {UserHandler.Player2.Name}\nPlayerId: {UserHandler.Player2.Id}\n\n");
			}
		}

		public async void SetMarker(Field field)
		{
			foreach (var item in UserHandler.Board.Fields)
			{
				if (item.X == field.X && item.Y == field.Y)
				{
					if (UserHandler.player1Turn)
					{
						item.Contents = UserHandler.Player1.type;
						UserHandler.player1Turn = false;
					} 
					else 
					{
						item.Contents = UserHandler.Player2.type;
						UserHandler.player1Turn = true;
					}
					break;
				}
			}

			await Clients.All.SendAsync("MarkerSet", UserHandler.Board);
		}

		public async Task AddWin(string playerName)
		{
			Console.WriteLine("AddWin");

			if (UserHandler.Scoreboard.ContainsKey(playerName))
			{
				int currentWins = UserHandler.Scoreboard[playerName];
				UserHandler.Scoreboard.Remove(playerName);
				UserHandler.Scoreboard.Add(playerName, currentWins + 1);
			} 
			else
			{
				UserHandler.Scoreboard.Add(playerName, 1);
			}

			foreach (var kvp in UserHandler.Scoreboard)
			{
				Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
			}

			await Clients.Caller.SendAsync("WinAdded", new object());
		}

	}
}
