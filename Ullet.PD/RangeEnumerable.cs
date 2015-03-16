/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;

namespace Ullet.PD
{
  /// TODO <summary></summary>
  public class RangeEnumerable : IEnumerable<int>
  {
    private int _start;
    private int _end;
    private int _step;

    /// TODO <summary></summary>
    public RangeEnumerable(int? start=null, int? end=null, int? step=null)
    {
      _start = start ?? 0;
      _end = end ?? _start;
      _step = step ?? 1;
    }

    /// TODO <summary></summary>
    public RangeEnumerable From(int start)
    {
      _start = start;
      return this;
    }

    /// TODO <summary></summary>
    public RangeEnumerable To(int end)
    {
      _end = end;
      return this;
    }

    /// TODO <summary></summary>
    public virtual RangeEnumerable Step(int step)
    {
      _step = step;
      return this;
    }

    /// TODO <summary></summary>
    public IEnumerator<int> GetEnumerator()
    {
      return new RangeEnumerator(_start, _end, _step);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
