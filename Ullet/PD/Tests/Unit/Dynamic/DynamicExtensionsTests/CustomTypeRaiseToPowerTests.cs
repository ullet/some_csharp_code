/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Dynamic.Tests.Unit.DynamicExtensionsTests
{
  // Overly complex tests using an arbitrary custom type just to prove the
  // obvious point that 'dynamic' is dynamic!  [TestFixture]
  public class CustomTypeRaiseToPowerTests
    : PreciseTypeRaiseToPowerTests<
        CustomTypeRaiseToPowerTests.CompatibleCustomType>
  {
#pragma warning disable 659 // Only used in tests, don't need GetHashCode()
    public class CompatibleCustomType
    {
      private readonly int[] _values;

      public CompatibleCustomType(object o)
      {
        var ints = o as int[];
        if (ints == null)
        {
          var t = o as CompatibleCustomType;
          if (t != null)
          {
            ints = new[] {t.Value};
          }
        }
        // ReSharper disable once PossibleNullReferenceException
        _values = new int[ints.Length];
        ints.CopyTo(_values, 0);
      }

      public CompatibleCustomType(params int[] values) : this((object) values)
      {
      }

      public static CompatibleCustomType One
      {
        get { return new CompatibleCustomType(1); }
      }

      public static CompatibleCustomType Zero
      {
        get { return new CompatibleCustomType(0); }
      }

      public int Value
      {
        get { return _values.Product(); }
      }

      public static CompatibleCustomType operator *(
        CompatibleCustomType a, CompatibleCustomType b)
      {
        return new CompatibleCustomType(a.Value, b.Value);
      }

      public static implicit operator CompatibleCustomType(int x)
      {
        return new CompatibleCustomType(x);
      }

      public override bool Equals(object obj)
      {
        return obj != null &&
               (ReferenceEquals(this, obj) ||
                Equals(new CompatibleCustomType(obj)));
      }

      public bool Equals(CompatibleCustomType t)
      {
        return t != null && (ReferenceEquals(this, t) || Value == t.Value);
      }

      public override string ToString()
      {
        return "[" + string.Join(",", _values) + "]";
      }
    }
#pragma warning restore 659

    protected override
      IEnumerable<CompatibleCustomType> RaiseToPowerZeroTestCases
    {
      get
      {
        return new[]
        {
          new CompatibleCustomType(1),
          new CompatibleCustomType(73),
          new CompatibleCustomType(-101)
        };
      }
    }

    protected override IEnumerable RaiseValueToPowerTestCases
    {
      get
      {
        return new[]
        {
          new TestCaseData(
            new CompatibleCustomType(1), 3U, new CompatibleCustomType(1)),
          new TestCaseData(
            new CompatibleCustomType(100), 0U, new CompatibleCustomType(1)),
          new TestCaseData(
            new CompatibleCustomType(7),
            3U,
            new CompatibleCustomType(7, 7, 7))
        };
      }
    }

    protected override CompatibleCustomType Zero
    {
      get { return CompatibleCustomType.Zero; }
    }

    protected override CompatibleCustomType One
    {
      get { return CompatibleCustomType.One; }
    }
  }
}
