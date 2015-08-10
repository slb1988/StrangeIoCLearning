// Decompiled with JetBrains decompiler
// Type: EventListener`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

public class EventListener<Delegate>
{
  protected Delegate m_callback;
  protected object m_userData;

  public EventListener()
  {
  }

  public EventListener(Delegate callback, object userData)
  {
    this.m_callback = callback;
    this.m_userData = userData;
  }

  public override bool Equals(object obj)
  {
    EventListener<Delegate> eventListener = obj as EventListener<Delegate>;
    if (eventListener == null)
      return base.Equals(obj);
    if (this.m_callback.Equals((object) eventListener.m_callback))
      return this.m_userData == eventListener.m_userData;
    return false;
  }

  public override int GetHashCode()
  {
    int num = 23;
    if ((object) this.m_callback != null)
      num = num * 17 + this.m_callback.GetHashCode();
    if (this.m_userData != null)
      num = num * 17 + this.m_userData.GetHashCode();
    return num;
  }

  public Delegate GetCallback()
  {
    return this.m_callback;
  }

  public void SetCallback(Delegate callback)
  {
    this.m_callback = callback;
  }

  public object GetUserData()
  {
    return this.m_userData;
  }

  public void SetUserData(object userData)
  {
    this.m_userData = userData;
  }
}
