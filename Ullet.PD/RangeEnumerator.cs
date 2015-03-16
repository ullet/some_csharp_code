/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace Ullet.PD
{
  /// TODO <summary></summary>
  public class RangeEnumerator : IEnumerator<int>
  {
    private readonly int _start;
    private readonly int _end;
    private readonly int _step;
    private int? _current;

    /// TODO <summary></summary>
    public RangeEnumerator(int start, int end, int step)
    {
      _start = start;
      _end = end;
      _step = step == 0 ? 1 : step;
    }

    /// TODO <summary></summary>
    public void Dispose()
    {
      // nothing to dispose
    }

    /// TODO <summary></summary>
    public bool MoveNext()
    {
      if (_current == null)
        _current = _start;
      else
        _current += _step;
      return Sign * _current <= Sign * _end;
    }

    /// TODO <summary></summary>
    public void Reset()
    {
      _current = null;
    }

    /// TODO <summary></summary>
    public int Current
    {
      get
      {
        if (!_current.HasValue)
          throw new InvalidOperationException();
        return _current.Value;
      }
    }

    object IEnumerator.Current
    {
      get { return Current; }
    }

    private int Sign
    {
      get
      {
        return _step/Math.Abs(_step);
      }
    }
  }
}
