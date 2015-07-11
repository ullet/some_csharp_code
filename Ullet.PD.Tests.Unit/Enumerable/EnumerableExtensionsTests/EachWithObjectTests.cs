/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable.EnumerableExtensionsTests
{
  [TestFixture]
  public class EachWithObjectTests
  {
    [Test]
    public void EnumeratesAllItems()
    {
      var list = new List<int> {3, 6, 1, 8};
      var enumerated = new List<int>();

      list.EachWithObject(new object(), (x, o) => enumerated.Add(x));

      Assert.That(enumerated, Is.EqualTo(list));
    }

    [Test]
    public void ObjectCanBeNull()
    {
      var list = new List<int> { 3, 6, 1, 8 };

      Assert.DoesNotThrow(
        () => list.EachWithObject((object) null, (x, o) => { }));
    }

    [Test]
    public void InputObjectUsedWithAllItems()
    {
      var list = new List<int> { 3, 6, 1, 8 };
      var inputObject = new object();
      var usedObjects = new List<object>();

      list.EachWithObject(inputObject, (x, o) => usedObjects.Add(o));

      Assert.That(usedObjects, Has.All.SameAs(inputObject));
    }

    [Test]
    public void ReturnsInputObject()
    {
      var list = new List<int> { 3, 6, 1, 8 };
      var inputObject = new object();

      var returnedObject = list.EachWithObject(inputObject, (x, o) => { });

      Assert.That(returnedObject, Is.SameAs(inputObject));
    }

    [Test]
    public void ActionCanMutateObject()
    {
      var list = new List<int> { 3, 6, 1, 8 };

      var collected = list.EachWithObject(new List<int>(), (x, o) => o.Add(x));

      Assert.That(collected, Is.EqualTo(list));
    }
  }
}
