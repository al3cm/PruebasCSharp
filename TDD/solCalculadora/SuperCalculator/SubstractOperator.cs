using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public class SubstractOperator : MathOperator
    {
        public SubstractOperator()
            : base(1)
        {
            _token = "-";
        }
        public override int Resolve(int a, int b, CalculatorProxy calcProxy)
        {
            return calcProxy.BinaryOperation(calcProxy.Calculator.Substract, a, b);
        }
    }
}
