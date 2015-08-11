// Decompiled with JetBrains decompiler
// Type: CheatMgr
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheatMgr : MonoBehaviour
{
  private Map<string, List<CheatMgr.ProcessCheatCallback>> m_funcMap = new Map<string, List<CheatMgr.ProcessCheatCallback>>();
  private Map<string, string> m_cheatAlias = new Map<string, string>();
  private Map<string, string> m_cheatDesc = new Map<string, string>();
  private Map<string, string> m_cheatArgs = new Map<string, string>();
  private Map<string, string> m_cheatExamples = new Map<string, string>();
  private int m_cheatHistoryIndex = -1;
  private static CheatMgr s_instance;
  private Rect m_cheatInputBackground;
  private bool m_inputActive;
  private List<string> m_cheatHistory;
  private string m_cheatTextBeforeScrollingThruHistory;

  public Map<string, string> cheatDesc
  {
    get
    {
      return this.m_cheatDesc;
    }
  }

  public Map<string, string> cheatArgs
  {
    get
    {
      return this.m_cheatArgs;
    }
  }

  public Map<string, string> cheatExamples
  {
    get
    {
      return this.m_cheatExamples;
    }
  }

  public static CheatMgr Get()
  {
    return CheatMgr.s_instance;
  }

  public void Awake()
  {
    CheatMgr.s_instance = this;
    this.m_cheatHistory = new List<string>();
    Cheats.Initialize();
  }

  public Map<string, List<CheatMgr.ProcessCheatCallback>>.KeyCollection GetCheatCommands()
  {
    return this.m_funcMap.Keys;
  }

  public bool HandleKeyboardInput()
  {
    return false;
  }

  private bool OnInputPreprocess(Event e)
  {
    if (e.type != EventType.KeyDown)
      return false;
    KeyCode keyCode = e.keyCode;
    if (keyCode == KeyCode.BackQuote && string.IsNullOrEmpty(UniversalInputManager.Get().GetInputText()))
    {
      UniversalInputManager.Get().CancelTextInput(this.gameObject, false);
      return true;
    }
    if (this.m_cheatHistory.Count < 1)
      return false;
    if (keyCode == KeyCode.UpArrow)
    {
      if (this.m_cheatHistoryIndex >= this.m_cheatHistory.Count - 1)
        return true;
      string inputText = UniversalInputManager.Get().GetInputText();
      if (this.m_cheatTextBeforeScrollingThruHistory == null)
        this.m_cheatTextBeforeScrollingThruHistory = inputText;
      UniversalInputManager.Get().SetInputText(this.m_cheatHistory[++this.m_cheatHistoryIndex]);
      ApplicationMgr.Get().ScheduleCallback(0.0f, false, (ApplicationMgr.ScheduledCallback) (u =>
      {
        TextEditor textEditor = (TextEditor) GUIUtility.GetStateObject(typeof (TextEditor), GUIUtility.keyboardControl);
        if (textEditor == null)
          return;
        textEditor.MoveTextEnd();
      }), (object) null);
      return true;
    }
    if (keyCode != KeyCode.DownArrow)
      return false;
    string text;
    if (this.m_cheatHistoryIndex <= 0)
    {
      this.m_cheatHistoryIndex = -1;
      if (this.m_cheatTextBeforeScrollingThruHistory == null)
        return false;
      text = this.m_cheatTextBeforeScrollingThruHistory;
      this.m_cheatTextBeforeScrollingThruHistory = (string) null;
    }
    else
      text = this.m_cheatHistory[--this.m_cheatHistoryIndex];
    UniversalInputManager.Get().SetInputText(text);
    return true;
  }

  public void RegisterCheatHandler(string func, CheatMgr.ProcessCheatCallback callback, string desc = null, string argDesc = null, string exampleArgs = null)
  {
    this.RegisterCheatHandler_(func, callback);
    if (desc != null)
      this.m_cheatDesc[func] = desc;
    if (argDesc != null)
      this.m_cheatArgs[func] = argDesc;
    if (exampleArgs == null)
      return;
    this.m_cheatExamples[func] = exampleArgs;
  }

  public void RegisterCheatAlias(string func, params string[] aliases)
  {
    List<CheatMgr.ProcessCheatCallback> list;
    if (!this.m_funcMap.TryGetValue(func, out list))
    {
      Debug.LogError((object) string.Format("CheatMgr.RegisterCheatAlias() - cannot register aliases for func {0} because it does not exist", (object) func));
    }
    else
    {
      foreach (string index in aliases)
        this.m_cheatAlias[index] = func;
    }
  }

  public void UnregisterCheatHandler(string func, CheatMgr.ProcessCheatCallback callback)
  {
    this.UnregisterCheatHandler_(func, callback);
  }

  public void OnGUI()
  {
    if (!this.m_inputActive)
      return;
    if (!UniversalInputManager.Get().IsTextInputActive())
    {
      this.m_inputActive = false;
    }
    else
    {
      GUI.depth = 1000;
      GUI.backgroundColor = Color.black;
      GUI.Box(this.m_cheatInputBackground, GUIContent.none);
      GUI.Box(this.m_cheatInputBackground, GUIContent.none);
      GUI.Box(this.m_cheatInputBackground, GUIContent.none);
    }
  }

  private void RegisterCheatHandler_(string func, CheatMgr.ProcessCheatCallback callback)
  {
    if (string.IsNullOrEmpty(func.Trim()))
    {
      Debug.LogError((object) "CheatMgr.RegisterCheatHandler() - FAILED to register a null, empty, or all-whitespace function name");
    }
    else
    {
      List<CheatMgr.ProcessCheatCallback> list;
      if (this.m_funcMap.TryGetValue(func, out list))
      {
        if (list.Contains(callback))
          return;
        list.Add(callback);
      }
      else
      {
        list = new List<CheatMgr.ProcessCheatCallback>();
        this.m_funcMap.Add(func, list);
        list.Add(callback);
      }
    }
  }

  private void UnregisterCheatHandler_(string func, CheatMgr.ProcessCheatCallback callback)
  {
    List<CheatMgr.ProcessCheatCallback> list;
    if (!this.m_funcMap.TryGetValue(func, out list))
      return;
    list.Remove(callback);
  }

  private void OnInputComplete(string inputCommand)
  {
    this.m_inputActive = false;
    inputCommand = inputCommand.TrimStart();
    if (string.IsNullOrEmpty(inputCommand))
      return;
    string message = this.ProcessCheat(inputCommand);
    if (string.IsNullOrEmpty(message))
      return;
    //UIStatus.Get().AddError(message);
  }

  public string ProcessCheat(string inputCommand)
  {
    string func = this.ExtractFunc(inputCommand);
    if (func == null)
    {
      string str1 = "\"";
      string str2 = inputCommand;
      char[] chArray = new char[1];
      int index = 0;
      int num = 32;
      chArray[index] = (char) num;
      string str3 = str2.Split(chArray)[0];
      string str4 = "\" cheat command not found!";
      return str1 + str3 + str4;
    }
    int length = func.Length;
    string str;
    string[] args;
    if (length == inputCommand.Length)
    {
      str = string.Empty;
      args = new string[1]
      {
        string.Empty
      };
    }
    else
    {
      str = inputCommand.Remove(0, length + 1);
      MatchCollection matchCollection = Regex.Matches(str, "\\S+");
      if (matchCollection.Count == 0)
      {
        args = new string[1]
        {
          string.Empty
        };
      }
      else
      {
        args = new string[matchCollection.Count];
        for (int index = 0; index < matchCollection.Count; ++index)
          args[index] = matchCollection[index].Value;
      }
    }
    List<CheatMgr.ProcessCheatCallback> list = this.m_funcMap[this.GetOriginalFunc(func)];
    bool flag = false;
    for (int index = 0; index < list.Count; ++index)
      flag = list[index](func, args, str) || flag;
    if (this.m_cheatHistory.Count < 1 || !this.m_cheatHistory[0].Equals(inputCommand))
      this.m_cheatHistory.Insert(0, inputCommand);
    this.m_cheatHistoryIndex = 0;
    this.m_cheatTextBeforeScrollingThruHistory = (string) null;
    if (!flag)
      return "\"" + func + "\" cheat command executed, but failed!";
    return (string) null;
  }

  private string ExtractFunc(string inputCommand)
  {
    string str1 = inputCommand;
    char[] chArray = new char[1];
    int index1 = 0;
    int num = 47;
    chArray[index1] = (char) num;
    inputCommand = str1.TrimStart(chArray);
    inputCommand = inputCommand.Trim();
    int index2 = 0;
    List<string> funcs = new List<string>();
    using (Map<string, List<CheatMgr.ProcessCheatCallback>>.KeyCollection.Enumerator enumerator = this.m_funcMap.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        funcs.Add(current);
        if (current.Length > funcs[index2].Length)
          index2 = funcs.Count - 1;
      }
    }
    using (Map<string, string>.KeyCollection.Enumerator enumerator = this.m_cheatAlias.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        funcs.Add(current);
        if (current.Length > funcs[index2].Length)
          index2 = funcs.Count - 1;
      }
    }
    int index3;
    for (index3 = 0; index3 < inputCommand.Length; ++index3)
    {
      char c = inputCommand[index3];
      int index4 = 0;
      while (index4 < funcs.Count)
      {
        string str2 = funcs[index4];
        if (index3 == str2.Length)
        {
          if (char.IsWhiteSpace(c))
            return str2;
          funcs.RemoveAt(index4);
          if (index4 <= index2)
            index2 = this.ComputeLongestFuncIndex(funcs);
        }
        else if ((int) str2[index3] != (int) c)
        {
          funcs.RemoveAt(index4);
          if (index4 <= index2)
            index2 = this.ComputeLongestFuncIndex(funcs);
        }
        else
          ++index4;
      }
      if (funcs.Count == 0)
        return (string) null;
    }
    if (funcs.Count > 1)
    {
      using (List<string>.Enumerator enumerator = funcs.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          if (inputCommand == current)
            return current;
        }
      }
      return (string) null;
    }
    string str3 = funcs[0];
    if (index3 < str3.Length)
      return (string) null;
    return str3;
  }

  private int ComputeLongestFuncIndex(List<string> funcs)
  {
    int index1 = 0;
    for (int index2 = 1; index2 < funcs.Count; ++index2)
    {
      if (funcs[index2].Length > funcs[index1].Length)
        index1 = index2;
    }
    return index1;
  }

  private string GetOriginalFunc(string func)
  {
    string str;
    if (!this.m_cheatAlias.TryGetValue(func, out str))
      str = func;
    return str;
  }

  public delegate bool ProcessCheatCallback(string func, string[] args, string rawArgs);
}
