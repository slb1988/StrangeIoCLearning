// Decompiled with JetBrains decompiler
// Type: ConfigFile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ConfigFile
{
  private List<ConfigFile.Line> m_lines = new List<ConfigFile.Line>();
  private string m_path;

  public string GetPath()
  {
    return this.m_path;
  }

  public bool LightLoad(string path)
  {
    return this.Load(path, true);
  }

  public bool FullLoad(string path)
  {
    return this.Load(path, false);
  }

  public bool Save(string path = null)
  {
    if (path == null)
      path = this.m_path;
    if (path == null)
    {
      Debug.LogError((object) "ConfigFile.Save() - no path given");
      return false;
    }
    string contents = this.GenerateText();
    try
    {
      FileUtils.SetFileWritableFlag(path, true);
      System.IO.File.WriteAllText(path, contents);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("ConfigFile.Save() - Failed to write file at {0}. Exception={1}", (object) path, (object) ex.Message));
      return false;
    }
    this.m_path = path;
    return true;
  }

  public bool Has(string key)
  {
    return this.FindEntry(key) != null;
  }

  public bool Delete(string key, bool removeEmptySections = true)
  {
    int entryIndex = this.FindEntryIndex(key);
    if (entryIndex < 0)
      return false;
    this.m_lines.RemoveAt(entryIndex);
    if (removeEmptySections)
    {
      int index1;
      for (index1 = entryIndex - 1; index1 >= 0; --index1)
      {
        ConfigFile.Line line = this.m_lines[index1];
        if (line.m_type != ConfigFile.LineType.SECTION)
        {
          if (!string.IsNullOrEmpty(line.m_raw.Trim()))
            return true;
        }
        else
          break;
      }
      int index2;
      for (index2 = entryIndex; index2 < this.m_lines.Count; ++index2)
      {
        ConfigFile.Line line = this.m_lines[index2];
        if (line.m_type != ConfigFile.LineType.SECTION)
        {
          if (!string.IsNullOrEmpty(line.m_raw.Trim()))
            return true;
        }
        else
          break;
      }
      int count = index2 - index1;
      this.m_lines.RemoveRange(index1, count);
    }
    return true;
  }

  public void Clear()
  {
    this.m_lines.Clear();
  }

  public string Get(string key, string defaultVal = "")
  {
    ConfigFile.Line entry = this.FindEntry(key);
    if (entry == null)
      return defaultVal;
    return entry.m_value;
  }

  public bool Get(string key, bool defaultVal = false)
  {
    ConfigFile.Line entry = this.FindEntry(key);
    if (entry == null)
      return defaultVal;
    return GeneralUtils.ForceBool(entry.m_value);
  }

  public int Get(string key, int defaultVal = 0)
  {
    ConfigFile.Line entry = this.FindEntry(key);
    if (entry == null)
      return defaultVal;
    return GeneralUtils.ForceInt(entry.m_value);
  }

  public float Get(string key, float defaultVal = 0.0f)
  {
    ConfigFile.Line entry = this.FindEntry(key);
    if (entry == null)
      return defaultVal;
    return GeneralUtils.ForceFloat(entry.m_value);
  }

  public bool Set(string key, object val)
  {
    string val1 = val != null ? val.ToString() : string.Empty;
    return this.Set(key, val1);
  }

  public bool Set(string key, bool val)
  {
    string val1 = !val ? "false" : "true";
    return this.Set(key, val1);
  }

  public bool Set(string key, string val)
  {
    ConfigFile.Line line = this.RegisterEntry(key);
    if (line == null)
      return false;
    line.m_value = val;
    return true;
  }

  public List<ConfigFile.Line> GetLines()
  {
    return this.m_lines;
  }

  public string GenerateText()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.m_lines.Count; ++index)
    {
      ConfigFile.Line line = this.m_lines[index];
      switch (line.m_type)
      {
        case ConfigFile.LineType.SECTION:
          stringBuilder.AppendFormat("[{0}]", (object) line.m_sectionName);
          break;
        case ConfigFile.LineType.ENTRY:
          if (line.m_quoteValue)
          {
            stringBuilder.AppendFormat("{0} = \"{1}\"", (object) line.m_lineKey, (object) line.m_value);
            break;
          }
          stringBuilder.AppendFormat("{0} = {1}", (object) line.m_lineKey, (object) line.m_value);
          break;
        default:
          stringBuilder.Append(line.m_raw);
          break;
      }
      stringBuilder.AppendLine();
    }
    return stringBuilder.ToString();
  }

  private bool Load(string path, bool ignoreUselessLines)
  {
      this.m_path = null;
      this.m_lines.Clear();
      if (!File.Exists(path))
      {
          Debug.LogError("Error loading config file " + path);
          return false;
      }
      int num = 1;
      using (StreamReader streamReader = File.OpenText(path))
      {
          string text = string.Empty;
          while (streamReader.Peek() != -1)
          {
              string text2 = streamReader.ReadLine();
              string text3 = text2.Trim();
              if (!ignoreUselessLines || text3.Length > 0)
              {
                  bool flag = text3.Length > 0 && text3[0] == ';';
                  if (!ignoreUselessLines || !flag)
                  {
                      ConfigFile.Line line = new ConfigFile.Line();
                      line.m_raw = text2;
                      line.m_sectionName = text;
                      if (flag)
                      {
                          line.m_type = ConfigFile.LineType.COMMENT;
                      }
                      else if (text3.Length > 0)
                      {
                          if (text3[0] == '[')
                          {
                              if (text3.Length < 2 || text3[text3.Length - 1] != ']')
                              {
                                  Debug.LogWarning(string.Format("ConfigFile.Load() - invalid section \"{0}\" on line {1} in file {2}", text2, num, path));
                                  if (!ignoreUselessLines)
                                  {
                                      this.m_lines.Add(line);
                                  }
                                  continue;
                              }
                              line.m_type = ConfigFile.LineType.SECTION;
                              text = (line.m_sectionName = text3.Substring(1, text3.Length - 2));
                              this.m_lines.Add(line);
                              continue;
                          }
                          else
                          {
                              int num2 = text3.IndexOf('=');
                              if (num2 < 0)
                              {
                                  Debug.LogWarning(string.Format("ConfigFile.Load() - invalid entry \"{0}\" on line {1} in file {2}", text2, num, path));
                                  if (!ignoreUselessLines)
                                  {
                                      this.m_lines.Add(line);
                                  }
                                  continue;
                              }
                              string text4 = text3.Substring(0, num2).Trim();
                              string text5 = text3.Substring(num2 + 1, text3.Length - num2 - 1).Trim();
                              if (text5.Length > 2)
                              {
                                  int index = text5.Length - 1;
                                  if ((text5[0] == '"' || text5[0] == '“' || text5[0] == '”') && (text5[index] == '"' || text5[index] == '“' || text5[index] == '”'))
                                  {
                                      text5 = text5.Substring(1, text5.Length - 2);
                                      line.m_quoteValue = true;
                                  }
                              }
                              line.m_type = ConfigFile.LineType.ENTRY;
                              line.m_fullKey = string.Format("{0}.{1}", text, text4);
                              line.m_lineKey = text4;
                              line.m_value = text5;
                          }
                      }
                      this.m_lines.Add(line);
                  }
              }
          }
      }
      this.m_path = path;
      return true;
  }

  private int FindSectionIndex(string sectionName)
  {
    for (int index = 0; index < this.m_lines.Count; ++index)
    {
      ConfigFile.Line line = this.m_lines[index];
      if (line.m_type == ConfigFile.LineType.SECTION && line.m_sectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase))
        return index;
    }
    return -1;
  }

  private ConfigFile.Line FindEntry(string fullKey)
  {
    int entryIndex = this.FindEntryIndex(fullKey);
    if (entryIndex < 0)
      return (ConfigFile.Line) null;
    return this.m_lines[entryIndex];
  }

  private int FindEntryIndex(string fullKey)
  {
    for (int index = 0; index < this.m_lines.Count; ++index)
    {
      ConfigFile.Line line = this.m_lines[index];
      if (line.m_type == ConfigFile.LineType.ENTRY && line.m_fullKey.Equals(fullKey, StringComparison.OrdinalIgnoreCase))
        return index;
    }
    return -1;
  }

  private ConfigFile.Line RegisterEntry(string fullKey)
  {
    if (string.IsNullOrEmpty(fullKey))
      return (ConfigFile.Line) null;
    int length = fullKey.IndexOf('.');
    if (length < 0)
      return (ConfigFile.Line) null;
    string sectionName = fullKey.Substring(0, length);
    string str = string.Empty;
    if (fullKey.Length > length + 1)
      str = fullKey.Substring(length + 1, fullKey.Length - length - 1);
    ConfigFile.Line line1 = (ConfigFile.Line) null;
    int sectionIndex = this.FindSectionIndex(sectionName);
    if (sectionIndex < 0)
    {
      ConfigFile.Line line2 = new ConfigFile.Line();
      if (this.m_lines.Count > 0)
        line2.m_sectionName = this.m_lines[this.m_lines.Count - 1].m_sectionName;
      this.m_lines.Add(line2);
      this.m_lines.Add(new ConfigFile.Line()
      {
        m_type = ConfigFile.LineType.SECTION,
        m_sectionName = sectionName
      });
      line1 = new ConfigFile.Line();
      line1.m_type = ConfigFile.LineType.ENTRY;
      line1.m_sectionName = sectionName;
      line1.m_lineKey = str;
      line1.m_fullKey = fullKey;
      this.m_lines.Add(line1);
    }
    else
    {
      int index;
      for (index = sectionIndex + 1; index < this.m_lines.Count; ++index)
      {
        ConfigFile.Line line2 = this.m_lines[index];
        if (line2.m_type != ConfigFile.LineType.SECTION)
        {
          if (line2.m_type == ConfigFile.LineType.ENTRY && line2.m_lineKey.Equals(str, StringComparison.OrdinalIgnoreCase))
          {
            line1 = line2;
            break;
          }
        }
        else
          break;
      }
      if (line1 == null)
      {
        line1 = new ConfigFile.Line();
        line1.m_type = ConfigFile.LineType.ENTRY;
        line1.m_sectionName = sectionName;
        line1.m_lineKey = str;
        line1.m_fullKey = fullKey;
        this.m_lines.Insert(index, line1);
      }
    }
    return line1;
  }

  public enum LineType
  {
    UNKNOWN,
    COMMENT,
    SECTION,
    ENTRY,
  }

  public class Line
  {
    public string m_raw = string.Empty;
    public string m_sectionName = string.Empty;
    public string m_lineKey = string.Empty;
    public string m_fullKey = string.Empty;
    public string m_value = string.Empty;
    public ConfigFile.LineType m_type;
    public bool m_quoteValue;
  }
}
