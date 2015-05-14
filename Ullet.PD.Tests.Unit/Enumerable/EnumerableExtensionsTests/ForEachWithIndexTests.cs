/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable.EnumerableExtensionsTests
{
  [TestFixture]
  public class ForEachWithIndexTests
  {
    [Test]
    public void ActionPerformedForEachItemInEnumerableWithIndex()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEachWithIndex(
        (i, x) =>
        {
          enumeratedIndexes.Add(i);
          enumeratedItems.Add(x);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] {0, 1, 2}));
    }

    [Test]
    public void ForEachWithIndexHasOverloadTakingActionOfItemWithIndex()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEachWithIndex(
        o =>
        {
          enumeratedIndexes.Add(o.Index);
          enumeratedItems.Add(o.Item);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] {0, 1, 2}));
    }
  }
}
