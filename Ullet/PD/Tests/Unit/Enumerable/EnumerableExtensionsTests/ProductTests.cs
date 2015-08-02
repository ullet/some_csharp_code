/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.PD.Enumerable.Tests.Unit.EnumerableExtensionsTests
{
  [TestFixture]
  public class ProductTests
  {
    [Test]
    public void ExceptionIfNull()
    {
      Assert.Throws(
        Is.InstanceOf<Exception>(),
        () => ((int[]) null).Product());
    }

    [Test]
    public void ExceptionIfEmpty()
    {
      Assert.Throws(
        Is.InstanceOf<Exception>(),
        () => (new int[] {}).Product());
    }

    [TestCase(new[] {1}, 1)]
    [TestCase(new[] {1, 1, 1}, 1)]
    [TestCase(new[] {2, 3, 4}, 24)]
    [TestCase(new[] {99, 73, 0, 16, 11}, 0)]
    public void CalculatesProduct(int[] values, int expectedProduct)
    {
      Assert.That(values.Product(), Is.EqualTo(expectedProduct));
    }
  }
}
