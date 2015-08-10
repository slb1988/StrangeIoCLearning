// Decompiled with JetBrains decompiler
// Type: Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Text;
using UnityEngine;

public class Logger
{
  private const string OUTPUT_DIRECTORY_NAME = "Logs";
  private const string OUTPUT_FILE_EXTENSION = "log";
  private string m_name;
  private StreamWriter m_fileWriter;
  private bool m_fileWriterInitialized;

  public Logger(string name)
  {
    this.m_name = name;
  }

  public bool CanPrint(LogTarget target, LogLevel level, bool verbose)
  {
    LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
    if (logInfo == null || level < logInfo.m_minLevel || verbose && !logInfo.m_verbose)
      return false;
    switch (target)
    {
      case LogTarget.CONSOLE:
        return logInfo.m_consolePrinting;
      case LogTarget.SCREEN:
        return logInfo.m_screenPrinting;
      case LogTarget.FILE:
        return logInfo.m_filePrinting;
      default:
        return false;
    }
  }

  public bool CanPrint()
  {
    LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
    return logInfo != null && (logInfo.m_consolePrinting || logInfo.m_screenPrinting || logInfo.m_filePrinting);
  }

  public LogLevel GetDefaultLevel()
  {
    LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
    if (logInfo == null)
      return LogLevel.Debug;
    return logInfo.m_defaultLevel;
  }

  public bool IsVerbose()
  {
    LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
    if (logInfo == null)
      return false;
    return logInfo.m_verbose;
  }

  public void Print(string format, params object[] args)
  {
    this.Print(this.GetDefaultLevel(), false, format, args);
  }

  public void Print(LogLevel level, string format, params object[] args)
  {
    this.Print(level, false, format, args);
  }

  public void Print(bool verbose, string format, params object[] args)
  {
    this.Print(this.GetDefaultLevel(), verbose, format, args);
  }

  public void Print(LogLevel level, bool verbose, string format, params object[] args)
  {
    string message = GeneralUtils.SafeFormat(format, args);
    this.Print(level, verbose, message);
  }

  public void Print(LogLevel level, bool verbose, string message)
  {
    this.FilePrint(level, verbose, message);
    this.ConsolePrint(level, verbose, message);
    this.ScreenPrint(level, verbose, message);
  }

  public void PrintDebug(string format, params object[] args)
  {
    this.PrintDebug(false, format, args);
  }

  public void PrintDebug(bool verbose, string format, params object[] args)
  {
    this.Print(LogLevel.Debug, verbose, format, args);
  }

  public void PrintInfo(string format, params object[] args)
  {
    this.PrintInfo(false, format, args);
  }

  public void PrintInfo(bool verbose, string format, params object[] args)
  {
    this.Print(LogLevel.Info, verbose, format, args);
  }

  public void PrintWarning(string format, params object[] args)
  {
    this.PrintWarning(false, format, args);
  }

  public void PrintWarning(bool verbose, string format, params object[] args)
  {
    this.Print(LogLevel.Warning, verbose, format, args);
  }

  public void PrintError(string format, params object[] args)
  {
    this.PrintError(false, format, args);
  }

  public void PrintError(bool verbose, string format, params object[] args)
  {
    this.Print(LogLevel.Error, verbose, format, args);
  }

  public void FilePrint(string format, params object[] args)
  {
    this.FilePrint(this.GetDefaultLevel(), false, format, args);
  }

  public void FilePrint(LogLevel level, string format, params object[] args)
  {
    this.FilePrint(level, false, format, args);
  }

  public void FilePrint(bool verbose, string format, params object[] args)
  {
    this.FilePrint(this.GetDefaultLevel(), verbose, format, args);
  }

  public void FilePrint(LogLevel level, bool verbose, string format, params object[] args)
  {
    string message = GeneralUtils.SafeFormat(format, args);
    this.FilePrint(level, verbose, message);
  }

