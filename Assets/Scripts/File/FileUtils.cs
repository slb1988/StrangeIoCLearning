// Decompiled with JetBrains decompiler
// Type: FileUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

public class FileUtils
{
  public static readonly char[] FOLDER_SEPARATOR_CHARS;

  private static string BasePersistentDataPath
  {
    get
    {
      return string.Format("{0}/Blizzard/Hearthstone", (object) Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace('\\', '/'));
    }
  }

  private static string PublicPersistentDataPath
  {
    get
    {
      return FileUtils.BasePersistentDataPath;
    }
  }

  private static string InternalPersistentDataPath
  {
    get
    {
      return string.Format("{0}/Dev", (object) FileUtils.BasePersistentDataPath);
    }
  }

  public static string PersistentDataPath
  {
    get
    {
      string path = //!ApplicationMgr.IsInternal() ? FileUtils.PublicPersistentDataPath : 
          FileUtils.InternalPersistentDataPath;
      if (!Directory.Exists(path))
      {
        try
        {
          Directory.CreateDirectory(path);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) string.Format("FileUtils.PersistentDataPath - Error creating {0}. Exception={1}", (object) path, (object) ex.Message));
//          Error.AddFatalLoc("GLOBAL_ERROR_ASSET_CREATE_PERSISTENT_DATA_PATH");
        }
      }
      return path;
    }
  }

  public static string CachePath
  {
    get
    {
      string path = string.Format("{0}/Cache", (object) FileUtils.PersistentDataPath);
      if (!Directory.Exists(path))
      {
        try
        {
          Directory.CreateDirectory(path);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) string.Format("FileUtils.CachePath - Error creating {0}. Exception={1}", (object) path, (object) ex.Message));
        }
      }
      return path;
    }
  }

  static FileUtils()
  {
    char[] chArray = new char[2];
    int index1 = 0;
    int num1 = 47;
    chArray[index1] = (char) num1;
    int index2 = 1;
    int num2 = 92;
    chArray[index2] = (char) num2;
    FileUtils.FOLDER_SEPARATOR_CHARS = chArray;
  }

  public static string MakeSourceAssetPath(DirectoryInfo folder)
  {
    return FileUtils.MakeSourceAssetPath(folder.FullName);
  }

  public static string MakeSourceAssetPath(FileInfo fileInfo)
  {
    return FileUtils.MakeSourceAssetPath(fileInfo.FullName);
  }

  public static string MakeSourceAssetPath(string path)
  {
    string str = path.Replace("\\", "/");
    int num = str.IndexOf("/Assets", StringComparison.OrdinalIgnoreCase);
    return str.Remove(0, num + 1);
  }

  public static string MakeMetaPathFromSourcePath(string path)
  {
    return string.Format("{0}.meta", (object) path);
  }

  public static string MakeSourceAssetMetaPath(string path)
  {
    return FileUtils.MakeMetaPathFromSourcePath(FileUtils.MakeSourceAssetPath(path));
  }

  //public static string MakeLocalizedPathFromSourcePath(Locale locale, string path)
  //{
  //  string str = System.IO.Path.GetDirectoryName(path);
  //  string fileName = System.IO.Path.GetFileName(path);
  //  int startIndex = str.LastIndexOf("/");
  //  if (startIndex >= 0 && str.Substring(startIndex + 1).Equals(Localization.DEFAULT_LOCALE_NAME))
  //    str = str.Remove(startIndex);
  //  return string.Format("{0}/{1}/{2}", (object) str, (object) locale, (object) fileName);
  //}

  //public static Locale? GetLocaleFromSourcePath(string path)
  //{
  //  string directoryName = System.IO.Path.GetDirectoryName(path);
  //  int num = directoryName.LastIndexOf("/");
  //  if (num < 0)
  //    return new Locale?();
  //  string str = directoryName.Substring(num + 1);
  //  Locale locale;
  //  try
  //  {
  //    locale = EnumUtils.Parse<Locale>(str);
  //  }
  //  catch (Exception ex)
  //  {
  //    return new Locale?();
  //  }
  //  return new Locale?(locale);
  //}

  //public static Locale? GetForeignLocaleFromSourcePath(string path)
  //{
  //  Locale? localeFromSourcePath = FileUtils.GetLocaleFromSourcePath(path);
  //  if (!localeFromSourcePath.HasValue)
  //    return new Locale?();
  //  if (localeFromSourcePath.Value == Locale.enUS)
  //    return new Locale?();
  //  return localeFromSourcePath;
  //}

  //public static bool IsForeignLocaleSourcePath(string path)
  //{
  //  return FileUtils.GetForeignLocaleFromSourcePath(path).HasValue;
  //}

  //public static string StripLocaleFromPath(string path)
  //{
  //  string directoryName = System.IO.Path.GetDirectoryName(path);
  //  string fileName = System.IO.Path.GetFileName(path);
  //  if (Localization.IsValidLocaleName(System.IO.Path.GetFileName(directoryName)))
  //    return string.Format("{0}/{1}", (object) System.IO.Path.GetDirectoryName(directoryName), (object) fileName);
  //  return path;
  //}

  public static string GameToSourceAssetPath(string path, string dotExtension = ".prefab")
  {
    return string.Format("{0}{1}", (object) path, (object) dotExtension);
  }

  public static string GameToSourceAssetName(string folder, string name, string dotExtension = ".prefab")
  {
    return string.Format("{0}/{1}{2}", (object) folder, (object) name, (object) dotExtension);
  }

  public static string SourceToGameAssetPath(string path)
  {
    int length = path.LastIndexOf('.');
    if (length < 0)
      return path;
    return path.Substring(0, length);
  }

  public static string SourceToGameAssetName(string path)
  {
    int num = path.LastIndexOf('/');
    if (num < 0)
      return path;
    int length = path.LastIndexOf('.');
    if (length < 0)
      return path;
    return path.Substring(num + 1, length);
  }

  public static string GameAssetPathToName(string path)
  {
    int num = path.LastIndexOf('/');
    if (num < 0)
      return path;
    return path.Substring(num + 1);
  }

  public static string GetAssetPath(string fileName)
  {
    return fileName;
  }
  public static string GetOnDiskCapitalizationForFile(string filePath)
  {
    return FileUtils.GetOnDiskCapitalizationForFile(new FileInfo(filePath));
  }

  public static string GetOnDiskCapitalizationForDir(string dirPath)
  {
    return FileUtils.GetOnDiskCapitalizationForDir(new DirectoryInfo(dirPath));
  }

  public static string GetOnDiskCapitalizationForFile(FileInfo fileInfo)
  {
    DirectoryInfo directory = fileInfo.Directory;
    string name = directory.GetFiles(fileInfo.Name)[0].Name;
    return System.IO.Path.Combine(FileUtils.GetOnDiskCapitalizationForDir(directory), name);
  }

  public static string GetOnDiskCapitalizationForDir(DirectoryInfo dirInfo)
  {
    DirectoryInfo parent = dirInfo.Parent;
    if (parent == null)
      return dirInfo.Name;
    string name = parent.GetDirectories(dirInfo.Name)[0].Name;
    return System.IO.Path.Combine(FileUtils.GetOnDiskCapitalizationForDir(parent), name);
  }

  public static bool GetLastFolderAndFileFromPath(string path, out string folderName, out string fileName)
  {
    folderName = (string) null;
    fileName = (string) null;
    if (string.IsNullOrEmpty(path))
      return false;
    int num1 = path.LastIndexOfAny(FileUtils.FOLDER_SEPARATOR_CHARS);
    if (num1 > 0)
    {
      int num2 = path.LastIndexOfAny(FileUtils.FOLDER_SEPARATOR_CHARS, num1 - 1);
      int startIndex = num2 >= 0 ? num2 + 1 : 0;
      int length = num1 - startIndex;
      folderName = path.Substring(startIndex, length);
    }
    if (num1 < 0)
      fileName = path;
    else if (num1 < path.Length - 1)
      fileName = path.Substring(num1 + 1);
    if (folderName == null)
      return fileName != null;
    return true;
  }

  public static bool SetFolderWritableFlag(string dirPath, bool writable)
  {
    foreach (string path in Directory.GetFiles(dirPath))
      FileUtils.SetFileWritableFlag(path, writable);
    foreach (string dirPath1 in Directory.GetDirectories(dirPath))
      FileUtils.SetFolderWritableFlag(dirPath1, writable);
    return true;
  }

  public static bool SetFileWritableFlag(string path, bool setWritable)
  {
    if (!System.IO.File.Exists(path))
      return false;
    try
    {
      FileAttributes attributes = System.IO.File.GetAttributes(path);
      FileAttributes fileAttributes = !setWritable ? attributes | FileAttributes.ReadOnly : attributes & ~FileAttributes.ReadOnly;
      if (setWritable && Environment.OSVersion.Platform == PlatformID.MacOSX)
        fileAttributes |= FileAttributes.Normal;
      if (fileAttributes == attributes)
        return true;
      System.IO.File.SetAttributes(path, fileAttributes);
      return System.IO.File.GetAttributes(path) == fileAttributes;
    }
    catch (DirectoryNotFoundException ex)
    {
    }
    catch (FileNotFoundException ex)
    {
    }
    catch (Exception ex)
    {
    }
    return false;
  }
}
