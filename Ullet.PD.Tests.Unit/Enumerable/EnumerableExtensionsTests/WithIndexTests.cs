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
  public class WithIndexTests
  {
    [Test]
    public void CanMapToEnumerationOfItemsWithIndex()
    {
      IEnumerable<char> enumerable = new[] {'A', 'B', 'C'};

      IList<ItemWithIndex<char>> mapped = enumerable.WithIndex().ToList();

      var indexes = mapped.Select(m => m.Index).ToArray();
      var items = mapped.Select(m => m.Item).ToArray();
      Assert.That(indexes, Is.EqualTo(new[] {0, 1, 2}));
      Assert.That(items, Is.EqualTo(enumerable));
    }
  }
}
