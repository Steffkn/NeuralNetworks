using BigMath;
using BigMath.ActivationFunctions;

using System;

namespace Brain
{
    public class NeuralNetwork
    {
        private readonly int inputsNumber;
        private readonly int hiddenNumber;
        private readonly int outputsNumber;
        private Matrix weightsHI;
        private Matrix weightsHO1;
        private Matrix weightsHO2;
        private Matrix biasH1;
        private Matrix biasH2;
        private Matrix biasO;
        private float LearningRate = 0.2f;
        private IActivationFunction activationFunction;

        public Random Random { get; }

        public NeuralNetwork(int inputsNumber, int hiddenNumber, int outputsNumber)
        {
            this.Random = new Random(DateTime.Now.Millisecond);
            this.inputsNumber = inputsNumber;
            this.hiddenNumber = hiddenNumber;
            this.outputsNumber = outputsNumber;

            weightsHI = new Matrix(this.hiddenNumber, this.inputsNumber);
            weightsHO1 = new Matrix(this.hiddenNumber, this.hiddenNumber);
            weightsHO2 = new Matrix(this.outputsNumber, this.hiddenNumber);
            weightsHI.Randomize(this.Random);
            weightsHO1.Randomize(this.Random);
            weightsHO2.Randomize(this.Random);

            biasH1 = new Matrix(1, 1);
            biasH1.Randomize(this.Random);
            biasH2 = new Matrix(1, 1);
            biasH2.Randomize(this.Random);
            biasO = new Matrix(1, 1);
            biasO.Randomize(this.Random);

            activationFunction = new Sigmoid();
        }

        public float[] FeedForward(float[] input_array)
        {
            Matrix inputs = new Matrix(input_array);

            Matrix hidden1 = this.weightsHI * inputs;
            hidden1.Add(biasH1[0, 0]);
            hidden1.Map(activationFunction.Activate);

            Matrix hidden2 = this.weightsHO1 * hidden1;
            hidden2.Add(biasH2[0, 0]);
            hidden2.Map(activationFunction.Activate);

            Matrix output = this.weightsHO2 * hidden2;
            output.Add(biasO[0, 0]);
            output.Map(activationFunction.Activate);

            return output.ToArray();
        }

        public void Train(float[] input_array, float[] targets_array)
        {
            Matrix inputs = new Matrix(input_array);

            Matrix hidden1 = this.weightsHI * inputs;
            hidden1.Add(biasH1[0, 0]);
            hidden1.Map(activationFunction.Activate);

            Matrix hidden2 = this.weightsHO1 * hidden1;
            hidden2.Add(biasH2[0, 0]);
            hidden2.Map(activationFunction.Activate);

            Matrix outputs = this.weightsHO2 * hidden2;
            outputs.Add(biasO[0, 0]);
            outputs.Map(activationFunction.Activate);

            // training
            Matrix targets = new Matrix(targets_array);

            // error
            Matrix output_errors = targets - outputs;

            // gradient
            Matrix gradients = new Matrix(outputs);
            gradients.Map(activationFunction.Deactivate);
            gradients *= output_errors;
            gradients *= this.LearningRate;

            // deltas 
            Matrix hidden2T = hidden2.Transpose();
            //Matrix weight_ho_deltas = gradients * hidden2T;

            // adjust 
            this.weightsHO2.Add(gradients * hidden2T);
            this.biasO.Add(gradients);

                    // error H
                    Matrix w_ho2_t = this.weightsHO2.Transpose();
                    Matrix hidden2_error = w_ho2_t * output_errors;


                    // gradient H
                    Matrix hidden2_gradient = new Matrix(hidden2);
                    hidden2_gradient.Map(activationFunction.Deactivate);
                    hidden2_gradient *= hidden2_error;
                    hidden2_gradient *= this.LearningRate;

                    // deltas H
                    Matrix hidden1T = hidden1.Transpose();
                    Matrix weight_ho2_deltas = hidden2_gradient * hidden1T;

                    // adjust H
                    this.weightsHO1.Add(weight_ho2_deltas);
                    this.biasH2.Add(hidden2_gradient);


            // error H
            Matrix w_ho1_t = this.weightsHO1.Transpose();
            Matrix hidden1_error = w_ho1_t * hidden2_error;

            // gradient H
            Matrix hidden1_gradient = new Matrix(hidden1);
            hidden1_gradient.Map(activationFunction.Deactivate);
            hidden1_gradient *= hidden1_error;
            hidden1_gradient *= this.LearningRate;

            // deltas H
            Matrix inputsT = inputs.Transpose();
            Matrix weight_hi_deltas = hidden1_gradient * inputsT;

            // adjust H
            this.weightsHI.Add(weight_hi_deltas);
            this.biasH1.Add(hidden1_gradient);
        }
    }
}
