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
        public override int Resolve(int a, int b)
        {
            return CalcProxy.BinaryOperation(CalcProxy.Calculator.Substract, a, b);
        }
    }
}
