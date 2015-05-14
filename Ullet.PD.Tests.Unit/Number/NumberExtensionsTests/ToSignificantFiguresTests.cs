/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using NUnit.Framework;
using Ullet.PD.Number;

namespace Ullet.PD.Tests.Unit.Number.NumberExtensionsTests
{
  [TestFixture]
  public class ToSignificantFiguresTests
  {
    [TestCaseSource("TestCases")]
    public void NumberToSignificantFigures(
      decimal inputValue, int numberOfSigFigures, decimal expectedOutputValue)
    {
      decimal actualOutputValue =
        inputValue.ToSignificantFigures(numberOfSigFigures);

      Assert.That(actualOutputValue, Is.EqualTo(expectedOutputValue));
    }

    private static IEnumerable TestCases
    {
      get
      {
        return new[]
        {
          new TestCaseData(9m, 1, 9m),
          new TestCaseData(5m, 1, 5m),
          new TestCaseData(1m, 1, 1m),
          new TestCaseData(0m, 1, 0m),
          new TestCaseData(91m, 2, 91m),
          new TestCaseData(35m, 2, 35m),
          new TestCaseData(71m, 2, 71m),
          new TestCaseData(60m, 2, 60m),
          new TestCaseData(91m, 1, 90m),
          new TestCaseData(35m, 1, 40m),
          new TestCaseData(71m, 1, 70m),
          new TestCaseData(60m, 1, 60m),
          new TestCaseData(9m, 2, 9m),
          new TestCaseData(5m, 2, 5m),
          new TestCaseData(1m, 2, 1m),
          new TestCaseData(0m, 2, 0m),
          new TestCaseData(9451m, 2, 9500m),
          new TestCaseData(549m, 1, 500m),
          new TestCaseData(703m, 5, 703m),
          new TestCaseData(0.1234m, 2, 0.12m),
          new TestCaseData(0.01234m, 2, 0.012m),
          new TestCaseData(0.10234m, 2, 0.10m),
          new TestCaseData(0.85517353m, 1, 0.9m),
          new TestCaseData(0.9999m, 3, 1.00m),
          new TestCaseData(12.34m, 2, 12m),
          new TestCaseData(1.234m, 2, 1.2m),
          new TestCaseData(1023.4m, 2, 1000m),
          new TestCaseData(85.517353m, 1, 90m),
          new TestCaseData(9.999m, 3, 10.0m),
          new TestCaseData(9.949m, 3, 9.95m),
          new TestCaseData(-12.34m, 2, -12m),
          new TestCaseData(-1.234m, 2, -1.2m),
          new TestCaseData(-1023.4m, 2, -1000m),
          new TestCaseData(-85.517353m, 1, -90m),
          new TestCaseData(-9.999m, 3, -10.0m),
          new TestCaseData(-9.949m, 3, -9.95m)
        };
      }
    }
  }
}
