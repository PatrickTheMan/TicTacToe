namespace TicTacToe.Domain
{
    public class Field
    {
        public Field(string contents, int x, int y)
        {
            Contents = contents;
            X = x;
            Y = y;
        }

        public string Contents { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
