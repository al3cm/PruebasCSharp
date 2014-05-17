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

        [Test]
        public void ValidateMoreThanOneDigitExpression()
        {
            string expression = "25 + 287";

            bool result = MathRegex.IsExpressionValid(expression);

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
                Assert.IsTrue(MathRegex.IsExpressionValid(expression), "Failure with operator: " + operatorChar);
            }
        }

        [Test]
        public void ValidateWithSpaces()
        {
            string expression = "2    +   287";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateFailsNoSpaces()
        {
            string expression = "2+7";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateComplexExpression()
        {
            string expression = "2 + 7 - 2 * 4";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateComplexWrongExpression()
        {
            string expression = "2 + 7 a 2 b 4";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateSimpleWrongExpression()
        {
            string expression = "2a7";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateWrongExpressionWithValidSubexpression()
        {
            string expression = "2 + 7 - 2 a 3 b";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateWithSeveralOperatorsTogether()
        {
            string expression = "+ + 7";
            bool result = MathRegex.IsExpressionValid(expression);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateWithNegativeNumbers()
        {
            Assert.IsTrue(MathRegex.IsExpressionValid("-7 + 1"));
        }

        [Test]
        public void ValidateWithNegativeNumbersAtTheEnd()
        {
            Assert.IsTrue(MathRegex.IsExpressionValid("7 - -1"));
        }

        [Test]
        public void IsSubExpression()
        {
            Assert.IsTrue(MathRegex.IsSubExpression("2 + 2"));
        }

        [Test]
        public void IsNumber()
        {
            Assert.IsTrue(MathRegex.IsNumber("22"));
        }

        [Test]
        public void IsOperator()
        {
            string operators = "*+/-";
            foreach (char op in operators)
                Assert.IsTrue(MathRegex.IsOperator(op.ToString()));
        }

    }
}
