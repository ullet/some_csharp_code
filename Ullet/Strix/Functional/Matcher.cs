/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using Ullet.Strix.Enumerable;
using Ullet.Strix.Generic;

namespace Ullet.Strix.Functional
{
  /// <summary>
  /// Match against a value to conditionally return a result.
  /// </summary>
  public class Matcher<T, TResult>
  {
    private readonly DefaultDictionary<Maybe<T>, Maybe<TResult>> _matches;
    private readonly IList<T> _matchCases;
    private readonly Maybe<T> _matchValue;

    /// <summary>
    /// Construct a Matcher for match value to be specified later;
    /// </summary>
    public Matcher()
    {
      _matches =
        new DefaultDictionary<Maybe<T>, Maybe<TResult>>(Fn.Nothing<TResult>());
      _matchCases = new List<T>();
      _matchValue = Fn.Nothing<T>();
    }

    /// <summary>
    /// Construct a Matcher matching against <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value to match against.</param>
    public Matcher(T value)
      : this()
    {
      _matchValue = value;
    }

    /// <summary>
    /// Match on one or more values to return a result.
    /// </summary>
    /// <param name="firstMatchCase">Value to match on.</param>
    /// <param name="otherMatchCases">
    /// Optional additional values to match on.
    /// </param>
    /// <returns>
    /// Matcher instance to either specify more cases or evaluate the result.
    /// </returns>
    public Matcher<T, TResult> Case(
      T firstMatchCase, params T[] otherMatchCases)
    {
      var allCases = otherMatchCases.Append(firstMatchCase);
      allCases.ForEach(matchCase => _matchCases.Add(matchCase));
      return this;
    }

    /// <summary>
    /// Result to return for previousy specified case or cases.
    /// </summary>
    /// <returns>
    /// Matcher instance to either specify more cases or evaluate the result.
    /// </returns>
    public Matcher<T, TResult> Then(TResult matchResult)
    {
      _matchCases.Each(matchCase => _matches.Add(matchCase, matchResult));
      _matchCases.Clear();
      return this;
    }

    /// <summary>
    /// Default value if no match found.
    /// </summary>
    public Matcher<T, TResult> Else(TResult defaultValue)
    {
      _matches.DefaultValue = defaultValue;
      return this;
    }

    /// <summary>
    /// Evaluate the result that matches the match on value.
    /// </summary>
    /// <returns>The matching result.</returns>
    public Maybe<TResult> Evaluate()
    {
      return Evaluate(_matchValue);
    }

    /// <summary>
    /// Evaluate the result that matches the match on value.
    /// </summary>
    /// <returns>The matching result.</returns>
    public Maybe<TResult> On(T matchValue)
    {
      return Evaluate(matchValue);
    }

    /// <summary>
    /// Evaluate the result that matches the match on value.
    /// </summary>
    /// <returns>The matching result.</returns>
    public Maybe<TResult> Evaluate(T matchValue)
    {
      return Evaluate(Fn.Just(matchValue));
    }

    /// <summary>
    /// Explicit conversion to function delegate to evaluate the match.
    /// </summary>
    /// <returns>Function delegate to evaluate the match.</returns>
    public Func<T, Maybe<TResult>> ToFunction()
    {
      return Evaluate;
    }

    /// <summary>
    /// Implicit conversion to function delegate to evaluate the match.
    /// </summary>
    /// <param name="matcher">Matcher delegating to for evaluation.</param>
    /// <returns>Function delegate to evaluate the match.</returns>
    public static implicit operator Func<T, Maybe<TResult>>(
      Matcher<T, TResult> matcher)
    {
      return matcher.ToFunction();
    }

    private Maybe<TResult> Evaluate(Maybe<T> matchValue)
    {
      return _matches[matchValue];
    }
  }
}
