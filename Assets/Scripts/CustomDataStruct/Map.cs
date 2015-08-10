// Decompiled with JetBrains decompiler
// Type: Map`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

public class Map<TKey, TValue> : IEnumerable, IEnumerable<KeyValuePair<TKey, TValue>>
{
  private const int INITIAL_SIZE = 4;
  private const float DEFAULT_LOAD_FACTOR = 0.9f;
  private const int NO_SLOT = -1;
  private const int HASH_FLAG = -2147483648;
  private int[] table;
  private Link[] linkSlots;
  private TKey[] keySlots;
  private TValue[] valueSlots;
  private IEqualityComparer<TKey> hcp;
  private int touchedSlots;
  private int emptySlot;
  private int count;
  private int threshold;
  private int generation;

  public int Count
  {
    get
    {
      return this.count;
    }
  }

  public TValue this[TKey key]
  {
    get
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      int num = this.hcp.GetHashCode(key) | int.MinValue;
      for (int index = this.table[(num & int.MaxValue) % this.table.Length] - 1; index != -1; index = this.linkSlots[index].Next)
      {
        if (this.linkSlots[index].HashCode == num && this.hcp.Equals(this.keySlots[index], key))
          return this.valueSlots[index];
      }
      throw new KeyNotFoundException();
    }
    set
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      int num = this.hcp.GetHashCode(key) | int.MinValue;
      int index1 = (num & int.MaxValue) % this.table.Length;
      int index2 = this.table[index1] - 1;
      int index3 = -1;
      if (index2 != -1)
      {
        while (this.linkSlots[index2].HashCode != num || !this.hcp.Equals(this.keySlots[index2], key))
        {
          index3 = index2;
          index2 = this.linkSlots[index2].Next;
          if (index2 == -1)
            break;
        }
      }
      if (index2 == -1)
      {
        if (++this.count > this.threshold)
        {
          this.Resize();
          index1 = (num & int.MaxValue) % this.table.Length;
        }
        index2 = this.emptySlot;
        if (index2 == -1)
          index2 = this.touchedSlots++;
        else
          this.emptySlot = this.linkSlots[index2].Next;
        this.linkSlots[index2].Next = this.table[index1] - 1;
        this.table[index1] = index2 + 1;
        this.linkSlots[index2].HashCode = num;
        this.keySlots[index2] = key;
      }
      else if (index3 != -1)
      {
        this.linkSlots[index3].Next = this.linkSlots[index2].Next;
        this.linkSlots[index2].Next = this.table[index1] - 1;
        this.table[index1] = index2 + 1;
      }
      this.valueSlots[index2] = value;
      ++this.generation;
    }
  }

  public Map<TKey, TValue>.KeyCollection Keys
  {
    get
    {
      return new Map<TKey, TValue>.KeyCollection(this);
    }
  }

  public Map<TKey, TValue>.ValueCollection Values
  {
    get
    {
      return new Map<TKey, TValue>.ValueCollection(this);
    }
  }

  public Map()
  {
    this.Init(4, (IEqualityComparer<TKey>) null);
  }

  public Map(int count)
  {
    this.Init(count, (IEqualityComparer<TKey>) null);
  }

  public Map(IEqualityComparer<TKey> comparer)
  {
    this.Init(4, comparer);
  }

  public Map(IEnumerable<KeyValuePair<TKey, TValue>> copy)
  {
    this.Init(4, (IEqualityComparer<TKey>) null);
    foreach (KeyValuePair<TKey, TValue> keyValuePair in copy)
      this[keyValuePair.Key] = keyValuePair.Value;
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new Map<TKey, TValue>.Enumerator(this);
  }

  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<TKey, TValue>>) new Map<TKey, TValue>.Enumerator(this);
  }

  private void Init(int capacity, IEqualityComparer<TKey> hcp)
  {
    this.hcp = hcp ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    capacity = Math.Max(1, (int) ((double) capacity / 0.899999976158142));
    this.InitArrays(capacity);
  }

  private void InitArrays(int size)
  {
    this.table = new int[size];
    this.linkSlots = new Link[size];
    this.emptySlot = -1;
    this.keySlots = new TKey[size];
    this.valueSlots = new TValue[size];
    this.touchedSlots = 0;
    this.threshold = (int) ((double) this.table.Length * 0.899999976158142);
    if (this.threshold != 0 || this.table.Length <= 0)
      return;
    this.threshold = 1;
  }

  private void CopyToCheck(Array array, int index)
  {
    if (array == null)
      throw new ArgumentNullException("array");
    if (index < 0)
      throw new ArgumentOutOfRangeException("index");
    if (index > array.Length)
      throw new ArgumentException("index larger than largest valid index of array");
    if (array.Length - index < this.Count)
      throw new ArgumentException("Destination array cannot hold the requested elements!");
  }

  private void CopyKeys(TKey[] array, int index)
  {
    for (int index1 = 0; index1 < this.touchedSlots; ++index1)
    {
      if ((this.linkSlots[index1].HashCode & int.MinValue) != 0)
        array[index++] = this.keySlots[index1];
    }
  }

  private void CopyValues(TValue[] array, int index)
  {
    for (int index1 = 0; index1 < this.touchedSlots; ++index1)
    {
      if ((this.linkSlots[index1].HashCode & int.MinValue) != 0)
        array[index++] = this.valueSlots[index1];
    }
  }

  private static KeyValuePair<TKey, TValue> make_pair(TKey key, TValue value)
  {
    return new KeyValuePair<TKey, TValue>(key, value);
  }

  private static TKey pick_key(TKey key, TValue value)
  {
    return key;
  }

  private static TValue pick_value(TKey key, TValue value)
  {
    return value;
  }

  private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
  {
    this.CopyToCheck((Array) array, index);
    for (int index1 = 0; index1 < this.touchedSlots; ++index1)
    {
      if ((this.linkSlots[index1].HashCode & int.MinValue) != 0)
        array[index++] = new KeyValuePair<TKey, TValue>(this.keySlots[index1], this.valueSlots[index1]);
    }
  }

  private void Do_ICollectionCopyTo<TRet>(Array array, int index, Map<TKey, TValue>.Transform<TRet> transform)
  {
    System.Type c = typeof (TRet);
    System.Type elementType = array.GetType().GetElementType();
    try
    {
      if ((c.IsPrimitive || elementType.IsPrimitive) && !elementType.IsAssignableFrom(c))
        throw new Exception();
      object[] objArray = (object[]) array;
      for (int index1 = 0; index1 < this.touchedSlots; ++index1)
      {
        if ((this.linkSlots[index1].HashCode & int.MinValue) != 0)
          objArray[index++] = (object) transform(this.keySlots[index1], this.valueSlots[index1]);
      }
    }
    catch (Exception ex)
    {
      throw new ArgumentException("Cannot copy source collection elements to destination array", "array", ex);
    }
  }

  private void Resize()
  {
    int length = HashPrimeNumbers.ToPrime(this.table.Length << 1 | 1);
    int[] numArray = new int[length];
    Link[] linkArray = new Link[length];
    for (int index1 = 0; index1 < this.table.Length; ++index1)
    {
      for (int index2 = this.table[index1] - 1; index2 != -1; index2 = this.linkSlots[index2].Next)
      {
        int index3 = ((linkArray[index2].HashCode = this.hcp.GetHashCode(this.keySlots[index2]) | int.MinValue) & int.MaxValue) % length;
        linkArray[index2].Next = numArray[index3] - 1;
        numArray[index3] = index2 + 1;
      }
    }
    this.table = numArray;
    this.linkSlots = linkArray;
    TKey[] keyArray = new TKey[length];
    TValue[] objArray = new TValue[length];
    Array.Copy((Array) this.keySlots, 0, (Array) keyArray, 0, this.touchedSlots);
    Array.Copy((Array) this.valueSlots, 0, (Array) objArray, 0, this.touchedSlots);
    this.keySlots = keyArray;
    this.valueSlots = objArray;
    this.threshold = (int) ((double) length * 0.899999976158142);
  }

  public void Add(TKey key, TValue value)
  {
    if ((object) key == null)
      throw new ArgumentNullException("key");
    int num = this.hcp.GetHashCode(key) | int.MinValue;
    int index1 = (num & int.MaxValue) % this.table.Length;
    for (int index2 = this.table[index1] - 1; index2 != -1; index2 = this.linkSlots[index2].Next)
    {
      if (this.linkSlots[index2].HashCode == num && this.hcp.Equals(this.keySlots[index2], key))
        throw new ArgumentException("An element with the same key already exists in the dictionary.");
    }
    if (++this.count > this.threshold)
    {
      this.Resize();
      index1 = (num & int.MaxValue) % this.table.Length;
    }
    int index3 = this.emptySlot;
    if (index3 == -1)
      index3 = this.touchedSlots++;
    else
      this.emptySlot = this.linkSlots[index3].Next;
    this.linkSlots[index3].HashCode = num;
    this.linkSlots[index3].Next = this.table[index1] - 1;
    this.table[index1] = index3 + 1;
    this.keySlots[index3] = key;
    this.valueSlots[index3] = value;
    ++this.generation;
  }

  public void Clear()
  {
    if (this.count == 0)
      return;
    this.count = 0;
    Array.Clear((Array) this.table, 0, this.table.Length);
    Array.Clear((Array) this.keySlots, 0, this.keySlots.Length);
    Array.Clear((Array) this.valueSlots, 0, this.valueSlots.Length);
    Array.Clear((Array) this.linkSlots, 0, this.linkSlots.Length);
    this.emptySlot = -1;
    this.touchedSlots = 0;
    ++this.generation;
  }

  public bool ContainsKey(TKey key)
  {
    if ((object) key == null)
      throw new ArgumentNullException("key");
    int num = this.hcp.GetHashCode(key) | int.MinValue;
    for (int index = this.table[(num & int.MaxValue) % this.table.Length] - 1; index != -1; index = this.linkSlots[index].Next)
    {
      if (this.linkSlots[index].HashCode == num && this.hcp.Equals(this.keySlots[index], key))
        return true;
    }
    return false;
  }

  public bool ContainsValue(TValue value)
  {
    IEqualityComparer<TValue> equalityComparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
    for (int index1 = 0; index1 < this.table.Length; ++index1)
    {
      for (int index2 = this.table[index1] - 1; index2 != -1; index2 = this.linkSlots[index2].Next)
      {
        if (equalityComparer.Equals(this.valueSlots[index2], value))
          return true;
      }
    }
    return false;
  }

  public bool Remove(TKey key)
  {
    if ((object) key == null)
      throw new ArgumentNullException("key");
    int num = this.hcp.GetHashCode(key) | int.MinValue;
    int index1 = (num & int.MaxValue) % this.table.Length;
    int index2 = this.table[index1] - 1;
    if (index2 == -1)
      return false;
    int index3 = -1;
    while (this.linkSlots[index2].HashCode != num || !this.hcp.Equals(this.keySlots[index2], key))
    {
      index3 = index2;
      index2 = this.linkSlots[index2].Next;
      if (index2 == -1)
        break;
    }
    if (index2 == -1)
      return false;
    --this.count;
    if (index3 == -1)
      this.table[index1] = this.linkSlots[index2].Next + 1;
    else
      this.linkSlots[index3].Next = this.linkSlots[index2].Next;
    this.linkSlots[index2].Next = this.emptySlot;
    this.emptySlot = index2;
    this.linkSlots[index2].HashCode = 0;
    this.keySlots[index2] = default (TKey);
    this.valueSlots[index2] = default (TValue);
    ++this.generation;
    return true;
  }

  public bool TryGetValue(TKey key, out TValue value)
  {
    if ((object) key == null)
      throw new ArgumentNullException("key");
    int num = this.hcp.GetHashCode(key) | int.MinValue;
    for (int index = this.table[(num & int.MaxValue) % this.table.Length] - 1; index != -1; index = this.linkSlots[index].Next)
    {
      if (this.linkSlots[index].HashCode == num && this.hcp.Equals(this.keySlots[index], key))
      {
        value = this.valueSlots[index];
        return true;
      }
    }
    value = default (TValue);
    return false;
  }

  public Map<TKey, TValue>.Enumerator GetEnumerator()
  {
    return new Map<TKey, TValue>.Enumerator(this);
  }

  public struct Enumerator : IDisposable, IEnumerator, IEnumerator<KeyValuePair<TKey, TValue>>
  {
    private Map<TKey, TValue> dictionary;
    private int next;
    private int stamp;
    internal KeyValuePair<TKey, TValue> current;

    object IEnumerator.Current
    {
      get
      {
        this.VerifyCurrent();
        return (object) this.current;
      }
    }

    public KeyValuePair<TKey, TValue> Current
    {
      get
      {
        return this.current;
      }
    }

    internal TKey CurrentKey
    {
      get
      {
        this.VerifyCurrent();
        return this.current.Key;
      }
    }

    internal TValue CurrentValue
    {
      get
      {
        this.VerifyCurrent();
        return this.current.Value;
      }
    }

    internal Enumerator(Map<TKey, TValue> dictionary)
    {
      this.dictionary = dictionary;
      this.stamp = dictionary.generation;
      this.next = 0;
      this.current =  new KeyValuePair<TKey, TValue>();
    }

    void IEnumerator.Reset()
    {
      this.Reset();
    }

    public bool MoveNext()
    {
      this.VerifyState();
      if (this.next < 0)
        return false;
      while (this.next < this.dictionary.touchedSlots)
      {
        int index = this.next++;
        if ((this.dictionary.linkSlots[index].HashCode & int.MinValue) != 0)
        {
          this.current = new KeyValuePair<TKey, TValue>(this.dictionary.keySlots[index], this.dictionary.valueSlots[index]);
          return true;
        }
      }
      this.next = -1;
      return false;
    }

    internal void Reset()
    {
      this.VerifyState();
      this.next = 0;
    }

    private void VerifyState()
    {
      if (this.dictionary == null)
        throw new ObjectDisposedException((string) null);
      if (this.dictionary.generation != this.stamp)
        throw new InvalidOperationException("out of sync");
    }

    private void VerifyCurrent()
    {
      this.VerifyState();
      if (this.next <= 0)
        throw new InvalidOperationException("Current is not valid");
    }

    public void Dispose()
    {
      this.dictionary = (Map<TKey, TValue>) null;
    }
  }

  public sealed class KeyCollection : ICollection, IEnumerable, ICollection<TKey>, IEnumerable<TKey>
  {
    private Map<TKey, TValue> dictionary;

    bool ICollection<TKey>.IsReadOnly
    {
      get
      {
        return true;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return ((ICollection) this.dictionary).SyncRoot;
      }
    }

    public int Count
    {
      get
      {
        return this.dictionary.Count;
      }
    }

    public KeyCollection(Map<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      this.dictionary = dictionary;
    }

    void ICollection<TKey>.Add(TKey item)
    {
      throw new NotSupportedException("this is a read-only collection");
    }

    void ICollection<TKey>.Clear()
    {
      throw new NotSupportedException("this is a read-only collection");
    }

    bool ICollection<TKey>.Contains(TKey item)
    {
      return this.dictionary.ContainsKey(item);
    }

    bool ICollection<TKey>.Remove(TKey item)
    {
      throw new NotSupportedException("this is a read-only collection");
    }

    IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
    {
      return (IEnumerator<TKey>) this.GetEnumerator();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      TKey[] array1 = array as TKey[];
      if (array1 != null)
      {
        this.CopyTo(array1, index);
      }
      else
      {
        this.dictionary.CopyToCheck(array, index);
        this.dictionary.Do_ICollectionCopyTo<TKey>(array, index, new Map<TKey, TValue>.Transform<TKey>(Map<TKey, TValue>.pick_key));
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void CopyTo(TKey[] array, int index)
    {
      this.dictionary.CopyToCheck((Array) array, index);
      this.dictionary.CopyKeys(array, index);
    }

    public Map<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
    {
      return new Map<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
    }

    public struct Enumerator : IDisposable, IEnumerator, IEnumerator<TKey>
    {
      private Map<TKey, TValue>.Enumerator host_enumerator;

      object IEnumerator.Current
      {
        get
        {
          return (object) this.host_enumerator.CurrentKey;
        }
      }

      public TKey Current
      {
        get
        {
          return this.host_enumerator.current.Key;
        }
      }

      internal Enumerator(Map<TKey, TValue> host)
      {
        this.host_enumerator = host.GetEnumerator();
      }

      void IEnumerator.Reset()
      {
        this.host_enumerator.Reset();
      }

      public void Dispose()
      {
        this.host_enumerator.Dispose();
      }

      public bool MoveNext()
      {
        return this.host_enumerator.MoveNext();
      }
    }
  }

  public sealed class ValueCollection : ICollection, IEnumerable, ICollection<TValue>, IEnumerable<TValue>
  {
    private Map<TKey, TValue> dictionary;

    bool ICollection<TValue>.IsReadOnly
    {
      get
      {
        return true;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return ((ICollection) this.dictionary).SyncRoot;
      }
    }

    public int Count
    {
      get
      {
        return this.dictionary.Count;
      }
    }

    public ValueCollection(Map<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      this.dictionary = dictionary;
    }

    void ICollection<TValue>.Add(TValue item)
    {
      throw new NotSupportedException("this is a read-only collection");
    }

    void ICollection<TValue>.Clear()
    {
      throw new NotSupportedException("this is a read-only collection");
    }

    bool ICollection<TValue>.Contains(TValue item)
    {
      return this.dictionary.ContainsValue(item);
    }

    bool ICollection<TValue>.Remove(TValue item)
    {
      throw new NotSupportedException("this is a read-only collection");
    }

    IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
    {
      return (IEnumerator<TValue>) this.GetEnumerator();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      TValue[] array1 = array as TValue[];
      if (array1 != null)
      {
        this.CopyTo(array1, index);
      }
      else
      {
        this.dictionary.CopyToCheck(array, index);
        this.dictionary.Do_ICollectionCopyTo<TValue>(array, index, new Map<TKey, TValue>.Transform<TValue>(Map<TKey, TValue>.pick_value));
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void CopyTo(TValue[] array, int index)
    {
      this.dictionary.CopyToCheck((Array) array, index);
      this.dictionary.CopyValues(array, index);
    }

    public Map<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
    {
      return new Map<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
    }

    public struct Enumerator : IDisposable, IEnumerator, IEnumerator<TValue>
    {
      private Map<TKey, TValue>.Enumerator host_enumerator;

      object IEnumerator.Current
      {
        get
        {
          return (object) this.host_enumerator.CurrentValue;
        }
      }

      public TValue Current
      {
        get
        {
          return this.host_enumerator.current.Value;
        }
      }

      internal Enumerator(Map<TKey, TValue> host)
      {
        this.host_enumerator = host.GetEnumerator();
      }

      void IEnumerator.Reset()
      {
        this.host_enumerator.Reset();
      }

      public void Dispose()
      {
        this.host_enumerator.Dispose();
      }

      public bool MoveNext()
      {
        return this.host_enumerator.MoveNext();
      }
    }
  }

  private delegate TRet Transform<TRet>(TKey key, TValue value);
}
