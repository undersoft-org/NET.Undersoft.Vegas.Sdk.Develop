﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace System.Instant.Mathline
{
    [Serializable]
    public class Divide : BinaryOperator
    {
        public override double Apply(double a, double b)
        {
            return a / b;
        }
        public override void Compile(ILGenerator g)
        {
            g.Emit(OpCodes.Div);
        }
    }
}