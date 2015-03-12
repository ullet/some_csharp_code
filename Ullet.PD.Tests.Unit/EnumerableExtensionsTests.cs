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

    [Test]
    public void EachIsAliasForForEach()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var visitRecord = new List<int>();

      enumerable.Each(x => visitRecord.Add(x));

      Assert.That(visitRecord.SequenceEqual(enumerable), Is.True);
    }

    [Test]
    public void ActionPerformedForEachItemInEnumerableWithIndex()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
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
    public void EachWithIndexIsAliasForForEachWithIndex()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.EachWithIndex(
        (i, x) =>
        {
          enumeratedIndexes.Add(i);
          enumeratedItems.Add(x);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }

    [Test]
    public void ForEachHasWithIndexOverload()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEach(
        (i, x) =>
        {
          enumeratedIndexes.Add(i);
          enumeratedItems.Add(x);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }

    [Test]
    public void EachHasWithIndexOverload()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEach(
        (i, x) =>
        {
          enumeratedIndexes.Add(i);
          enumeratedItems.Add(x);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }

    [Test]
    public void CanMapToEnumerationOfItemsWithIndex()
    {
      IEnumerable<char> enumerable = new[] { 'A', 'B', 'C' };

      IList<ItemWithIndex<char>> mapped = enumerable.WithIndex().ToList();

      var indexes = mapped.Select(m => m.Index).ToArray();
      var items = mapped.Select(m => m.Item).ToArray();
      Assert.That(indexes, Is.EqualTo(new[] {0, 1, 2}));
      Assert.That(items, Is.EqualTo(enumerable));
    }

    [Test]
    public void ForEachWithIndexHasOverloadTakingActionOfItemWithIndex()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEachWithIndex(
        o =>
        {
          enumeratedIndexes.Add(o.Index);
          enumeratedItems.Add(o.Item);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }

    [Test]
    public void EachWithIndexHasOverloadTakingActionOfItemWithIndex()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.EachWithIndex(
        o =>
        {
          enumeratedIndexes.Add(o.Index);
          enumeratedItems.Add(o.Item);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }

    [Test]
    public void ForEachHasOverloadTakingActionOfItemWithIndex()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.ForEach(
        o =>
        {
          enumeratedIndexes.Add(o.Index);
          enumeratedItems.Add(o.Item);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }

    [Test]
    public void EachHasOverloadTakingActionOfItemWithIndex()
    {
      IEnumerable<int> enumerable = new[] { 1, 2, 3 };
      var enumeratedItems = new List<int>();
      var enumeratedIndexes = new List<int>();

      enumerable.Each(
        o =>
        {
          enumeratedIndexes.Add(o.Index);
          enumeratedItems.Add(o.Item);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
    }
  }
}
