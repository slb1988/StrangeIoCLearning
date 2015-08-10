// Decompiled with JetBrains decompiler
// Type: Error
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class Error
{
  public static readonly PlatformDependentValue<bool> HAS_APP_STORE = new PlatformDependentValue<bool>(PlatformCategory.OS)
  {
    PC = false,
    Mac = false,
    iOS = true,
    Android = true
  };

  //public static void AddWarning(string header, string message, params object[] messageArgs)
  //{
  //  Error.AddWarning(new ErrorParams()
  //  {
  //    m_header = header,
  //    m_message = string.Format(message, messageArgs)
  //  });
  //}

  //public static void AddWarningLoc(string headerKey, string messageKey, params object[] messageArgs)
  //{
  //  Error.AddWarning(new ErrorParams()
  //  {
  //    m_header = GameStrings.Get(headerKey),
  //    m_message = GameStrings.Format(messageKey, messageArgs)
  //  });
  //}

  //public static void AddDevWarning(string header, string message, params object[] messageArgs)
  //{
  //  string str = string.Format(message, messageArgs);
  //  if (!ApplicationMgr.IsInternal())
  //    Debug.LogWarning((object) string.Format("Error.AddDevWarning() - header={0} message={1}", (object) header, (object) str));
  //  else
  //    Error.AddWarning(new ErrorParams()
  //    {
  //      m_header = header,
  //      m_message = str
  //    });
  //}

  //public static void AddWarning(ErrorParams parms)
  //{
  //  if (!(bool) ((Object) DialogManager.Get()))
  //  {
  //    Error.AddFatal(parms);
  //  }
  //  else
  //  {
  //    Debug.LogWarning((object) string.Format("Error.AddWarning() - header={0} message={1}", (object) parms.m_header, (object) parms.m_message));
  //    if ((Object) UniversalInputManager.Get() != (Object) null)
  //      UniversalInputManager.Get().CancelTextInput((GameObject) null, true);
  //    Error.ShowWarningDialog(parms);
  //  }
  //}

  //public static void AddFatal(string message)
  //{
  //  Error.AddFatal(new ErrorParams()
  //  {
  //    m_message = message
  //  });
  //}

  //public static void AddFatalLoc(string messageKey, params object[] messageArgs)
  //{
  //  Error.AddFatal(new ErrorParams()
  //  {
  //    m_message = GameStrings.Format(messageKey, messageArgs)
  //  });
  //}

  //public static void AddDevFatal(string message, params object[] messageArgs)
  //{
  //  string str = string.Format(message, messageArgs);
  //  if (!ApplicationMgr.IsInternal())
  //    Debug.LogError((object) string.Format("Error.AddDevFatal() - message={0}", (object) str));
  //  else
  //    Error.AddFatal(new ErrorParams()
  //    {
  //      m_message = str
  //    });
  //}

  //public static void AddFatal(ErrorParams parms)
  //{
  //  Debug.LogError((object) string.Format("Error.AddFatal() - message={0}", (object) parms.m_message));
  //  if ((Object) UniversalInputManager.Get() != (Object) null)
  //    UniversalInputManager.Get().CancelTextInput((GameObject) null, true);
  //  if (Error.ShouldUseWarningDialogForFatalError())
  //  {
  //    if (string.IsNullOrEmpty(parms.m_header))
  //      parms.m_header = "Fatal Error as Warning";
  //    Error.ShowWarningDialog(parms);
  //  }
  //  else
  //  {
  //    parms.m_type = ErrorType.FATAL;
  //    FatalErrorMgr.Get().Add(new FatalErrorMessage()
  //    {
  //      m_id = (parms.m_header ?? string.Empty) + parms.m_message,
  //      m_text = parms.m_message,
  //      m_ackCallback = parms.m_ackCallback,
  //      m_ackUserData = parms.m_ackUserData,
  //      m_allowClick = parms.m_allowClick,
  //      m_redirectToStore = parms.m_redirectToStore,
  //      m_delayBeforeNextReset = parms.m_delayBeforeNextReset
  //    });
  //  }
  //}

  //private static bool ShouldUseWarningDialogForFatalError()
  //{
  //  if (ApplicationMgr.IsPublic() || !(bool) ((Object) DialogManager.Get()))
  //    return false;
  //  return !Options.Get().GetBool(Option.ERROR_SCREEN);
  //}

  //private static void ShowWarningDialog(ErrorParams parms)
  //{
  //  parms.m_type = ErrorType.WARNING;
  //  DialogManager.Get().ShowPopup(new AlertPopup.PopupInfo()
  //  {
  //    m_id = parms.m_header + parms.m_message,
  //    m_headerText = parms.m_header,
  //    m_text = parms.m_message,
  //    m_responseCallback = new AlertPopup.ResponseCallback(Error.OnWarningPopupResponse),
  //    m_responseUserData = (object) parms,
  //    m_showAlertIcon = true
  //  });
  //}

  //private static void OnWarningPopupResponse(AlertPopup.Response response, object userData)
  //{
  //  ErrorParams errorParams = (ErrorParams) userData;
  //  if (errorParams.m_ackCallback == null)
  //    return;
  //  errorParams.m_ackCallback(errorParams.m_ackUserData);
  //}

  public delegate void AcknowledgeCallback(object userData);
}
