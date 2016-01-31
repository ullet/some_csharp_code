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
  public class MutateWithObjectTests
  {
    [Test]
    public void CanMutateInstanceWithAnotherObject()
    {
      var o = new ExampleClass();
      Action<ExampleClass, int> mutateAction = (x, n) => x.SomeValue = 123 + n;

      var instance = o.MutateWithObject(111, mutateAction);

      Assert.That(o.SomeValue, Is.EqualTo(234));
      Assert.That(instance, Is.SameAs(o));
    }

    [Test]
    public void CanUseMutateWithObjectAsAnInitializer()
    {
      var o = new ExampleClass()
        .MutateWithObject(111, (x, n) => x.SomeValue = 123 + n);
      Assert.That(o.SomeValue, Is.EqualTo(234));
    }

    [Test]
    public void ExecuteWithObjectIsAliasForMutateWithObject()
    {
      var o = new ExampleClass();
      Action<ExampleClass, int> mutateAction = (x, n) => x.SomeValue = 123 + n;

      var instance = o.ExecuteWithObject(111, mutateAction);

      Assert.That(o.SomeValue, Is.EqualTo(234));
      Assert.That(instance, Is.SameAs(o));
    }

    private class ExampleClass
    {
      public int SomeValue { get; set; }
    }
  }
}
