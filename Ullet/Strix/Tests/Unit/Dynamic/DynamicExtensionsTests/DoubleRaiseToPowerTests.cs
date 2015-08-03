/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.Strix.Dynamic.Tests.Unit.DynamicExtensionsTests
{
  [TestFixture]
  public class DoubleRaiseToPowerTests : ImpreciseTypeRaiseToPowerTests<double>
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
}
