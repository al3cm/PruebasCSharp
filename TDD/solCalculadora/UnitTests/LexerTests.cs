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
      
        [SetUp]
        public void SetUp()
        {
            _lexer = new MathLexer(new ExpressionFixer());
            
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
            List<MathExpression> expressions = _lexer.GetExpressions("(2 + 2)");
            Assert.AreEqual(1, expressions.Count);
            Assert.AreEqual("2 + 2", expressions[0].Expression);
        }

        [Test]
        public void GetExpressionsWithNestedParentesis()
        {
            List<MathExpression> expressions = _lexer.GetExpressions("((2) + 2)");
            failIfOtherSubExpressionThan(expressions, "2", "+");    
        }

        [Test]
        public void GetNestedExpressions()
        {
            List<MathExpression> expressions = _lexer.GetExpressions("((2 + 1) + 2)");
            Assert.AreEqual(3, expressions.Count);
            failIfOtherSubExpressionThan(expressions, "2 + 1", "+", "2");
        }

        private void failIfOtherSubExpressionThan(List<MathExpression> expressions,params string[] expectedSubExpressions)
        {
            bool isSubExpression = false;
            foreach (string subExpression in expectedSubExpressions)
            {
                isSubExpression = false;
                foreach (MathExpression exp in expressions)
                    if (exp.Expression == subExpression)
                    {
                        isSubExpression = true;
                        break;
                    }
                if (!isSubExpression)
                    Assert.Fail("Wrong expression split:" + subExpression);
            }
        }


        [Test]
        public void GetExpressionWithParenthesisAtTheEnd()
        {
            List<MathExpression> expressions = _lexer.GetExpressions("2 + (3 * 1)");
            failIfOtherSubExpressionThan(expressions, "3 * 1", "+", "2");
        }

        [Test]
        public void ThrowExceptionOnOpenParenthesis()
        {
            try
            {
                List<MathExpression> expressions =_lexer.GetExpressions("(2 + 3 * 1");
                Assert.Fail("Exception didn't arise!");
            }
            catch (InvalidOperationException)
            { }
        }

        [Test]
        public void GetExpressionWithTwoGroups()
        {
            List<MathExpression> expressions = _lexer.GetExpressions("(2 + 2) * (3 + 1)");
            failIfOtherSubExpressionThan(expressions, "3 + 1", "2 + 2", "*");
        }

        [Test]
        public void GetSeveralParenthesisExpressionsInOrder()
        {
            List<MathExpression> expressions = _lexer.GetExpressions("(2 + 2) * (3 + 1)");
            Assert.AreEqual("2 + 2", expressions[0].Expression);
            Assert.AreEqual("*", expressions[1].Expression);
            Assert.AreEqual("3 + 1", expressions[2].Expression);
        }

        [Test]
        public void GetTokensRightSubClasses()
        {
            List<MathToken> tokens = _lexer.GetTokens("2 + 2");
            Assert.IsTrue(tokens[0] is MathNumber);
            Assert.IsTrue(tokens[1] is MathOperator);
        }

        [Test]
        public void GetComplexNestedExpressions()
        {
            List<MathExpression> expressions = _lexer.GetExpressions("((2 + 2) + 1)  * (3 + 1)");
            failIfOtherSubExpressionThan(expressions, "3 + 1", "2 + 2", "+", "*", "1");
        }
/*
        [Test]
        public void ProcessComplexNestedExpressions()
        {
            Assert.AreEqual(20, _parser.ProcessExpression("((2 + 2) + 1)  * (3 + 1)"));
        }
        */

    }
}
