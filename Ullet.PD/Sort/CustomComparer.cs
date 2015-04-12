/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;

namespace Ullet.PD.Sort
{
  /// <summary>
  /// Wraps a function delegate in a <see cref="IComparer{T}"/> instance to
  /// allow any compare function with correct signature to be used wherever an
  /// <see cref="IComparer{T}"/> instance is required.
  /// </summary>
  /*
   * One use of this class is to create a dynamic comparer instance.
   * It is not possible to create a class that implements IComparer<dynamic> but
   * it is possible to have an instance of IComparer<dynamic>.
   * Example:
   *   var dynamicComparer = 
   *     CustomComparer.Create<dynamic>((x, y) => x.CompareTo(y));
   *   // Compare double to long: 1.234 is less than 2
   *   var result1 = dynamicComparer.Compare(1.234, 2L); // returns -1
   *   // Use same comparer instance to compare chars
   *   var result2 = dynamicComparer.Compare('X', 'X'); // returns 0
   */
  public static class CustomComparer
  {
    /// <summary>
    /// Create a <see cref="IComparer{T}"/> instance from the given
    /// <see cref="Func{T, T, Int32}"/> delegate.
    /// </summary>
    public static IComparer<T> Create<T>(Func<T, T, int> compare)
    {
      return compare.ToComparer();
    }

    /// <summary>
    /// Create a <see cref="IComparer{T}"/> instance from this
    /// <see cref="Func{T, T, Int32}"/> delegate.
    /// </summary>
    public static IComparer<T> ToComparer<T>(this Func<T, T, int> compare)
    {
      return new CustomFuncComparer<T>(compare);
    }

    private class CustomFuncComparer<T> : IComparer<T>
    {
      private readonly Func<T, T, int> _compare;

      public CustomFuncComparer(Func<T, T, int> compare)
      {
        if (compare == null)
          throw new ArgumentNullException("compare");
        _compare = compare;
      }

      public int Compare(T x, T y)
      {
        return _compare(x, y);
      }
    }
  }
}
