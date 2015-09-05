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
    /// <remarks>
    /// <para>
    /// Since an action has no return value, something must be mutated in
    /// order for this method to do anything useful.  The method name "Mutate"
    /// is intended to make it clear that this is definately not a pure
    /// function.
    /// </para>
    /// <para>
    /// One advantage of using Mutate rather than just calling the action
    /// directly is that it returns the original instance so enables chaining
    /// calls.
    /// </para>
    /// </remarks>
    public static T Mutate<T>(this T t, Action<T> action)
    {
      action(t);
      return t;
    }

    /// <summary>
    /// Execute an action against an instance of an object with an additional
    /// object and then return the original instance.
    /// </summary>
    public static TInst MutateWithObject<TInst, TObj>(
      this TInst t, TObj o, Action<TInst, TObj> action)
    {
      action(t, o);
      return t;
    }
  }
}
