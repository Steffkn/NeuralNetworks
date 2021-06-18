using System;
using System.Collections.Generic;
using System.Text;

namespace BigMath.ActivationFunctions
{
    public class Sigmoid : IActivationFunction
    {
        public float Activate(float x)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-x));
        }

        public float Deactivate(float y)
        {
            return y * (1 - y);
        }
    }
}
