/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.PD.Dynamic.Tests.Unit.DynamicExtensionsTests
{
  public abstract class RaiseToPowerTests<T>
  {
    protected abstract IEnumerable<T> RaiseToPowerZeroTestCases { get; }

    protected abstract T Zero { get; }

    protected abstract T One { get; }

    [TestCaseSource("RaiseToPowerZeroTestCases")]
    public void AnyNonZeroValueRaisedToPowerZeroIsOne(T number)
    {
      Assert.That(number.RaiseToPower(0), Is.EqualTo(One));
    }

    [Test]
    public void ZeroValueRaisedToPowerZeroIsOne()
    {
      Assert.That(Zero.RaiseToPower(0), Is.EqualTo(One));
    }

    [TestCase(1u)]
    [TestCase(33u)]
    [TestCase(101u)]
    public void OneRaisedToAnyPositivePowerIsOne(uint power)
    {
      Assert.That(One.RaiseToPower(power), Is.EqualTo(One));
    }
  }
}
