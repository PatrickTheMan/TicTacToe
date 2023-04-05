using TicTacToe.View;
using TicTacToe.ViewModel;

namespace TicTacToe;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddTransient<GamePage>();
		builder.Services.AddTransient<GamePageViewModel>();

		builder.Services.AddSingleton<ScoreboardPage>();
		builder.Services.AddSingleton<ScoreboardPageViewModel>();


		// Creates a single instance of the page easy use of the same instance
		//builder.Services.AddSingleton<>();

		// Creates a new page each time it is switched to
		//builder.Services.AddTransient<>();


		return builder.Build();
	}
}
