// Decompiled with JetBrains decompiler
// Type: VarKey
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

public class VarKey
{
  private string m_key;

  public VarKey(string key)
  {
    this.m_key = key;
  }

  public VarKey(string key, string subKey)
  {
    this.m_key = key + "." + subKey;
  }

  public VarKey Key(string subKey)
  {
    return new VarKey(this.m_key, subKey);
  }

  public string GetStr(string def)
  {
    if (VarsInternal.Get().Contains(this.m_key))
      return VarsInternal.Get().Value(this.m_key);
    return def;
  }

  public int GetInt(int def)
  {
    if (VarsInternal.Get().Contains(this.m_key))
      return GeneralUtils.ForceInt(VarsInternal.Get().Value(this.m_key));
    return def;
  }

  public float GetFloat(float def)
  {
    if (VarsInternal.Get().Contains(this.m_key))
      return GeneralUtils.ForceFloat(VarsInternal.Get().Value(this.m_key));
    return def;
  }

  public bool GetBool(bool def)
  {
    if (VarsInternal.Get().Contains(this.m_key))
      return GeneralUtils.ForceBool(VarsInternal.Get().Value(this.m_key));
    return def;
  }
}
