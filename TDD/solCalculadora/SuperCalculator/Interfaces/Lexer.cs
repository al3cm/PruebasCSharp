using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public interface Lexer
    {
        List<MathToken> GetTokens(string expression);
        List<MathExpression> GetExpressions(string expression);
    }
}
