// Decompiled with JetBrains decompiler
// Type: GeneralUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

public static class GeneralUtils
{
  public const float DEVELOPMENT_BUILD_TEXT_WIDTH = 115f;

  public static void Swap<T>(ref T a, ref T b)
  {
    T obj = a;
    a = b;
    b = obj;
  }

  public static void ListSwap<T>(IList<T> list, int indexA, int indexB)
  {
    T obj = list[indexA];
    list[indexA] = list[indexB];
    list[indexB] = obj;
  }

  public static void ListMove<T>(IList<T> list, int srcIndex, int dstIndex)
  {
    if (srcIndex == dstIndex)
      return;
    T obj = list[srcIndex];
    list.RemoveAt(srcIndex);
    if (dstIndex > srcIndex)
      --dstIndex;
    list.Insert(dstIndex, obj);
  }

  public static T[] Combine<T>(T[] arr1, T[] arr2)
  {
    T[] objArray = new T[arr1.Length + arr2.Length];
    Array.Copy((Array) arr1, 0, (Array) objArray, 0, arr1.Length);
    Array.Copy((Array) arr1, 0, (Array) objArray, arr1.Length, arr2.Length);
    return objArray;
  }

  public static bool Contains(this string str, string val, StringComparison comparison)
  {
    return str.IndexOf(val, comparison) >= 0;
  }

  public static T[] Slice<T>(this T[] arr, int start, int end)
  {
    int length1 = arr.Length;
    if (start < 0)
      start = length1 + start;
    if (end < 0)
      end = length1 + end;
    int length2 = end - start;
    if (length2 <= 0)
      return new T[0];
    int num = length1 - start;
    if (length2 > num)
      length2 = num;
    T[] objArray = new T[length2];
    Array.Copy((Array) arr, start, (Array) objArray, 0, length2);
    return objArray;
  }

  public static T[] Slice<T>(this T[] arr, int start)
  {
    return GeneralUtils.Slice<T>(arr, start, arr.Length);
  }

  public static T[] Slice<T>(this T[] arr)
  {
    return GeneralUtils.Slice<T>(arr, 0, arr.Length);
  }

  public static bool IsOverriddenMethod(MethodInfo childMethod, MethodInfo ancestorMethod)
  {
    if (childMethod == null || ancestorMethod == null || childMethod.Equals((object) ancestorMethod))
      return false;
    MethodInfo baseDefinition = childMethod.GetBaseDefinition();
    while (!baseDefinition.Equals((object) childMethod) && !baseDefinition.Equals((object) ancestorMethod))
    {
      MethodInfo methodInfo = baseDefinition;
      baseDefinition = baseDefinition.GetBaseDefinition();
      if (baseDefinition.Equals((object) methodInfo))
        return false;
    }
    return baseDefinition.Equals((object) ancestorMethod);
  }

  public static bool IsObjectAlive(object obj)
  {
    if (obj == null)
      return false;
    if (!(obj is UnityEngine.Object))
      return true;
    return (bool) ((UnityEngine.Object) obj);
  }

  public static bool IsCallbackValid(Delegate callback)
  {
    bool flag = true;
    if (callback == null)
      flag = false;
    else if (!callback.Method.IsStatic)
    {
      flag = GeneralUtils.IsObjectAlive(callback.Target);
      if (!flag)
        Debug.LogError((object) string.Format("Target for callback {0} is null.", (object) callback.Method.Name));
    }
    return flag;
  }

  public static bool IsEditorPlaying()
  {
    return false;
  }

  public static void ExitApplication()
  {
    Application.Quit();
  }

  public static bool IsDevelopmentBuildTextVisible()
  {
    return Debug.isDebugBuild;
  }

  public static bool TryParseBool(string strVal, out bool boolVal)
  {
    if (bool.TryParse(strVal, out boolVal))
      return true;
    string str = strVal.ToLowerInvariant().Trim();
    if (str == "off" || str == "0" || str == "false")
    {
      boolVal = false;
      return true;
    }
    if (str == "on" || str == "1" || str == "true")
    {
      boolVal = true;
      return true;
    }
    boolVal = false;
    return false;
  }

  public static bool ForceBool(string strVal)
  {
    string str = strVal.ToLowerInvariant().Trim();
    return str == "on" || str == "1" || str == "true";
  }

  public static bool TryParseInt(string str, out int val)
  {
    return int.TryParse(str, NumberStyles.Any, (IFormatProvider) null, out val);
  }

  public static int ForceInt(string str)
  {
    int val = 0;
    GeneralUtils.TryParseInt(str, out val);
    return val;
  }

  public static bool TryParseLong(string str, out long val)
  {
    return long.TryParse(str, NumberStyles.Any, (IFormatProvider) null, out val);
  }

  public static long ForceLong(string str)
  {
    long val = 0L;
    GeneralUtils.TryParseLong(str, out val);
    return val;
  }

  public static bool TryParseULong(string str, out ulong val)
  {
    return ulong.TryParse(str, NumberStyles.Any, (IFormatProvider) null, out val);
  }

  public static ulong ForceULong(string str)
  {
    ulong val = 0UL;
    GeneralUtils.TryParseULong(str, out val);
    return val;
  }

