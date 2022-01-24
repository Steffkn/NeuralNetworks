using BigMath;
using BigMath.ActivationFunctions;

namespace Brain
{
    public class Layer
    {
        public Matrix Values { get; set; }

        public void FeedForward(Matrix inLayer, Matrix inOutWeights, float bias, IActivationFunction activationFunction)
        {
            this.Values = FeedForwardLayer(inLayer, inOutWeights, bias, activationFunction);
        }

        private Matrix FeedForwardLayer(Matrix inLayer, Matrix inOutWeights, float bias, IActivationFunction activationFunction)
        {
            Matrix outLayer = inOutWeights * inLayer;
            outLayer.Add(bias);
            outLayer.Map(activationFunction.Activate);

            return outLayer;
        }
    }
}
