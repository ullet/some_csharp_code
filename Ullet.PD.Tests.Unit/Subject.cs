/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.PD.Tests.Unit
{
  public static class Subject
  {
    /// <summary>
    /// Wraps property calls where don't care what return value is, e.g. when
    /// used in Assert.Throws, to avoid 'only assignment ... can be used as a
    /// statement' error.
    /// </summary>
    public static TestDelegate Getter<T>(Func<T> getter)
    {
      return () => getter();
    }
  }

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
