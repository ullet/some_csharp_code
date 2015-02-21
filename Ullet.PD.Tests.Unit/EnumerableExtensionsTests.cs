/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ullet.PD.Tests.Unit
{
  [TestFixture]
  public class EnumerableExtensionsTests
  {
    [Test]
    public void AppendedItemIsEnumerated()
    {
      var enumerable = new[] {1, 2, 3};

      var extendedEnumerable = enumerable.Append(4);

      var items = extendedEnumerable.ToArray();
      Assert.That(items, Is.EqualTo(new[] {1, 2, 3, 4}));
    }

    [Test]
    public void AppendedItemsAreEnumerated()
    {
      var enumerable = new[] { 1, 2, 3 };

      var extendedEnumerable = enumerable.Append(4, 7, -19);

      var items = extendedEnumerable.ToArray();
      Assert.That(items, Is.EqualTo(new[] { 1, 2, 3, 4, 7, -19 }));
    }

    [Test]
    public void ActionPerformedForEachItemInEnumerable()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var visitRecord = new List<int>();

      enumerable.ForEach(x => visitRecord.Add(x));

      Assert.That(visitRecord.SequenceEqual(enumerable), Is.True);
    }
  }
}
