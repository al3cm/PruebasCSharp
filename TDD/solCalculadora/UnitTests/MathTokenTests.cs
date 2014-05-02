using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;

namespace UnitTests
{
    [TestFixture]
    public class MathTokenTests
    {
        [Test]
        public void isOperator()
        {
            MathToken numberToken = new MathToken("22");
            Assert.IsFalse(numberToken.isOperator());
        }

        [Test]
        public void isOperatorTrue()
        {
            MathToken numberToken = new MathToken("*");
            Assert.IsTrue(numberToken.isOperator());
        }

    }

}
