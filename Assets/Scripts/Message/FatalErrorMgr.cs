// Decompiled with JetBrains decompiler
// Type: FatalErrorMgr
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class FatalErrorMgr
{
  private List<FatalErrorMessage> m_messages = new List<FatalErrorMessage>();
  private List<FatalErrorMgr.ErrorListener> m_errorListeners = new List<FatalErrorMgr.ErrorListener>();
  private static FatalErrorMgr s_instance;
  private string m_text;

  public static FatalErrorMgr Get()
  {
    if (FatalErrorMgr.s_instance == null)
      FatalErrorMgr.s_instance = new FatalErrorMgr();
    return FatalErrorMgr.s_instance;
  }

  public void Add(FatalErrorMessage message)
  {
    this.m_messages.Add(message);
    this.FireErrorListeners(message);
  }

  public bool AddUnique(FatalErrorMessage message)
  {
    if (!string.IsNullOrEmpty(message.m_id))
    {
      using (List<FatalErrorMessage>.Enumerator enumerator = this.m_messages.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.m_id == message.m_id)
            return false;
        }
      }
    }
    this.Add(message);
    return true;
  }

  public void ClearAllErrors()
  {
    this.m_messages.Clear();
  }

  public bool AddErrorListener(FatalErrorMgr.ErrorCallback callback)
  {
    return this.AddErrorListener(callback, (object) null);
  }

  public bool AddErrorListener(FatalErrorMgr.ErrorCallback callback, object userData)
  {
    FatalErrorMgr.ErrorListener errorListener = new FatalErrorMgr.ErrorListener();
    errorListener.SetCallback(callback);
    errorListener.SetUserData(userData);
    if (this.m_errorListeners.Contains(errorListener))
      return false;
    this.m_errorListeners.Add(errorListener);
    return true;
  }

  public bool RemoveErrorListener(FatalErrorMgr.ErrorCallback callback)
  {
    return this.RemoveErrorListener(callback, (object) null);
  }

  public bool RemoveErrorListener(FatalErrorMgr.ErrorCallback callback, object userData)
  {
    FatalErrorMgr.ErrorListener errorListener = new FatalErrorMgr.ErrorListener();
    errorListener.SetCallback(callback);
    errorListener.SetUserData(userData);
    return this.m_errorListeners.Remove(errorListener);
  }

  public List<FatalErrorMessage> GetMessages()
  {
    return this.m_messages;
  }

  public bool HasError()
  {
    return this.m_messages.Count > 0;
  }

  public void NotifyExitPressed()
  {
    Log.Mike.Print("FatalErrorDialog.NotifyExitPressed() - BEGIN");
    this.SendAcknowledgements();
    Log.Mike.Print("FatalErrorDialog.NotifyExitPressed() - calling ApplicationMgr.Get().Exit()");
    ApplicationMgr.Get().Exit();
    Log.Mike.Print("FatalErrorDialog.NotifyExitPressed() - END");
  }

  private void SendAcknowledgements()
  {
    foreach (FatalErrorMessage fatalErrorMessage in this.m_messages.ToArray())
    {
      if (fatalErrorMessage.m_ackCallback != null)
        fatalErrorMessage.m_ackCallback(fatalErrorMessage.m_ackUserData);
    }
  }

  protected void FireErrorListeners(FatalErrorMessage message)
  {
    foreach (FatalErrorMgr.ErrorListener errorListener in this.m_errorListeners.ToArray())
      errorListener.Fire(message);
  }

  protected class ErrorListener : EventListener<FatalErrorMgr.ErrorCallback>
  {
    public void Fire(FatalErrorMessage message)
    {
      if (!GeneralUtils.IsCallbackValid((Delegate) this.m_callback))
        return;
      this.m_callback(message, this.m_userData);
    }
  }

  public delegate void ErrorCallback(FatalErrorMessage message, object userData);
}
