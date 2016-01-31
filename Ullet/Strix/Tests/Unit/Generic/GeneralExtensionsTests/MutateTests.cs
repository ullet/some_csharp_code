/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Generic.Tests.Unit.GeneralExtensionsTests
{
  [TestFixture]
  public class MutateTests
  {
    [Test]
    public void CanMutateInstance()
    {
      var o = new ExampleClass();
      Action<ExampleClass> mutateAction = x => x.SomeValue = 123;

      var instance = o.Mutate(mutateAction);

      Assert.That(o.SomeValue, Is.EqualTo(123));
      Assert.That(instance, Is.SameAs(o));
    }

    [Test]
    public void CanUseMutateAsAnInitializer()
    {
      var o = new ExampleClass().Mutate(x => x.SomeValue = 123);

      Assert.That(o.SomeValue, Is.EqualTo(123));
    }

    [Test]
    public void ExecuteIsAliasForMutate()
    {
      const string obj = "some object";
      string actionCalledOn = null;

      obj.Execute(o => actionCalledOn = o);

      Assert.That(actionCalledOn, Is.SameAs(obj));
    }

    private class ExampleClass
    {
      public int SomeValue { get; set; }
    }
  }
}
