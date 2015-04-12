/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using Ullet.PD.Dynamic;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Tests.Unit.Dynamic
{
  public class DynamicExtensionsTests
  {
    [TestFixture]
    public class AnyTests
    {
      [Test]
      public void AnyIsFalseIfCountZero()
      {
        var countable = new Countable {Count = 0};

        Assert.That(countable.Any(), Is.False);
      }

      [Test]
      public void AnyIsFalseIfCountNegative()
      {
        var countable = new Countable {Count = -1};

        Assert.That(countable.Any(), Is.False);
      }

      [Test]
      public void AnyIsTrueIfCountPositive()
      {
        var countable = new Countable {Count = 1};

        Assert.That(countable.Any(), Is.True);
      }

      [Test]
      public void AnyThrowsExceptionIfDoesNotImplementCount()
      {
        var uncountable = new Uncountable();

        Assert.Throws<RuntimeBinderException>(() => uncountable.Any());
      }

      [Test]
      public void AnyIsTrueIfNonIntegerCountPositive()
      {
        var countable = new DoubleCountable {Count = 0.123};

        Assert.That(countable.Any(), Is.True);
      }

      [Test]
      public void AnyThrowsExceptionIfTypeOfCountNotComparableToInt()
      {
        var badCountable = new StringCountable {Count = "one"};

        Assert.Throws<RuntimeBinderException>(() => badCountable.Any());
      }
    }

    [TestFixture]
    public class CountTests
    {
      [TestCase(-101)]
      [TestCase(0)]
      [TestCase(14)]
      public void CountReturnsCount(int count)
      {
        var countable = new Countable {Count = count};

        Assert.That(countable.Count(), Is.EqualTo(count));
      }

      [Test]
      public void CountThrowsExceptionIfDoesNotImplementCount()
      {
        var uncountable = new Uncountable();

        Assert.Throws<RuntimeBinderException>(() => uncountable.Count());
      }

      [Test]
      public void CountConvertsToInt()
      {
        var countable = new DoubleCountable {Count = 0.123};

        Assert.That(countable.Count(), Is.EqualTo(0));
      }

      [Test]
      public void CountThrowsExceptionIfNotConvertibleToInt()
      {
        var badCountable = new StringCountable {Count = "one"};

        Assert.Throws<RuntimeBinderException>(() => badCountable.Count());
      }
    }

    public abstract class RaiseToPowerTests<T>
    {
      protected abstract IEnumerable<T> RaiseToPowerZeroTestCases { get; }

      protected abstract T Zero { get; }

      protected abstract T One { get; }

      [TestCaseSource("RaiseToPowerZeroTestCases")]
      public void AnyNonZeroValueRaisedToPowerZeroIsOne(T number)
      {
        Assert.That(number.RaiseToPower(0), Is.EqualTo(One));
      }

      [Test]
      public void ZeroValueRaisedToPowerZeroIsOne()
      {
        Assert.That(Zero.RaiseToPower(0), Is.EqualTo(One));
      }

      [TestCase(1u)]
      [TestCase(33u)]
      [TestCase(101u)]
      public void OneRaisedToAnyPositivePowerIsOne(uint power)
      {
        Assert.That(One.RaiseToPower(power), Is.EqualTo(One));
      }
    }

    public abstract class PreciseTypeRaiseToPowerTests<T> : RaiseToPowerTests<T>
    {
      protected abstract IEnumerable RaiseValueToPowerTestCases { get; }

      [TestCaseSource("RaiseValueToPowerTestCases")]
      public void CanRaiseToPower(T number, uint power, T expected)
      {
        Assert.That(number.RaiseToPower(power), Is.EqualTo(expected));
      }
    }

    public abstract class ImpreciseTypeRaiseToPowerTests<T> : RaiseToPowerTests<T>
    {
      protected abstract IEnumerable RaiseValueToPowerTestCases { get; }

      [TestCaseSource("RaiseValueToPowerTestCases")]
      public void CanRaiseToPower(
        T number, uint power, T expected, object tolerance)
      {
        Assert.That(
          number.RaiseToPower(power), 
          Is.EqualTo(expected).Within(tolerance));
      }
    }

    public abstract class IntegerTypeRaiseToPowerTests<T> 
      : PreciseTypeRaiseToPowerTests<T>
    {
      protected abstract T Max { get; }

      [Test]
      [Timeout(1000)]
      public void ExceptionThrownOnOverflow()
      {
        Assert.Throws<OverflowException>(() => Max.RaiseToPower(2));
      }
    }

    [TestFixture]
    public class LongRaiseToPowerTests : IntegerTypeRaiseToPowerTests<long>
    {
      protected override IEnumerable<long> RaiseToPowerZeroTestCases
      {
        get
        {
          return new[]
          {
            1L,
            -1L,
            751L,
            -42L
          };
        }
      }

      protected override IEnumerable RaiseValueToPowerTestCases
      {
        get
        {
          return new[]
          {
            new TestCaseData(17L, 3U, 4913L),
            new TestCaseData(11L, 2U, 121L),
            new TestCaseData(192L, 5U, 260919263232L),
            new TestCaseData(2147483648L, 2U, 4611686018427387904L),
          };
        }
      }

      protected override long Zero
      {
        get { return 0L; }
      }

      protected override long One
      {
        get { return 1L; }
      }

      protected override long Max
      {
        get { return Int64.MaxValue; }
      }
    }

    [TestFixture]
    public class DoubleRaiseToPowerTests
      : ImpreciseTypeRaiseToPowerTests<double>
    {
      protected override IEnumerable<double> RaiseToPowerZeroTestCases
      {
        get
        {
          return new[]
          {
            1D,
            -1D,
            75.1D,
            -4.2D
          };
        }
      }

      protected override IEnumerable RaiseValueToPowerTestCases
      {
        get
        {
          return new[]
          {
            new TestCaseData(0.1D, 3U, 0.001D, 0.0000001D),
            new TestCaseData(1.1D, 2U, 1.21D, 0.0000001D),
            new TestCaseData(192.17D, 5U, 262076421875.1622635857D, 0.0001D),
          };
        }
      }

      protected override double Zero
      {
        get { return 0D; }
      }

      protected override double One
      {
        get { return 1D; }
      }
    }

    [TestFixture]
    public class DecimalRaiseToPowerTests 
      : PreciseTypeRaiseToPowerTests<decimal>
    {
      protected override IEnumerable<decimal> RaiseToPowerZeroTestCases
      {
        get
        {
          return new[]
          {
            1m,
            -1m,
            75.1m,
            -4.2m
          };
        }
      }

      protected override IEnumerable RaiseValueToPowerTestCases
      {
        get
        {
          return new[]
          {
            new TestCaseData(0.1m, 3U, 0.001m),
            new TestCaseData(1.1m, 2U, 1.21m),
            new TestCaseData(192.17m, 5U, 262076421875.1622635857m),
          };
        }
      }

      protected override decimal Zero
      {
        get { return 0m; }
      }

      protected override decimal One
      {
        get { return 1m; }
      }
    }

    // Overly complex tests using an arbitrary custom type just to prove the
    // obvious point that 'dynamic' is dynamic!
    [TestFixture]
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
              ints = new [] {t.Value};
            }
          }
          // ReSharper disable once PossibleNullReferenceException
          _values = new int[ints.Length];
          ints.CopyTo(_values, 0);
        }

        public CompatibleCustomType(params int[] values) : this((object)values)
        {
        }

        public static CompatibleCustomType One
        {
          get
          {
            return new CompatibleCustomType(1);
          }
        }

        public static CompatibleCustomType Zero
        {
          get
          {
            return new CompatibleCustomType(0);
          }
        }

        public int Value
        {
          get
          {
            return _values.Product();
          }
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

    [TestFixture]
    public class NonNumericRaiseToPowerTests
    {
      [Test]
      public void ThrowsExceptionIfPowerZeroAndNoImplicitConversionFromInteger()
      {
        var ex = Assert.Throws<RuntimeBinderException>(
          () => (new NotConvertible()).RaiseToPower(0U));
        Assert.That(
          ex.Message,
          Is.StringContaining("Cannot implicitly convert type 'int'"));
      }

      [Test]
      public void ThrowsExceptionForTypeNotImplementingMultiplicationOperator()
      {
        var ex = Assert.Throws<RuntimeBinderException>(
          () => (new NotMultipliable()).RaiseToPower(3U));
        Assert.That(
          ex.Message, 
          Is.StringContaining("Operator '*' cannot be applied"));
      }

      [TestFixture]
      public class NotMultipliable
      {
        public static implicit operator NotMultipliable(int i)
        {
          return new NotMultipliable();
        }

        [Test]
        public void ImplicitOperatorImplemented()
        {
          NotMultipliable nm = 1;
          Assert.That(nm, Is.Not.Null);
        }
      }

      [TestFixture]
      public class NotConvertible
      {
        public static NotConvertible operator *(
          NotConvertible a, NotConvertible b)
        {
          return new NotConvertible();
        }

        [Test]
        public void MultiplicationOperatorImplemented()
        {
          var a = new NotConvertible();
          var b = new NotConvertible();
          Assert.That(a*b, Is.InstanceOf<NotConvertible>());
        }
      }
    }

    // Class must be public to be "seen" by runtime binder.
    public class Countable
    {
      public int Count { get; set; }
    }

    public class DoubleCountable
    {
      public double Count { get; set; }
    }

    public class Uncountable
    {
    }

    public class StringCountable
    {
      public string Count { get; set; }
    }
  }
}
