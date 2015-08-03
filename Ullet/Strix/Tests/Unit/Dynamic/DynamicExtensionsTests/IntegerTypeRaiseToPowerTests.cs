/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Dynamic.Tests.Unit.DynamicExtensionsTests
{
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
}
