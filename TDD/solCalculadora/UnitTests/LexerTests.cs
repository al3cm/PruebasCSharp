using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;

namespace UnitTests
{
    [TestFixture]
    public class LexerTests
    {
        private MathLexer _lexer;
        private MathRegex _regex;

        [SetUp]
        public void SetUp()
        {
                        _regex = new MathRegex();
            _lexer = new MathLexer(_regex, new ExpressionFixer(_regex));
            
        }

        [Test]
        public void GetTokens()
        {

            List<MathToken> tokens = _lexer.GetTokens("2 + 2");

            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual("2", tokens[0].Token);
            Assert.AreEqual("+", tokens[1].Token);
            Assert.AreEqual("2", tokens[2].Token);
        }

        [Test]
        public void GetTokensLongExpression()
        {
         
            List<MathToken> tokens = _lexer.GetTokens("2 - 1 + 3");

            Assert.AreEqual(5, tokens.Count);
            Assert.AreEqual("+", tokens[3].Token);
            Assert.AreEqual("3", tokens[4].Token);
        }

       [Test]
        public void GetTokensWrongExpression()
        {
            try
            {
                List<MathToken> tokens = _lexer.GetTokens("2 - 1++ 3");
                Assert.Fail("Exception did not arise!");
            }
            catch (InvalidOperationException)
            {
               
            }
        }

        [Test]
        public void GetTokensWithSpaces()
        {
            List<MathToken> tokens = _lexer.GetTokens("5 -   88");
            Assert.AreEqual("5",tokens[0].Token);
            Assert.AreEqual("-", tokens[1].Token);
            Assert.AreEqual("88", tokens[2].Token);
        }
      [Test]
        public void GetExpressionsWith1Parentesis()
        {
            List<string> expressions = _lexer.GetExpressions("(2 + 2)");
            Assert.AreEqual(1,expressions.Count);
            Assert.AreEqual("2 + 2",expressions[0]);
        }

        [Test]
        public void GetExpressionsWithNestedParentesis()
        {
            List<string> expressions = _lexer.GetExpressions("((2) + 2)");
            failIfOtherSubExpressionThan(expressions,"2","+");
        }

        [Test]
        public void GetNestedExpressions()
        {
          List<string> expressions = _lexer.GetExpressions("((2 + 1) + 2)");
            Assert.AreEqual(3, expressions.Count);
            failIfOtherSubExpressionThan(expressions,"2 + 1","+","2");
        }

        private void failIfOtherSubExpressionThan(List<string> expressions, params string[] expectedSubExpressions)
        {
            bool isSubExpression = false;
            foreach (string subExpression in expectedSubExpressions)
            {
                isSubExpression = false;
                foreach (string exp in expressions)
                {
                    if (exp == subExpression)
                    {
                        isSubExpression = true;
                        break;
                    }
                }
                if (!isSubExpression)
                    Assert.Fail("Wrong expression split: " + subExpression);
            }
        }

        [Test]
        public void GetExpressionWithParenthesisAtTheEnd()
        {
            List<string> expression = _lexer.GetExpressions("2 + (3 * 1)");
            failIfOtherSubExpressionThan(expression,"3 * 1","+","2");
        }

        [Test]
        public void ThrowExceptionOnOpenParenthesis()
        {
            try
            {
                List<string> expressions = _lexer.GetExpressions("(2 + 3 * 1");
                Assert.Fail("Exception didn't arise!");
            }
            catch (InvalidOperationException)
            {
                
            }
        }

    }
}
