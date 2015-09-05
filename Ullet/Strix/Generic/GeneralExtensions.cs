/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.Strix.Generic
{
  /// <summary>
  /// General extension methods for any type.
  /// </summary>
  public static class GeneralExtensions
  {
    /// <summary>
    /// Execute an action against an instance of an object and return the
    /// original instance.
    /// </summary>
    public static T InstanceExecute<T>(this T t, Action<T> action)
    {
      action(t);
      return t;
    }

    /// <summary>
    /// Execute an action against an instance of an object with an additional
    /// object and then return the original instance.
    /// </summary>
    public static TInst InstanceExecuteWithObject<TInst, TObj>(
      this TInst t, TObj o, Action<TInst, TObj> action)
    {
      action(t, o);
      return t;
    }
  }
}
