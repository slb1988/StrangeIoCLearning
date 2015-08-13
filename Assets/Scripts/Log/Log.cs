// Decompiled with JetBrains decompiler
// Type: Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

public class Log
{
    private const string CONFIG_FILE_NAME = "log.config";

  public static Logger Bob = new Logger("Bob");
  public static Logger Mike = new Logger("Mike");
  public static Logger Brian = new Logger("Brian");
  public static Logger Jay = new Logger("Jay");
  public static Logger Rachelle = new Logger("Rachelle");
  public static Logger Ben = new Logger("Ben");
  public static Logger Derek = new Logger("Derek");
  public static Logger Kyle = new Logger("Kyle");
  public static Logger Cameron = new Logger("Cameron");
  public static Logger Ryan = new Logger("Ryan");
  public static Logger JMac = new Logger("JMac");
  public static Logger Yim = new Logger("Yim");
  public static Logger Becca = new Logger("Becca");
  public static Logger Henry = new Logger("Henry");
  public static Logger MikeH = new Logger("MikeH");
  public static Logger Robin = new Logger("Robin");
  public static Logger BattleNet = new Logger("BattleNet");
  public static Logger Net = new Logger("Net");
  public static Logger Packets = new Logger("Packet");
  public static Logger Power = new Logger("Power");
  public static Logger Zone = new Logger("Zone");
  public static Logger Asset = new Logger("Asset");
  public static Logger Sound = new Logger("Sound");
  public static Logger HealthyGaming = new Logger("HealthyGaming");
  public static Logger FaceDownCard = new Logger("FaceDownCard");
  public static Logger LoadingScreen = new Logger("LoadingScreen");
  public static Logger MissingAssets = new Logger("MissingAssets");
  public static Logger UpdateManager = new Logger("UpdateManager");
  public static Logger GameMgr = new Logger("GameMgr");
  public static Logger CardbackMgr = new Logger("CardbackMgr");
  public static Logger Reset = new Logger("Reset");
  public static Logger DbfXml = new Logger("DbfXml");
  public static Logger BIReport = new Logger("BIReport");
  public static Logger Downloader = new Logger("Downloader");
  public static Logger PlayErrors = new Logger("PlayErrors");
  public static Logger Hand = new Logger("Hand");
  public static Logger ConfigFile = new Logger("ConfigFile");
  public static Logger DeviceEmulation = new Logger("DeviceEmulation");
  public static Logger Spectator = new Logger("Spectator");
  public static Logger Party = new Logger("Party");
  public static Logger FullScreenFX = new Logger("FullScreenFX");
  public static Logger InnKeepersSpecial = new Logger("InnKeepersSpecial");
  public static Logger EventTiming = new Logger("EventTiming");
  public static Logger Arena = new Logger("Arena");
  private readonly LogInfo[] DEFAULT_LOG_INFOS = new LogInfo[]
	{
		new LogInfo
		{
			m_name = "Party",
			m_filePrinting = true
		}
	};
  private static Log s_instance;
  private Map<string, LogInfo> m_logInfos;

  public Log()
  {
      string text = string.Format("{0}/{1}", FileUtils.PersistentDataPath, "log.config");
      if (File.Exists(text))
      {
          this.m_logInfos.Clear();
          this.LoadConfig(text);
      }
      LogInfo[] dEFAULT_LOG_INFOS = this.DEFAULT_LOG_INFOS;
      for (int i = 0; i < dEFAULT_LOG_INFOS.Length; i++)
      {
          LogInfo logInfo = dEFAULT_LOG_INFOS[i];
          if (!this.m_logInfos.ContainsKey(logInfo.m_name))
          {
              this.m_logInfos.Add(logInfo.m_name, logInfo);
          }
      }
      Log.ConfigFile.Print("log.config location: " + text);
  }

  public static Log Get()
  {
    if (Log.s_instance == null)
    {
      Log.s_instance = new Log();
      Log.s_instance.Initialize();
    }
    return Log.s_instance;
  }

  public void Load()
  {
    string path = string.Format("{0}/{1}", (object) FileUtils.PersistentDataPath, (object) "log.config");
    if (System.IO.File.Exists(path))
    {
      this.m_logInfos.Clear();
      this.LoadConfig(path);
    }
    foreach (LogInfo logInfo in this.DEFAULT_LOG_INFOS)
    {
      if (!this.m_logInfos.ContainsKey(logInfo.m_name))
        this.m_logInfos.Add(logInfo.m_name, logInfo);
    }
    Log.ConfigFile.Print("log.config location: " + path);
  }

  public LogInfo GetLogInfo(string name)
  {
    LogInfo logInfo = (LogInfo) null;
    this.m_logInfos.TryGetValue(name, out logInfo);
    return logInfo;
  }

  private void Initialize()
  {
    this.Load();
  }

  private void LoadConfig(string path)
  {
      ConfigFile configFile = new ConfigFile();
      if (!configFile.LightLoad(path))
      {
          return;
      }
      foreach (ConfigFile.Line current in configFile.GetLines())
      {
          string sectionName = current.m_sectionName;
          string lineKey = current.m_lineKey;
          string value = current.m_value;
          LogInfo logInfo;
          if (!this.m_logInfos.TryGetValue(sectionName, out logInfo))
          {
              logInfo = new LogInfo
              {
                  m_name = sectionName
              };
              this.m_logInfos.Add(logInfo.m_name, logInfo);
          }
          if (lineKey.Equals("ConsolePrinting", StringComparison.OrdinalIgnoreCase))
          {
              logInfo.m_consolePrinting = GeneralUtils.ForceBool(value);
          }
          else if (lineKey.Equals("ScreenPrinting", StringComparison.OrdinalIgnoreCase))
          {
              logInfo.m_screenPrinting = GeneralUtils.ForceBool(value);
          }
          else if (lineKey.Equals("FilePrinting", StringComparison.OrdinalIgnoreCase))
          {
              logInfo.m_filePrinting = GeneralUtils.ForceBool(value);
          }
          else if (lineKey.Equals("MinLevel", StringComparison.OrdinalIgnoreCase))
          {
              try
              {
                  LogLevel @enum = EnumUtils.GetEnum<LogLevel>(value, StringComparison.OrdinalIgnoreCase);
                  logInfo.m_minLevel = @enum;
              }
              catch (ArgumentException)
              {
              }
          }
          else if (lineKey.Equals("DefaultLevel", StringComparison.OrdinalIgnoreCase))
          {
              try
              {
                  LogLevel enum2 = EnumUtils.GetEnum<LogLevel>(value, StringComparison.OrdinalIgnoreCase);
                  logInfo.m_defaultLevel = enum2;
              }
              catch (ArgumentException)
              {
              }
          }
          else if (lineKey.Equals("Verbose", StringComparison.OrdinalIgnoreCase))
          {
              logInfo.m_verbose = GeneralUtils.ForceBool(value);
          }
      }
  }
}
