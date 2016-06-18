namespace ParserDemo.Tests
{
    public class Declaration : Command
    {
        public string VariableName { get; private set; }
        public string VariableType { get; private set; }
        public string InitialValue { get; private set; }

        public Declaration(string variableName, string variableType, string initialValue)
        {
            this.VariableName = variableName;
            this.VariableType = variableType;
            this.InitialValue = initialValue;
        }
    }
}
