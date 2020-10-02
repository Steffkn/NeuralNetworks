using BigMath;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace BigMathTests.MatrixTests
{
    public class AddTests
    {
        Matrix matrix;

        [SetUp]
        public void Setup()
        {
            int rows = 2;
            int cols = 3;
            matrix = new Matrix(rows, cols);
        }

        [Test]
        public void Adding_ShouldReturnAllValuesTheSame([Values(-1f, -0.5f, 0f, 0.5f, 2f)] float n)
        {
            matrix.Add(n);

            for (int i = 0; i < this.matrix.Rows; i++)
            {
                for (int j = 0; j < this.matrix.Cols; j++)
                {
                    Assert.AreEqual(n, matrix.Values[i, j]);
                }
            }

            Assert.Pass();
        }
    }
}
