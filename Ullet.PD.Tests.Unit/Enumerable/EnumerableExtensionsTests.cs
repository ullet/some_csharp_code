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

namespace Ullet.PD.Tests.Unit.Enumerable
{
  public class EnumerableExtensionsTests
  {
    [TestFixture]
    public class AppendTests
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
        var enumerable = new[] {1, 2, 3};

        var extendedEnumerable = enumerable.Append(4, 7, -19);

        var items = extendedEnumerable.ToArray();
        Assert.That(items, Is.EqualTo(new[] {1, 2, 3, 4, 7, -19}));
      }
    }

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
    }

    [TestFixture]
    public class WithIndexTests
    {
      [Test]
      public void CanMapToEnumerationOfItemsWithIndex()
      {
        IEnumerable<char> enumerable = new[] { 'A', 'B', 'C' };

        IList<ItemWithIndex<char>> mapped = enumerable.WithIndex().ToList();

        var indexes = mapped.Select(m => m.Index).ToArray();
        var items = mapped.Select(m => m.Item).ToArray();
        Assert.That(indexes, Is.EqualTo(new[] { 0, 1, 2 }));
        Assert.That(items, Is.EqualTo(enumerable));
      }
    }

    [TestFixture]
    public class ForEachWithIndexTests
    {
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
        Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
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
    }

    [TestFixture]
    public class EachTests
    {
      [Test]
      public void EachIsAliasForForEach()
      {
        IEnumerable<int> enumerable = new[] { 1, 2, 3 };
        var visitRecord = new List<int>();

        enumerable.Each(x => visitRecord.Add(x));

        Assert.That(visitRecord.SequenceEqual(enumerable), Is.True);
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

      [Test]
      public void EachHasWithIndexOverload()
      {
        IEnumerable<int> enumerable = new[] { 1, 2, 3 };
        var enumeratedItems = new List<int>();
        var enumeratedIndexes = new List<int>();

        enumerable.Each(
          (i, x) =>
          {
            enumeratedIndexes.Add(i);
            enumeratedItems.Add(x);
          });

        Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
        Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] { 0, 1, 2 }));
      }
    }

    [TestFixture]
    public class EachWithIndexTests
    {
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
    }

    [TestFixture]
    public class ProductTests
    {
      [Test]
      public void ExceptionIfNull()
      {
        Assert.Throws(
          Is.InstanceOf<Exception>(),
          () => ((int[]) null).Product());
      }

      [Test]
      public void ExceptionIfEmpty()
      {
        Assert.Throws(
          Is.InstanceOf<Exception>(),
          () => (new int[] {}).Product());
      }

      [TestCase(new[] { 1 }, 1)]
      [TestCase(new[] { 1, 1, 1 }, 1)]
      [TestCase(new[] { 2, 3, 4 }, 24)]
      [TestCase(new[] { 99, 73, 0, 16, 11 }, 0)]
      public void CalculatesProduct(int[] values, int expectedProduct)
      {
        Assert.That(values.Product(), Is.EqualTo(expectedProduct));
      }
    }

    [TestFixture]
    public class CheckedProductTests
    {
      [Test]
      public void ExceptionIfNull()
      {
        Assert.Throws(
          Is.InstanceOf<Exception>(),
          () => ((int[])null).CheckedProduct());
      }

      [Test]
      public void ExceptionIfEmpty()
      {
        Assert.Throws(
          Is.InstanceOf<Exception>(),
          () => (new int[] { }).CheckedProduct());
      }

      [TestCase(new[] { 1 }, 1)]
      [TestCase(new[] { 1, 1, 1 }, 1)]
      [TestCase(new[] { 2, 3, 4 }, 24)]
      [TestCase(new[] { 99, 73, 0, 16, 11 }, 0)]
      public void CalculatesProduct(int[] values, int expectedProduct)
      {
        Assert.That(values.CheckedProduct(), Is.EqualTo(expectedProduct));
      }

      [Test]
      public void ExceptionIfOverflow()
      {
        Assert.Throws(
          Is.InstanceOf<OverflowException>(),
          () => (new[] { Int32.MaxValue, 2 }).CheckedProduct());
      }
    }

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

      [Test]
      [Explicit("Very slow test")]
      public void RepeatMaxInt32Times()
      {
        Assert.That(
          9.Repeat(Int32.MaxValue).Count(x => x == 9),
          Is.EqualTo(Int32.MaxValue));
      }

      [Test]
      [Explicit("Very slow test")]
      public void RepeatMaxUInt32Times()
      {
        Assert.That(
          9.Repeat(UInt32.MaxValue).BigCount(x => x == 9),
          Is.EqualTo(UInt32.MaxValue));
      }
    }

    [TestFixture]
    public class BigCountTests
    {
      [Test]
      public void EmptySequenceCountIsZero()
      {
        Assert.That(new object[] {}.BigCount(), Is.EqualTo(0UL));
      }

      [Test]
      public void NullSequenceThrowsException()
      {
        Assert.Throws<NullReferenceException>(
          () => ((object[]) null).BigCount());
      }

      [TestCase(1)]
      [TestCase(10)]
      [TestCase(100)]
      [TestCase(1000000)]
      public void CountSequence(int itemsInSequence)
      {
        Assert.That(
          new Object().Repeat(itemsInSequence).BigCount(),
          Is.EqualTo(itemsInSequence));
      }
    }
  }
}
