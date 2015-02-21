/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace Ullet.PD.Tests.Unit
{
  [TestFixture]
  public class DynamicExtensionsTests
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
      var countable = new Countable { Count = -1 };

      Assert.That(countable.Any(), Is.False);
    }

    [Test]
    public void AnyIsTrueIfCountPositive()
    {
      var countable = new Countable { Count = 1 };

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
      var countable = new DoubleCountable { Count = 0.123 };

      Assert.That(countable.Any(), Is.True);
    }

    [Test]
    public void AnyThrowsExceptionIfTypeOfCountNotComparableToInt()
    {
      var badCountable = new StringCountable { Count = "one" };

      Assert.Throws<RuntimeBinderException>(() => badCountable.Any());
    }

    [TestCase(-101)]
    [TestCase(0)]
    [TestCase(14)]
    public void CountReturnsCount(int count)
    {
      var countable = new Countable { Count = count };

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
      var countable = new DoubleCountable { Count = 0.123 };

      Assert.That(countable.Count(), Is.EqualTo(0));
    }

    [Test]
    public void CountThrowsExceptionIfNotConvertableToInt()
    {
      var badCountable = new StringCountable {Count = "one"};

      Assert.Throws<RuntimeBinderException>(() => badCountable.Count());
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
