/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Linq;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable.EnumerableExtensionsTests
{
  [TestFixture]
  public class SelectPreviousCurrentNextTests
  {
    [Test]
    public void ReturnEmptyForEmptyInput()
    {
      Assert.That(new object[]{}.Select((p, c, n) => 0), Is.Empty);
    }

    [Test]
    public void PreviousNullForFirstItemReferenceType()
    {
      var previous = new[] {new object()}.Select((p, c, n) => p).First();

      Assert.That(previous, Is.Null);
    }

    [Test]
    public void PreviousNotNullAfterFirstItemForReferenceType()
    {
      /*
       * Enumerable could, of course, enumerate null values, so previous can
       * in that case be null even for middle items, but for this test set up
       * to have all non-null values in enumeration.
       */

      var previousItems = new[] {new object(), new object(), new object()}
        .Select((p, c, n) => p).ToList();

      Assert.That(previousItems, Has.Count.EqualTo(3));
      Assert.That(previousItems.Skip(1), Has.All.Not.Null);
    }

    [Test]
    public void NextNullForLastItemForReferenceType()
    {
      var next = new[] { new object() }.Select((p, c, n) => n).First();

      Assert.That(next, Is.Null);
    }

    [Test]
    public void NextNotNullBeforeLastItemForReferenceType()
    {
      var nextItems = new[] {new object(), new object(), new object()}
        .Select((p, c, n) => n).ToList();

      Assert.That(nextItems, Has.Count.EqualTo(3));
      Assert.That(nextItems.Take(2), Has.All.Not.Null);
    }

    [Test]
    public void CurrentNotNullForAllItemsForReferenceType()
    {
      var currentItems = new[] {new object(), new object(), new object()}
        .Select((p, c, n) => c).ToList();

      Assert.That(currentItems, Has.Count.EqualTo(3));
      Assert.That(currentItems, Has.All.Not.Null);
    }

    [Test]
    public void EnumeratedCurrentSequenceEqualToSource()
    {
      var source = new[] {"one", "two", "three"};

      var currentItems = source.Select((p, c, n) => c).ToArray();

      Assert.That(currentItems, Is.EqualTo(source));
    }

    [Test]
    public void EnumeratedPreviousSequenceEqualToSourceShiftedRight()
    {
      var source = new[] {"one", "two", "three"};
      var rightShiftedSource = new[] {null, "one", "two"};

      var previousItems = source.Select((p, c, n) => p).ToArray();

      Assert.That(previousItems, Is.EqualTo(rightShiftedSource));
    }

    [Test]
    public void EnumeratedNextSequenceEqualToSourceShiftedLeft()
    {
      var source = new[] {"one", "two", "three"};
      var rightShiftedSource = new[] {"two", "three", null};

      var previousItems = source.Select((p, c, n) => n).ToArray();

      Assert.That(previousItems, Is.EqualTo(rightShiftedSource));
    }

    [Test]
    public void PreviousNullForFirstItemNullableType()
    {
      var previous = new[] { (int?)7 }.Select((p, c, n) => p).First();

      Assert.That(previous, Is.Null);
    }

    [Test]
    public void NextNullForLastItemNullableType()
    {
      var next = new[] { (int?)7 }.Select((p, c, n) => n).First();

      Assert.That(next, Is.Null);
    }

    [Test]
    public void PreviousDefaultValueForFirstItemValueType()
    {
      var previous = new[] { 7 }.Select((p, c, n) => p).First();

      Assert.That(previous, Is.EqualTo(default(Int32)));
    }

    [Test]
    public void NextDefaultValueForLastItemValueType()
    {
      var next = new[] { 7 }.Select((p, c, n) => n).First();

      Assert.That(next, Is.EqualTo(default(Int32)));
    }
  }
}
