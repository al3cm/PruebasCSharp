using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public interface CalculatorProxy
    {
        int BinaryOperation(SingleBinaryOperation operation, int arg1, int arg2);
        BasicCalculator Calculator { get; set; }
    }
}
