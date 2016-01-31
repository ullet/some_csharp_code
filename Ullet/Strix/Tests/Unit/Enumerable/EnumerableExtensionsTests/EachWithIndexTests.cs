/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.Strix.Enumerable.Tests.Unit.EnumerableExtensionsTests
{
  [TestFixture]
  public class EachWithIndexTests
  {
    [Test]
    public void EachWithIndexIsAliasForForEachWithIndex()
    {
      IEnumerable<int> enumerable = new[] {1, 2, 3};
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.EachWithIndex(
        (t, i) =>
        {
          enumeratedItems.Add(t);
          enumeratedIndexes.Add(i);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] {0, 1, 2}));
    }
  }
}
