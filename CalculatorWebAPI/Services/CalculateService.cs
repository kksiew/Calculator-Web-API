using Calculator.Interfaces;
using Calculator.Models;
using Calculator.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Calculator.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IConfiguration _configuration;

        public CalculateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Main calculation engine
        /// </summary>
        /// <param name="tokenList">Tokens in linked list</param>
        /// <param name="maxDepth">Maximum depth level of brackets</param>
        /// <returns>Sum of the math expression</returns>
        public double Calculate(LinkedList<Token> tokenList, int maxDepth)
        {
            double result = 0.0;

            // Process based on depth level
            for (int iDepth = maxDepth; iDepth >= 0; iDepth--)
            {
                LinkedListNode<Token> currentNode = tokenList.First;

                // Process multiplication and division first
                while (currentNode != null)
                {
                    LinkedListNode<Token> nextNode = currentNode.Next;

                    if (currentNode.Value.Depth == iDepth)
                    {
                        switch (currentNode.Value.Operation)
                        {
                            case Operation.Multiply:
                                currentNode.Next.Value.Number = currentNode.Previous.Value.Number * currentNode.Next.Value.Number;
                                tokenList.Remove(currentNode.Previous);
                                tokenList.Remove(currentNode);
                                break;
                            case Operation.Divide:
                                currentNode.Next.Value.Number = currentNode.Previous.Value.Number / currentNode.Next.Value.Number;
                                tokenList.Remove(currentNode.Previous);
                                tokenList.Remove(currentNode);
                                break;
                        }
                    }
                    currentNode = nextNode;
                }

                // Go back to first node
                currentNode = tokenList.First;

                // Process add and subtract
                while (currentNode != null)
                {
                    LinkedListNode<Token> nextNode = currentNode.Next;

                    if (currentNode.Value.Depth == iDepth)
                    {
                        switch (currentNode.Value.Operation)
                        {
                            case Operation.Add:
                                currentNode.Next.Value.Number = currentNode.Previous.Value.Number + currentNode.Next.Value.Number;
                                tokenList.Remove(currentNode.Previous);
                                tokenList.Remove(currentNode);
                                break;
                            case Operation.Subtract:
                                currentNode.Next.Value.Number = currentNode.Previous.Value.Number - currentNode.Next.Value.Number;
                                tokenList.Remove(currentNode.Previous);
                                tokenList.Remove(currentNode);
                                break;
                        }
                    }
                    currentNode = nextNode;
                }
            }

            if (tokenList.First.Value != null)
            {
                int roundToDecimal = int.Parse(_configuration.GetSection("RoundToDecimal").Value);
                result = Math.Round(tokenList.First.Value.Number, roundToDecimal);
            }

            return result;
        }
    }
}
