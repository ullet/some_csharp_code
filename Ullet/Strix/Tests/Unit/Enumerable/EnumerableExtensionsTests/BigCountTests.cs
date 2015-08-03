/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Enumerable.Tests.Unit.EnumerableExtensionsTests
{
  [TestFixture]
  public class BigCountTests
  {
    [Test]
    public void EmptySequenceCountIsZero()
    {
      Assert.That(new object[] {}.BigCount(), Is.EqualTo(0UL));
    }

    [Test]
    public void NullSequenceThrowsException()
    {
      Assert.Throws(
        Is.InstanceOf<Exception>(),
        () => ((object[]) null).BigCount());
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000000)]
    public void CountSequence(int itemsInSequence)
    {
      Assert.That(
        new Object().Repeat(itemsInSequence).BigCount(),
        Is.EqualTo(itemsInSequence));
    }
  }
}
