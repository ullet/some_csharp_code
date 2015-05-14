/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using Ullet.PD.Dynamic;

namespace Ullet.PD.Tests.Unit.Dynamic.DynamicExtensionsTests
{
  [TestFixture]
  public class AnyTests : CountableDynamicExtensionsTestBase
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
}
