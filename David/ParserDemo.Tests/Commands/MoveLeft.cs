namespace ParserDemo.Tests.Commands
{
    public class MoveLeft : Command
    {
        public int Distance { get; private set; }

        public MoveLeft(int distance)
        {
            this.Distance = distance;
        }
    }
}
