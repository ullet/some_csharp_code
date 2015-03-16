/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.PD
{
  /// TODO <summary></summary>
  public class IncrementingRangeEnumerable : RangeEnumerable
  {
    /// TODO <summary></summary>
    public IncrementingRangeEnumerable(
      int? start = null, int? end = null, int? step = null)
      : base(start, end, PositiveStep(step))
    {
    }

    /// TODO <summary></summary>
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
