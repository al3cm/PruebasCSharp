using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public abstract class MathOperator
    {
        protected int _precedence = 0;
        protected string _token = String.Empty;
        int _index = -1;

        public MathOperator(int precedence)
        {
            _precedence = precedence;
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public string Token
        {
            get { return _token; }
        }

        public int Precedence
        {
            get { return _precedence; }
        }
        /*
        public int Resolve(int a, int b, CalculatorProxy calcProxy)
        {
            if (Token == "*")
                return calcProxy.BinaryOperation(calcProxy.Calculator.Multiply, a, b);
            if (Token == "+")
                return calcProxy.BinaryOperation(calcProxy.Calculator.Add,a,b);
            if (Token == "/")
                return calcProxy.BinaryOperation(calcProxy.Calculator.Divide, a, b);
            if (Token == "-")
                return calcProxy.BinaryOperation(calcProxy.Calculator.Substract, a, b);
            return 0;
        }
        */
        public abstract int Resolve(int a, int b, CalculatorProxy calcProxy);
    }
}
