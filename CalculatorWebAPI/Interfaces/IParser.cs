using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface IParser
    {
        public LinkedList<Token> ExpressionParser(string inputExpression, out int maxDepth);
    }
}
