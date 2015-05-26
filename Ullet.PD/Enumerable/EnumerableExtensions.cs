/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ullet.PD.Enumerable
{
  /// <summary>
  /// General extension methods for <see cref="IEnumerable{T}"/> and similar
  /// "enumerable" types.
  /// </summary>
  public static class EnumerableExtensions
  {
    /// <summary>
    /// Append zero or more <paramref name="items"/> to end of
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
      foreach (var item in enumerable)
        action(item);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="enumerable"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void ForEach<T>(
      this IEnumerable<T> enumerable, Action<int, T> action)
    {
      enumerable.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each indexed item in
    /// <paramref name="enumerable"/>.
    /// </summary>
    public static void ForEach<T>(
      this IEnumerable<T> enumerable, Action<ItemWithIndex<T>> action)
    {
      enumerable.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="enumerable"/>.
    /// </summary>
    public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
      enumerable.ForEach(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="enumerable"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void Each<T>(
      this IEnumerable<T> enumerable, Action<int, T> action)
    {
      enumerable.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each indexed item in
    /// <paramref name="enumerable"/>.
    /// </summary>
    public static void Each<T>(
      this IEnumerable<T> enumerable, Action<ItemWithIndex<T>> action)
    {
      enumerable.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="enumerable"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void ForEachWithIndex<T>(
      this IEnumerable<T> enumerable, Action<int, T> action)
    {
      enumerable.ForEachWithIndex(x => action(x.Index, x.Item));
    }

    /// <summary>
    /// Perform specified action on each indexed item in
    /// <paramref name="enumerable"/>.
    /// </summary>
    public static void ForEachWithIndex<T>(
      this IEnumerable<T> enumerable, Action<ItemWithIndex<T>> action)
    {
      enumerable.WithIndex().ForEach(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="enumerable"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void EachWithIndex<T>(
      this IEnumerable<T> enumerable, Action<int, T> action)
    {
      enumerable.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each indexed item in
    /// <paramref name="enumerable"/>.
    /// </summary>
    public static void EachWithIndex<T>(
      this IEnumerable<T> enumerable, Action<ItemWithIndex<T>> action)
    {
      enumerable.ForEachWithIndex(action);
    }

    /// <summary>
    /// Map items in enumerable to a <see cref="ItemWithIndex{T}"/> tuple
    /// pairing each item with its index within the enumeration.
    /// </summary>
    public static IEnumerable<ItemWithIndex<T>> WithIndex<T>(
      this IEnumerable<T> source)
    {
      var index = 0;
      return source.Select(x => new ItemWithIndex<T>(x, index++));
    }

    /// <summary>
    /// Calculate product of integer sequence.
    /// </summary>
    public static int Product(this IEnumerable<int> source)
    {
      if (source == null) throw new ArgumentNullException("source");
      var items = source.ToArray();
      if (!items.Any()) throw new ArgumentException("Sequence empty", "source");
      return items.Aggregate(1, (a, v) => a * v);
    }

    /// <summary>
    /// Calculate product of integer sequence with overflow checking.
    /// </summary>
    /// <exception cref="OverflowException">
    /// Thrown if result would be greater than of <see cref="Int32.MaxValue"/>.
    /// </exception>
    public static int CheckedProduct(this IEnumerable<int> source)
    {
      if (source == null) throw new ArgumentNullException("source");
      var items = source.ToArray();
      if (!items.Any()) throw new ArgumentException("Sequence empty", "source");
      return items.Aggregate(1, CheckedProduct);
    }

    private static int CheckedProduct(int first, int second)
    {
      checked
      {
        return first*second;
      }
    }

    /// <summary>
    /// Create sequence repeating value integer number of times.
    /// </summary>
    public static IEnumerable<T> Repeat<T>(this T item, int repeats)
    {
      for (var i = 0; i < repeats; i++)
        yield return item;
    }

    /// <summary>
    /// Create sequence repeating value integer number of times.
    /// </summary>
    public static IEnumerable<T> Repeat<T>(this T item, uint repeats)
    {
      for (uint i = 0; i < repeats; i++)
        yield return item;
    }

    /// <summary>
    /// Count sequence with greater than <see cref="Int32.MaxValue"/> items.
    /// </summary>
    /// <remarks>
    /// Will take relatively long time to count if sequence has more than
    /// <see cref="Int32.MaxValue"/> items.  If sequence contains close to
    /// <see cref="Int64.MaxValue"/> items then do not expect this function to
    /// return within the life time of your civilization.
    /// </remarks>
    public static ulong BigCount<T>(
      this IEnumerable<T> source,
      Func<T, bool> predicate = null)
    {
      ulong count = 0;
      var filtered = predicate == null ? source : source.Where(predicate);
      filtered.Each((T x) => count++);
      return count;
    }

    /// <summary>
    /// Enumerate source passing previous, current, and next items to selector.
    /// </summary>
    /// <param name="source">Source enumerable.</param>
    /// <param name="selector">
    /// Function taking three <typeparamref name="TSource"/> parameters for
    /// previous, current, and next items from source, and returning a value of
    /// <typeparamref name="TResult"/>.
    /// </param>
    /// <returns>An enumerable of <typeparamref name="TResult"/>.</returns>
    public static IEnumerable<TResult> Select<TSource, TResult>(
      this IEnumerable<TSource> source,
      Func<TSource, TSource, TSource, TResult> selector)
    {
      var previous = default(TSource);
      var current = default(TSource);
      var first = true;
      foreach (var next in source.Append(default(TSource)))
      {
        if (first)
          first = false;
        else
          yield return selector(previous, current, next);
        previous = current;
        current = next;
      }
    }
  }
}
