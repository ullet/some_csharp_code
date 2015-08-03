/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using NUnit.Framework;

namespace Ullet.Strix.Number.Tests.Unit.NumberExtensionsTests
{
  [TestFixture]
  public class FormatToSignificantFiguresTests
  {
    [TestCaseSource("TestCases")]
    public void NumberToSignificantFiguresDefaultInvariantCulture(
      decimal inputValue, int numberOfSigFigures, string expectedOutputValue)
    {
      string actualOutputValue =
        inputValue.FormatToSignificantFigures(numberOfSigFigures);

      Assert.That(actualOutputValue, Is.EqualTo(expectedOutputValue));
    }

    private static IEnumerable TestCases
    {
      get
      {
        return new[]
        {
          new TestCaseData(9m, 1, "9"),
          new TestCaseData(5m, 1, "5"),
          new TestCaseData(1m, 1, "1"),
          new TestCaseData(0m, 1, "0"),
          new TestCaseData(91m, 2, "91"),
          new TestCaseData(35m, 2, "35"),
          new TestCaseData(71m, 2, "71"),
          new TestCaseData(60m, 2, "60"),
          new TestCaseData(91m, 1, "90"),
          new TestCaseData(35m, 1, "40"),
          new TestCaseData(71m, 1, "70"),
          new TestCaseData(60m, 1, "60"),
          new TestCaseData(9m, 2, "9.0"),
          new TestCaseData(5m, 2, "5.0"),
          new TestCaseData(1m, 2, "1.0"),
          new TestCaseData(0m, 2, "0.0"),
          new TestCaseData(9451m, 2, "9500"),
          new TestCaseData(549m, 1, "500"),
          new TestCaseData(703m, 5, "703.00"),
          new TestCaseData(0.1234m, 2, "0.12"),
          new TestCaseData(0.01234m, 2, "0.012"),
          new TestCaseData(0.10234m, 2, "0.10"),
          new TestCaseData(0.85517353m, 1, "0.9"),
          new TestCaseData(0.9999m, 3, "1.00"),
          new TestCaseData(12.34m, 2, "12"),
          new TestCaseData(1.234m, 2, "1.2"),
          new TestCaseData(1023.4m, 2, "1000"),
          new TestCaseData(85.517353m, 1, "90"),
          new TestCaseData(9.999m, 3, "10.0"),
          new TestCaseData(9.949m, 3, "9.95"),
          new TestCaseData(-12.34m, 2, "-12"),
          new TestCaseData(-1.234m, 2, "-1.2"),
          new TestCaseData(-1023.4m, 2, "-1000"),
          new TestCaseData(-85.517353m, 1, "-90"),
          new TestCaseData(-9.999m, 3, "-10.0"),
          new TestCaseData(-9.949m, 3, "-9.95")
        };
      }
    }
  }
}
