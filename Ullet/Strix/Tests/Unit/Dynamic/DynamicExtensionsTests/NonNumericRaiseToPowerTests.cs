/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace Ullet.Strix.Dynamic.Tests.Unit.DynamicExtensionsTests
{
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
}
