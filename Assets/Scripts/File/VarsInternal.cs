// Decompiled with JetBrains decompiler
// Type: VarsInternal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

internal class VarsInternal
{
  private static VarsInternal s_instance = new VarsInternal();
  private Map<string, string> m_vars = new Map<string, string>();

  private VarsInternal()
  {
    if (!this.LoadConfig(Vars.GetClientConfigPath()))
      ;
  }

  public static VarsInternal Get()
  {
    return VarsInternal.s_instance;
  }

  public static void RefreshVars()
  {
    VarsInternal.s_instance = new VarsInternal();
  }

  public bool Contains(string key)
  {
    return this.m_vars.ContainsKey(key);
  }

  public string Value(string key)
  {
    return this.m_vars[key];
  }

  private bool LoadConfig(string path)
  {
    ConfigFile configFile = new ConfigFile();
    if (!configFile.LightLoad(path))
      return false;
    using (List<ConfigFile.Line>.Enumerator enumerator = configFile.GetLines().GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        ConfigFile.Line current = enumerator.Current;
        this.m_vars[current.m_fullKey] = current.m_value;
      }
    }
    return true;
  }
}
