using TicTacToe.View;

namespace TicTacToe;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Setup the routing to the GamePage
		Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));

		// Setup the routing to the ScoreboardPage
		Routing.RegisterRoute(nameof(ScoreboardPage), typeof(ScoreboardPage));
	}
}
