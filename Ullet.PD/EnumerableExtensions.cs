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
  /// General extension methods for <see cref="IEnumerable{T}"/> and similar
  /// "enumerable" types.
  /// </summary>
  public static class EnumerableExtensions
  {
    /// <summary>
    /// Append one or more <paramref name="items"/> to end of 
    /// <paramref name="enumerable"/>.
    /// </summary>
    public static IEnumerable<T> Append<T>(
      this IEnumerable<T> enumerable, params T[] items)
    {
      return enumerable.Concat(items);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="enumerable"/>.
    /// </summary>
    public static void ForEach<T>(
      this IEnumerable<T> enumerable, Action<T> action)
    {
      enumerable.ToList().ForEach(action);
    }
  }
}
