using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public class OperatorFactory
    {
        public static MathOperator Create(MathToken token)
        {
            if (token.Token == "*")
                return new MultiplyOperator();
            else if (token.Token == "/")
                return new DivideOperator();
            else if (token.Token == "+")
                return new AddOperator();
            else if (token.Token == "-")
                return new SubstractOperator();

            throw new InvalidOperationException("The given token is not a valid operator");
        }
    }

}
