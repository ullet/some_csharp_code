/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ullet.Strix.Enumerable.Tests.Unit.EnumerableExtensionsTests
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
        (t, i) =>
        {
          enumeratedItems.Add(t);
          enumeratedIndexes.Add(i);
        });

      Assert.That(enumeratedItems.ToArray(), Is.EqualTo(enumerable));
      Assert.That(enumeratedIndexes.ToArray(), Is.EqualTo(new[] {0, 1, 2}));
    }

    [Test]
    public void WithObject_EnumeratesAllItems()
    {
      var list = new List<int> {3, 6, 1, 8};
      var enumerated = new List<int>();

      list.ForEach(new object(), (x, o) => enumerated.Add(x));

      Assert.That(enumerated, Is.EqualTo(list));
    }

    [Test]
    public void WithObject_ObjectCanBeNull()
    {
      var list = new List<int> { 3, 6, 1, 8 };

      Assert.DoesNotThrow(() => list.ForEach((object) null, (x, o) => { }));
    }

    [Test]
    public void WithObject_InputObjectUsedWithAllItems()
    {
      var list = new List<int> { 3, 6, 1, 8 };
      var inputObject = new object();
      var usedObjects = new List<object>();

      list.ForEach(inputObject, (x, o) => usedObjects.Add(o));

      Assert.That(usedObjects, Has.All.SameAs(inputObject));
    }

    [Test]
    public void WithObject_ReturnsInputObject()
    {
      var list = new List<int> { 3, 6, 1, 8 };
      var inputObject = new object();

      var returnedObject = list.ForEach(inputObject, (x, o) => { });

      Assert.That(returnedObject, Is.SameAs(inputObject));
    }

    [Test]
    public void WithObject_ActionCanMutateObject()
    {
      var list = new List<int> { 3, 6, 1, 8 };

      var collected = list.ForEach(new List<int>(), (x, o) => o.Add(x));

      Assert.That(collected, Is.EqualTo(list));
    }
  }
}
