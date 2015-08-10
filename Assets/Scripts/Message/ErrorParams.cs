// Decompiled with JetBrains decompiler
// Type: ErrorParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

public class ErrorParams
{
  public bool m_allowClick = true;
  public ErrorType m_type;
  public string m_header;
  public string m_message;
  public Error.AcknowledgeCallback m_ackCallback;
  public object m_ackUserData;
  public bool m_redirectToStore;
  public float m_delayBeforeNextReset;
}
