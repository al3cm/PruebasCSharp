﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public interface BasicCalculator
    {
        int Add(int a1, int a2);
        int Substract(int a1, int a2);
        int Multiply(int a1, int a2);
        int Divide(int a1, int a2);
    }
}
