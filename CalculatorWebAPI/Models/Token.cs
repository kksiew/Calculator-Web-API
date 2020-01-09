namespace Calculator.Models
{
    public class Token
    {
        public double Number { get; set; }
        public Operation Operation { get; set; }
        public int Depth { get; set; }
    }

    public enum Operation
    {
        None,
        Number,
        Add,
        Subtract,
        Multiply,
        Divide
    }
}
