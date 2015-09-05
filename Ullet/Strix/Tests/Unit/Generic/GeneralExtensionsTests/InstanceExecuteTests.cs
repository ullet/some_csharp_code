/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Generic.Tests.Unit.GeneralExtensionsTests
{
  [TestFixture]
  public class InstanceExecuteTests
  {
    [Test]
    public void CanExecuteActionOnInstance()
    {
      var o = new ExampleClass();
      Action<ExampleClass> actionToExecuteOnInstance = x => x.SomeValue = 123;

      o.InstanceExecute(actionToExecuteOnInstance);

      Assert.That(o.SomeValue, Is.EqualTo(123));
    }

    [Test]
    public void ReturnsInstanceAfterExecutingActione()
    {
      var o = new ExampleClass();
      Action<ExampleClass> actionToExecuteOnInstance = x => x.SomeValue = 123;

      var instance = o.InstanceExecute(actionToExecuteOnInstance);

      Assert.That(instance, Is.SameAs(o));
    }

    [Test]
    public void CanCombineCreationAndExecutionToAvoidTemporaryVariables()
    {
      Assert.That(
        new ExampleClass().InstanceExecute(x => x.SomeValue = 123).SomeValue,
        Is.EqualTo(123));
    }

    private class ExampleClass
    {
      public int SomeValue { get; set; }
    }
  }
}
