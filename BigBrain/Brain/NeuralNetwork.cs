using BigMath;

using System;

namespace Brain
{
    public class NeuralNetwork
    {
        private readonly int inputsNumber;
        private readonly int hiddenNumber;
        private readonly int outputsNumber;
        private Matrix weightsHI;
        private Matrix weightsHO;
        private Matrix biasH;
        private Matrix biasO;

        public Random Random { get; }

        public NeuralNetwork(int inputsNumber, int hiddenNumber, int outputsNumber)
        {
            this.Random = new Random(DateTime.Now.Millisecond);
            this.inputsNumber = inputsNumber;
            this.hiddenNumber = hiddenNumber;
            this.outputsNumber = outputsNumber;

            weightsHI = new Matrix(this.hiddenNumber, this.inputsNumber);
            weightsHO = new Matrix(this.outputsNumber, this.hiddenNumber);
            weightsHI.Randomize(this.Random);
            weightsHO.Randomize(this.Random);

            biasH = new Matrix(this.hiddenNumber, 1);
            biasH.Randomize(this.Random);
            biasO = new Matrix(this.outputsNumber, 1);
            biasO.Randomize(this.Random);
        }

        public float[] FeedForward(float[] input_array)
        {
            Matrix inputs = new Matrix(input_array.Length, 1);
            inputs.SetRow(input_array, 0);

            Matrix hidden = this.weightsHI.Multiply(inputs);
            hidden.Add(biasH);

            // activation function
            hidden.Map(ActivationFunctions.TanH);

            Matrix output = this.weightsHO.Multiply(hidden);
            output.Map(ActivationFunctions.TanH);

            return output.ToArray();
        }

        public void Train(float[] input_array, float[] targets_array)
        {
            float[] outputs_array = this.FeedForward(input_array);
            Matrix outputs = new Matrix(outputs_array);
            Matrix targets = new Matrix(targets_array);

            Matrix errors = targets - outputs;

            // loop for more layers
            Matrix w_ho_t = this.weightsHO.Transpose();
            Matrix hidden_error = w_ho_t * errors;
        }
    }
}
