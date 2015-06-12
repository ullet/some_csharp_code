/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable.EnumerableExtensionsTests
{
  [TestFixture]
  public class DistinctByTests
  {
    [Test]
    public void SelectsOnlyDistinctByKeySelector()
    {
      Func<int, int, Tuple<int, int>> p = Tuple.Create;
      var list = new[]
        {
          p(4, 2), p(15, 4), p(3, 1), p(15, 4), p(4, 4), p(1, 0), p(1, 8)
        };

      var distinct = list.DistinctBy(t => t.Item1);

      Assert.That(
        distinct.ToArray(),
        Is.EqualTo(new[] {p(4, 2), p(15, 4), p(3, 1), p(1, 0)}));
    }

    [Test]
    public void LazyEnumerates()
    {
      var list = new[] {1, 2, 1, 3, 1, 2, 4, 1, 2, 3, 5};
      var itemsVisited = new List<int>();

      var distinct = list
        .DistinctBy(item =>
          {
            itemsVisited.Add(item);
            return item;
          })
        .Take(4);

      Assert.That(distinct.ToArray(), Is.EqualTo(new[] {1, 2, 3, 4}));
      Assert.That(
        itemsVisited,
        Is.EqualTo(new List<int> {1, 2, 1, 3, 1, 2, 4}));
    }
  }
}
