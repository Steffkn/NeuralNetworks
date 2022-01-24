using BigMath;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Brain
{
    public class Program
    {
        static List<Tuple<float[], float[]>> trainingData = new List<Tuple<float[], float[]>>();
        static List<Tuple<float[], float[]>> testData = new List<Tuple<float[], float[]>>();

        public static Func<bool, bool, bool> testFunction { get => Functions.AND; }

        private static bool isTrainingEnabled = true;
        private static bool isManualTestEnabled = false;

        static void Main(string[] args)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Console.WriteLine("Generating training data..");
            GenerateTrainingData(random);
            Console.WriteLine("Generating test data..");
            GenerateTestData();

            Console.WriteLine("Creating new network..");
            var nn = new NeuralNetwork(2, 2, 1);

            if (isTrainingEnabled)
            {
                Console.WriteLine("Training the network..");
                for (int j = 0; j < 1000000; j++)
                {
                    var randomIndex = random.Next(trainingData.Count - 1);
                    nn.Train(trainingData[randomIndex].Item1, trainingData[randomIndex].Item2);
                }
            }

            Console.WriteLine("Testing the network..");
            foreach (var data in testData)
            {
                var inputs = data.Item1;
                var targets = data.Item2;
                var outputs = nn.FeedForward(inputs);

                Console.WriteLine("inputs");
                Matrix inputMatrix = new Matrix(inputs);
                Console.WriteLine(inputMatrix);

                Console.WriteLine("outputs");
                Matrix outputMatrix = new Matrix(outputs);
                Console.WriteLine(outputMatrix);

                Console.WriteLine("targets");
                Matrix targetMatrix = new Matrix(targets);
                Console.WriteLine(targetMatrix);
                Console.WriteLine("errors");
                Console.WriteLine(targetMatrix - outputMatrix);
                Console.WriteLine("==============================");
            }

            if (isManualTestEnabled)
            {
                Console.WriteLine("Manual testing the network..");
                while (true)
                {
                    try
                    {
                        var inputs = Console.ReadLine()
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => Convert.ToSingle(x))
                            .ToArray();
                        var targets = Console.ReadLine()
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => Convert.ToSingle(x))
                            .ToArray();
                        var outputs = nn.FeedForward(inputs);

                        Console.WriteLine("inputs");
                        Matrix inputMatrix = new Matrix(inputs);
                        Console.WriteLine(inputMatrix);

                        Console.WriteLine("outputs");
                        Matrix outputMatrix = new Matrix(outputs);
                        Console.WriteLine(outputMatrix);

                        Console.WriteLine("targets");
                        Matrix targetMatrix = new Matrix(targets);
                        Console.WriteLine(targetMatrix);
                        Console.WriteLine("errors");
                        Console.WriteLine(targetMatrix - outputMatrix);
                        Console.WriteLine("==============================");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }
                }
            }
        }

        private static void GenerateTestData()
        {
            testData.Add(new Tuple<float[], float[]>(new float[] { 0, 1 }, new float[] { 1 }));
            testData.Add(new Tuple<float[], float[]>(new float[] { 1, 0 }, new float[] { 1 }));
            testData.Add(new Tuple<float[], float[]>(new float[] { 0, 0 }, new float[] { 0 }));
            testData.Add(new Tuple<float[], float[]>(new float[] { 1, 1 }, new float[] { 1 }));
        }

        private static void GenerateTrainingData(Random random)
        {
            float firstInput;
            float secondInput;
            bool left;
            bool right;
            bool expectedResult;
            for (int i = 0; i < 1000; i++)
            {
                firstInput = random.Next(0, 10) / 10f;
                secondInput = random.Next(0, 10) / 10f;
                left = firstInput > 0.4;
                right = secondInput > 0.4;

                expectedResult = testFunction(left, right);

                trainingData.Add(new Tuple<float[], float[]>(new float[] { firstInput, secondInput }, new float[] { expectedResult ? 1 : 0 }));
            }
        }
    }
}
