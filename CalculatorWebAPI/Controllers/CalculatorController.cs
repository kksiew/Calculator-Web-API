using System.Collections.Generic;
using Calculator.Interfaces;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculateService _calculateService;
        private readonly IValidationService _validationService;
        private readonly IParser _parser;

        public CalculatorController(ICalculateService calculateService,
                                    IValidationService validationService,
                                    IParser parser)
        {
            _calculateService = calculateService;
            _validationService = validationService;
            _parser = parser;
        }

        [HttpPost]
        public IActionResult Calculate([FromBody]Input input)
        {
            Result response = new Result();

            // Validate the user input
            response.error = _validationService.ValidateExpression(input.sum) ;

            if (string.IsNullOrEmpty(response.error))
            {
                // Parse the math expression from raw string
                LinkedList<Token> tokenList = _parser.ExpressionParser(input.sum, out int maxDepth);

                // Do calculation and return result
                response.result = string.Empty;

                if (tokenList.Count > 0)
                    response.result = _calculateService.Calculate(tokenList, maxDepth).ToString();

                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
