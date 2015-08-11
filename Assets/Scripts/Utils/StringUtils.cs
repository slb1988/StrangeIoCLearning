// Decompiled with JetBrains decompiler
// Type: StringUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text.RegularExpressions;

public class StringUtils
{
  private static readonly string[] SPLIT_LINES_CHARS;

  static StringUtils()
  {
    string[] strArray = new string[2];
    int index1 = 0;
    string str1 = "\n";
    strArray[index1] = str1;
    int index2 = 1;
    string str2 = "\r";
    strArray[index2] = str2;
    StringUtils.SPLIT_LINES_CHARS = strArray;
  }

  public static string StripNonNumbers(string str)
  {
    return Regex.Replace(str, "[^0-9]", string.Empty);
  }

  public static string StripNewlines(string str)
  {
    return Regex.Replace(str, "[\\r\\n]", string.Empty);
  }

  public static string[] SplitLines(string str)
  {
    return str.Split(StringUtils.SPLIT_LINES_CHARS, StringSplitOptions.RemoveEmptyEntries);
  }

  public static bool CompareIgnoreCase(string a, string b)
  {
    return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;
  }
}
