/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
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
    public static decimal ToSignificantFigures(this decimal value, int figures)
    {
      if (value == 0)
        return 0;
      if (value < 0)
        return -((-value).ToSignificantFigures(figures));
      var shift = 1 + (int)Math.Floor(Math.Log10((double) value)) - figures;
      return Math.Round(value.Shift(shift), 0).Shift(-shift);
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
