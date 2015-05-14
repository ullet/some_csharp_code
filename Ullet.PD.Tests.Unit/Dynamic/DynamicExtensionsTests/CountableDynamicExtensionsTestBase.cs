/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

namespace Ullet.PD.Tests.Unit.Dynamic.DynamicExtensionsTests
{
  public abstract class CountableDynamicExtensionsTestBase
  {
    // Class must be public to be "seen" by runtime binder.
    public class Countable
    {
      public int Count { get; set; }
    }

    public class DoubleCountable
    {
      public double Count { get; set; }
    }

    public class Uncountable
    {
    }

    public class StringCountable
    {
      public string Count { get; set; }
    }
  }
}
