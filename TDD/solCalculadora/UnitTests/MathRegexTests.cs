using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;

namespace UnitTests
{
    [TestFixture]
    class MathRegexTests
    {

        private MathRegex _regex;


        [SetUp]
        public void SetUp()
        {

            _regex = new MathRegex();

        }

        [Test]
        public void ValidateMoreThanOneDigitExpression()
        {
            string expression = "25 + 287";

            bool result = _regex.IsExpressionValid(expression);

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateSimpleExpressionWithAllOperations()
        {
            string operators = "+-*/";
            string expression = String.Empty;
            foreach (char operatorChar in operators)
            {
                expression = "2 " + operatorChar + " 2";
                Assert.IsTrue(_regex.IsExpressionValid(expression), "Failure with operator: " + operatorChar);
            }
        }

        [Test]
        public void ValidateWithSpaces()
        {
            string expression = "2    +   287";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateFailsNoSpaces()
        {
            string expression = "2+7";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateComplexExpression()
        {
            string expression = "2 + 7 - 2 * 4";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateComplexWrongExpression()
        {
            string expression = "2 + 7 a 2 b 4";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateSimpleWrongExpression()
        {
            string expression = "2a7";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateWrongExpressionWithValidSubexpression()
        {
            string expression = "2 + 7 - 2 a 3 b";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateWithSeveralOperatorsTogether()
        {
            string expression = "+ + 7";
            bool result = _regex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateWithNegativeNumbers()
        {
            Assert.IsTrue(_regex.IsExpressionValid("-7 + 1"));
        }

        [Test]
        public void ValidateWithNegativeNumbersAtTheEnd()
        {
            Assert.IsTrue(_regex.IsExpressionValid("7 - -1"));
        }


    }
}
