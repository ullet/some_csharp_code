/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2014, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Ullet.PD
{
  /// <summary>
  /// Object extension methods that rely on dynamic to assume exisitance of
  /// specific properties or methods skipping compile-time type checking.
  /// </summary>
  public static class DynamicExtensions
  {
    /// <summary>
    /// Tests if a "countable" instance "contains" any items.
    /// </summary>
    /// <param name="countable">
    /// An instance of a type that implements property Count.
    /// </param>
    /// <typeparam name="T">
    /// Any non-dynamic non-anonymous public type that implements public
    /// property Count.  Count will typically return <see cref="Int32"/> but
    /// can return any type comparable to Int32.
    /// </typeparam>
    /// <exception cref="RuntimeBinderException">
    /// Thrown if <typeparamref name="T"/> does not implement property Count or
    /// return type of Count cannot be compared to Int32.
    /// </exception>
    public static bool Any<T>(this T countable)
    {
      return ((dynamic)countable).Count > 0;
    }

    /// <summary>
    /// Returns count for a "countable" instance.
    /// </summary>
    /// <param name="countable">
    /// An instance of a type that implements property Count.
    /// </param>
    /// <typeparam name="T">
    /// Any non-dynamic non-anonymous public type that implements public
    /// property Count.  Count must return <see cref="Int32"/> or a type that
    /// can be cast or converted to Int32.
    /// </typeparam>
    /// <exception cref="RuntimeBinderException">
    /// Thrown if <typeparamref name="T"/> does not implement property Count or
    /// return type of Count cannot be cast or converted to Int32.
    /// </exception>
    public static int Count<T>(this T countable)
    {
      return (int)((dynamic)countable).Count;
    }
  }
}
