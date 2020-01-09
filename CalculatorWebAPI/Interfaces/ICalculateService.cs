using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.Interfaces
{
    public interface ICalculateService
    {
        public double Calculate(LinkedList<Token> tokenList, int maxDepth);
    }
}
