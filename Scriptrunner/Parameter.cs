namespace Scriptrunner
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Datatype { get; set; }
        public string Value { get; set; }

        public Parameter(string name, string type)
        {
            Name = name;
            Datatype = type;
        }
    }
}