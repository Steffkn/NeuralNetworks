using System;
using System.Text;

namespace BigMath
{
    public class Matrix
    {
        private const string PRINT_FORMAT = "{0,10:N6}";

        public int Rows { get; }
        public int Cols { get; }

        public float[,] Values { get => this.values; }

        private float[,] values { get; }

        public Matrix(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;

            this.values = new float[rows, cols];
        }

        public Matrix(float[] values_array)
            : this(values_array.Length, 1)
        {
            this.SetRow(values_array, 0);
        }

        public Matrix(Matrix other)
            : this(other.Rows, other.Cols)
        {
            for (int i = 0; i < other.Rows; i++)
            {
                for (int j = 0; j < other.Cols; j++)
                {
                    this[i, j] = other[i, j];
                }
            }
        }

        public Matrix Add(float n)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this.values[i, j] += n;
                }
            }

            return this;
        }

        public Matrix Add(Matrix other)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this.values[i, j] += other.Values[i, j];
                }
            }

            return this;
        }

        public Matrix Subtract(float n)
        {
            this.Add(-n);
            return this;
        }

        public Matrix Subtract(Matrix other)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this.values[i, j] -= other.Values[i, j];
                }
            }

            return this;
        }

        public Matrix Add(int other)
        {
            return this.Map((currentValue) => currentValue + other);
        }

        public Matrix Multiply(float n)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this.values[i, j] *= n;
                }
            }

            return this;
        }

        public Matrix Multiply(Matrix other)
        {
            Matrix result = new Matrix(this.Rows, other.Cols);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Cols; j++)
                {
                    float currentResult = 0;
                    for (int k = 0; k < this.Cols; k++)
                    {
                        currentResult += this.values[i, k] * other.Values[k, j];
                    }

                    result.Values[i, j] = currentResult;
                }
            }

            return result;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(this.Cols, this.Rows);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Cols; j++)
                {
                    result.Values[i, j] = this.Values[j, i];
                }
            }

            return result;
        }

        public Matrix Map(Func<float, float> operation)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this.values[i, j] = operation(this.values[i, j]);
                }
            }

            return this;
        }

        public bool IsSameSizeAs(Matrix other)
        {
            return this.Rows == other.Rows && this.Cols == other.Cols;
        }

        public bool CanMultiplyBy(Matrix other)
        {
            return this.Rows == other.Cols;
        }

        public void Randomize(Random random)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    this.values[i, j] = random.Next(-10, 10) / 10f;
                }
            }
        }

        public float this[int rowIndex, int colIndex]
        {
            get { return this.values[rowIndex, colIndex]; }
            set { this.values[rowIndex, colIndex] = value; }
        }

        public float[] this[int rowIndex]
        {
            get { return GetRow(rowIndex); }
            set { SetRow(value, rowIndex); }
        }

        public float[] GetRow(int number)
        {
            float[] result = new float[this.Cols];
            for (int i = 0; i < this.Cols; i++)
            {
                result[i] = this.values[number, i];
            }

            return result;
        }

        public void SetRow(float[] row, int rowIndex)
        {
            for (int i = 0; i < this.Cols; i++)
            {
                this.values[rowIndex, i] = row[i];
            }
        }

        public float[] GetCol(int columnIndex)
        {
            float[] result = new float[this.Rows];
            for (int i = 0; i < this.Rows; i++)
            {
                result[i] = this.values[i, columnIndex];
            }

            return result;
        }

        public void SetCol(float[] column, int colIndex)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                this.values[i, colIndex] = column[i];
            }
        }

        public static Matrix operator +(Matrix matrixA, float number)
        {
            Matrix result = new Matrix(matrixA);
            return result.Add(number);
        }

        public static Matrix operator +(Matrix matrixA, Matrix matrixB)
        {
            Matrix result = new Matrix(matrixA);
            return result.Add(matrixB);
        }

        public static Matrix operator -(Matrix matrixA, float number)
        {
            Matrix result = new Matrix(matrixA);
            return result.Subtract(number);
        }

        public static Matrix operator -(Matrix matrixA, Matrix matrixB)
        {
            Matrix result = new Matrix(matrixA);
            return result.Subtract(matrixB);
        }

        public static Matrix operator *(Matrix matrixA, float number)
        {
            Matrix result = new Matrix(matrixA);
            return result.Multiply(number);
        }

        public static Matrix operator *(Matrix matrixA, Matrix matrixB)
        {
            Matrix result = new Matrix(matrixA);
            return result.Multiply(matrixB);
        }

        /// <summary>
        /// Transpose the matrix.
        /// </summary>
        /// <param name="matrixA">Input matrix</param>
        /// <returns>Transposed matrix</returns>
        public static Matrix operator !(Matrix matrixA)
        {
            return matrixA.Transpose();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            const string separator = "|";
            for (int i = 0; i < this.Rows; i++)
            {
                stringBuilder.Append(separator);
                for (int j = 0; j < this.Cols; j++)
                {
                    stringBuilder.AppendFormat(PRINT_FORMAT, this.values[i, j]);
                }

                stringBuilder.AppendLine(separator);
            }

            return stringBuilder.ToString();
        }

        public float[] ToArray()
        {
            float[] output = new float[this.Rows * this.Cols];
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    output[i + j] = this.values[i, j];
                }
            }

            return output;
        }
    }
}
