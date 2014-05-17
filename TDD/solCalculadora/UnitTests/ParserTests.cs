using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;
using Rhino.Mocks;

namespace UnitTests
{
    [TestFixture]
    public class ParserTests
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
          
            _lexer = new MathLexer(new ExpressionFixer());
            _calculator = new Calculator();
            _calcProxy = new CalcProxy(new Validator(-100, 100), _calculator);
            _precedence = new Precedence();
            _resolver = new Resolver(_lexer, _calcProxy,_precedence);
            _parser = new MathParser(_lexer,_resolver);
            
        }

        [Test]
        public void ProcessSimpleExpression()
        {
            Assert.AreEqual(4, _parser.ProcessExpression("2 + 2"));
        }

        [Test]
        public void ProcessExpression2Operators()
        {
            Assert.AreEqual(6,_parser.ProcessExpression("3 + 1 + 2"));
        }

        [Test]
        public void ProcessAcceptanceExpression()
        {
            Assert.AreEqual(9,_parser.ProcessExpression("5 + 4 * 2 / 2"));
        }

        [Test]
        public void ProcessAcceptanceExpressionWithAllOperators()
        { 
            Assert.AreEqual(8,_parser.ProcessExpression("5 + 4 - 1 * 2 / 2"));
        }

        
        [Test]
        public void ProcessAcceptanceExpressionWithParenthesis()
        {
            Assert.AreEqual(16,_parser.ProcessExpression("(2 + 2) * (3 + 1)") );
        }

    } 
}
