namespace ParserDemo.Tests.Commands
{
    public class MoveDown : Command
    {
        public int Distance { get; private set; }

        public MoveDown(int distance)
        {
            this.Distance = distance;
        }
    }
}
