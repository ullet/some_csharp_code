/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;

namespace Ullet.Strix.Enumerable
{
  /// <summary>
  /// Enumerable of a bounded sequence of incrementing or decrementing integer
  /// values.
  /// </summary>
  public class RangeEnumerable : IEnumerable<int>
  {
    private int _start;
    private int _end;
    private int _step;

    /// <summary>
    /// Initialize a new range enumeration.
    /// </summary>
    public RangeEnumerable(int? start=null, int? end=null, int? step=null)
    {
      _start = start ?? 0;
      _end = end ?? _start;
      _step = step ?? 1;
    }

    /// <summary>
    /// Set starting value of the enumeration.
    /// </summary>
    public RangeEnumerable From(int start)
    {
      _start = start;
      return this;
    }

    /// <summary>
    /// Set final/terminating value of the enumeration.
    /// </summary>
    /// <remarks>
    /// This value may not be in the enumeration if <see cref="Step"/> is set to
    /// a non-unit value.  Enumeration terminates at a value not greater than
    /// this upper bound (or less than this lower bound if negative step).
    /// </remarks>
    public RangeEnumerable To(int end)
    {
      _end = end;
      return this;
    }

    /// <summary>
    /// Value to increment successive values in the range sequence.  Negative
    /// value will produce a decrementing sequence.  <see cref="From"/> must be
    /// less than or equal to <see cref="To"/> is <see cref="Step"/> is a
    /// positive value.  <see cref="From"/> must be
    /// greater than or equal to <see cref="To"/> is <see cref="Step"/> is a
    /// negative value.
    /// </summary>
    public virtual RangeEnumerable Step(int step)
    {
      _step = step;
      return this;
    }

    /// <summary>
    /// Get enumerator for this enumerable.
    /// </summary>
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
