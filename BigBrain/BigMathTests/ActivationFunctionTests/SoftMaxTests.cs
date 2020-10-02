using BigMath;

using NUnit.Framework;

using System.Linq;

namespace BigMathTests.ActivationFunctionTests
{
    public class SoftMaxTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SoftMax()
        {
            var inputs = new float[] { 2, 1, 0, 1 };
            var results = ActivationFunctions.SoftMax(inputs);

            Assert.AreEqual(inputs.Length, results.Length);
            Assert.AreEqual(1, results.Sum());
        }
    }
}
