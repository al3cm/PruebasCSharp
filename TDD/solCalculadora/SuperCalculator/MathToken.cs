using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperCalculator
{
    public class MathToken
    {
        protected string _token = String.Empty;
        protected int _intValue;

        public MathToken(string token)
        {
            _token = token;
            Regex regex=new Regex(@"\d+");
            if (regex.IsMatch(_token))
                _intValue = Convert.ToInt32(_token);
        }

        public string Token
        {
            get { return _token; }
        }

        public int IntValue
        {
            get { return _intValue; }
        }

        public bool isOperator()
        {
            string operators = "+-*/";
            foreach (char op in operators)
            {
                if (_token == op.ToString())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
