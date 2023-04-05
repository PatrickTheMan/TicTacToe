using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain;
using TicTacToe.EventHandler;

namespace TicTacToe.Handlers
{
    public class HubConnectionHandler
    {
        public HubConnection _connection { get; set; }

        public HubConnectionHandler()
        {
            // Create HubConnection
            _connection = new HubConnectionBuilder()
                .WithUrl("http://172.23.0.1:5218/ttt", (opts) =>
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            // always verify the SSL certificate
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    }
                )
                .Build();
        }

        public void AddHappening(string serverTask, Action<string> action)
        {
            // Setup happening
            _connection.On(serverTask, action);
        }

		public void AddHappening(string serverTask, Action<bool> action)
		{
			// Setup happening
			_connection.On(serverTask, action);
		}

		public void AddHappening(string serverTask, Action<List<string>> action)
		{
			// Setup happening
			_connection.On(serverTask, action);
		}

		public void AddHappening(string serverTask, Action<List<Player>> action)
        {
            // Setup happening
            _connection.On(serverTask, action);
        }

		public void AddHappening(string serverTask, Action<Board> action)
		{
			// Setup happening
			_connection.On(serverTask, action);
		}

		public void AddHappening(string serverTask, Action<Dictionary<string,int>> action)
		{
			// Setup happening
			_connection.On(serverTask, action);
		}

		public void AddHappening(string serverTask, Action action)
        {
            // Setup happening
            _connection.On(serverTask, action);
        }

        public void RemoveHappening(string methodName)
        {
            _connection.Remove(methodName);
        }

        public async void Invoke(string methodName, object[] args)
        {
            await _connection.InvokeCoreAsync(methodName, args);
        }

        #region Connect

        public event EventHandler<ConnectionStatusEventArgs> IsConnected;

        private bool connectionStatus = false;
        public bool ConnectionStatus { get; }

        public void Connect(IDispatcher dispatcher)
        {
            // Connect to hub
            Task.Run(() =>
            {
                if (_connection.ConnectionId == null)
                {
                    dispatcher.Dispatch(async () =>
                        await _connection.StartAsync().ContinueWith
                        (
                            (t) =>
                            {
                                connectionStatus = _connection.ConnectionId != null;
                                IsConnected.Invoke(this, new ConnectionStatusEventArgs(connectionStatus));
                                Debug.WriteLine("Connection Status: " + connectionStatus);
                            }
                        )
                    );
                }
            });
        }

        #endregion

    }
}
