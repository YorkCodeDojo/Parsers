namespace ParserDemo.Tests.Commands
{
    public class MoveRight : Command
    {
        public int Distance { get; private set; }

        public MoveRight(int distance)
        {
            this.Distance = distance;
        }
    }
}
