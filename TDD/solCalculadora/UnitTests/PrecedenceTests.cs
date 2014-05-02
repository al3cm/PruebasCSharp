using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;

namespace UnitTests
{
    [TestFixture]
    public class PrecedenceTests
    {
        private MathParser _parser;
        private MathLexer _lexer;
        private MathRegex _regex;
        private CalculatorProxy _calcProxy;
        private Calculator _calculator;

        [SetUp]
        public void SetUp()
        {

            _regex = new MathRegex();
            _lexer = new MathLexer(_regex, new ExpressionFixer(_regex));
            _calculator = new Calculator();
            _calcProxy = new CalcProxy(new Validator(-100, 100), _calculator);
            _parser = new MathParser(_lexer, _calcProxy);

        }

        [Test]
        public void ProcessExpressionWithPrecedence()
        {
            Assert.AreEqual(9,_parser.ProcessExpression("3 + 3 * 2"));
        }

        [Test]
        public void GetMaxPrecedence()
        {
            List<MathToken> tokens = _lexer.GetTokens("3 + 3 * 2");
            MathOperator op = _parser.GetMaxPrecedence(tokens);
            Assert.AreEqual(op.Token,"*");
        }

    }

}
