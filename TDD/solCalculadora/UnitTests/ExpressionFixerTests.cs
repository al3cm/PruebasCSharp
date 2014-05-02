using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;

namespace UnitTests
{
    [TestFixture]
    public class ExpressionFixerTests
    {
        private MathRegex _regex;


        [SetUp]
        public void SetUp()
        {

            _regex = new MathRegex();

        }


        [Test]
        public void SplitExpressionWhenOperatorAtTheEnd()
        {
            ExpressionFixer fixer = new ExpressionFixer(_regex);
            List<string> expressions = new List<string>();
            expressions.Add("2 +");
            fixer.FixExpressions(expressions);
            Assert.Contains("2",expressions);
            Assert.Contains("+", expressions);
        }
    }
}
