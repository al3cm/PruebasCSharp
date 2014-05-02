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
        private MathRegex _regex;
        private CalculatorProxy _calcProxy;
        private Calculator _calculator;

        [SetUp]
        public void SetUp() 
        {
          
            _regex = new MathRegex();
            _lexer = new MathLexer(_regex,new ExpressionFixer(_regex));
            _calculator = new Calculator();
            _calcProxy = new CalcProxy(new Validator(-100, 100), _calculator);
            _parser = new MathParser(_lexer,_calcProxy);
            
        }

        [Test]
        public void ProcessSimpleExpression()
        {
            Assert.AreEqual(4, _parser.ProcessExpression("2 + 2"));
        }

        [Test]
        public void ParserWorksWithCalcProxy()
        {
            CalculatorProxy calcProxyMock = MockRepository.GenerateMock<CalculatorProxy>();
            calcProxyMock.Expect(x => x.Calculator).Return(_calculator);
            calcProxyMock.Expect(x => x.BinaryOperation(_calculator.Add,2,2)).Return(4);

            MathParser parser = new MathParser(_lexer,calcProxyMock);
            parser.ProcessExpression("2 + 2");

            calcProxyMock.VerifyAllExpectations();
        }

        [Test]
        public void ParserWorksWithLexer()
        {
            List<MathToken> tokens = new List<MathToken>();

            tokens.Add(new MathToken("2"));
            tokens.Add(new MathToken("+"));
            tokens.Add(new MathToken("2"));
            Lexer lexerMock = MockRepository.GenerateStrictMock<Lexer>();
            lexerMock.Expect(x => x.GetTokens("2 + 2")).Return(tokens);

            MathParser parser = new MathParser(lexerMock,new CalcProxy(new Validator(-100,100),new Calculator()));
            parser.ProcessExpression("2 + 2");
            lexerMock.VerifyAllExpectations();
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



    } 
}
