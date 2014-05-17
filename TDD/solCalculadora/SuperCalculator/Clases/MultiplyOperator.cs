using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public class MultiplyOperator : MathOperator
    {
        public MultiplyOperator()
            : base(2)
        {
            _token = "*";
        }
        public override int Resolve(int a, int b)
        {
            return CalcProxy.BinaryOperation(CalcProxy.Calculator.Multiply,a,b);
        }
    }
}
