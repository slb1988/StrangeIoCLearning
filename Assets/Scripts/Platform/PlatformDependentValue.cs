// Decompiled with JetBrains decompiler
// Type: PlatformDependentValue`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlatformDependentValue<T>
{
  private PlatformSetting<T> PCSetting = new PlatformSetting<T>();
  private PlatformSetting<T> MacSetting = new PlatformSetting<T>();
  private PlatformSetting<T> iOSSetting = new PlatformSetting<T>();
  private PlatformSetting<T> AndroidSetting = new PlatformSetting<T>();
  private PlatformSetting<T> TabletSetting = new PlatformSetting<T>();
  private PlatformSetting<T> MiniTabletSetting = new PlatformSetting<T>();
  private PlatformSetting<T> PhoneSetting = new PlatformSetting<T>();
  private PlatformSetting<T> MouseSetting = new PlatformSetting<T>();
  private PlatformSetting<T> TouchSetting = new PlatformSetting<T>();
  private PlatformSetting<T> LowMemorySetting = new PlatformSetting<T>();
  private PlatformSetting<T> MediumMemorySetting = new PlatformSetting<T>();
  private PlatformSetting<T> HighMemorySetting = new PlatformSetting<T>();
  private PlatformSetting<T> NormalScreenDensitySetting = new PlatformSetting<T>();
  private PlatformSetting<T> HighScreenDensitySetting = new PlatformSetting<T>();
  private bool resolved;
  private T result;
  private PlatformCategory type;
  private T defaultValue;

  public T PC
  {
    get
    {
      return this.PCSetting.Get();
    }
    set
    {
      this.PCSetting.Set(value);
    }
  }

  public T Mac
  {
    get
    {
      return this.MacSetting.Get();
    }
    set
    {
      this.MacSetting.Set(value);
    }
  }

  public T iOS
  {
    get
    {
      return this.iOSSetting.Get();
    }
    set
    {
      this.iOSSetting.Set(value);
    }
  }

  public T Android
  {
    get
    {
      return this.AndroidSetting.Get();
    }
    set
    {
      this.AndroidSetting.Set(value);
    }
  }

  public T Tablet
  {
    get
    {
      return this.TabletSetting.Get();
    }
    set
    {
      this.TabletSetting.Set(value);
    }
  }

  public T MiniTablet
  {
    get
    {
      return this.MiniTabletSetting.Get();
    }
    set
    {
      this.MiniTabletSetting.Set(value);
    }
  }

  public T Phone
  {
    get
    {
      return this.PhoneSetting.Get();
    }
    set
    {
      this.PhoneSetting.Set(value);
    }
  }

  public T Mouse
  {
    get
    {
      return this.MouseSetting.Get();
    }
    set
    {
      this.MouseSetting.Set(value);
    }
  }

  public T Touch
  {
    get
    {
      return this.TouchSetting.Get();
    }
    set
    {
      this.TouchSetting.Set(value);
    }
  }

  public T LowMemory
  {
    get
    {
      return this.LowMemorySetting.Get();
    }
    set
    {
      this.LowMemorySetting.Set(value);
    }
  }

  public T MediumMemory
  {
    get
    {
      return this.MediumMemorySetting.Get();
    }
    set
    {
      this.MediumMemorySetting.Set(value);
    }
  }

  public T HighMemory
  {
    get
    {
      return this.HighMemorySetting.Get();
    }
    set
    {
      this.HighMemorySetting.Set(value);
    }
  }

  public T NormalScreenDensity
  {
    get
    {
      return this.NormalScreenDensitySetting.Get();
    }
    set
    {
      this.NormalScreenDensitySetting.Set(value);
    }
  }

  public T HighScreenDensity
  {
    get
    {
      return this.HighScreenDensitySetting.Get();
    }
    set
    {
      this.HighScreenDensitySetting.Set(value);
    }
  }

  private T Value
  {
    get
    {
      if (this.resolved)
        return this.result;
      switch (this.type)
      {
        case PlatformCategory.OS:
          this.result = this.GetOSSetting(PlatformSettings.OS);
          break;
        case PlatformCategory.Screen:
          this.result = this.GetScreenSetting(PlatformSettings.Screen);
          break;
        case PlatformCategory.Memory:
          this.result = this.GetMemorySetting(PlatformSettings.Memory);
          break;
        case PlatformCategory.Input:
          this.result = this.GetInputSetting(PlatformSettings.Input);
          break;
      }
      this.resolved = true;
      return this.result;
    }
  }

  public PlatformDependentValue(PlatformCategory t)
  {
    this.type = t;
  }

  public static implicit operator T(PlatformDependentValue<T> val)
  {
    return val.Value;
  }

  public void Reset()
  {
    this.resolved = false;
  }

  private T GetOSSetting(OSCategory os)
  {
    switch (os)
    {
      case OSCategory.PC:
        if (this.PCSetting.WasSet)
          return this.PC;
        break;
      case OSCategory.Mac:
        if (this.MacSetting.WasSet)
          return this.Mac;
        return this.GetOSSetting(OSCategory.PC);
      case OSCategory.iOS:
        if (this.iOSSetting.WasSet)
          return this.iOS;
        return this.GetOSSetting(OSCategory.PC);
      case OSCategory.Android:
        if (this.AndroidSetting.WasSet)
          return this.Android;
        return this.GetOSSetting(OSCategory.PC);
    }
    Debug.LogError((object) "Could not find OS dependent value");
    return default (T);
  }

  private T GetScreenSetting(ScreenCategory screen)
  {
    switch (screen)
    {
      case ScreenCategory.Phone:
        if (this.PhoneSetting.WasSet)
          return this.Phone;
        return this.GetScreenSetting(ScreenCategory.Tablet);
      case ScreenCategory.MiniTablet:
        if (this.MiniTabletSetting.WasSet)
          return this.MiniTablet;
        return this.GetScreenSetting(ScreenCategory.Tablet);
      case ScreenCategory.Tablet:
        if (this.TabletSetting.WasSet)
          return this.Tablet;
        return this.GetScreenSetting(ScreenCategory.PC);
      case ScreenCategory.PC:
        if (this.PCSetting.WasSet)
          return this.PC;
        break;
    }
    Debug.LogError((object) "Could not find screen dependent value");
    return default (T);
  }

  private T GetMemorySetting(MemoryCategory memory)
  {
    switch (memory)
    {
      case MemoryCategory.Low:
        if (this.LowMemorySetting.WasSet)
          return this.LowMemory;
        break;
      case MemoryCategory.Medium:
        if (this.MediumMemorySetting.WasSet)
          return this.MediumMemory;
        return this.GetMemorySetting(MemoryCategory.Low);
      case MemoryCategory.High:
        if (this.HighMemorySetting.WasSet)
          return this.HighMemory;
        return this.GetMemorySetting(MemoryCategory.Medium);
    }
    Debug.LogError((object) "Could not find memory dependent value");
    return default (T);
  }

  private T GetInputSetting(InputCategory input)
  {
    switch (input)
    {
      case InputCategory.Mouse:
        if (this.MouseSetting.WasSet)
          return this.Mouse;
        break;
      case InputCategory.Touch:
        if (this.TouchSetting.WasSet)
          return this.Touch;
        return this.GetInputSetting(InputCategory.Mouse);
    }
    Debug.LogError((object) "Could not find input dependent value");
    return default (T);
  }

  private T GetScreenDensitySetting(ScreenDensityCategory input)
  {
    switch (input)
    {
      case ScreenDensityCategory.Normal:
        if (this.NormalScreenDensitySetting.WasSet)
          return this.NormalScreenDensity;
        break;
      case ScreenDensityCategory.High:
        if (this.HighScreenDensitySetting.WasSet)
          return this.HighScreenDensity;
        return this.GetScreenDensitySetting(ScreenDensityCategory.Normal);
    }
    Debug.LogError((object) "Could not find screen density dependent value");
    return default (T);
  }
}
