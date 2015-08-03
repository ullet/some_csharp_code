/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Number.Tests.Unit.NumberExtensionsTests
{
  [TestFixture]
  public class RaiseToPowerTests
  {
    [TestCase(1)]
    [TestCase(-1)]
    [TestCase(751)]
    [TestCase(-42)]
    public void AnyNonZeroIntegerRaisedToPowerZeroIsOne(int number)
    {
      Assert.That(number.RaiseToPower(0), Is.EqualTo(1));
    }

    [Test]
    public void ZeroRaisedToPowerZeroIsOne()
    {
      Assert.That(0.RaiseToPower(0), Is.EqualTo(1));
    }

    [TestCase(1u)]
    [TestCase(33u)]
    [TestCase(10001u)]
    public void OneRaisedToAnyPositivePowerIsOne(uint power)
    {
      Assert.That(1.RaiseToPower(power), Is.EqualTo(1));
    }

    [Test]
    [Timeout(100)]
    public void ExceptionThrownOnOverflow()
    {
      Assert.Throws<OverflowException>(() => Int32.MaxValue.RaiseToPower(2));
    }

    [TestCase(17, 3u, 4913)]
    [TestCase(11, 2u, 121)]
    [TestCase(64, 5u, 1073741824)]
    public void CanRaiseToPower(int number, uint power, int expected)
    {
      Assert.That(number.RaiseToPower(power), Is.EqualTo(expected));
    }
  }
}
