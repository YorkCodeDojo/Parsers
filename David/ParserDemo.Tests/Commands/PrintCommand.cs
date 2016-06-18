namespace ParserDemo.Tests
{
    public class PrintCommand : Command
    {
        public string ThingToPrint { get; private set; }
        public PrintCommand(string thingToPrint)
        {
            this.ThingToPrint = thingToPrint;
        }
    }
}
