using BigMath;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace BigMathTests.MatrixTests
{
    public class MultiplyTests
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
        public void MultiplyingFromDefaulth_ShouldReturnAllValuesZeroes([Values(-1f, -0.5f, 0f, 0.5f, 2f)] float n)
        {
            matrix.Multiply(n);

            for (int i = 0; i < this.matrix.Rows; i++)
            {
                for (int j = 0; j < this.matrix.Cols; j++)
                {
                    Assert.AreEqual(0, matrix.Values[i, j]);
                }
            }

            Assert.Pass();
        }

        [Test]
        public void MultiplyingOne_ShouldReturnAllValuesTheSame([Values(-1f, -0.5f, 0f, 0.5f, 2f)] float n)
        {
            for (int i = 0; i < this.matrix.Rows; i++)
            {
                for (int j = 0; j < this.matrix.Cols; j++)
                {
                    matrix.Values[i, j] = 1;
                }
            }

            matrix.Multiply(n);

            for (int i = 0; i < this.matrix.Rows; i++)
            {
                for (int j = 0; j < this.matrix.Cols; j++)
                {
                    Assert.AreEqual(n, matrix.Values[i, j]);
                }
            }

            Assert.Pass();
        }

        [Test]
        public void MultiplyingMinusTwo_ShouldReturnAllValuesCorrectly([Values(-1f, -0.5f, 0f, 0.5f, 2f)] float n)
        {
            for (int i = 0; i < this.matrix.Rows; i++)
            {
                for (int j = 0; j < this.matrix.Cols; j++)
                {
                    matrix.Values[i, j] = -2;
                }
            }

            matrix.Multiply(n);

            float expectedResult = -2 * n;

            for (int i = 0; i < this.matrix.Rows; i++)
            {
                for (int j = 0; j < this.matrix.Cols; j++)
                {
                    Assert.AreEqual(expectedResult, matrix.Values[i, j]);
                }
            }

            Assert.Pass();
        }


        [Test]
        public void MultiplyingTwoMatrices_ShouldReturnAllValuesCorrectly(float n)
        {
            Assert.Fail("Matrix*Matrix Not implemented");
        }
    }
}
