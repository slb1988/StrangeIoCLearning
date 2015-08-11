// Decompiled with JetBrains decompiler
// Type: CameraShaker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraShaker : MonoBehaviour
{
  public Vector3 m_Amount;
  public float m_Time;

  public void StartShake()
  {
    CameraShakeMgr.Shake(Camera.main, this.m_Amount, this.m_Time);
  }
}
