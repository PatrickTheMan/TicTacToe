namespace TicTacToe.Server.Domain
{
    public class Board
    {

        public List<Field> Fields { get; set; }

        public Board() 
        {
            Fields = new List<Field>(){
				new Field("", 1, 0),
				new Field("", 2, 0),
				new Field("", 3, 0),
				new Field("", 1, 1),
				new Field("", 2, 1),
				new Field("", 3, 1),
				new Field("", 1, 2),
				new Field("", 2, 2),
				new Field("", 3, 2),
			};
        }

    }
}
