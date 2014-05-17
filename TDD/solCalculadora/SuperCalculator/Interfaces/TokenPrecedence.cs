using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public interface TokenPrecedence
    {
        MathOperator GetMaxPrecedence(List<MathToken> tokens);
    }
}
