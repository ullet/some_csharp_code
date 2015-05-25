/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Globalization;
using Ullet.PD.Enumerable;

namespace Ullet.PD.Number
{
  /// <summary>
  /// Extension methods for numeric types.
  /// </summary>
  public static class NumberExtensions
  {
    /// <summary>
    /// Round value to specified number of signifant figures.
    /// </summary>
    /// <remarks>
    /// Extra processing may be needed if intend to convert the output of this
    /// function to a string showing the correct number of significant figures.
    /// Zero significant figures after decimal places will not be correctly
    /// displayed if simply call ToString() on the return value.
    /// </remarks>
    public static decimal ToSignificantFigures(this decimal number, int figures)
    {
      if (number == 0)
        return 0;
      var shift = 1 + MostSignificantDigitNumber(number) - figures;
      return Math.Round(number.Shift(shift), 0).Shift(-shift);
    }

    /// <summary>
    /// Convert number to string rounded to specified number of signifant
    /// figures.
    /// </summary>
    public static string FormatToSignificantFigures(
      this decimal number,
      int figures,
      IFormatProvider formatProvider = null)
    {
      var rounded = number.ToSignificantFigures(figures);
      var decimalPlaces =
        Math.Max(0, figures - MostSignificantDigitNumber(rounded) - 1);
      var formatString =
        decimalPlaces == 0 ? "0" : "0." + new String('0', decimalPlaces);
      return rounded.ToString(
        formatString,
        formatProvider ?? CultureInfo.InvariantCulture);
    }

    // Digits numbered as:
    // ...   2   1   0   -1   -2  ...
    // ... 100s 10s 1s 1/10 1/100 ...
    private static int MostSignificantDigitNumber(decimal number)
    {
      return number == 0
        ? 0
        : (int) Math.Floor(Math.Log10(Math.Abs((double) number)));
    }

    private static decimal Shift(this decimal value, int shift)
    {
      return shift < 1
        ? value.LeftShift((uint) -shift)
        : value.RightShift((uint) shift);
    }

    /// <summary>
    /// Decimal left shift <param name="value"/> by
    /// <param name="shift"/> places.
    /// </summary>
    public static decimal LeftShift(this decimal value, uint shift)
    {
      return value*10.RaiseToPower(shift);
    }

    /// <summary>
    /// Decimal right shift <param name="value"/> by
    /// <param name="shift"/> places.
    /// </summary>
    public static decimal RightShift(this decimal value, uint shift)
    {
      return value/10.RaiseToPower(shift);
    }

    /// <summary>
    /// Raise integer to non-negative power.
    /// </summary>
    /// <exception cref="OverflowException">
    /// Thrown if result would be greater than of <see cref="Int32.MaxValue"/>.
    /// </exception>
    public static int RaiseToPower(this int value, uint power)
    {
      return power == 0 || value == 1
        ? 1
        : value.Repeat(power).CheckedProduct();
    }
  }
}
