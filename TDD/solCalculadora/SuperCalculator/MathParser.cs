using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SuperCalculator
{
    

    public class MathParser 
    {
        Lexer _lexer;
        CalculatorProxy _calcProxy;

        public MathParser(Lexer lexer,CalculatorProxy calcProxy)
        {
            _lexer = lexer;
            _calcProxy = calcProxy;
        }

        public int ProcessExpression(string expression)
        {
            List<MathToken> tokens = _lexer.GetTokens(expression);

            while (tokens.Count>1)
            {
                MathOperator op = GetMaxPrecedence(tokens);
                int firstNumber = tokens[op.Index - 1].IntValue;
                int secondNumber = tokens[op.Index + 1].IntValue;
                int result = op.Resolve(firstNumber, secondNumber,_calcProxy);
                replaceTokensWithResult(tokens,op.Index,result);
            }
            return tokens[0].IntValue;
        }

        private void replaceTokensWithResult(List<MathToken> tokens,int indexOfOperator,int result)
        { 
                tokens[indexOfOperator - 1] = new MathToken(result.ToString());
                tokens.RemoveAt(indexOfOperator);
                tokens.RemoveAt(indexOfOperator);
        }

        public MathOperator GetMaxPrecedence(List<MathToken> tokens)
        {
            int precedence = 0;
            MathOperator maxPrecedenceOperator = null;
            int index = -1;

            foreach (MathToken token in tokens)
            {
                index++;
                if (token.isOperator())
                {
                    MathOperator op = OperatorFactory.Create(token);
                    if (op.Precedence >= precedence)
                    {
                        precedence = op.Precedence;
                        maxPrecedenceOperator = op;
                        maxPrecedenceOperator.Index = index;
                    }
                }
            }
            return maxPrecedenceOperator;
        }

        

    }

}
