namespace ParserDemo.Tests.Commands
{
    public class MoveUp : Command
    {
        public int Distance { get; private set; }

        public MoveUp(int distance)
        {
            this.Distance = distance;
        }
    }
}
