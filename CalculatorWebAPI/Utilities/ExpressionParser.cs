using Calculator.Models;
using Calculator.Interfaces;
using System.Collections.Generic;

namespace Calculator.Utilities
{
    public class Parser : IParser
    {
        /// <summary>
        /// Convert the raw user input string to tokens
        /// </summary>
        /// <param name="inputExpression">User input of math expression</param>
        /// <param name="maxDepth">Maximum depth level of brackets</param>
        /// <returns>Tokens in linked list</returns>
        public LinkedList<Token> ExpressionParser(string inputExpression, out int maxDepth)
        {
            LinkedList<Token> tokenList = new LinkedList<Token>();
            Operation parseOperation = Operation.None;
            int parseDepth = 0;
            maxDepth = 0;

            foreach (string element in inputExpression.Split(" "))
            {
                bool addToken = false;
                double parseNumber;

                // Process numbers here
                if (double.TryParse(element, out double outNumber))
                {
                    parseNumber = outNumber;
                    parseOperation = Operation.Number;
                    addToken = true;
                }
                // Process math operators here
                else
                {
                    parseNumber = 0.0;

                    switch (element)
                    {
                        case "+":
                            parseOperation = Operation.Add;
                            addToken = true;
                            break;
                        case "-":
                            parseOperation = Operation.Subtract;
                            addToken = true;
                            break;
                        case "*":
                            parseOperation = Operation.Multiply;
                            addToken = true;
                            break;
                        case "/":
                            parseOperation = Operation.Divide;
                            addToken = true;
                            break;
                        case "(":
                            // Increase depth by 1 for opening bracket
                            parseDepth++;
                            if (parseDepth > maxDepth)
                                maxDepth = parseDepth;
                            break;
                        case ")":
                            // Decrease depth by 1 for closing bracket
                            parseDepth--;
                            break;
                    }
                }

                if (addToken)
                {
                    Token token = new Token()
                    {
                        Number = parseNumber,
                        Operation = parseOperation,
                        Depth = parseDepth
                    };
                    tokenList.AddLast(token);
                }
            }

            return tokenList;
        }
    }
}
