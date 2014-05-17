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
       
        ExpressionFixer _fixer;
        List<MathExpression> _expressions;
        

        [SetUp]
        public void SetUp()
        {

            _fixer = new ExpressionFixer();
            _expressions = new List<MathExpression>();
        }


        [Test]
        public void SplitExpressionWhenOperatorAtTheEnd()
        {
            _expressions.Add(new MathExpression("2 +"));
            _fixer.FixExpressions(_expressions);
            Assert.IsTrue(_expressions[0].Expression == "2" || _expressions[0].Expression == "+");
            Assert.IsTrue(_expressions[1].Expression == "2" || _expressions[1].Expression == "+");
        }

        [Test]
        public void Trim()
        {
            _expressions.Add(new MathExpression(" * "));
            _fixer.FixExpressions(_expressions);
            Assert.AreEqual("*", _expressions[0].Expression);
        }

    }
}
