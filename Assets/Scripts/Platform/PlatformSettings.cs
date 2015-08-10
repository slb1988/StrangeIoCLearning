// Decompiled with JetBrains decompiler
// Type: PlatformSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PlatformSettings
{
  public static bool s_isDeviceSupported = true;
  public static OSCategory s_os = OSCategory.PC;
  public static MemoryCategory s_memory = MemoryCategory.High;
  public static ScreenCategory s_screen = ScreenCategory.PC;
  public static ScreenDensityCategory s_screenDensity = ScreenDensityCategory.High;
  public static InputCategory s_input;

  public static OSCategory OS
  {
    get
    {
      return PlatformSettings.s_os;
    }
  }

  public static MemoryCategory Memory
  {
    get
    {
      return PlatformSettings.s_memory;
    }
  }

  public static ScreenCategory Screen
  {
    get
    {
      return PlatformSettings.s_screen;
    }
  }

  public static InputCategory Input
  {
    get
    {
      return PlatformSettings.s_input;
    }
  }

  public static ScreenDensityCategory ScreenDensity
  {
    get
    {
      return PlatformSettings.s_screenDensity;
    }
  }

  public static string DeviceName
  {
    get
    {
      if (string.IsNullOrEmpty(SystemInfo.deviceModel))
        return "unknown";
      return SystemInfo.deviceModel;
    }
  }

  static PlatformSettings()
  {
    PlatformSettings.RecomputeDeviceSettings();
  }

  public static int GetBestScreenMatch(List<ScreenCategory> categories)
  {
    int num1 = 0;
    int num2 = (int) (4 - (int)PlatformSettings.Screen);
    for (int index = 1; index < categories.Count; ++index)
    {
      int num3 = categories[index] - PlatformSettings.Screen;
      if (num3 >= 0 && num3 < num2)
      {
        num1 = index;
        num2 = num3;
      }
    }
    return num1;
  }

  private static void RecomputeDeviceSettings()
  {
    if (PlatformSettings.EmulateMobileDevice())
      return;
    PlatformSettings.s_os = OSCategory.PC;
    PlatformSettings.s_input = InputCategory.Mouse;
    PlatformSettings.s_screen = ScreenCategory.PC;
    PlatformSettings.s_screenDensity = ScreenDensityCategory.High;
    PlatformSettings.s_os = OSCategory.PC;
    int systemMemorySize = SystemInfo.systemMemorySize;
    if (systemMemorySize < 500)
    {
      Debug.LogWarning((object) ("Low Memory Warning: Device has only " + (object) systemMemorySize + "MBs of system memory"));
      PlatformSettings.s_memory = MemoryCategory.Low;
    }
    else if (systemMemorySize < 1000)
      PlatformSettings.s_memory = MemoryCategory.Low;
    else if (systemMemorySize < 1500)
      PlatformSettings.s_memory = MemoryCategory.Medium;
    else
      PlatformSettings.s_memory = MemoryCategory.High;
  }

  private static bool EmulateMobileDevice()
  {
    ConfigFile config = new ConfigFile();
    if (!config.FullLoad(Vars.GetClientConfigPath()))
    {
        Debug.LogWarning("Failed to read DeviceEmulation from client.config");
        return false;
    }
    DevicePreset devicePreset = new DevicePreset();
    devicePreset.ReadFromConfig(config);
    if (devicePreset.name == "No Emulation" || !config.Get("Emulation.emulateOnDevice", false))
      return false;
    PlatformSettings.s_os = devicePreset.os;
    PlatformSettings.s_input = devicePreset.input;
    PlatformSettings.s_screen = devicePreset.screen;
    PlatformSettings.s_screenDensity = devicePreset.screenDensity;
    Log.DeviceEmulation.Print("Emulating an " + devicePreset.name);
    return true;
  }

  private static void SetIOSSettings()
  {
  }

  private static void SetAndroidSettings()
  {
    PlatformSettings.s_os = OSCategory.Android;
    PlatformSettings.s_input = InputCategory.Touch;
  }

  public static void Refresh()
  {
    PlatformSettings.RecomputeDeviceSettings();
  }
}
