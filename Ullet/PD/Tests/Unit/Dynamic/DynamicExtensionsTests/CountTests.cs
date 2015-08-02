/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace Ullet.PD.Dynamic.Tests.Unit.DynamicExtensionsTests
{
  [TestFixture]
  public class CountTests : CountableDynamicExtensionsTestBase
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
}
