/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Linq;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable
{
  [TestFixture]
  public class SignedRangeTests
  {
    [Test]
    public void CanEnumerateRangeUpToValue()
    {
      Assert.That(7.UpTo(11).ToArray(), Is.EqualTo(new[] {7, 8, 9, 10, 11}));
    }

    [Test]
    public void EmptyIfRangeUpToLessThanFrom()
    {
      Assert.That(7.UpTo(5), Is.Empty);
    }

    [Test]
    public void CanEnumerateRangeUpToValueByPositiveStep()
    {
      Assert.That(7.UpTo(11).Step(2).ToArray(), Is.EqualTo(new[] {7, 9, 11}));
    }

    [Test]
    public void EmptyIfRangeUpToLessThanFromWithNegativeStep()
    {
      Assert.That(7.UpTo(5).Step(-1), Is.Empty);
    }

    [Test]
    public void DefaultUpToStepOneIfZeroStep()
    {
      Assert.That(5.UpTo(7).Step(0).ToArray(), Is.EqualTo(new[]{5,6,7}));
    }

    [Test]
    public void CanEnumerateRangeDownToValue()
    {
      Assert.That(11.DownTo(7).ToArray(), Is.EqualTo(new[] {11, 10, 9, 8, 7}));
    }

    [Test]
    public void EmptyIfRangeDownToGreaterThanFrom()
    {
      Assert.That(5.DownTo(7), Is.Empty);
    }

    [Test]
    public void CanEnumerateRangeDownToValueByNegativeStep()
    {
      Assert.That(
        11.DownTo(7).Step(-2).ToArray(),
        Is.EqualTo(new[] {11, 9, 7}));
    }

    [Test]
    public void EmptyIfRangeDownToGreaterThanFromWithPositiveStep()
    {
      Assert.That(5.DownTo(7).Step(1), Is.Empty);
    }

    [Test]
    public void DefaultDownToStepMinusOneIfZeroStep()
    {
      Assert.That(7.DownTo(5).Step(0).ToArray(), Is.EqualTo(new[] {7, 6, 5}));
    }

    [Test]
    public void CanEnumerateIncrementingRangeFromValueToValueWithStep()
    {
      Assert.That(
        Range.From(3).To(15).Step(4).ToArray(),
        Is.EqualTo(new[] { 3, 7, 11, 15 }));
    }

    [Test]
    public void CanEnumerateDecrementingRangeFromValueToValueWithNegativeStep()
    {
      Assert.That(
        Range.From(15).To(3).Step(-4).ToArray(),
        Is.EqualTo(new[] { 15, 11, 7, 3 }));
    }

    [Test]
    public void CanEnumerateRangeToGreaterValue()
    {
      Assert.That(3.To(7).ToArray(), Is.EqualTo(new[] { 3, 4, 5, 6, 7 }));
    }

    [Test]
    public void CanEnumerateRangeToLowerValue()
    {
      Assert.That(7.To(3).ToArray(), Is.EqualTo(new[] { 7, 6, 5, 4, 3 }));
    }

    [Test]
    public void CanEnumerateRangeToGreaterValueWithPositiveStep()
    {
      Assert.That(3.To(7).Step(2).ToArray(), Is.EqualTo(new[] {3, 5, 7}));
    }

    [Test]
    public void CanEnumerateRangeToLowerValueWithNegativeStep()
    {
      Assert.That(7.To(3).Step(-2).ToArray(), Is.EqualTo(new[] { 7, 5, 3 }));
    }

    [Test]
    public void EmptyIfRangeToGreaterValueWithNegativeStep()
    {
      Assert.That(3.To(7).Step(-1), Is.Empty);
    }

    [Test]
    public void EmptyIfRangeToLowerValueWithPositiveStep()
    {
      Assert.That(7.To(3).Step(1), Is.Empty);
    }

    [Test]
    public void GeneratedRangeCanIncludeNegativeValues()
    {
      Assert.That(2.To(-2).Step(-2).ToArray(), Is.EqualTo(new[] { 2, 0, -2 }));
    }
  }
}
