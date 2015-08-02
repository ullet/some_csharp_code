/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Ullet.PD.Dynamic.Tests.Unit.DynamicExtensionsTests
{
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
}
