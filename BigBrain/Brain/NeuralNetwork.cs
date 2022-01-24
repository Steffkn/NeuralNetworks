using BigMath;
using BigMath.ActivationFunctions;

using System;
using System.Collections.Generic;

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
        private Matrix weightsHO3;
        private List<Matrix> weightList;
        private Matrix biasI;
        private Matrix biasH1;
        private Matrix biasH2;
        private Matrix biasH3;
        private Matrix biasO;
        private List<Matrix> biasList;
        private List<Matrix> layerList;
        private List<Layer> layers;
        private float LearningRate = 0.2f;
        private IActivationFunction activationFunction;

        public Random Random { get; }

        public NeuralNetwork(int inputsNumber, int hiddenNumber, int outputsNumber)
        {
            this.Random = new Random(DateTime.Now.Millisecond);
            this.inputsNumber = inputsNumber;
            this.hiddenNumber = hiddenNumber;
            this.outputsNumber = outputsNumber;

            layers = new List<Layer>();

            weightsHI = new Matrix(this.hiddenNumber, this.inputsNumber);
            weightsHO1 = new Matrix(this.hiddenNumber, this.hiddenNumber);
            weightsHO2 = new Matrix(this.hiddenNumber, this.hiddenNumber);
            weightsHO3 = new Matrix(this.outputsNumber, this.hiddenNumber);

            weightsHI.Randomize(this.Random);
            weightsHO1.Randomize(this.Random);
            weightsHO2.Randomize(this.Random);
            weightsHO3.Randomize(this.Random);

            weightList = new List<Matrix>();
            weightList.Add(weightsHI);
            weightList.Add(weightsHO1);
            weightList.Add(weightsHO2);
            weightList.Add(weightsHO3);

            biasI = new Matrix(1, 1);
            biasH1 = new Matrix(1, 1);
            biasH1.Randomize(this.Random);
            biasH2 = new Matrix(1, 1);
            biasH2.Randomize(this.Random);
            biasH3 = new Matrix(1, 1);
            biasH3.Randomize(this.Random);
            biasO = new Matrix(1, 1);
            biasO.Randomize(this.Random);

            biasList = new List<Matrix>();
            biasList.Add(biasI);
            biasList.Add(biasH1);
            biasList.Add(biasH2);
            biasList.Add(biasH3);
            biasList.Add(biasO);

            activationFunction = new Sigmoid();
        }

        public float[] FeedForward(float[] input_array)
        {
            return FeedForwardV1(input_array).ToArray();
        }

        private Matrix FeedForwardV1(float[] input_array)
        {
            Matrix inputs = new Matrix(input_array);

            Matrix hidden1 = this.weightsHI * inputs;
            hidden1.Add(biasH1[0, 0]);
            hidden1.Map(activationFunction.Activate);

            Matrix hidden2 = this.weightsHO1 * hidden1;
            hidden2.Add(biasH2[0, 0]);
            hidden2.Map(activationFunction.Activate);

            Matrix hidden3 = this.weightsHO2 * hidden1;
            hidden3.Add(biasH3[0, 0]);
            hidden3.Map(activationFunction.Activate);
            
            Matrix output = this.weightsHO3 * hidden3;
            output.Add(biasO[0, 0]);
            output.Map(activationFunction.Activate);

            return output;
        }

        private Matrix FeedForwardV2(float[] input_array)
        {
            layers = new List<Layer>();

            Matrix inputs = new Matrix(input_array);
            layers.Add(new Layer() { Values = inputs });

            for (int i = 0; i < this.weightList.Count; i++)
            {
                //var hiddenLayer = FeedForwardLayer(layers[i].Values, this.weightList[i], biasList[i + 1][0, 0]);
                //layers.Add(new Layer() { Values = hiddenLayer });

                var newLayer = new Layer();
                newLayer.FeedForward(layers[i].Values, this.weightList[i], biasList[i + 1][0, 0], activationFunction);
                layers.Add(newLayer);
            }

            return layers[layers.Count - 1].Values;
        }

        private Matrix FeedForwardLayer(Matrix inLayer, Matrix inOutWeights, float bias)
        {
            Matrix outLayer = inOutWeights * inLayer;
            outLayer.Add(bias);
            outLayer.Map(activationFunction.Activate);

            return outLayer;
        }

        public void Train(float[] input_array, float[] targets_array)
        {
            TrainV1(input_array, targets_array);
        }

        private void TrainV1(float[] input_array, float[] targets_array)
        {
            Matrix inputs = new Matrix(input_array);

            Matrix hidden1 = this.weightsHI * inputs;
            hidden1.Add(biasH1[0, 0]);
            hidden1.Map(activationFunction.Activate);

            Matrix hidden2 = this.weightsHO1 * hidden1;
            hidden2.Add(biasH2[0, 0]);
            hidden2.Map(activationFunction.Activate);

            Matrix hidden3 = this.weightsHO2 * hidden1;
            hidden3.Add(biasH3[0, 0]);
            hidden3.Map(activationFunction.Activate);

            Matrix outputs = this.weightsHO3 * hidden3;
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

        private void TrainV2(float[] input_array, float[] targets_array)
        {
            // training
            Matrix targets = new Matrix(targets_array);
            Matrix output = FeedForwardV2(input_array);
            Matrix errors = null;

            for (int i = this.weightList.Count - 1; i > 0; i--)
            {
                // targets, output, hidden2, hidden1, inputs
                // errors
                if (errors == null)
                {
                    errors = targets - output;
                }
                else
                {
                    // skips first cycle so we have to add 1 for the next
                    errors = this.weightList[i + 1].Transpose() * errors;
                }


                Console.WriteLine(string.Join(" ", errors.ToString()));
                Matrix gradient = calculateGradient(layers[i].Values, errors);
                Matrix delta = gradient * layers[i - 1].Values.Transpose();

                // adjust H
                this.weightList[i - 1].Add(delta);
                this.biasList[i].Add(gradient);
            }
        }

        private Matrix calculateGradient(Matrix output, Matrix output_errors)
        {
            // gradient
            Matrix gradients = new Matrix(output);
            gradients.Map(activationFunction.Deactivate);
            gradients *= output_errors;
            gradients *= this.LearningRate;
            return gradients;
        }
    }
}
