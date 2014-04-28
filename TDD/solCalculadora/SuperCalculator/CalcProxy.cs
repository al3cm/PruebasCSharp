using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SuperCalculator
{
    public class CalcProxy
    {
        private Calculator _calculator;
        private Validator _validator;


        public CalcProxy(Validator validator,Calculator calculator)
        {
            _validator = validator;
            _calculator = calculator;
        }


        public int BinaryOperation(SingleBinaryOperation operation,int arg1, int arg2)
        {
            _validator.ValidateArgs(arg1, arg2);   
            int result = 0;
            MethodInfo[] calculatorMethods = _calculator.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo method in calculatorMethods)
            {
        	    if (method == operation.Method)
                {
            		 result = (int)method.Invoke(_calculator, new Object[]{arg1,arg2});
                }	 
            }
            _validator.ValidateResult(result);
            return result;
        }
    }
}
