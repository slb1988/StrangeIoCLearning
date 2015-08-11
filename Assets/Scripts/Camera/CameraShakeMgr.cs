// Decompiled with JetBrains decompiler
// Type: CameraShakeMgr
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraShakeMgr : MonoBehaviour
{
  private Vector3 m_amount;
  private float m_durationSec;
  private bool m_started;
  private Vector3 m_initialPos;
  private float m_progressSec;

  private void Update()
  {
    if (!this.m_started)
      return;
    this.UpdateShake();
  }

  public static void Shake(Camera camera, Vector3 amount, float time)
  {
    if (!(bool) ((Object) camera) || (double) time <= 0.0)
      return;
    CameraShakeMgr cameraShakeMgr = camera.GetComponent<CameraShakeMgr>();
    if (!(bool) ((Object) cameraShakeMgr))
      cameraShakeMgr = camera.gameObject.AddComponent<CameraShakeMgr>();
    cameraShakeMgr.StartShake(amount, time);
  }

  public static void Stop(Camera camera, float time = 0.0f)
  {
    if (!(bool) ((Object) camera) || (double) time < 0.0)
      return;
    CameraShakeMgr component = camera.GetComponent<CameraShakeMgr>();
    if (!(bool) ((Object) component))
      return;
    component.StopShake(time);
  }

  private void StartShake(Vector3 amount, float durationSec)
  {
    if (!this.m_started)
      this.m_initialPos = this.transform.position;
    this.m_started = true;
    this.m_amount = amount;
    this.m_durationSec = durationSec;
    this.m_progressSec = 0.0f;
  }

  private void StopShake(float durationSec)
  {
    this.m_durationSec = durationSec;
    if ((double) durationSec > 0.0)
      return;
    this.DestroyShake();
  }

  private void UpdateShake()
  {
    float num = 1f - this.m_progressSec / this.m_durationSec;
    this.transform.position = this.m_initialPos + new Vector3()
    {
      x = Random.Range(-this.m_amount.x * num, this.m_amount.x * num),
      y = Random.Range(-this.m_amount.y * num, this.m_amount.y * num),
      z = Random.Range(-this.m_amount.z * num, this.m_amount.z * num)
    };
    if ((double) this.m_progressSec >= (double) this.m_durationSec)
    {
      this.DestroyShake();
    }
    else
    {
      this.m_progressSec += UnityEngine.Time.deltaTime;
      if ((double) this.m_progressSec <= (double) this.m_durationSec)
        return;
      this.m_progressSec = this.m_durationSec;
    }
  }

  private void DestroyShake()
  {
    this.transform.position = this.m_initialPos;
    Object.Destroy((Object) this);
  }
}
