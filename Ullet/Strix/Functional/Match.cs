/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

namespace Ullet.Strix.Functional
{
  /// <summary>
  /// Match value to conditionally return a result.
  /// </summary>
  public static class Match<TOn, TResult>
  {
    /// <summary>
    /// Value to match against.
    /// </summary>
    public static Matcher<TOn, TResult> On(TOn value)
    {
      return new Matcher<TOn, TResult>(value);
    }

    /// <summary>
    /// Match an expression with cases to be specified.
    /// </summary>
    public static Matcher<TOn, TResult> With
    {
      get { return new Matcher<TOn, TResult>(); }
    }
  }
}
