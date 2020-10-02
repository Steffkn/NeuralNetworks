using BigMath;

using NUnit.Framework;

namespace BigMathTests.MatrixTests
{
    public class MatrixConstructorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MatrixInitializationTest(
            [Values(0, 2, 3)] int rows,
            [Values(0, 3, 2)] int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            Assert.AreEqual(rows, matrix.Rows);
            Assert.AreEqual(cols, matrix.Cols);
        }

        [Test]
        public void MatrixInitializationTest_MatrixValuesInitializedCorrectly(
            [Values(0, 2, 3)] int rows,
            [Values(0, 3, 2)] int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            Assert.AreEqual(rows, matrix.Values.GetLength(0));
            Assert.AreEqual(cols, matrix.Values.GetLength(1));
            Assert.AreEqual(rows, matrix.Rows);
            Assert.AreEqual(cols, matrix.Cols);
        }

        [Test]
        public void MatrixInitializationTest_MatrixValuesAreAllZeros(
            [Values(0, 2, 3)] int rows,
            [Values(0, 3, 2)] int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(0, matrix.Values[i, j]);
                }
            }

            Assert.AreEqual(rows, matrix.Rows);
            Assert.AreEqual(cols, matrix.Cols);

            Assert.Pass();
        }
    }
}