using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SuperCalculator;


namespace UnitTests
{
    [TestFixture]
    public class CalcProxyTests
    {
        private Calculator _calculator;
        private CalcProxy _calcProxy;
        private CalcProxy _calcProxyWithLimits;
        
        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
            _calcProxy = new CalcProxy(new Validator(-100, 100), _calculator);
            _calcProxyWithLimits = new CalcProxy(new Validator(-10, 10), _calculator);
        }
        
        [Test]
        public void Add()
        {

            int result = _calcProxy.BinaryOperation(_calculator.Add,2,2);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Substract()
        {
            int result = _calcProxy.BinaryOperation(_calculator.Substract, 5, 3);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddWithDifferentArguments()
        {
            int result = _calcProxy.BinaryOperation(_calculator.Add, 2, 5);
            Assert.AreEqual(7, result);
        }
        
        [Test]
        public void SubstractReturningNegative()
        {
            int result = _calcProxy.BinaryOperation(_calculator.Substract, 3, 5);
            Assert.AreEqual(-2, result);
        }

        [Test]

        public void ArgumentsExceedLimits()
        {
            try
            {
                _calcProxyWithLimits.BinaryOperation(_calculator.Add, 30, 50);
                Assert.Fail("This should fail: arguments exceed both limits");
            }
            catch (OverflowException)
            {
                //Ok, this works
            }
        }

        [Test]
        public void ArgumentsExceedLimitsInverse()
        {

            try
            {
                _calcProxyWithLimits.BinaryOperation(_calculator.Add, -30, -50);
                Assert.Fail("This should fail: arguments exceed both limits");
            }
            catch (OverflowException)
            {
                //Ok, this works
            }
        }

        [Test]
        public void ValidateResultExceedingUpperLimit()
        {
            try
            {
                _calcProxyWithLimits.BinaryOperation(_calculator.Add, 10, 10);
                Assert.Fail("This should fail as result exceed upper limit");
            }
            catch (OverflowException)
            {
                //Ok, this works
            }
        }

        [Test]
        public void ValidateResultExceedingLowerLimit()
        {
            try
            {
                _calcProxyWithLimits.BinaryOperation(_calculator.Add, -20, -1);
                Assert.Fail("This should fail as result exceed lower limit");
            }
            catch (OverflowException)
            {
                //Ok, this works
            }
        }

        [Test]
        public void Multiply()
        {
            Assert.AreEqual(_calcProxy.BinaryOperation(_calculator.Multiply,2,5),10);
        }

        [Test]
        public void Division()
        {
            Assert.AreEqual(_calcProxy.BinaryOperation(_calculator.Divide,10, 2), 5);
        }
    }
}
