/* template class */

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace BigMathTests.MatrixTests
{
    public class ClassName
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMethod() { }
    }
}

[TestCase(12,3,4)]
[TestCase(12,2,6)]
[TestCase(12,4,3)]
public void DivideTest(int n, int d, int q)
{
  Assert.AreEqual( q, n / d );
}

[TestCase(12,3, Result=4)]
[TestCase(12,2, Result=6)]
[TestCase(12,4, Result=3)]
public int DivideTest(int n, int d)
{
  return( n / d );
}

[Test]
public void MyTest(
    [Values(1,2,3)] int x,
    [Values("A","B")] string s)
{
    ...
}

[Test]
public void MyTest(
    [Values(1,2,3)] int x,
    [Random(-1.0, 1.0, 5)] double d)
{
    ...
}