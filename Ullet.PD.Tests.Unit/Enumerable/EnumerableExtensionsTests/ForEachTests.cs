/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable.EnumerableExtensionsTests
{
  [TestFixture]
  public class ForEachTests
  {
    [Test]
    public void ActionPerformedForEachItemInEnumerable()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var visitRecord = new List<int>();

      enumerable.ForEach(x => visitRecord.Add(x));

      Assert.That(visitRecord.SequenceEqual(enumerable), Is.True);
    }

    [Test]
    public void ForEachHasWithIndexOverload()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEach(
        (i, x) =>
        {
          enumeratedIndexes.Add(i);
          enumeratedItems.Add(x);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] {0, 1, 2}));
    }

    [Test]
    public void ForEachHasOverloadTakingActionOfItemWithIndex()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEach(
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
