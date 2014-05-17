using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SuperCalculator
{
    public class MathLexer : Lexer
    {
        
        ExpressionFixer _fixer;
        static char OPEN_SUBEXPRESSION = '(';
        static char CLOSE_SUBEXPRESSION = ')';

        public MathLexer(ExpressionFixer fixer)
        {
            _fixer = fixer;
        }

        public List<MathToken> GetTokens(string expression)
        {
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
                if (MathRegex.IsOperator(item))
                    tokens.Add(OperatorFactory.Create(item));
                else
                    tokens.Add(new MathNumber(item));
            }
            return tokens;
        }

        public List<MathExpression> GetExpressions(string expression)
        {
            int openedParenthesis = 0;
            ExpressionBuilder expBuilder = ExpressionBuilder.Create();
            expBuilder.InputText = expression;
            getExpressions(expBuilder,ref openedParenthesis);
            if(openedParenthesis != 0)
                throw new InvalidOperationException("Parenthesis do not match");
            _fixer.FixExpressions(expBuilder.AllExpressions);
            expBuilder.AllExpressions.Sort(); // ---> Ordenación
            return expBuilder.AllExpressions;

       }

        private void getExpressions(ExpressionBuilder expBuilder, ref int openedParenthesis)
        {
            while (expBuilder.ThereAreMoreChars())
            {
                char currentChar = expBuilder.GetCurrentChar();
                if (currentChar == OPEN_SUBEXPRESSION)
                {
                    openedParenthesis++;
                    getExpressions(expBuilder.ProcessNewSubExpression(), ref openedParenthesis);
                }
                else if (currentChar == CLOSE_SUBEXPRESSION)
                {
                    expBuilder.SubExpressionEndFound();
                    openedParenthesis--;
                    return;
                }
                else
                    expBuilder.AddSubExpressionChar();
            }
            expBuilder.SubExpressionEndFound();
        }
    }
}
