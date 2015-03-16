/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.PD
{
  /// TODO<summary></summary>
  public static class Range
  {
    /// TODO<summary></summary>
    /// TODO<param name="start"></param>
    /// TODO<param name="end"></param>
    /// TODO<returns></returns>
    public static RangeEnumerable UpTo(this int start, int end)
    {
      return new IncrementingRangeEnumerable(start, end);
    }

    /// TODO<summary></summary>
    /// TODO<param name="start"></param>
    /// TODO<param name="end"></param>
    /// TODO<returns></returns>
    public static RangeEnumerable DownTo(this int start, int end)
    {
      return new DecrementingRangeEnumerable(start, end);
    }

    /// TODO<summary></summary>
    public static RangeEnumerable From(int start)
    {
      return new RangeEnumerable(start);
    }

    /// TODO<summary></summary>
    public static RangeEnumerable To(this int start, int end)
    {
      var diff = end - start;
      var step = diff == 0 ? 1 : diff/Math.Abs(diff);
      return new RangeEnumerable(start, end, step);
    }
  }
}
