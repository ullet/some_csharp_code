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

  [TestFixture]
  public class UnsignedRangeTests
  {
    [Test]
    public void CanEnumerateRangeUpToValue()
    {
      Assert.That(
        7U.UpTo(11U).ToArray(), 
        Is.EqualTo(new[] { 7U, 8U, 9U, 10U, 11U }));
    }

    [Test]
    public void EmptyIfRangeUpToLessThanFrom()
    {
      Assert.That(7.UpTo(5), Is.Empty);
    }

    [Test]
    public void CanEnumerateRangeUpToValueByPositiveStep()
    {
      Assert.That(
        7U.UpTo(11U).Step(2).ToArray(),
        Is.EqualTo(new[] { 7U, 9U, 11U }));
    }

    [Test]
    public void EmptyIfRangeUpToLessThanFromWithNegativeStep()
    {
      Assert.That(7.UpTo(5).Step(-1), Is.Empty);
    }

    [Test]
    public void DefaultUpToStepOneIfZeroStep()
    {
      Assert.That(
        5U.UpTo(7U).Step(0).ToArray(),
        Is.EqualTo(new[] { 5U, 6U, 7U }));
    }

    [Test]
    public void CanEnumerateRangeDownToValue()
    {
      Assert.That(
        11U.DownTo(7U).ToArray(),
        Is.EqualTo(new[] { 11U, 10U, 9U, 8U, 7U }));
    }

    [Test]
    public void EmptyIfRangeDownToGreaterThanFrom()
    {
      Assert.That(5U.DownTo(7U), Is.Empty);
    }

    [Test]
    public void CanEnumerateRangeDownToValueByNegativeStep()
    {
      Assert.That(
        11U.DownTo(7U).Step(-2).ToArray(),
        Is.EqualTo(new[] { 11U, 9U, 7U }));
    }

    [Test]
    public void EmptyIfRangeDownToGreaterThanFromWithPositiveStep()
    {
      Assert.That(5U.DownTo(7U).Step(1), Is.Empty);
    }

    [Test]
    public void DefaultDownToStepMinusOneIfZeroStep()
    {
      Assert.That(
        7U.DownTo(5U).Step(0).ToArray(),
        Is.EqualTo(new[] { 7U, 6U, 5U }));
    }

    [Test]
    public void CanEnumerateIncrementingRangeFromValueToValueWithStep()
    {
      Assert.That(
        Range.From(3U).To(15U).Step(4).ToArray(),
        Is.EqualTo(new[] { 3U, 7U, 11U, 15U }));
    }

    [Test]
    public void CanEnumerateDecrementingRangeFromValueToValueWithNegativeStep()
    {
      Assert.That(
        Range.From(15U).To(3U).Step(-4).ToArray(),
        Is.EqualTo(new[] { 15U, 11U, 7U, 3U }));
    }

    [Test]
    public void CanEnumerateRangeToGreaterValue()
    {
      Assert.That(
        3U.To(7U).ToArray(),
        Is.EqualTo(new[] { 3U, 4U, 5U, 6U, 7U }));
    }

    [Test]
    public void CanEnumerateRangeToLowerValue()
    {
      Assert.That(
        7U.To(3U).ToArray(),
        Is.EqualTo(new[] { 7U, 6U, 5U, 4U, 3U }));
    }

    [Test]
    public void CanEnumerateRangeToGreaterValueWithPositiveStep()
    {
      Assert.That(
        3U.To(7U).Step(2).ToArray(),
        Is.EqualTo(new[] { 3U, 5U, 7U }));
    }

    [Test]
    public void CanEnumerateRangeToLowerValueWithNegativeStep()
    {
      Assert.That(
        7U.To(3U).Step(-2).ToArray(),
        Is.EqualTo(new[] { 7U, 5U, 3U }));
    }

    [Test]
    public void EmptyIfRangeToGreaterValueWithNegativeStep()
    {
      Assert.That(3U.To(7U).Step(-1), Is.Empty);
    }

    [Test]
    public void EmptyIfRangeToLowerValueWithPositiveStep()
    {
      Assert.That(7U.To(3U).Step(1), Is.Empty);
    }
  }
}