  public void FilePrint(LogLevel level, bool verbose, string message)
  {
    if (!this.CanPrint(LogTarget.FILE, level, verbose))
      return;
    this.InitFileWriter();
    if (this.m_fileWriter == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    switch (level)
    {
      case LogLevel.Debug:
        stringBuilder.Append("D ");
        break;
      case LogLevel.Info:
        stringBuilder.Append("I ");
        break;
      case LogLevel.Warning:
        stringBuilder.Append("W ");
        break;
      case LogLevel.Error:
        stringBuilder.Append("E ");
        break;
    }
    stringBuilder.Append(DateTime.Now.TimeOfDay.ToString());
    stringBuilder.Append(" ");
    stringBuilder.Append(message);
    this.m_fileWriter.WriteLine(stringBuilder.ToString());
    this.m_fileWriter.Flush();
  }

  public void ConsolePrint(string format, params object[] args)
  {
    this.ConsolePrint(this.GetDefaultLevel(), false, format, args);
  }

  public void ConsolePrint(LogLevel level, string format, params object[] args)
  {
    this.ConsolePrint(level, false, format, args);
  }

  public void ConsolePrint(bool verbose, string format, params object[] args)
  {
    this.ConsolePrint(this.GetDefaultLevel(), verbose, format, args);
  }

  public void ConsolePrint(LogLevel level, bool verbose, string format, params object[] args)
  {
    string message = GeneralUtils.SafeFormat(format, args);
    this.ConsolePrint(level, verbose, message);
  }

  public void ConsolePrint(LogLevel level, bool verbose, string message)
  {
    if (!this.CanPrint(LogTarget.CONSOLE, level, verbose))
      return;
    string str = string.Format("[{0}] {1}", (object) this.m_name, (object) message);
    switch (level)
    {
      case LogLevel.Debug:
      case LogLevel.Info:
        Debug.Log((object) str);
        break;
      case LogLevel.Warning:
        Debug.LogWarning((object) str);
        break;
      case LogLevel.Error:
        Debug.LogError((object) str);
        break;
    }
  }

  public void ScreenPrint(string format, params object[] args)
  {
    this.ScreenPrint(this.GetDefaultLevel(), false, format, args);
  }

  public void ScreenPrint(LogLevel level, string format, params object[] args)
  {
    this.ScreenPrint(level, false, format, args);
  }

  public void ScreenPrint(bool verbose, string format, params object[] args)
  {
    this.ScreenPrint(this.GetDefaultLevel(), verbose, format, args);
  }

  public void ScreenPrint(LogLevel level, bool verbose, string format, params object[] args)
  {
    string message = GeneralUtils.SafeFormat(format, args);
    this.ScreenPrint(level, verbose, message);
  }

  public void ScreenPrint(LogLevel level, bool verbose, string message)
  {
    //if (!this.CanPrint(LogTarget.SCREEN, level, verbose) || (UnityEngine.Object) SceneDebugger.Get() == (UnityEngine.Object) null)
    //  return;
    Debug.Log("SceneDebugger " + string.Format("[{0}] {1}", (object) this.m_name, (object) message));
    //SceneDebugger.Get().AddMessage(string.Format("[{0}] {1}", (object) this.m_name, (object) message));
  }

  private void InitFileWriter()
  {
    if (this.m_fileWriterInitialized)
      return;
    this.m_fileWriter = (StreamWriter) null;
    string path1 = "Logs";
    if (!Directory.Exists(path1))
    {
      try
      {
        Directory.CreateDirectory(path1);
      }
      catch (Exception ex)
      {
      }
    }
    string path2 = string.Format("{0}/{1}.{2}", (object) path1, (object) this.m_name, (object) "log");
    try
    {
      this.m_fileWriter = new StreamWriter((Stream) new FileStream(path2, FileMode.Create, FileAccess.ReadWrite));
    }
    catch (Exception ex)
    {
    }
    this.m_fileWriterInitialized = true;
  }
}
