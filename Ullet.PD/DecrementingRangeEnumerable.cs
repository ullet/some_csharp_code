/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.PD
{
  /// <summary>
  /// Enumerable of a bounded sequence of decrementing integer values.
  /// </summary>
  public class DecrementingRangeEnumerable : RangeEnumerable
  {
    /// <summary>
    /// Initialize a new decrementing range enumeration.
    /// </summary>
    public DecrementingRangeEnumerable(
      int? start = null, int? end = null, int? step = null)
      : base(start, end, NegativeStep(step))
    {
    }

    /// <summary>
    /// Value to decrement successive values in the range sequence.  Positive
    /// and negative values are regarded as equivalent, both specifying the
    /// absolute decrement value.
    /// </summary>
    public override RangeEnumerable Step(int step)
    {
      return base.Step(NegativeStep(step));
    }

    private static int NegativeStep(int? step)
    {
      return !step.HasValue || step == 0 ? -1 : -Math.Abs(step.Value);
    }
  }
}
