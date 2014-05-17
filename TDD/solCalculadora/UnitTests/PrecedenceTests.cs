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
        private CalculatorProxy _calcProxy;
        private Calculator _calculator;
        private Resolver _resolver;
        private Precedence _precedence;

        [SetUp]
        public void SetUp()
        {

            _lexer = new MathLexer( new ExpressionFixer());
            _calculator = new Calculator();
            _calcProxy = new CalcProxy(new Validator(-100, 100), _calculator);
            _precedence = new Precedence();
            _resolver = new Resolver(_lexer, _calcProxy,_precedence);
            _parser = new MathParser( _lexer ,_resolver);

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
            MathToken token = _precedence.GetMaxPrecedence(tokens);
            MathOperator op = OperatorFactory.Create(token.Token);
            Assert.AreEqual(op.Token,"*");
        }

    }

}
