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
      enumerable.ToList().ForEach(action);
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
  }
}
