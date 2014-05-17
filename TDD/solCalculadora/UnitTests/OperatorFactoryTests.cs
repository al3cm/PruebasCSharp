using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;

namespace UnitTests
{
    [TestFixture]
    public class OperatorFactoryTests
    {
        [Test]
        public void CreateMultiplyOperator()
        {
            MathOperator op = OperatorFactory.Create("*");
            Assert.AreEqual(op.Precedence,2);
        }

        [Test]
        public void CreateDivisionOperator()
        {
            MathOperator op = OperatorFactory.Create("/");
            Assert.AreEqual(op.Precedence, 2);
        }


    }
}
