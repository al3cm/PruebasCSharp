using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SuperCalculator
{
    public class MathLexer : Lexer
    {
        MathRegex _validator;
        ExpressionFixer _fixer;
        static char OPEN_SUBEXPRESSION = '(';
        static char CLOSE_SUBEXPRESSION = ')';

        public MathLexer(MathRegex validator,ExpressionFixer fixer)
        {
            _validator = validator;
            _fixer = fixer;
        }

        public List<MathToken> GetTokens(string expression)
        {
            if (!_validator.IsExpressionValid(expression))
                throw new InvalidOperationException(expression);
             
            String[] items = splitExpression(expression);

            return createTokensFromStrings(items);
        }

        private string[] splitExpression(string expression)
        {
            return expression.Split((new char[] { ' ', '\t' }), StringSplitOptions.RemoveEmptyEntries);
        }

        private List<MathToken> createTokensFromStrings(string[] items)
        {
            List<MathToken> tokens = new List<MathToken>();
            foreach (String item in items)
            {
                tokens.Add(new MathToken(item));
            }
            return tokens;
        }

        public List<string> GetExpressions(string expression)
        {
            List<string> totalExpressionsFound = new List<string>();
            int openedParenthesis = 0;
            int startSearchingAt = 0;
            getExpressions(expression, ref startSearchingAt, String.Empty, totalExpressionsFound, ref openedParenthesis);
            if(openedParenthesis != 0)
                throw new InvalidOperationException("Parenthesis do not match");
            _fixer.FixExpressions(totalExpressionsFound);
            return totalExpressionsFound;

       }

        ///<sumary>
        /// Returns the position where the close parenthesis is found or 
        /// position of the last char in the string.
        /// Also populates the list of expressions along the way
        ///</sumary>

        private void getExpressions(string fullInputExpression,ref int subExpressionStartIndex,string subExpressionUnderConstruction,List<string> totalSubExpressionsFound, ref int openedParenthesis) 
        {
            for (int currentIndex = subExpressionStartIndex; currentIndex < fullInputExpression.Length; currentIndex++)
            {
                char currentChar = fullInputExpression[currentIndex];

                if (currentChar == OPEN_SUBEXPRESSION)
                {
                    openedParenthesis++;
                    subExpressionStartIndex = currentIndex + 1;
                    getExpressions(fullInputExpression, ref subExpressionStartIndex, String.Empty, totalSubExpressionsFound, ref openedParenthesis);
                    currentIndex = subExpressionStartIndex;
                }
                else if (currentChar == CLOSE_SUBEXPRESSION)
                {
                    totalSubExpressionsFound.Add(subExpressionUnderConstruction);
                    subExpressionStartIndex = currentIndex;
                    openedParenthesis--;
                    return;
                }
                else
                {
                    subExpressionUnderConstruction += fullInputExpression[currentIndex].ToString();
                }
            }
            totalSubExpressionsFound.Add(subExpressionUnderConstruction);
            subExpressionStartIndex = subExpressionUnderConstruction.Length;
        }





    }
}
