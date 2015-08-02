/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.PD.Dynamic.Tests.Unit.DynamicExtensionsTests
{
  [TestFixture]
  public class DecimalRaiseToPowerTests : PreciseTypeRaiseToPowerTests<decimal>
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
}
