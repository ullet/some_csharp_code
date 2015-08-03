/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.Strix.Enumerable
{
  /// <summary>
  /// Enumerable of a bounded sequence of incrementing integer values.
  /// </summary>
  public class IncrementingRangeEnumerable : RangeEnumerable
  {
    /// <summary>
    /// Initialize a new incrementing range enumeration.
    /// </summary>
    public IncrementingRangeEnumerable(
      int? start = null, int? end = null, int? step = null)
      : base(start, end, PositiveStep(step))
    {
    }

    /// <summary>
    /// Value to increment successive values in the range sequence.  Positive
    /// and negative values are regarded as equivalent, both specifying the
    /// absolute increment value.
    /// </summary>
    /// <remarks>
    /// Although positive and negative values of equivalent magnitude result in
    /// the generation of the same sequence, a negative value is likely to cause
    /// confusion.
    /// </remarks>
    public override RangeEnumerable Step(int step)
    {
      return base.Step(PositiveStep(step));
    }

    private static int PositiveStep(int? step)
    {
      return !step.HasValue || step == 0 ? 1 : Math.Abs(step.Value);
    }
  }
}
