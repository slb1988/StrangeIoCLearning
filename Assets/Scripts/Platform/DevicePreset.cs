// Decompiled with JetBrains decompiler
// Type: DevicePreset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class DevicePreset : ICloneable
{
  public string name = "No Emulation";
  public OSCategory os = OSCategory.PC;
  public ScreenCategory screen = ScreenCategory.PC;
  public ScreenDensityCategory screenDensity = ScreenDensityCategory.High;
  public InputCategory input;

  public object Clone()
  {
    return this.MemberwiseClone();
  }

  public void ReadFromConfig(ConfigFile config)
  {
    this.name = config.Get("Emulation.DeviceName", this.name.ToString());
    string str1 = config.Get("Emulation.OSCategory", this.os.ToString());
    string str2 = config.Get("Emulation.InputCategory", this.input.ToString());
    string str3 = config.Get("Emulation.ScreenCategory", this.screen.ToString());
    string str4 = config.Get("Emulation.ScreenDensityCategory", this.screenDensity.ToString());
    Log.ConfigFile.Print("Reading Emulated Device: " + this.name + " from " + config.GetPath());
    try
    {
      this.os = (OSCategory) Enum.Parse(typeof (OSCategory), str1);
      this.input = (InputCategory) Enum.Parse(typeof (InputCategory), str2);
      this.screen = (ScreenCategory) Enum.Parse(typeof (ScreenCategory), str3);
      this.screenDensity = (ScreenDensityCategory) Enum.Parse(typeof (ScreenDensityCategory), str4);
    }
    catch (ArgumentException ex)
    {
      string format = "Could not parse {0} in {1} as a valid device!";
      object[] objArray = new object[2];
      int index1 = 0;
      string str5 = this.name;
      objArray[index1] = (object) str5;
      int index2 = 1;
      string path = config.GetPath();
      objArray[index2] = (object) path;
//      Blizzard.Log.Warning(format, objArray);
    }
  }

  public void WriteToConfig(ConfigFile config)
  {
    Log.ConfigFile.Print("Writing Emulated Device: " + this.name + " to " + config.GetPath());
    config.Set("Emulation.DeviceName", this.name.ToString());
    config.Set("Emulation.OSCategory", this.os.ToString());
    config.Set("Emulation.InputCategory", this.input.ToString());
    config.Set("Emulation.ScreenCategory", this.screen.ToString());
    config.Set("Emulation.ScreenDensityCategory", this.screenDensity.ToString());
    config.Save((string) null);
  }
}
