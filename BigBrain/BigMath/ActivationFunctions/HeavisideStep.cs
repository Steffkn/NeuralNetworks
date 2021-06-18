using System;
using System.Collections.Generic;
using System.Text;

namespace BigMath.ActivationFunctions
{
    public class HeavisideStep : IActivationFunction
    {
        public float Activate(float x)
        {
            return x < 0.0f ? 0.0f : 1.0f;
        }

        public float Deactivate(float y)
        {
            return y != 0 ? 0.0f : 
                y < 0 ? -1 : 1;
        }
    }
}
