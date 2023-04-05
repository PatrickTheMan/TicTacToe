using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Singleton;

namespace TicTacToe.Domain
{
	public class PlayField : Button
	{
		public Field field;
		public PlayField(Field field)
		{
			this.field = field;

			Grid.SetColumn(this, field.X);
			Grid.SetRow(this, field.Y);

			this.Clicked += ClickField;

			this.BackgroundColor = Color.Parse("White");
			this.BorderColor = Color.Parse("Black");

			this.FontSize = 64;

			this.Text = field.Contents;
		}

		void ClickField(object sender, EventArgs ea)
		{
			if (this.Text == null || this.Text.Equals(string.Empty))
			{
				HubConnectionHandlerSingleton.Instance.Invoke("SetMarker", args: new[] { field });
			}
		}

	}
}