  public static bool TryParseFloat(string str, out float val)
  {
    return float.TryParse(str, NumberStyles.Any, (IFormatProvider) null, out val);
  }

  public static float ForceFloat(string str)
  {
    float val = 0.0f;
    GeneralUtils.TryParseFloat(str, out val);
    return val;
  }

  public static bool RandomBool()
  {
    return UnityEngine.Random.Range(0, 2) == 0;
  }

  public static int UnsignedMod(int x, int y)
  {
    int num = x % y;
    if (num < 0)
      num += y;
    return num;
  }

  public static bool IsEven(int n)
  {
    return (n & 1) == 0;
  }

  public static bool IsOdd(int n)
  {
    return (n & 1) == 1;
  }

  public static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T> func)
  {
    if (enumerable == null)
      return;
    foreach (T obj in enumerable)
      func(obj);
  }

  public static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T, int> func)
  {
    if (enumerable == null)
      return;
    int num = 0;
    foreach (T obj in enumerable)
    {
      func(obj, num);
      ++num;
    }
  }

  public static void ForEachReassign<T>(this T[] array, Func<T, T> func)
  {
    if (array == null)
      return;
    for (int index = 0; index < array.Length; ++index)
      array[index] = func(array[index]);
  }

  public static void ForEachReassign<T>(this T[] array, Func<T, int, T> func)
  {
    if (array == null)
      return;
    for (int index = 0; index < array.Length; ++index)
      array[index] = func(array[index], index);
  }

  public static bool AreArraysEqual<T>(T[] arr1, T[] arr2)
  {
    if (arr1 == arr2)
      return true;
    if (arr1 == null || arr2 == null || arr1.Length != arr2.Length)
      return false;
    for (int index = 0; index < arr1.Length; ++index)
    {
      if (!arr1[index].Equals((object) arr2[index]))
        return false;
    }
    return true;
  }

  public static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
  {
    return GeneralUtils.AreArraysEqual<byte>(bytes1, bytes2);
  }

  public static T DeepClone<T>(T obj)
  {
    return (T) GeneralUtils.CloneValue((object) obj, ((object) obj).GetType());
  }

  private static object CloneClass(object obj, System.Type objType)
  {
    object newType = GeneralUtils.CreateNewType(objType);
    foreach (FieldInfo fieldInfo in objType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      fieldInfo.SetValue(newType, GeneralUtils.CloneValue(fieldInfo.GetValue(obj), fieldInfo.FieldType));
    return newType;
  }

  private static object CloneValue(object src, System.Type type)
  {
    if (src != null && type != typeof (string) && type.IsClass)
    {
      if (!type.IsGenericType)
        return GeneralUtils.CloneClass(src, type);
      if (src is IDictionary)
      {
        IDictionary dictionary1 = src as IDictionary;
        IDictionary dictionary2 = GeneralUtils.CreateNewType(type) as IDictionary;
        System.Type type1 = type.GetGenericArguments()[0];
        System.Type type2 = type.GetGenericArguments()[1];
        foreach (DictionaryEntry dictionaryEntry in dictionary1)
          dictionary2.Add(GeneralUtils.CloneValue(dictionaryEntry.Key, type1), GeneralUtils.CloneValue(dictionaryEntry.Value, type2));
        return (object) dictionary2;
      }
      if (src is IList)
      {
        IList list1 = src as IList;
        IList list2 = GeneralUtils.CreateNewType(type) as IList;
        System.Type type1 = type.GetGenericArguments()[0];
        foreach (object src1 in (IEnumerable) list1)
          list2.Add(GeneralUtils.CloneValue(src1, type1));
        return (object) list2;
      }
    }
    return src;
  }

  private static object CreateNewType(System.Type type)
  {
    object instance = Activator.CreateInstance(type);
    if (instance == null)
      throw new SystemException(string.Format("Unable to instantiate type {0} with default constructor.", (object) type.Name));
    return instance;
  }

  public static void DeepReset<T>(T obj)
  {
    System.Type type = typeof (T);
    T instance = Activator.CreateInstance<T>();
    if ((object) instance == null)
      throw new SystemException(string.Format("Unable to instantiate type {0} with default constructor.", (object) type.Name));
    foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
      fieldInfo.SetValue((object) obj, fieldInfo.GetValue((object) instance));
  }

  public static void CleanNullObjectsFromList<T>(List<T> list)
  {
    int index = 0;
    while (index < list.Count)
    {
      if ((object) list[index] == null)
        list.RemoveAt(index);
      else
        ++index;
    }
  }

  public static void CleanDeadObjectsFromList<T>(List<T> list) where T : Component
  {
    int index = 0;
    while (index < list.Count)
    {
      if ((bool) ((UnityEngine.Object) list[index]))
        ++index;
      else
        list.RemoveAt(index);
    }
  }

  public static void CleanDeadObjectsFromList(List<GameObject> list)
  {
    int index = 0;
    while (index < list.Count)
    {
      if ((bool) ((UnityEngine.Object) list[index]))
        ++index;
      else
        list.RemoveAt(index);
    }
  }

  public static string SafeFormat(string format, params object[] args)
  {
    return args.Length != 0 ? string.Format(format, args) : format;
  }
}
