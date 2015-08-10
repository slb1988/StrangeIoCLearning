// Decompiled with JetBrains decompiler
// Type: EnumUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.ComponentModel;

public class EnumUtils
{
  private static Map<System.Type, Map<string, object>> s_enumCache = new Map<System.Type, Map<string, object>>();

  public static string GetString<T>(T enumVal)
  {
    string name = enumVal.ToString();
    DescriptionAttribute[] descriptionAttributeArray = (DescriptionAttribute[]) enumVal.GetType().GetField(name).GetCustomAttributes(typeof (DescriptionAttribute), false);
    if (descriptionAttributeArray.Length > 0)
      return descriptionAttributeArray[0].Description;
    return name;
  }

  public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T result)
  {
    System.Type type = typeof (T);
    Map<string, object> map;
    EnumUtils.s_enumCache.TryGetValue(type, out map);
    object obj;
    if (map != null && map.TryGetValue(str, out obj))
    {
      result = (T) obj;
      return true;
    }
    foreach (T enumVal in Enum.GetValues(type))
    {
      bool flag = false;
      if (EnumUtils.GetString<T>(enumVal).Equals(str, comparisonType))
      {
        flag = true;
        result = enumVal;
      }
      else
      {
        foreach (DescriptionAttribute descriptionAttribute in (DescriptionAttribute[]) enumVal.GetType().GetField(enumVal.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false))
        {
          if (descriptionAttribute.Description.Equals(str, comparisonType))
          {
            flag = true;
            break;
          }
        }
      }
      if (flag)
      {
        if (map == null)
        {
          map = new Map<string, object>();
          EnumUtils.s_enumCache.Add(type, map);
        }
        if (!map.ContainsKey(str))
          map.Add(str, (object) enumVal);
        result = enumVal;
        return true;
      }
    }
    result = default (T);
    return false;
  }

  public static T GetEnum<T>(string str)
  {
    return EnumUtils.GetEnum<T>(str, StringComparison.Ordinal);
  }

  public static T GetEnum<T>(string str, StringComparison comparisonType)
  {
    T result;
    if (EnumUtils.TryGetEnum<T>(str, comparisonType, out result))
      return result;
    throw new ArgumentException(string.Format("EnumUtils.GetEnum() - \"{0}\" has no matching value in enum {1}", (object) str, (object) typeof (T)));
  }

  public static bool TryGetEnum<T>(string str, out T outVal)
  {
    return EnumUtils.TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
  }

  public static T Parse<T>(string str)
  {
    return (T) Enum.Parse(typeof (T), str);
  }

  public static T SafeParse<T>(string str)
  {
    try
    {
      return (T) Enum.Parse(typeof (T), str);
    }
    catch (Exception ex)
    {
      return default (T);
    }
  }

  public static bool TryCast<T>(object inVal, out T outVal)
  {
    outVal = default (T);
    try
    {
      outVal = (T) inVal;
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public static int Length<T>()
  {
    return Enum.GetValues(typeof (T)).Length;
  }
}
