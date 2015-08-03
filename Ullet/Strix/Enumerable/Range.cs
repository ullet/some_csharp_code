/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.Strix.Enumerable
{
  /// <summary>
  /// Generate enumerations of integer values over a range.
  /// </summary>
  public static class Range
  {
    /// <summary>
    /// Generate an incrementing enumeration of <see cref="Int32"/> from
    /// <paramref name="start"/> to <paramref name="end"/> inclusive with
    /// default increment of 1 (one).
    /// </summary>
    /// <example>
    /// <code>
    /// var range = 12.UpTo(15); // 12,13,14,15
    /// </code>
    /// </example>
    /// <remarks>
    /// The <see cref="RangeEnumerable.Step"/> method can optionally be used to
    /// specify an increment step, e.g.
    /// <code>
    /// var range = 11.UpTo(15).Step(2); // 11,13,15
    /// </code>
    /// </remarks>
    public static RangeEnumerable UpTo(this int start, int end)
    {
      return new IncrementingRangeEnumerable(start, end);
    }

    /// <summary>
    /// Generate an incrementing enumeration of <see cref="UInt32"/> from
    /// <paramref name="start"/> to <paramref name="end"/> inclusive with
    /// default increment of 1 (one).
    /// </summary>
    /// <example>
    /// <code>
    /// var range = 12.UpTo(15); // 12,13,14,15
    /// </code>
    /// </example>
    /// <remarks>
    /// The <see cref="RangeEnumerable.Step"/> method can optionally be used to
    /// specify an increment step, e.g.
    /// <code>
    /// var range = 11.UpTo(15).Step(2); // 11,13,15
    /// </code>
    /// </remarks>
    public static UnsignedRangeEnumerable UpTo(this uint start, uint end)
    {
      return new UnsignedIncrementingRangeEnumerable(start, end);
    }

    /// <summary>
    /// Generate an decrementing enumeration of <see cref="Int32"/> from
    /// <paramref name="start"/> to <paramref name="end"/> inclusive with
    /// default decrement of 1 (one).
    /// </summary>
    /// <example>
    /// <code>
    /// var range = 15.DownTo(12); // 15,14,13,12
    /// </code>
    /// </example>
    /// <remarks>
    /// The <see cref="RangeEnumerable.Step"/> method can optionally be used to
    /// specify a decrement step, e.g.
    /// <code>
    /// var range = 15.DownTo(11).Step(2); // 15,13,11
    /// // or
    /// var range = 15.DownTo(11).Step(-2); // 15,13,11
    /// </code>
    /// </remarks>
    public static RangeEnumerable DownTo(this int start, int end)
    {
      return new DecrementingRangeEnumerable(start, end);
    }

    /// <summary>
    /// Generate an decrementing enumeration of <see cref="Int32"/> from
    /// <paramref name="start"/> to <paramref name="end"/> inclusive with
    /// default decrement of 1 (one).
    /// </summary>
    /// <example>
    /// <code>
    /// var range = 15.DownTo(12); // 15,14,13,12
    /// </code>
    /// </example>
    /// <remarks>
    /// The <see cref="RangeEnumerable.Step"/> method can optionally be used to
    /// specify a decrement step, e.g.
    /// <code>
    /// var range = 15.DownTo(11).Step(2); // 15,13,11
    /// // or
    /// var range = 15.DownTo(11).Step(-2); // 15,13,11
    /// </code>
    /// </remarks>
    public static UnsignedRangeEnumerable DownTo(this uint start, uint end)
    {
      return new UnsignedDecrementingRangeEnumerable(start, end);
    }

    /// <summary>
    /// Initialize an enumeration of <see cref="Int32"/> starting from
    /// <paramref name="start"/>.
    /// </summary>
    /// <remarks>
    /// Methods <see cref="RangeEnumerable.To"/> and
    /// <see cref="RangeEnumerable.Step"/> can be used specify end point and
    /// increment/decrement step for the range.  If not specified end is set to
    /// <paramref name="start"/> and step to +1.
    /// Use this method for a long-hand fluent definition of a range, e.g.
    /// <code>
    /// var range = Range.From(10).To(20).Step(2);
    /// </code>
    /// </remarks>
    public static RangeEnumerable From(int start)
    {
      return new RangeEnumerable(start);
    }

    /// <summary>
    /// Initialize an enumeration of <see cref="Int32"/> starting from
    /// <paramref name="start"/>.
    /// </summary>
    /// <remarks>
    /// Methods <see cref="RangeEnumerable.To"/> and
    /// <see cref="RangeEnumerable.Step"/> can be used specify end point and
    /// increment/decrement step for the range.  If not specified end is set to
    /// <paramref name="start"/> and step to +1.
    /// Use this method for a long-hand fluent definition of a range, e.g.
    /// <code>
    /// var range = Range.From(10).To(20).Step(2);
    /// </code>
    /// </remarks>
    public static UnsignedRangeEnumerable From(uint start)
    {
      return new UnsignedRangeEnumerable(start);
    }

    /// <summary>
    /// Generate an enumeration of <see cref="Int32"/> from
    /// <paramref name="start"/> to <paramref name="end"/> inclusive with
    /// default increment of 1 (one).
    /// </summary>
    /// <example>
    /// <code>
    /// var range = 12.To(15); // 12,13,14,15
    /// </code>
    /// </example>
    /// <remarks>
    /// The <see cref="RangeEnumerable.Step"/> method can optionally be used to
    /// specify a increment or decrement step, e.g.
    /// <code>
    /// var range = 11.To(15).Step(2); // 11,13,15
    /// // or
    /// var range = 15.To(11).Step(-2); // 15,13,11
    /// </code>
    /// </remarks>
    public static RangeEnumerable To(this int start, int end)
    {
      var diff = end - start;
      var step = diff == 0 ? 1 : diff/Math.Abs(diff);
      return new RangeEnumerable(start, end, step);
    }

    /// <summary>
    /// Generate an enumeration of <see cref="Int32"/> from
    /// <paramref name="start"/> to <paramref name="end"/> inclusive with
    /// default increment of 1 (one).
    /// </summary>
    /// <example>
    /// <code>
    /// var range = 12.To(15); // 12,13,14,15
    /// </code>
    /// </example>
    /// <remarks>
    /// The <see cref="RangeEnumerable.Step"/> method can optionally be used to
    /// specify a increment or decrement step, e.g.
    /// <code>
    /// var range = 11.To(15).Step(2); // 11,13,15
    /// // or
    /// var range = 15.To(11).Step(-2); // 15,13,11
    /// </code>
    /// </remarks>
    public static UnsignedRangeEnumerable To(this uint start, uint end)
    {
      var step = end < start ? -1 : 1;
      return new UnsignedRangeEnumerable(start, end, step);
    }
  }
}
