/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.Strix.Enumerable.Tests.Unit.EnumerableExtensionsTests
{
  [TestFixture]
  public class CheckedProductTests
  {
    [Test]
    public void ExceptionIfNull()
    {
      Assert.Throws(
        Is.InstanceOf<Exception>(),
        () => ((int[]) null).CheckedProduct());
    }

    [Test]
    public void ExceptionIfEmpty()
    {
      Assert.Throws(
        Is.InstanceOf<Exception>(),
        () => (new int[] {}).CheckedProduct());
    }

    [TestCaseSource("CalculatesProductTestCases")]
    public void CalculatesProduct(int[] values, int expectedProduct)
    {
      Assert.That(values.CheckedProduct(), Is.EqualTo(expectedProduct));
    }

    [Test]
    public void ExceptionIfOverflow()
    {
      Assert.Throws(
        Is.InstanceOf<OverflowException>(),
        () => (new[] {Int32.MaxValue, 2}).CheckedProduct());
    }

    private IEnumerable<TestCaseData> CalculatesProductTestCases
    {
      get
      {
        yield return new TestCaseData(new[] {1}, 1);
        yield return new TestCaseData(new[] {1, 1, 1}, 1);
        yield return new TestCaseData(new[] {2, 3, 4}, 24);
        yield return new TestCaseData(new[] {99, 73, 0, 16, 11}, 0);
      }
    }
  }
}
