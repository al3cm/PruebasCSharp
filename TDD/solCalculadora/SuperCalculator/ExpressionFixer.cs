using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SuperCalculator
{
    public class ExpressionFixer
    {
        MathRegex _mathRegex;

        public ExpressionFixer(MathRegex mathRegex)
        {
            _mathRegex = mathRegex;
        }

        public void FixExpressions(List<string> expressions)
        {
            bool listHasChanged = true;
            while (listHasChanged)
            {
                listHasChanged = false;
                for (int i = 0; i < expressions.Count; i++)
                {
                    if (_mathRegex.IsNumberAndOperator(expressions[i]))
                    {
                        splitByOperator(expressions,expressions[i],i);
                        listHasChanged = true;
                        break;
                    }
                    if (expressions[i].Length==0)
                    {
                        expressions.RemoveAt(i);
                        listHasChanged = true;
                        break;
                    }
                }
            }
        }

          
        private void splitByOperator(List<string> expressions,string inputExpression,int position)
        {
            string[] nextExps = Regex.Split(inputExpression,@"([+|\-|/|*])");
            int j = position;
            expressions.RemoveAt(j);
            foreach (string subExp in nextExps)
            {
               // expressions.Insert(j, new MathExpression(subExp.Trim()));
                expressions.Insert(j, subExp.Trim());
                j++;
            }
        }

        /*
        public bool IsEmptyExpressionThenRemove(List<string> expressions, int index)
        {
            if (expressions[index].Length == 0)
            {
                expressions.RemoveAt(index);
                return true;
            }
            return false;
        }
        */
    }
}
