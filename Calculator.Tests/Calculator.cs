using NUnit.Framework;
using Calculator.Utilities;
using Calculator.Services;
using System.Collections.Generic;
using Calculator.Models;

namespace Calculate.Services.Tests
{
    public class Calculator_Tests
    {
        private readonly CalculateService _calculateService;
        private readonly ValidationService _validationService;
        private readonly Parser _parser;

        public Calculator_Tests()
        {
            _parser = new Parser();
            _calculateService = new CalculateService();
            _validationService = new ValidationService();
        }

        [Test]
        public void Calculate_Simple_Return_Correct_Value()
        {
            // "1 + 2 + 3"
            LinkedList<Token> testLinkedList = new LinkedList<Token>();
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 1, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 0, Operation = Operation.Add });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 2, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 0, Operation = Operation.Add });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 3, Operation = Operation.Number });
            double result = _calculateService.Calculate(testLinkedList, 0);
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Calculate_Bracket_Return_Correct_Value()
        {
            // "23 - ( 29.3 - 12.5 )"
            LinkedList<Token> testLinkedList = new LinkedList<Token>();
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 23, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 0, Operation = Operation.Subtract });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 29.3, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 0, Operation = Operation.Subtract });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 12.5, Operation = Operation.Number });
            double result = _calculateService.Calculate(testLinkedList, 1);
            Assert.AreEqual(6.2, result);
        }

        [Test]
        public void Calculate_Nested_Bracket_Return_Correct_Value()
        {
            // "1.1 + ( 2 + 3 * ( 7 - 5 ) - 1 ) / 2"
            LinkedList<Token> testLinkedList = new LinkedList<Token>();
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 1.1, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 0, Operation = Operation.Add });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 2, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 0, Operation = Operation.Add });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 3, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 0, Operation = Operation.Multiply });
            testLinkedList.AddLast(new Token() { Depth = 2, Number = 7, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 2, Number = 0, Operation = Operation.Subtract });
            testLinkedList.AddLast(new Token() { Depth = 2, Number = 5, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 0, Operation = Operation.Subtract });
            testLinkedList.AddLast(new Token() { Depth = 1, Number = 1, Operation = Operation.Number });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 0, Operation = Operation.Divide });
            testLinkedList.AddLast(new Token() { Depth = 0, Number = 2, Operation = Operation.Number });
            double result = _calculateService.Calculate(testLinkedList, 2);
            Assert.AreEqual(4.6, result);
        }

        [Test]
        public void Calculate_Input_Validation_Negative()
        {
            string result = _validationService.ValidateExpression("A + B + C");
            Assert.AreEqual("Validation Error: The math expressions consist of unsupported characters.", result);
        }

        [Test]
        public void Calculate_Input_Validation_Positive()
        {
            string result = _validationService.ValidateExpression("1.1 + ( 2 + 3 * ( 7 - 5 ) - 1 ) / 2");
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void Expression_Parser_Count()
        {
            LinkedList<Token> testLinkedList = _parser.ExpressionParser("1 + 2 + 3", out int maxDepth);
            Assert.AreEqual(5, testLinkedList.Count);
        }

        [Test]
        public void Expression_Parser_MaxDepth()
        {
            LinkedList<Token> testLinkedList = _parser.ExpressionParser("1 + ( ( 2 + 3 ) + 4 ) + 5", out int maxDepth);

            Assert.AreEqual(2, maxDepth);
        }
    }
}