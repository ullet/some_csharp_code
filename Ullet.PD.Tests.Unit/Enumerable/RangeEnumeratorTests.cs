/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Enumerable
{
  [TestFixture]
  public class RangeEnumeratorTests
  {
    [Test]
    public void MoveNextFalseForEmptyRange()
    {
      var enumerator = new RangeEnumerator(1, 2, -1);
      Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void MoveNextTrueWhileInRange()
    {
      var enumerator = new RangeEnumerator(1, 3, 1);
      Assert.That(enumerator.MoveNext(), Is.True);
      Assert.That(enumerator.MoveNext(), Is.True);
      Assert.That(enumerator.MoveNext(), Is.True);
      Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void EnumerateOverAllValuesInRange()
    {
      var enumerator = new RangeEnumerator(1, 3, 1);
      var enumerated = new List<int>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> {1, 2, 3}));
    }

    [Test]
    public void EnumerateOverAllValuesInRangeWithStep()
    {
      var enumerator = new RangeEnumerator(2, 8, 2);
      var enumerated = new List<int>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> {2, 4, 6, 8}));
    }

    [Test]
    public void EnumerateOverAllValuesInRangeWithNegativeStep()
    {
      var enumerator = new RangeEnumerator(6, -3, -3);
      var enumerated = new List<int>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> {6, 3, 0, -3}));
    }

    [Test]
    public void EnumerationStaysWithinBoundsOfRange()
    {
      var enumerator = new RangeEnumerator(6, -2, -3);
      var enumerated = new List<int>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> { 6, 3, 0 }));
    }

    [Test]
    public void IsDisposable()
    {
      Assert.DoesNotThrow(() => new RangeEnumerator(6, -2, -3).Dispose());
    }

    [Test]
    public void CanReset()
    {
      var enumerator = new RangeEnumerator(6, -3, -3);
      while (enumerator.MoveNext())
      {
      }
      var enumerated = new List<int>();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> { 6, 3, 0, -3 }));
    }

    [Test]
    public void CanUseAsIEnumerator()
    {
      IEnumerator enumerator = new RangeEnumerator(6, -3, -3);
      var enumerated = new List<int>();
      while (enumerator.MoveNext())
      {
        enumerated.Add((int)enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> { 6, 3, 0, -3 }));
    }

    [Test]
    public void CurrentThrowsExceptionBeforeFireMoveNext()
    {
      IEnumerator enumerator = new RangeEnumerator(6, -3, -3);
      Assert.Throws<InvalidOperationException>(
        Subject.Getter(() => enumerator.Current));
    }
  }
}
