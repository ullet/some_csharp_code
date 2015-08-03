/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.Strix.Generic
{
  /// <summary>
  /// Wrap a reference type around a value type to get "out parameter" like
  /// behaviour where not able to use an out parameter.
  /// </summary>
  /// <typeparam name="T">Value type to wrap.</typeparam>
  public class Mutable<T> where T : struct
  {
    private T _value;

    /// <summary>
    /// Create a new Mutable{T} instance without an initial value.
    /// </summary>
    public Mutable()
    {
    }

    /// <summary>
    /// Create a new Mutable{T} instance with an initial value.
    /// </summary>
    public Mutable(T value)
    {
      Value = value;
    }

    /// <summary>
    /// Returns a boolean indicate if the current Mutable{T} has a value.
    /// </summary>
    public bool HasValue { get; private set; }

    /// <summary>
    /// Gets or sets the value of the current Mutable{T} instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if get Value and no value has been set.
    /// </exception>
    public T Value
    {
      get
      {
        if (!HasValue)
          throw new InvalidOperationException("Value not set");
        return _value;
      }

      set
      {
        _value = value;
        HasValue = true;
      }
    }

    /// <summary>
    /// Convert value type to Mutable{T} instance.
    /// </summary>
    public static implicit operator Mutable<T> (T value)
    {
      return new Mutable<T>(value);
    }

    /// <summary>
    /// Convert Mutable{T} instance to its value type.
    /// </summary>
    public static implicit operator T(Mutable<T> mutable)
    {
      if (mutable == null)
        throw new NullReferenceException("Cannot convert null to value type");
      return mutable.Value;
    }

    /// <summary>
    /// Convert Mutable{T} instance to nullable of value type.
    /// </summary>
    public static implicit operator T?(Mutable<T> mutable)
    {
      return mutable == null || !mutable.HasValue ? null : (T?)mutable.Value;
    }
  }
}
