using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public class AddOperator : MathOperator 
    {
        public AddOperator()
            : base(1)
        {
            _token = "+";
        }
        public override int Resolve(int a, int b)
        {
            return CalcProxy.BinaryOperation(CalcProxy.Calculator.Add, a, b);
        }
    }
}
