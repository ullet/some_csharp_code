/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using NUnit.Framework;

namespace Ullet.Strix.Dynamic.Tests.Unit.DynamicExtensionsTests
{
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
}
