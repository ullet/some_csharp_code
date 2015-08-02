/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using NUnit.Framework;

namespace Ullet.PD.Testing.Tests.Unit
{
  [TestFixture]
  public class SubjectTests
  {
    [Test]
    public void GetterReturnsDelegateCallingGetter()
    {
      var called = false;
      var testDelegate = Subject.Getter(() =>
      {
        called = true;
        return "some property value";
      });
      testDelegate();
      Assert.That(called, Is.True);
    }
  }
}
