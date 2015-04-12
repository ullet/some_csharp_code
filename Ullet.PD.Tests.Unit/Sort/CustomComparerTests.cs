/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ullet.PD.Sort;

namespace Ullet.PD.Tests.Unit.Sort
{
  [TestFixture]
  public class CustomComparerTests
  {
    [Test]
    public void ThrowsExceptionIfFunctionDelegateNotSpecified()
    {
      Assert.Throws<ArgumentNullException>(
         () => CustomComparer.Create<object>(null));
    }

    [TestCase(-1)]
    [TestCase(1)]
    [TestCase(0)]
    public void ReturnsResultOfDelegate(int compareDelegateReturnValue)
    {
      var comparer =
        CustomComparer.Create<int>((x, y) => compareDelegateReturnValue);

      // sample set to represent all possible values
      var allChars = new[] { 'A', 'Z', '@' };
      var allCharPairs =
        allChars.SelectMany(x => allChars.Select(y => new { x, y })).ToList();

      Assert.That(
        allCharPairs.Select(p => comparer.Compare(p.x, p.y)).ToArray(),
        Has.All.EqualTo(compareDelegateReturnValue));
    }

    [Test]
    public void CanCreateWithDynamic()
    {
      IComparer<dynamic> dynamicComparer =
        CustomComparer.Create<dynamic>((x, y) => x.CompareTo(y));

      Assert.That(dynamicComparer, Is.Not.Null);
      Assert.That(dynamicComparer.Compare(1001, 999), Is.GreaterThan(0));
      Assert.That(dynamicComparer.Compare("Apple", "Orange"), Is.LessThan(0));
      Assert.That(dynamicComparer.Compare('X', 'X'), Is.EqualTo(0));
      Assert.That(dynamicComparer.Compare(1L, 1), Is.EqualTo(0));
      Assert.That(dynamicComparer.Compare(42.32, 14L), Is.GreaterThan(0));
      Assert.Throws(
        Is.InstanceOf<Exception>(),
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        () => dynamicComparer.Compare("cat", 42),
        "Should only be able to compare 'comparable' objects");
    }
  }
}
