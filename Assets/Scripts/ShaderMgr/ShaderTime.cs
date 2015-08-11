// Decompiled with JetBrains decompiler
// Type: ShaderTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ShaderTime : MonoBehaviour
{
  private float m_maxTime = 999f;
  private float m_time;

  private void Awake()
  {
  }

  private void Update()
  {
    this.UpdateShaderAnimationTime();
  }

  private void OnDestroy()
  {
    Shader.SetGlobalFloat("_ShaderTime", 0.0f);
  }

  private void UpdateShaderAnimationTime()
  {
    this.m_time += UnityEngine.Time.deltaTime / 20f;
    if ((double) this.m_time > (double) this.m_maxTime)
    {
      this.m_time = this.m_time - this.m_maxTime;
      if ((double) this.m_time <= 0.0)
        this.m_time = 0.0001f;
    }
    Shader.SetGlobalFloat("_ShaderTime", this.m_time);
  }
}
