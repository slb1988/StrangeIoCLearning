// Decompiled with JetBrains decompiler
// Type: PlatformSetting`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

public class PlatformSetting<T>
{
  public T Setting = default (T);
  public bool WasSet;

  public void Set(T val)
  {
    this.Setting = val;
    this.WasSet = true;
  }

  public T Get()
  {
    return this.Setting;
  }
}
