/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections;
using System.Collections.Generic;

namespace Ullet.PD.Generic
{
  /// <summary>
  /// Dictionary of keys and values returning a default value when key not
  /// found.
  /// </summary>
  /// <remarks>
  /// Looking up a value for a key not in the dictionary returns default rather
  /// than throw an exception.
  /// Does NOT implement interface <see cref="T:IDictionary{TKey, TValue}"/>.
  /// The dictionary interface expects exceptions to be thrown for missing and
  /// null keys, but this class intentionally breaks that convention.
  /// Unlike the IDictionary interface, this class does not have a TryGetValue
  /// method. This is because the indexer does not throw an exception if the key
  /// is not found, so not expensive to try using an invalid key with the
  /// indexer, and the only reason to use this class, instead of a standard
  /// <see cref="T:Dictionary{TKey, TValue}"/>, is because you don't really care
  /// if the key exists or not; you just want to use a default value if it does
  /// not. A <see cref="ContainsKey"/> method is available if do need to
  /// explicitly check if a key exists.
  /// </remarks>
  /*
   * An explicit interface implementation could be used to "preserve"
   * IDictionary behaviour when cast to the interface, however that would lead
   * to the confusing situation of very different behaviour when cast to the
   * interface. The implementation of this class would be overly complicated by
   * essentially being two implementations in one.  The explicit
   * IDictionary<TKey, TValue> interface would behave exactly like a standard
   * Dictionary<TKey, TValue> class, losing everything "special" about this
   * class.
   */
  public class DefaultDictionary<TKey, TValue>
    : IEnumerable<KeyValuePair<TKey, TValue>>
  {
    private readonly IDictionary<TKey, TValue> _innerDictionary;

    /// <summary>
    /// Create new dictionary of values.
    /// </summary>
    public DefaultDictionary()
      : this(default(TValue))
    {
    }

    /// <summary>
    /// Create new dictionary of values.
    /// </summary>
    public DefaultDictionary(TValue defaultValue)
    {
      _innerDictionary = new Dictionary<TKey, TValue>();
      DefaultValue = defaultValue;
    }

    /// <summary>
    /// Get or set default value returned if key not found.
    /// </summary>
    public TValue DefaultValue { get; set; }

    /// <summary>
    /// Adds an element with the provided key and value to the dictionary.
    /// </summary>
    /// <param name="key">
    /// The object to use as the key of the element to add.
    /// </param>
    /// <param name="value">
    /// The object to use as the value of the element to add.
    /// </param>
    public bool Add(TKey key, TValue value)
    {
      if (Equals(null, key) || ContainsKey(key))
        return false;
      _innerDictionary.Add(key, value);
      return true;
    }

    /// <summary>
    /// Removes all items from the dictionary.
    /// </summary>
    public void Clear()
    {
      _innerDictionary.Clear();
    }

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified
    /// key.
    /// </summary>
    /// <returns>
    /// true if the dictionary contains an element with the key;
    /// otherwise, false.
    /// </returns>
    /// <param name="key">The key to locate in the dictionary.</param>
    public bool ContainsKey(TKey key)
    {
      return !Equals(key, null) && _innerDictionary.ContainsKey(key);
    }

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    /// <returns>
    /// The number of elements contained in the dictionary.
    /// </returns>
    public int Count
    {
      get { return _innerDictionary.Count; }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return _innerDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    /// <summary>
    /// Gets a collection containing the keys of the dictionary.
    /// </summary>
    /// <returns>
    /// A collection containing the keys of the dictionary.
    /// </returns>
    public ICollection<TKey> Keys
    {
      get { return _innerDictionary.Keys; }
    }

    /// <summary>
    /// Removes the element with the specified key from the dictionary.
    /// </summary>
    /// <returns>
    /// true if the element is successfully removed; otherwise, false.
    /// This method also returns false if <paramref name="key"/> was not
    /// found in the original dictionary.
    /// </returns>
    /// <param name="key">The key of the element to remove.</param>
    public bool Remove(TKey key)
    {
      return !Equals(null, key) && _innerDictionary.Remove(key);
    }

    /// <summary>
    /// Gets or sets the element with the specified key.
    /// </summary>
    /// <returns>
    /// The element with the specified key.
    /// </returns>
    /// <param name="key">The key of the element to get or set.</param>
    /// <remarks>
    /// Returns default value for type if key not found or key is null.
    /// Element will not be set if key is null, an exception will not be thrown.
    /// </remarks>
    public TValue this[TKey key]
    {
      get
      {
        return ContainsKey(key) ? _innerDictionary[key] : DefaultValue;
      }
      set
      {
        if (!Equals(null, key))
          _innerDictionary[key] = value;
      }
    }

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    /// <returns>
    /// A collection containing the values in the dictionary.
    /// </returns>
    public ICollection<TValue> Values
    {
      get { return _innerDictionary.Values; }
    }
  }
}
