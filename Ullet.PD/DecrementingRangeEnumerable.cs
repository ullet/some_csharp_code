/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.PD
{
  /// TODO <summary></summary>
  public class DecrementingRangeEnumerable : RangeEnumerable
  {
    /// TODO <summary></summary>
    public DecrementingRangeEnumerable(
      int? start = null, int? end = null, int? step = null)
      : base(start, end, NegativeStep(step))
    {
    }

    /// TODO <summary></summary>
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
