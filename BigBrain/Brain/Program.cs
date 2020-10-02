using BigMath;

using System;
using System.Linq;

namespace Brain
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            float[] inputs = new float[] { 1, 2 };
            float[] targets = new float[] { 1};
            float[] outputs = new float[1];

            var nn = new NeuralNetwork(inputs.Length, 2, outputs.Length);

            nn.Train(inputs, targets);
        }
    }
}
