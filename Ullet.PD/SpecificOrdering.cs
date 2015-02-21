/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ullet.PD
{
  /// <summary>
  /// Implementation of <see cref="IComparer{T}"/> that preferentially compares
  /// objects using the ordering specified in the constructor.
  /// </summary>
  /*
   * Sometimes it is necessary to order a collection in a custom order other
   * than the natural order of the type, e.g. "number" or "alphabetic" order.
   * This class allows that custom order to be described explicitly using an
   * enumerable passed to the constructor.  Objects in the explicit ordering
   * are always considered lower (i.e. come before) objects not explicity
   * listed.  Where neither object is in the ordering the comparison falls
   * back to the natural order, if defined, or assumes objects equal if not.
   */
  public class SpecificOrdering<T> : IComparer<T>
  {
    private const int FirstLessThanSecond = -1;
    private const int FirstGreaterThanSecond = 1;
    private const int FirstEquivalentToSecond = 0;
    private readonly IList<T> _ordering;

    /// <summary>
    /// Construct comparer instance using the specified ordering.
    /// </summary>
    public SpecificOrdering(IEnumerable<T> ordering)
    {
      _ordering = ordering.ToList();
    }

    /// <summary>
    /// Compares two objects and returns a value indicating whether one is
    /// less than, equal to, or greater than the other.
    /// </summary>
    public int Compare(T x, T y)
    {
      if (BothInOrdering(x, y))
        return CompareByIndex(x, y);
      if (FirstOnlyInOrdering(x, y))
        return FirstLessThanSecond;
      if (SecondOnlyInOrdering(x, y))
        return FirstGreaterThanSecond;
      if (NeitherInOrdering(x, y) && TypeHasNaturalOrdering())
        return CompareByNaturalOrdering(x, y);

      return FirstEquivalentToSecond;
    }

    private bool BothInOrdering(T x, T y)
    {
      return InOrdering(x) && InOrdering(y);
    }

    private bool FirstOnlyInOrdering(T x, T y)
    {
      return InOrdering(x) && !InOrdering(y);
    }

    private bool SecondOnlyInOrdering(T x, T y)
    {
      return !InOrdering(x) && InOrdering(y);
    }

    private bool NeitherInOrdering(T x, T y)
    {
      return !InOrdering(x) && !InOrdering(y);
    }

    private int CompareByIndex(T x, T y)
    {
      return _ordering.IndexOf(x).CompareTo(_ordering.IndexOf(y));
    }

    private static int CompareByNaturalOrdering(T x, T y)
    {
      return ((IComparable)x).CompareTo(y);
    }

    private static bool TypeHasNaturalOrdering()
    {
      return typeof(IComparable).IsAssignableFrom(typeof(T));
    }

    private bool InOrdering(T x)
    {
      return _ordering.Contains(x);
    }
  }
}
