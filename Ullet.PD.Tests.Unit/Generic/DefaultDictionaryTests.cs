/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using NUnit.Framework;
using Ullet.PD.Generic;

namespace Ullet.PD.Tests.Unit.Generic
{
  [TestFixture]
  public class DefaultDictionaryTests
  {
    [Test]
    public void CanAddKeyAndValueToDictionary()
    {
      var dictionary =
        new DefaultDictionary<string, string> { { "key", "value" } };

      Assert.That(dictionary["key"], Is.EqualTo("value"));
    }

    [Test]
    public void CanAddMultipleKeysAndValuesToDictionary()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      Assert.That(
        new[] { dictionary["key1"], dictionary["key2"], dictionary["key3"] },
        Is.EqualTo(new[] { "value1", "value2", "value3" }));
    }

    [Test]
    public void AddReturnsFalseIfKeyAlreadyAdded()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"}
        };

      var result = dictionary.Add("key", "other-value");

      Assert.That(result, Is.False);
    }

    [Test]
    public void DoesNotAddElementIfKeyAlreadyAdded()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"},
          {"key", "other-value"}
        };

      Assert.That(dictionary, Has.Count.EqualTo(1));
      Assert.That(dictionary["key"], Is.EqualTo("value"));
    }

    [Test]
    public void AddReturnsTrueIfSuccessfullyAdded()
    {
      var dictionary = new DefaultDictionary<string, string>();

      var result = dictionary.Add("key", "value");

      Assert.That(result, Is.True);
      Assert.That(dictionary["key"], Is.EqualTo("value"));
    }

    [Test]
    public void CanClearAllElements()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"}
        };

      dictionary.Clear();

      Assert.That(dictionary, Is.Empty);
    }

    [Test]
    public void CanTestContainsKeyWhenKeyExists()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"}
        };

      Assert.That(dictionary.ContainsKey("key"), Is.True);
    }

    [Test]
    public void CanTestContainsKeyWhenKeyDoesNotExist()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"}
        };

      Assert.That(dictionary.ContainsKey("other-key"), Is.False);
    }

    [Test]
    public void NeverContainsNullKey()
    {
      var dictionary = new DefaultDictionary<string, string>();

      Assert.That(dictionary.ContainsKey(null), Is.False);
    }

    [Test]
    public void CanCountNumberOfElements()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      Assert.That(dictionary.Count, Is.EqualTo(3));
    }

    [Test]
    public void HasEnumeratorOfKeyValuePairs()
    {
      var enumerator = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        }.GetEnumerator();
      var enumerated = new List<KeyValuePair<string, string>>();

      while (enumerator.MoveNext())
        enumerated.Add(enumerator.Current);

      Assert.That(
        enumerated,
        Is.EquivalentTo(new[]
          {
            new KeyValuePair<string, string>("key1", "value1"),
            new KeyValuePair<string, string>("key2", "value2"),
            new KeyValuePair<string, string>("key3", "value3")
          }));
    }

    [Test]
    public void CanGetCollectionOfKeys()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      var keys = dictionary.Keys;

      Assert.That(keys, Is.EquivalentTo(new[] { "key1", "key2", "key3" }));
    }

    [Test]
    public void CanRemoveElementByItsKey()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      dictionary.Remove("key2");

      Assert.That(
        dictionary,
        Is.EquivalentTo(
          new Dictionary<string, string>
            {
              {"key1", "value1"},
              {"key3", "value3"}
            }));
    }

    [Test]
    public void RemoveReturnsTrueIfElementisRemoved()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      Assert.That(dictionary.Remove("key2"), Is.True);
    }

    [Test]
    public void RemoveReturnsFalseIfKeyNotFound()
    {
      var dictionary = new DefaultDictionary<string, string>();

      Assert.That(dictionary.Remove("key"), Is.False);
    }

    [Test]
    public void NothingRemovedIfKeyIsNull()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      dictionary.Remove(null);

      Assert.That(
        dictionary,
        Is.EquivalentTo(
          new Dictionary<string, string>
            {
              {"key1", "value1"},
              {"key2", "value2"},
              {"key3", "value3"}
            }));
    }

    [Test]
    public void IndexerAddsNewElementForNewKey()
    {
      var dictionary = new DefaultDictionary<string, string>();

      dictionary["key"] = "value";

      Assert.That(dictionary["key"], Is.EqualTo("value"));
    }

    [Test]
    public void IndexerOverwritesValueForExistingKey()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"}
        };

      dictionary["key"] = "other-value";

      Assert.That(dictionary["key"], Is.EqualTo("other-value"));
    }

    [Test]
    public void IndexerAddsNothingIfKeyIsNull()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key", "value"}
        };

      dictionary[null] = "other-value";

      Assert.That(dictionary, Has.Count.EqualTo(1));
      Assert.That(dictionary["key"], Is.EqualTo("value"));
    }

    [Test]
    public void CanGetValueOfElementByItsKey()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      Assert.That(dictionary["key2"], Is.EqualTo("value2"));
    }

    [Test]
    public void IndexerReturnsDefaultForTypeIfKeyNotFound()
    {
      // ReSharper disable once CollectionNeverUpdated.Local
      var dictionary = new DefaultDictionary<string, string>();

      Assert.That(dictionary["missing-key"], Is.EqualTo(null));
    }

    [Test]
    public void IndexerReturnsDefaultForAnotherTypeIfKeyNotFound()
    {
      // ReSharper disable once CollectionNeverUpdated.Local
      var dictionary = new DefaultDictionary<string, int>();

      Assert.That(dictionary["missing-key"], Is.EqualTo(0));
    }

    [Test]
    public void IndexerReturnsDefaultIfKeyNull()
    {
      // ReSharper disable once CollectionNeverUpdated.Local
      var dictionary = new DefaultDictionary<string, int>();

      Assert.That(dictionary[null], Is.EqualTo(0));
    }

    [Test]
    public void CanGetCollectionOfValues()
    {
      var dictionary = new DefaultDictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"},
          {"key3", "value3"}
        };

      var keys = dictionary.Values;

      Assert.That(keys, Is.EquivalentTo(new[] {"value1", "value2", "value3"}));
    }
  }
}
