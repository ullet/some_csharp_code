/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;
using Ullet.PD.Sort;

namespace Ullet.PD.Tests.Unit.Sort
{
  [TestFixture]
  public class SpecificOrderingTests
  {
    [Test]
    public void ImplementsIComparerInterface()
    {
      Assert.That(
        typeof(IComparer<object>)
          .IsAssignableFrom(typeof(SpecificOrdering<object>)),
        Is.True);
    }

    [Test]
    public void CompareByNaturalOrderingIfComparableAndEmptyOrdering()
    {
      var ordering = new SpecificOrdering<int>(new int[] { });
      Assert.That(ordering.Compare(1, 2), Is.LessThan(0));
      Assert.That(ordering.Compare(2, 1), Is.GreaterThan(0));
      Assert.That(ordering.Compare(1, 1), Is.EqualTo(0));
    }

    [Test]
    public void AllEquivalentIfNotComparableAndEmptyOrdering()
    {
      var ordering = new SpecificOrdering<int[]>(new int[][] { });
      Assert.That(ordering.Compare(new[] { 1 }, new[] { 2 }), Is.EqualTo(0));
      Assert.That(ordering.Compare(new[] { 2 }, new[] { 1 }), Is.EqualTo(0));
      Assert.That(ordering.Compare(new[] { 1 }, new[] { 1 }), Is.EqualTo(0));
    }

    [Test]
    public void EqualValuesAreAlwaysEquivalent()
    {
      var ordering1 = new SpecificOrdering<int>(new int[] { });
      var ordering2 = new SpecificOrdering<int>(new[] { 8, 4 });

      Assert.That(ordering1.Compare(8, 8), Is.EqualTo(0));
      Assert.That(ordering2.Compare(8, 8), Is.EqualTo(0));
    }

    [Test]
    public void CompareByPositionInOrderingIfBothValuesInOrdering()
    {
      var ordering = new SpecificOrdering<int>(new[] { 8, 4 });
      Assert.That(ordering.Compare(4, 8), Is.GreaterThan(0));
      Assert.That(ordering.Compare(8, 4), Is.LessThan(0));
    }

    [Test]
    public void FirstAlwaysLessThanSecondIfFirstOnlyOneInOrdering()
    {
      var ordering = new SpecificOrdering<int>(new[] { 8, 4 });
      Assert.That(ordering.Compare(4, 1), Is.LessThan(0));
      Assert.That(ordering.Compare(8, 7), Is.LessThan(0));
    }

    [Test]
    public void FirstAlwaysGreaterThanSecondIfSecondOnlyOneInOrdering()
    {
      var ordering = new SpecificOrdering<int>(new[] { 8, 4 });
      Assert.That(ordering.Compare(1, 4), Is.GreaterThan(0));
      Assert.That(ordering.Compare(7, 8), Is.GreaterThan(0));
    }

    [Test]
    public void CompareByNaturalOrderingIfNeitherInOrdering()
    {
      var ordering = new SpecificOrdering<int>(new[] { 8, 4 });
      Assert.That(ordering.Compare(5, 7), Is.LessThan(0));
      Assert.That(ordering.Compare(7, 3), Is.GreaterThan(0));
    }
  }
}
