/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable
{
  [TestFixture]
  public class RangeEnumerableTests
  {
    [Test]
    public void DefaultRangeOfSingleZeroValue()
    {
      Assert.That(new RangeEnumerable().ToArray(), Is.EqualTo(new[] {0}));
    }

    [Test]
    public void DefaultRangeToIsStart()
    {
      Assert.That(new RangeEnumerable(10).ToArray(), Is.EqualTo(new[] {10}));
    }

    [Test]
    public void DefaultStepIsPositiveOne()
    {
      Assert.That(
        new RangeEnumerable(1, 5).ToArray(),
        Is.EqualTo(new[] {1, 2, 3, 4, 5}));
    }

    [Test]
    public void CanSpecifyStepInConstructor()
    {
      Assert.That(
        new RangeEnumerable(1, 5, 2).ToArray(),
        Is.EqualTo(new[] { 1, 3, 5 }));
    }

    [Test]
    public void CanSpecifyNegativeStepInConstructor()
    {
      Assert.That(
        new RangeEnumerable(5, 1, -2).ToArray(),
        Is.EqualTo(new[] { 5, 3, 1 }));
    }

    [Test]
    public void CanDefineUsingFluentInterface()
    {
      Assert.That(
        new RangeEnumerable().From(5).To(1).Step(-2).ToArray(),
        Is.EqualTo(new[] { 5, 3, 1 }));
    }

    [Test]
    public void CanUseAsIEnumerable()
    {
      var enumerable = (IEnumerable) new RangeEnumerable(5, 1, -2);
      var enumerator = enumerable.GetEnumerator();
      var enumerated = new List<int>();
      while (enumerator.MoveNext())
      {
        enumerated.Add((int)enumerator.Current);
      }
      Assert.That(enumerated.ToArray(), Is.EqualTo(new[] {5, 3, 1}));
    }
  }
}
