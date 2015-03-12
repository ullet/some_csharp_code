/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

namespace Ullet.PD
{
  /// <summary>
  /// Named item and index tuple.
  /// </summary>
  public class ItemWithIndex<T>
  {
    /// <summary>
    /// Create item and index tuple.
    /// </summary>
    public ItemWithIndex(T item, int index)
    {
      Item = item;
      Index = index;
    }

    /// <summary>
    /// The item.
    /// </summary>
    public T Item { get; private set; }

    /// <summary>
    /// Index of the item.
    /// </summary>
    public int Index { get; private set; }
  }
}
