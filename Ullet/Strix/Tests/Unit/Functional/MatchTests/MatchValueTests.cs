/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Functional.Tests.Unit.MatchTests
{
  [TestFixture]
  public class MatchValueTests
  {
    [TestCase(2, "two")]
    [TestCase(4, "four")]
    public void MatchSingleValueToSingleResult(
      int expression, string expectedResult)
    {
      Maybe<string> result = Match<int, string>.On(expression)
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Case(3).Then("three")
        .Case(4).Then("four")
        .Case(5).Then("five")
        .Evaluate();

      Assert.That((string)result, Is.EqualTo(expectedResult));
    }

    [TestCase(2, "one")]
    [TestCase(3, "three")]
    [TestCase(4, "four")]
    [TestCase(5, "four")]
    public void MatchMultipleValuesToResult(
      int expression, string expectedResult)
    {
      Maybe<string> result = Match<int, string>.On(expression)
        .Case(1).Case(2).Then("one")
        .Case(3).Then("three")
        .Case(4).Case(5).Then("four")
        .Evaluate();

      Assert.That((string)result, Is.EqualTo(expectedResult));
    }

    [TestCase(2, "one")]
    [TestCase(3, "three")]
    [TestCase(4, "four")]
    [TestCase(5, "four")]
    public void MatchMultipleValuesSpecifiedAsParamsArrayToResult(
      int expression, string expectedResult)
    {
      Maybe<string> result = Match<int, string>.On(expression)
        .Case(1, 2).Then("one")
        .Case(3).Then("three")
        .Case(4, 5).Then("four")
        .Evaluate();

      Assert.That((string)result, Is.EqualTo(expectedResult));
    }

    [TestCase(2, "two")]
    [TestCase(4, "four")]
    public void MatchWithExpressionInFinalEvaluate(
      int expression, string expectedResult)
    {
      Maybe<string> result = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Case(3).Then("three")
        .Case(4).Then("four")
        .Case(5).Then("five")
        .Evaluate(expression);

      Assert.That((string)result, Is.EqualTo(expectedResult));
    }

    [TestCase(2, "two")]
    [TestCase(4, "four")]
    public void MatchWithExpressionInFinalOn(
      int expression, string expectedResult)
    {
      Maybe<string> result = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Case(3).Then("three")
        .Case(4).Then("four")
        .Case(5).Then("five")
        .On(expression);

      Assert.That((string)result, Is.EqualTo(expectedResult));
    }

    [TestCase(2, "two")]
    [TestCase(4, "four")]
    public void MatchToFunctionDelegate(int expression, string expectedResult)
    {
      // implicit conversion
      Func<int, Maybe<string>> matcher = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Case(3).Then("three")
        .Case(4).Then("four")
        .Case(5).Then("five");

      Assert.That((string)matcher(expression), Is.EqualTo(expectedResult));
    }

    [TestCase(2, "two")]
    [TestCase(4, "four")]
    public void MatchToFunctionDelegateWithExplicitCall(
      int expression, string expectedResult)
    {
      Func<int, Maybe<string>> matcher = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Case(3).Then("three")
        .Case(4).Then("four")
        .Case(5).Then("five")
        .ToFunction();

      Assert.That((string)matcher(expression), Is.EqualTo(expectedResult));
    }

    [Test]
    public void MatchWithoutExpressionToMatchAgainstReturnsNothing()
    {
      Maybe<string> result = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Case(3).Then("three")
        .Case(4).Then("four")
        .Case(5).Then("five")
        .Evaluate();

      Assert.That(result, Is.InstanceOf<Nothing<string>>());
    }

    [Test]
    public void MatchWithNoMatchingValueReturnsNothing()
    {
      Maybe<string> result = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Evaluate(3);

      Assert.That(result, Is.InstanceOf<Nothing<string>>());
    }

    [Test]
    public void MatchWithDefaultButNoMatchingValueReturnsDefault()
    {
      const string defaultValue = "default";
      Maybe<string> result = Match<int, string>.With
        .Case(1).Then("one")
        .Case(2).Then("two")
        .Else(defaultValue)
        .Evaluate(3);

      Assert.That((string)result, Is.EqualTo(defaultValue));
    }
  }
}
