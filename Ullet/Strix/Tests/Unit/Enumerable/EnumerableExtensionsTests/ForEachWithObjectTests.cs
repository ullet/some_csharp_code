/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.Strix.Enumerable.Tests.Unit.EnumerableExtensionsTests
{
  [TestFixture]
  public class ForEachWithObjectTests
  {
    [Test]
    public void EnumeratesAllItems()
    {
      var list = new List<int> {3, 6, 1, 8};
      var enumerated = new List<int>();

      list.ForEachWithObject(new object(), (x, o) => enumerated.Add(x));

      Assert.That(enumerated, Is.EqualTo(list));
    }

    [Test]
    public void ObjectCanBeNull()
    {
      var list = new List<int> { 3, 6, 1, 8 };

      Assert.DoesNotThrow(
        () => list.ForEachWithObject((object) null, (x, o) => { }));
    }

    [Test]
    public void InputObjectUsedWithAllItems()
    {
      var list = new List<int> { 3, 6, 1, 8 };
      var inputObject = new object();
      var usedObjects = new List<object>();

      list.ForEachWithObject(inputObject, (x, o) => usedObjects.Add(o));

      Assert.That(usedObjects, Has.All.SameAs(inputObject));
    }

    [Test]
    public void ReturnsInputObject()
    {
      var list = new List<int> { 3, 6, 1, 8 };
      var inputObject = new object();

      var returnedObject = list.ForEachWithObject(inputObject, (x, o) => { });

      Assert.That(returnedObject, Is.SameAs(inputObject));
    }

    [Test]
    public void ActionCanMutateObject()
    {
      var list = new List<int> { 3, 6, 1, 8 };

      var collected =
        list.ForEachWithObject(new List<int>(), (x, o) => o.Add(x));

      Assert.That(collected, Is.EqualTo(list));
    }
  }
}
