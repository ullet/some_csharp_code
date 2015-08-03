/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using NUnit.Framework;

namespace Ullet.Strix.Enumerable.Tests.Unit.EnumerableExtensionsTests
{
  [TestFixture]
  public class RepeatTests
  {
    [Test]
    public void EmptySequenceIfRepeatZeroTimes()
    {
      Assert.That(9.Repeat(0), Is.Empty);
      Assert.That(9.Repeat(0U), Is.Empty);
    }

    [Test]
    public void EmptySequenceIfRepeatNegativeTimes()
    {
      Assert.That(9.Repeat(-3), Is.Empty);
    }

    [Test]
    public void RepeatMultipleTimes()
    {
      Assert.That(9.Repeat(3), Is.EquivalentTo(new[] {9, 9, 9}));
      Assert.That(9.Repeat(3U), Is.EquivalentTo(new[] {9, 9, 9}));
    }
  }
}
