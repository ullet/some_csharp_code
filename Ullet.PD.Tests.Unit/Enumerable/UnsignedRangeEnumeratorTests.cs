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
  public class UnsignedRangeEnumeratorTests
  {
    [Test]
    public void MoveNextFalseForEmptyRange()
    {
      var enumerator = new UnsignedRangeEnumerator(1, 2, -1);
      Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void MoveNextTrueWhileInRange()
    {
      var enumerator = new UnsignedRangeEnumerator(1, 3, 1);
      Assert.That(enumerator.MoveNext(), Is.True);
      Assert.That(enumerator.MoveNext(), Is.True);
      Assert.That(enumerator.MoveNext(), Is.True);
      Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void EnumerateOverAllValuesInRange()
    {
      var enumerator = new UnsignedRangeEnumerator(1, 3, 1);
      var enumerated = new List<uint>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<uint> {1, 2, 3}));
    }

    [Test]
    public void EnumerateOverAllValuesInRangeWithStep()
    {
      var enumerator = new UnsignedRangeEnumerator(2, 8, 2);
      var enumerated = new List<uint>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<uint> {2, 4, 6, 8}));
    }

    [Test]
    public void EnumerateOverAllValuesInRangeWithNegativeStep()
    {
      var enumerator = new UnsignedRangeEnumerator(8, 2, -3);
      var enumerated = new List<uint>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<uint> {8, 5, 2}));
    }

    [Test]
    public void EnumerationStaysWithinBoundsOfRange()
    {
      var enumerator = new UnsignedRangeEnumerator(6, 1, -3);
      var enumerated = new List<uint>();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<int> { 6, 3 }));
    }

    [Test]
    public void IsDisposable()
    {
      Assert.DoesNotThrow(
        () => new UnsignedRangeEnumerator(6, 1, -3).Dispose());
    }

    [Test]
    public void CanReset()
    {
      var enumerator = new UnsignedRangeEnumerator(6, 0, -3);
      while (enumerator.MoveNext())
      {
      }
      var enumerated = new List<uint>();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        enumerated.Add(enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<uint> { 6, 3, 0 }));
    }

    [Test]
    public void CanUseAsIEnumerator()
    {
      IEnumerator enumerator = new UnsignedRangeEnumerator(6, 0, -3);
      var enumerated = new List<uint>();
      while (enumerator.MoveNext())
      {
        enumerated.Add((uint)enumerator.Current);
      }
      Assert.That(enumerated, Is.EqualTo(new List<uint> { 6, 3, 0 }));
    }

    [Test]
    public void CurrentThrowsExceptionBeforeFireMoveNext()
    {
      IEnumerator enumerator = new UnsignedRangeEnumerator(6, 0, -3);
      Assert.Throws<InvalidOperationException>(
        Subject.Getter(() => enumerator.Current));
    }
  }
}
