/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;
using Ullet.PD.Testing;

namespace Ullet.PD.Generic.Tests.Unit
{
  [TestFixture]
  public class MutableTests
  {
    [Test]
    public void CanCreateWithoutValue()
    {
      Assert.That(new Mutable<int>().HasValue, Is.False);
    }

    [Test]
    public void CanCreateWithValue()
    {
      var m = new Mutable<double>(3.6);

      Assert.That(
        m,
        Has.Property("HasValue").EqualTo(true) &
        Has.Property("Value").EqualTo(3.6d).Within(0.000001));
    }

    [Test]
    public void ThrowsExceptionIfGetValueWhenValueNotSet()
    {
      var m = new Mutable<long>();

      Assert.Throws<InvalidOperationException>(Subject.Getter(() => m.Value));
    }

    [Test]
    public void CanModifyValue()
    {
      // ReSharper disable once UseObjectOrCollectionInitializer
      var m = new Mutable<char>('A');

      m.Value = 'B';

      Assert.That(m.Value, Is.EqualTo('B'));
    }

    [Test]
    public void ModifyingValueDoesNotCreateANewMutableInstance()
    {
      var m = new Mutable<float>(1.0f);
      var original = m;

      m.Value = 2.0f;

      Assert.That(m, Is.SameAs(original));
    }

    [Test]
    public void CanExplicitlyConvertValueTypeToMutable()
    {
      const int value = 73;

      var m = (Mutable<int>) value;

      Assert.That(
        m,
        Is.InstanceOf<Mutable<int>>() &
        Has.Property("HasValue").EqualTo(true) &
        Has.Property("Value").EqualTo(value));
    }

    [Test]
    public void CanImplicitlyConvertValueTypeToMutable()
    {
      const long value = 123;

      // Assigning to a variable of a different type is an implicit conversion.
      Mutable<long> m = value;

      Assert.That(
        m,
        Is.InstanceOf<Mutable<long>>() &
        Has.Property("HasValue").EqualTo(true) &
        Has.Property("Value").EqualTo(value));
    }

    [Test]
    public void CanExplicitlyConvertMutableToValueType()
    {
      var m = new Mutable<int>(61);

      var value = (int) m;

      Assert.That(value, Is.EqualTo(m.Value));
    }

    [Test]
    public void CanImplicitlyConvertMutableToValueType()
    {
      var m = new Mutable<byte>(212);

      // Assigning to a variable of a different type is an implicit conversion.
      byte value = m;

      Assert.That(value, Is.EqualTo(m.Value));
    }

    [Test]
    public void ThrowsExceptionIfConvertToValueTypeWhenValueNotSet()
    {
      var m = new Mutable<long>();

      Assert.Throws<InvalidOperationException>(Subject.Getter(() => (long) m));
    }

    [Test]
    public void CanExplicitlyConvertMutableWithValueToNullableOfValueType()
    {
      var m = new Mutable<int>(1);

      var value = (int?)m;

      Assert.That(value.HasValue, Is.True);
      // ReSharper disable once PossibleInvalidOperationException
      Assert.That(value.Value, Is.EqualTo(m.Value));
    }

    [Test]
    public void CanExplicitlyConvertMutableWithoutValueToNullableOfValueType()
    {
      var m = new Mutable<int>();

      var value = (int?)m;

      Assert.That(value.HasValue, Is.False);
    }

    [Test]
    public void CanImplicitlyConvertMutableWithValueToNullableOfValueType()
    {
      var m = new Mutable<int>(1);

      int? value = m;

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      Assert.That(value.HasValue, Is.True);
      // ReSharper disable once PossibleInvalidOperationException
      Assert.That(value.Value, Is.EqualTo(m.Value));
    }

    [Test]
    public void CanImplicitlyConvertMutableWithoutValueToNullableOfValueType()
    {
      var m = new Mutable<int>();

      int? value = m;

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      Assert.That(value.HasValue, Is.False);
    }

    [Test]
    public void ThrowsExceptionIfConvertNullToValueType()
    {
      Mutable<long> m = null;

      // ReSharper disable once ExpressionIsAlwaysNull
      Assert.Throws<NullReferenceException>(Subject.Getter(() => (long)m));
    }

    [Test]
    public void ConvertsNullToNullableOfValueTypeWithoutValueSet()
    {
      Mutable<double> m = null;

      // ReSharper disable once ExpressionIsAlwaysNull
      var nullable = (double?) m;

      Assert.That(nullable.HasValue, Is.False);
    }
  }
}
