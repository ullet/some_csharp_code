using System;
using NUnit.Framework;

namespace Ullet.Strix.Generic.Tests.Unit.GeneralExtensionsTests
{
  [TestFixture]
  public class InstanceExecuteWithObjectTests
  {
    [Test]
    public void CanExecuteActionWithObjectOnInstance()
    {
      var o = new ExampleClass();
      Action<ExampleClass, int> actionToExecuteOnInstance =
        (x, n) => x.SomeValue = 123 + n;

      o.InstanceExecuteWithObject(111, actionToExecuteOnInstance);

      Assert.That(o.SomeValue, Is.EqualTo(234));
    }

    [Test]
    public void ReturnsInstanceAfterExecutingActione()
    {
      var o = new ExampleClass();
      Action<ExampleClass, int> actionToExecuteOnInstance =
        (x, n) => x.SomeValue = 123 + n;

      var instance =
        o.InstanceExecuteWithObject(111, actionToExecuteOnInstance);

      Assert.That(instance, Is.SameAs(o));
    }

    [Test]
    public void CanCombineCreationAndExecutionToAvoidTemporaryVariables()
    {
      Assert.That(
        new ExampleClass()
          .InstanceExecuteWithObject(
            111, (x, n) => x.SomeValue = 123 + n)
          .SomeValue,
        Is.EqualTo(234));
    }

    private class ExampleClass
    {
      public int SomeValue { get; set; }
    }
  }
}
