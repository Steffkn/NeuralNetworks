using System;
using System.Collections.Generic;
using System.Text;

namespace BigMath.ActivationFunctions
{
    public class TanH : IActivationFunction
    {
        public float Activate(float x)
        {
            return (float)Math.Tanh(x);
        }

        public float Deactivate(float x)
        {
            float y = 1.0f / (float)Math.Pow(Math.Cosh((float)x), 2);
            return y;
        }
    }
}
