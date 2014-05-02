using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperCalculator
{
    public class MathRegex
    {
        public bool IsExpressionValid(string expression)
        {
            Regex fullRegex = new Regex(@"^-{0,1}\d+((\s+)[+|\-|/|*](\s+)-{0,1}\d+)+$");
            Regex singleOperator = new Regex(@"^[+|\-|/|*]$");
            Regex singleNumber = new Regex(@"^\d+$");
            return (fullRegex.IsMatch(expression,0)||singleOperator.IsMatch(expression,0)||singleNumber.IsMatch(expression,0));
        }


       public bool IsNumberAndOperator(string expression)
        {
            Regex startsWithOperator = new Regex(@"^(\s*)([+|\-|/|*])(\s+)");
            Regex endsWithOperator = new Regex(@"(\s+)([+|\-|/|*])(\s*)$");
            string exp = expression;
           
            if (startsWithOperator.IsMatch(exp) || endsWithOperator.IsMatch(exp))
                return true;
           
           return false;
        }

    }
}
