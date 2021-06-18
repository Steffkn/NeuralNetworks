using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMath
{
    public static class Functions
    {
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

        public static bool XOR(bool a, bool b)
        {
            return a ^ b;
        }

        public static bool OR(bool a, bool b)
        {
            return a || b;
        }

        public static bool AND(bool a, bool b)
        {
            return a && b;
        }
    }
}
