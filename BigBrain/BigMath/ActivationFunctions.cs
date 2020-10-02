using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMath
{
    public static class ActivationFunctions
    {
        public static float Sigmoid(float x)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-x));
        }

        public static float TanH(float x)
        {
            return (float)Math.Tanh(x);
        }

        public static float HeavisideStep(float x)
        {
            return x < 0.0f ? 0.0f : 1.0f;
        }

        public static float[] SoftMax(float[] inputs)
        {
            float[] result = new float[inputs.Length];
            float[] inputExponents = inputs.Select(x => (float)Math.Exp(x)).ToArray();
            float sumOfExponents = inputExponents.Sum();

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = inputExponents[i] / sumOfExponents;
            }

            return result;
        }
    }
}
