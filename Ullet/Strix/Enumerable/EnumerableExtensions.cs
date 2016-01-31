/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ullet.Strix.Enumerable
{
  /// <summary>
  /// General extension methods for <see cref="IEnumerable{T}"/> and similar
  /// "enumerable" types.
  /// </summary>
  public static class EnumerableExtensions
  {
    /// <summary>
    /// Append zero or more <paramref name="items"/> to end of
    /// <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<T> Append<T>(
      this IEnumerable<T> source, params T[] items)
    {
      return source.Concat(items);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/>.
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
        action(item);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void ForEach<T>(
      this IEnumerable<T> source, Action<T, int> action)
    {
      source.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/> and
    /// the passed in <typeparamref name="TObj"/> instance.
    /// </summary>
    /// <returns>Returns original object reference.</returns>
    /// <remarks>
    /// Referenced object could be mutated by the specified action.  A typical
    /// use of ForEach with an object is to construct an aggregate object by
    /// iterating over the source.
    /// </remarks>
    public static TObj ForEach<T, TObj>(
      this IEnumerable<T> source, TObj o, Action<T, TObj> action)
    {
      return source.ForEachWithObject(o, action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/>.
    /// </summary>
    public static void Each<T>(this IEnumerable<T> source, Action<T> action)
    {
      source.ForEach(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void Each<T>(
      this IEnumerable<T> source, Action<T, int> action)
    {
      source.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/> and
    /// the passed in <typeparamref name="TObj"/> instance.
    /// </summary>
    /// <returns>Returns original object reference.</returns>
    /// <remarks>
    /// Referenced object could be mutated by the specified action.  A typical
    /// use of Each with an object is to construct an aggregate object by
    /// iterating over the source.
    /// </remarks>
    public static TObj Each<T, TObj>(
      this IEnumerable<T> source, TObj o, Action<T, TObj> action)
    {
      return source.ForEachWithObject(o, action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void ForEachWithIndex<T>(
      this IEnumerable<T> source, Action<T, int> action)
    {
      source.Select((t, i) => new{t,i}).ForEach(it => action(it.t, it.i));
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/>
    /// along with each items index within the enumeration.
    /// </summary>
    public static void EachWithIndex<T>(
      this IEnumerable<T> source, Action<T, int> action)
    {
      source.ForEachWithIndex(action);
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/> and
    /// the passed in <typeparamref name="TObj"/> instance.
    /// </summary>
    /// <returns>Returns original object reference.</returns>
    /// <remarks>
    /// Referenced object could be mutated by the specified action.  A typical
    /// use of ForEachWithObject is to construct an aggregate object by
    /// iterating over the source.
    /// </remarks>
    public static TObj ForEachWithObject<T, TObj>(
      this IEnumerable<T> source, TObj o, Action<T, TObj> action)
    {
      source.ForEach(x => action(x, o));
      return o;
    }

    /// <summary>
    /// Perform specified action on each item in <paramref name="source"/> and
    /// the passed in <typeparamref name="TObj"/> instance.
    /// </summary>
    /// <returns>Returns original object reference.</returns>
    /// <remarks>
    /// Referenced object could be mutated by the specified action.  A typical
    /// use of EachWithObject is to construct an aggregate object by  iterating
    /// over the source.
    /// </remarks>
    public static TObj EachWithObject<T, TObj>(
      this IEnumerable<T> source, TObj o, Action<T, TObj> action)
    {
      return source.ForEachWithObject(o, action);
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
      return (predicate == null ? source : source.Where(predicate))
        .Aggregate(0ul, (c, x) => c + 1);
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

    /// <summary>
    /// Select distinct items using keySelector delegate.  Two items with same
    /// key are considered equivalent.  Only the first item in the sequence with
    /// a given key is retained.
    /// </summary>
    /// <param name="source">Source enumerable.</param>
    /// <param name="keySelector">
    /// Function selecting key used to test items for distinctness.
    /// </param>
    /// <returns>Enumerable retaining only distinct items.</returns>
    public static IEnumerable<T> DistinctBy<T, TKey>(
      this IEnumerable<T> source, Func<T, TKey> keySelector)
    {
      var distinctKeys = new HashSet<TKey>();
      return source.Where(item => distinctKeys.Add(keySelector(item)));
    }
  }
}
