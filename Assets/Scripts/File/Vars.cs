// Decompiled with JetBrains decompiler
// Type: Vars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

public class Vars
{
  public const string CONFIG_FILE_NAME = "client.config";

  public static VarKey Key(string key)
  {
    return new VarKey(key);
  }

  public static void RefreshVars()
  {
    VarsInternal.RefreshVars();
  }

  public static string GetClientConfigPath()
  {
    return "client.config";
  }
}
