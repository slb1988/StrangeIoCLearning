// Decompiled with JetBrains decompiler
// Type: FullScreenAntialiasing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FullScreenAntialiasing : MonoBehaviour
{
  public Shader m_FXAA_Shader;
  private Material m_FXAA_Material;

  protected Material FXAA_Material
  {
    get
    {
      if ((Object) this.m_FXAA_Material == (Object) null)
      {
        this.m_FXAA_Material = new Material(this.m_FXAA_Shader);
        //SceneUtils.SetHideFlags((Object) this.m_FXAA_Material, HideFlags.DontSave);
      }
      return this.m_FXAA_Material;
    }
  }

  private void Awake()
  {
    this.gameObject.GetComponent<Camera>().enabled = true;
    if (SystemInfo.supportsImageEffects && SystemInfo.supportsRenderTextures)
      return;
    this.enabled = false;
  }

  private void Start()
  {
    if ((Object) this.m_FXAA_Shader == (Object) null || (Object) this.FXAA_Material == (Object) null)
      this.enabled = false;
    if (this.m_FXAA_Shader.isSupported)
      return;
    this.enabled = false;
  }

  private void Update()
  {
  }

  private void OnDisable()
  {
    if (!((Object) this.m_FXAA_Material != (Object) null))
      return;
    Object.Destroy((Object) this.m_FXAA_Material);
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    Graphics.Blit((Texture) source, destination, this.FXAA_Material);
  }
}
