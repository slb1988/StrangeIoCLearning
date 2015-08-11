// Decompiled with JetBrains decompiler
// Type: FullScreenEffects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FullScreenEffects : MonoBehaviour
{
  private int m_LowQualityFreezeBufferSize = 512;
  public float m_BlurBlend = 1f;
  private float m_BlurAmount = 2f;
  private float m_BlurBrightness = 1f;
  private float m_PreviousBlurAmount = 1f;
  private float m_PreviousBlurBrightness = 1f;
  private Color m_BlendToColor = Color.white;
  private const int NO_WORK_FRAMES_BEFORE_DEACTIVATE = 2;
  private const string BLUR_SHADER_NAME = "Custom/FullScreen/Blur";
  private const string BLUR_VIGNETTING_SHADER_NAME = "Custom/FullScreen/BlurVignetting";
  private const string BLUR_DESATURATION_SHADER_NAME = "Custom/FullScreen/DesaturationBlur";
  private const string BLEND_SHADER_NAME = "Custom/FullScreen/Blend";
  private const string VIGNETTING_SHADER_NAME = "Custom/FullScreen/Vignetting";
  private const string BLEND_TO_COLOR_SHADER_NAME = "Custom/FullScreen/BlendToColor";
  private const string DESATURATION_SHADER_NAME = "Custom/FullScreen/Desaturation";
  private const string DESATURATION_VIGNETTING_SHADER_NAME = "Custom/FullScreen/DesaturationVignetting";
  private const string BLUR_DESATURATION_VIGNETTING_SHADER_NAME = "Custom/FullScreen/BlurDesaturationVignetting";
  private const int BLUR_BUFFER_SIZE = 512;
  private const float BLUR_SECOND_PASS_REDUCTION = 0.5f;
  private const float BLUR_PASS_1_OFFSET = 1f;
  private const float BLUR_PASS_2_OFFSET = 0.4f;
  private const float BLUR_PASS_3_OFFSET = -0.2f;
  public Texture2D m_VignettingMask;
  private bool m_BlurEnabled;
  private float m_BlurDesaturation;
  private bool m_VignettingEnable;
  private float m_PreviousBlurDesaturation;
  private float m_VignettingIntensity;
  private bool m_BlendToColorEnable;
  private float m_BlendToColorAmount;
  private bool m_DesaturationEnabled;
  private float m_Desaturation;
  private bool m_WireframeRender;
  private int m_DeactivateFrameCount;
  private Shader m_BlurShader;
  private Shader m_BlurVignettingShader;
  private Shader m_BlurDesaturationShader;
  private Shader m_BlendShader;
  private Shader m_VignettingShader;
  private Shader m_BlendToColorShader;
  private Shader m_DesaturationShader;
  private Shader m_DesaturationVignettingShader;
  private Shader m_BlurDesaturationVignettingShader;
  private bool m_FrozenState;
  private bool m_CaptureFrozenImage;
  private Texture2D m_FrozenScreenTexture;
  //private UniversalInputManager m_UniversalInputManager;
  private Camera m_Camera;
  private Material m_BlurMaterial;
  private Material m_BlurVignettingMaterial;
  private Material m_BlurDesatMaterial;
  private Material m_BlendMaterial;
  private Material m_VignettingMaterial;
  private Material m_BlendToColorMaterial;
  private Material m_DesaturationMaterial;
  private Material m_DesaturationVignettingMaterial;
  private Material m_BlurDesaturationVignettingMaterial;

  protected Material blurMaterial
  {
    get
    {
      if ((Object) this.m_BlurMaterial == (Object) null)
      {
        this.m_BlurMaterial = new Material(this.m_BlurShader);
        SceneUtils.SetHideFlags((Object) this.m_BlurMaterial, HideFlags.DontSave);
      }
      return this.m_BlurMaterial;
    }
  }

  protected Material blurVignettingMaterial
  {
    get
    {
      if ((Object) this.m_BlurVignettingMaterial == (Object) null)
      {
        this.m_BlurVignettingMaterial = new Material(this.m_BlurVignettingShader);
        SceneUtils.SetHideFlags((Object) this.m_BlurVignettingMaterial, HideFlags.DontSave);
      }
      return this.m_BlurVignettingMaterial;
    }
  }

  protected Material blurDesatMaterial
  {
    get
    {
      if ((Object) this.m_BlurDesatMaterial == (Object) null)
      {
        this.m_BlurDesatMaterial = new Material(this.m_BlurDesaturationShader);
        SceneUtils.SetHideFlags((Object) this.m_BlurDesatMaterial, HideFlags.DontSave);
      }
      return this.m_BlurDesatMaterial;
    }
  }

  protected Material blendMaterial
  {
    get
    {
      if ((Object) this.m_BlendMaterial == (Object) null)
      {
        this.m_BlendMaterial = new Material(this.m_BlendShader);
        SceneUtils.SetHideFlags((Object) this.m_BlendMaterial, HideFlags.DontSave);
      }
      return this.m_BlendMaterial;
    }
  }

  protected Material VignettingMaterial
  {
    get
    {
      if ((Object) this.m_VignettingMaterial == (Object) null)
      {
        this.m_VignettingMaterial = new Material(this.m_VignettingShader);
        SceneUtils.SetHideFlags((Object) this.m_VignettingMaterial, HideFlags.DontSave);
      }
      return this.m_VignettingMaterial;
    }
  }

  protected Material BlendToColorMaterial
  {
    get
    {
      if ((Object) this.m_BlendToColorMaterial == (Object) null)
      {
        this.m_BlendToColorMaterial = new Material(this.m_BlendToColorShader);
        SceneUtils.SetHideFlags((Object) this.m_BlendToColorMaterial, HideFlags.DontSave);
      }
      return this.m_BlendToColorMaterial;
    }
  }

  protected Material DesaturationMaterial
  {
    get
    {
      if ((Object) this.m_DesaturationMaterial == (Object) null)
      {
        this.m_DesaturationMaterial = new Material(this.m_DesaturationShader);
        SceneUtils.SetHideFlags((Object) this.m_DesaturationMaterial, HideFlags.DontSave);
      }
      return this.m_DesaturationMaterial;
    }
  }

  protected Material DesaturationVignettingMaterial
  {
    get
    {
      if ((Object) this.m_DesaturationVignettingMaterial == (Object) null)
      {
        this.m_DesaturationVignettingMaterial = new Material(this.m_DesaturationVignettingShader);
        SceneUtils.SetHideFlags((Object) this.m_DesaturationVignettingMaterial, HideFlags.DontSave);
      }
      return this.m_DesaturationVignettingMaterial;
    }
  }

  protected Material BlurDesaturationVignettingMaterial
  {
    get
    {
      if ((Object) this.m_BlurDesaturationVignettingMaterial == (Object) null)
      {
        this.m_BlurDesaturationVignettingMaterial = new Material(this.m_BlurDesaturationVignettingShader);
        SceneUtils.SetHideFlags((Object) this.m_BlurDesaturationVignettingMaterial, HideFlags.DontSave);
      }
      return this.m_BlurDesaturationVignettingMaterial;
    }
  }

  public bool BlurEnabled
  {
    get
    {
      return this.m_BlurEnabled;
    }
    set
    {
      if (!this.enabled && value)
        this.enabled = true;
      this.m_BlurEnabled = value;
    }
  }

  public float BlurBlend
  {
    get
    {
      return this.m_BlurBlend;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_BlurEnabled = true;
      this.m_BlurBlend = value;
    }
  }

  public float BlurAmount
  {
    get
    {
      return this.m_BlurAmount;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_BlurEnabled = true;
      this.m_BlurAmount = value;
      this.m_PreviousBlurAmount = value;
    }
  }

  public float BlurDesaturation
  {
    get
    {
      return this.m_BlurDesaturation;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_BlurEnabled = true;
      this.m_BlurDesaturation = value;
      this.m_PreviousBlurDesaturation = value;
    }
  }

  public float BlurBrightness
  {
    get
    {
      return this.m_BlurBrightness;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_BlurEnabled = true;
      this.m_BlurBrightness = value;
      this.m_PreviousBlurBrightness = value;
    }
  }

  public bool VignettingEnable
  {
    get
    {
      return this.m_VignettingEnable;
    }
    set
    {
      if (!this.enabled && value)
        this.enabled = true;
      this.m_VignettingEnable = value;
    }
  }

  public float VignettingIntensity
  {
    get
    {
      return this.m_VignettingIntensity;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_VignettingEnable = true;
      this.m_VignettingIntensity = value;
    }
  }

  public bool BlendToColorEnable
  {
    get
    {
      return this.m_BlendToColorEnable;
    }
    set
    {
      if (!this.enabled && value)
        this.enabled = true;
      this.m_BlendToColorEnable = value;
    }
  }

  public Color BlendToColor
  {
    get
    {
      return this.m_BlendToColor;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_BlendToColorEnable = true;
      this.m_BlendToColor = value;
    }
  }

  public float BlendToColorAmount
  {
    get
    {
      return this.m_BlendToColorAmount;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_BlendToColorEnable = true;
      this.m_BlendToColorAmount = value;
    }
  }

  public bool DesaturationEnabled
  {
    get
    {
      return this.m_DesaturationEnabled;
    }
    set
    {
      if (!this.enabled && value)
        this.enabled = true;
      this.m_DesaturationEnabled = value;
    }
  }

  public float Desaturation
  {
    get
    {
      return this.m_Desaturation;
    }
    set
    {
      if (!this.enabled)
        this.enabled = true;
      this.m_DesaturationEnabled = true;
      this.m_Desaturation = value;
    }
  }

  protected void OnDisable()
  {
    this.SetDefaults();
    if ((bool) ((Object) this.m_BlurMaterial))
      Object.Destroy((Object) this.m_BlurMaterial);
    if ((bool) ((Object) this.m_BlurVignettingMaterial))
      Object.Destroy((Object) this.m_BlurVignettingMaterial);
    if ((bool) ((Object) this.m_BlurDesatMaterial))
      Object.Destroy((Object) this.m_BlurDesatMaterial);
    if ((bool) ((Object) this.m_BlendMaterial))
      Object.Destroy((Object) this.m_BlendMaterial);
    if ((bool) ((Object) this.m_VignettingMaterial))
      Object.Destroy((Object) this.m_VignettingMaterial);
    if ((bool) ((Object) this.m_BlendToColorMaterial))
      Object.Destroy((Object) this.m_BlendToColorMaterial);
    if ((bool) ((Object) this.m_DesaturationMaterial))
      Object.Destroy((Object) this.m_DesaturationMaterial);
    if ((bool) ((Object) this.m_DesaturationVignettingMaterial))
      Object.Destroy((Object) this.m_DesaturationVignettingMaterial);
    if (!(bool) ((Object) this.m_BlurDesaturationVignettingMaterial))
      return;
    Object.Destroy((Object) this.m_BlurDesaturationVignettingMaterial);
  }

  protected void OnDestroy()
  {
    CheatMgr cheatMgr = CheatMgr.Get();
    if (!((Object) cheatMgr != (Object) null))
      return;
    cheatMgr.UnregisterCheatHandler("wireframe", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_RenderWireframe));
  }

  protected void Awake()
  {
    this.m_Camera = this.GetComponent<Camera>();
  }

  protected void Start()
  {
    CheatMgr cheatMgr = CheatMgr.Get();
    if ((Object) cheatMgr != (Object) null)
      cheatMgr.RegisterCheatHandler("wireframe", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_RenderWireframe), (string) null, (string) null, (string) null);
    this.gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
    if (!SystemInfo.supportsImageEffects)
    {
      Debug.LogError((object) "Fullscreen Effects not supported");
      this.enabled = false;
    }
    else
    {
      if ((Object) this.m_BlurShader == (Object) null)
        this.m_BlurShader = ShaderUtils.FindShader("Custom/FullScreen/Blur");
      if (!(bool) ((Object) this.m_BlurShader))
      {
        Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/Blur");
        this.enabled = false;
      }
      if (!(bool) ((Object) this.m_BlurShader) || !this.blurMaterial.shader.isSupported)
      {
        Debug.LogError((object) "Fullscreen Effect Shader not supported: Custom/FullScreen/Blur");
        this.enabled = false;
      }
      else
      {
        if ((Object) this.m_BlurVignettingShader == (Object) null)
          this.m_BlurVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/BlurVignetting");
        if (!(bool) ((Object) this.m_BlurVignettingShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlurVignetting");
          this.enabled = false;
        }
        if ((Object) this.m_BlurDesaturationShader == (Object) null)
          this.m_BlurDesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationBlur");
        if (!(bool) ((Object) this.m_BlurDesaturationShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/DesaturationBlur");
          this.enabled = false;
        }
        if ((Object) this.m_BlendShader == (Object) null)
          this.m_BlendShader = ShaderUtils.FindShader("Custom/FullScreen/Blend");
        if (!(bool) ((Object) this.m_BlendShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/Blend");
          this.enabled = false;
        }
        if ((Object) this.m_VignettingShader == (Object) null)
          this.m_VignettingShader = ShaderUtils.FindShader("Custom/FullScreen/Vignetting");
        if (!(bool) ((Object) this.m_VignettingShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/Vignetting");
          this.enabled = false;
        }
        if ((Object) this.m_BlendToColorShader == (Object) null)
          this.m_BlendToColorShader = ShaderUtils.FindShader("Custom/FullScreen/BlendToColor");
        if (!(bool) ((Object) this.m_BlendToColorShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlendToColor");
          this.enabled = false;
        }
        if ((Object) this.m_DesaturationShader == (Object) null)
          this.m_DesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/Desaturation");
        if (!(bool) ((Object) this.m_DesaturationShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/Desaturation");
          this.enabled = false;
        }
        if ((Object) this.m_DesaturationVignettingShader == (Object) null)
          this.m_DesaturationVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationVignetting");
        if (!(bool) ((Object) this.m_DesaturationVignettingShader))
        {
          Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/DesaturationVignetting");
          this.enabled = false;
        }
        if ((Object) this.m_BlurDesaturationVignettingShader == (Object) null)
          this.m_BlurDesaturationVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/BlurDesaturationVignetting");
        if ((bool) ((Object) this.m_BlurDesaturationVignettingShader))
          return;
        Debug.LogError((object) "Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlurDesaturationVignetting");
        this.enabled = false;
      }
    }
  }

  private void Update()
  {
//    this.UpdateUniversalInputManager();
  }

  private void SetDefaults()
  {
    this.m_BlurEnabled = false;
    this.m_BlurBlend = 1f;
    this.m_BlurAmount = 2f;
    this.m_BlurDesaturation = 0.0f;
    this.m_BlurBrightness = 1f;
    this.m_VignettingEnable = false;
    this.m_VignettingIntensity = 0.0f;
    this.m_BlendToColorEnable = false;
    this.m_BlendToColor = Color.white;
    this.m_BlendToColorAmount = 0.0f;
    this.m_DesaturationEnabled = false;
    this.m_Desaturation = 0.0f;
  }

  public void Disable()
  {
    this.enabled = false;
    this.SetDefaults();
//    FullScreenFXMgr fullScreenFxMgr = FullScreenFXMgr.Get();
    //if (!((Object) fullScreenFxMgr != (Object) null))
    //  return;
    //fullScreenFxMgr.OnReset();
  }

  [ContextMenu("Freeze")]
  public void Freeze()
  {
    Log.Kyle.Print("FullScreenEffects: Freeze()");
    this.enabled = true;
    if (this.m_FrozenState)
      return;
    this.m_BlurEnabled = true;
    this.m_BlurBlend = 1f;
    this.m_BlurAmount = this.m_PreviousBlurAmount * 0.75f;
    this.m_BlurDesaturation = this.m_PreviousBlurDesaturation;
    this.m_BlurBrightness = this.m_PreviousBlurBrightness;
    this.m_CaptureFrozenImage = true;
    int num1 = this.m_LowQualityFreezeBufferSize;
    int num2 = this.m_LowQualityFreezeBufferSize;
    int width;
    int height;
    //if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
    //{
    //  width = this.m_LowQualityFreezeBufferSize;
    //  height = this.m_LowQualityFreezeBufferSize;
    //}
    //else
    {
      width = Screen.currentResolution.width;
      height = Screen.currentResolution.height;
    }
    this.m_FrozenScreenTexture = new Texture2D(width, height, TextureFormat.RGB24, false, true);
    this.m_FrozenScreenTexture.filterMode = FilterMode.Point;
    this.m_FrozenScreenTexture.wrapMode = TextureWrapMode.Clamp;
  }

  [ContextMenu("Unfreeze")]
  public void Unfreeze()
  {
    Log.Kyle.Print("FullScreenEffects: Unfreeze()");
    this.m_BlurEnabled = false;
    this.m_BlurBlend = 0.0f;
    this.m_FrozenState = false;
    Object.Destroy((Object) this.m_FrozenScreenTexture);
    this.m_FrozenScreenTexture = (Texture2D) null;
  }

  public bool isActive()
  {
    return this.enabled && (this.m_FrozenState || this.m_BlurEnabled && (double) this.m_BlurBlend > 0.0 || (this.m_VignettingEnable || this.m_BlendToColorEnable || (this.m_DesaturationEnabled || this.m_WireframeRender)));
  }

  private void Sample(RenderTexture source, RenderTexture dest, float off, Material mat)
  {
    if ((Object) dest != (Object) null)
      dest.DiscardContents();
    RenderTexture renderTexture = source;
    RenderTexture dest1 = dest;
    Material mat1 = mat;
    Vector2[] vector2Array = new Vector2[4];
    int index1 = 0;
    vector2Array[index1] = new Vector2(-off, -off);
    int index2 = 1;
    vector2Array[index2] = new Vector2(-off, off);
    int index3 = 2;
    vector2Array[index3] = new Vector2(off, off);
    int index4 = 3;
    vector2Array[index4] = new Vector2(off, -off);
    Graphics.BlitMultiTap((Texture) renderTexture, dest1, mat1, vector2Array);
  }

  private void Blur(RenderTexture source, RenderTexture destination, Material blurMat)
  {
    float sizeX = (float) source.width;
    float sizeY = (float) source.height;
    this.CalcTextureSize(source.width, source.height, 512, out sizeX, out sizeY);
    RenderTexture temporary1 = RenderTexture.GetTemporary((int) sizeX, (int) sizeY, 0);
    RenderTexture temporary2 = RenderTexture.GetTemporary((int) ((double) sizeX * 0.5), (int) ((double) sizeY * 0.5), 0);
    temporary1.wrapMode = TextureWrapMode.Clamp;
    temporary2.wrapMode = TextureWrapMode.Clamp;
    blurMat.SetFloat("_BlurOffset", 1f);
    blurMat.SetFloat("_FirstPass", 1f);
    Graphics.Blit((Texture) source, temporary1, blurMat);
    blurMat.SetFloat("_BlurOffset", 0.4f);
    blurMat.SetFloat("_FirstPass", 0.0f);
    Graphics.Blit((Texture) temporary1, temporary2, blurMat);
    blurMat.SetFloat("_BlurOffset", -0.2f);
    blurMat.SetFloat("_FirstPass", 0.0f);
    Graphics.Blit((Texture) temporary2, destination, blurMat);
    RenderTexture.ReleaseTemporary(temporary1);
    RenderTexture.ReleaseTemporary(temporary2);
  }

  private void CalcTextureSize(int currentWidth, int currentHeight, int resolution, out float sizeX, out float sizeY)
  {
    float num1 = (float) currentWidth;
    float num2 = (float) currentHeight;
    float num3 = (float) resolution;
    if ((double) num1 > (double) num2)
    {
      sizeX = num3;
      sizeY = num3 * (num2 / num1);
    }
    else
    {
      sizeX = num3 * (num1 / num2);
      sizeY = num3;
    }
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if ((Object) source == (Object) null || source.width == 0 || source.height == 0)
      return;
    bool flag = false;
    if (this.m_CaptureFrozenImage && !this.m_FrozenState)
    {
      Log.Kyle.Print("FullScreenEffects: Capture Frozen Image!");
      Material blurMat = this.blurMaterial;
      blurMat.SetFloat("_Brightness", this.m_BlurBrightness);
      if ((double) this.m_BlurDesaturation > 0.0 && !this.m_VignettingEnable)
      {
        blurMat = this.blurDesatMaterial;
        blurMat.SetFloat("_Desaturation", this.m_BlurDesaturation);
      }
      else if (this.m_VignettingEnable && (double) this.m_BlurDesaturation == 0.0)
      {
        blurMat = this.blurVignettingMaterial;
        blurMat.SetFloat("_Amount", this.m_VignettingIntensity);
        blurMat.SetTexture("_MaskTex", (Texture) this.m_VignettingMask);
      }
      else if (this.m_VignettingEnable && (double) this.m_BlurDesaturation > 0.0)
      {
        blurMat = this.BlurDesaturationVignettingMaterial;
        blurMat.SetFloat("_Amount", this.m_VignettingIntensity);
        blurMat.SetTexture("_MaskTex", (Texture) this.m_VignettingMask);
        blurMat.SetFloat("_Desaturation", this.m_BlurDesaturation);
      }
      int num1 = this.m_LowQualityFreezeBufferSize;
      int num2 = this.m_LowQualityFreezeBufferSize;
      int width;
      int height;
      //if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
      //{
      //  width = this.m_LowQualityFreezeBufferSize;
      //  height = this.m_LowQualityFreezeBufferSize;
      //}
      //else
      {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
      }
      RenderTexture temporary = RenderTexture.GetTemporary(width, height);
      this.Blur(source, temporary, blurMat);
      RenderTexture.active = temporary;
      this.m_FrozenScreenTexture.ReadPixels(new Rect(0.0f, 0.0f, (float) width, (float) height), 0, 0, false);
      this.m_FrozenScreenTexture.Apply();
      RenderTexture.active = (RenderTexture) null;
      RenderTexture.ReleaseTemporary(temporary);
      this.m_CaptureFrozenImage = false;
      this.m_FrozenState = true;
      flag = true;
      this.m_DeactivateFrameCount = 0;
    }
    if (this.m_FrozenState)
    {
      if ((bool) ((Object) this.m_FrozenScreenTexture))
      {
        Material blendMaterial = this.blendMaterial;
        blendMaterial.SetFloat("_Amount", 1f);
        blendMaterial.SetTexture("_BlendTex", (Texture) this.m_FrozenScreenTexture);
        if (QualitySettings.antiAliasing > 0)
          blendMaterial.SetFloat("_Flip", 1f);
        else
          blendMaterial.SetFloat("_Flip", 0.0f);
        if ((Object) destination != (Object) null)
          destination.DiscardContents();
        Graphics.Blit((Texture) source, destination, blendMaterial);
        this.m_DeactivateFrameCount = 0;
        return;
      }
      Debug.LogWarning((object) "m_FrozenScreenTexture is null. FullScreenEffect Freeze disabled");
      this.m_FrozenState = false;
    }
    if (this.m_BlurEnabled && (double) this.m_BlurBlend > 0.0)
    {
      Material blurMat = this.blurMaterial;
      blurMat.SetFloat("_Brightness", this.m_BlurBrightness);
      if ((double) this.m_BlurDesaturation > 0.0 && !this.m_VignettingEnable)
      {
        blurMat = this.blurDesatMaterial;
        blurMat.SetFloat("_Desaturation", this.m_BlurDesaturation);
      }
      else if (this.m_VignettingEnable && (double) this.m_BlurDesaturation == 0.0)
      {
        blurMat = this.blurVignettingMaterial;
        blurMat.SetFloat("_Amount", this.m_VignettingIntensity);
        blurMat.SetTexture("_MaskTex", (Texture) this.m_VignettingMask);
      }
      else if (this.m_VignettingEnable && (double) this.m_BlurDesaturation > 0.0)
      {
        blurMat = this.BlurDesaturationVignettingMaterial;
        blurMat.SetFloat("_Amount", this.m_VignettingIntensity);
        blurMat.SetTexture("_MaskTex", (Texture) this.m_VignettingMask);
        blurMat.SetFloat("_Desaturation", this.m_BlurDesaturation);
      }
      if ((double) this.m_BlurBlend >= 1.0)
      {
        this.Blur(source, destination, blurMat);
        flag = true;
      }
      else
      {
        RenderTexture temporary = RenderTexture.GetTemporary(512, 512, 0);
        this.Blur(source, temporary, blurMat);
        Material blendMaterial = this.blendMaterial;
        blendMaterial.SetFloat("_Amount", this.m_BlurBlend);
        blendMaterial.SetTexture("_BlendTex", (Texture) temporary);
        Graphics.Blit((Texture) source, destination, blendMaterial);
        RenderTexture.ReleaseTemporary(temporary);
        flag = true;
      }
    }
    if (this.m_DesaturationEnabled && !flag)
    {
      Material mat = this.DesaturationMaterial;
      if (this.m_VignettingEnable)
      {
        mat = this.DesaturationVignettingMaterial;
        mat.SetFloat("_Amount", this.m_VignettingIntensity);
        mat.SetTexture("_MaskTex", (Texture) this.m_VignettingMask);
      }
      mat.SetFloat("_Desaturation", this.m_Desaturation);
      Graphics.Blit((Texture) source, destination, mat);
      flag = true;
    }
    if (this.m_VignettingEnable && !flag)
    {
      Material vignettingMaterial = this.VignettingMaterial;
      vignettingMaterial.SetFloat("_Amount", this.m_VignettingIntensity);
      vignettingMaterial.SetTexture("_MaskTex", (Texture) this.m_VignettingMask);
      Graphics.Blit((Texture) source, destination, vignettingMaterial);
      flag = true;
    }
    if (this.m_BlendToColorEnable && !flag)
    {
      Material blendToColorMaterial = this.BlendToColorMaterial;
      blendToColorMaterial.SetFloat("_Amount", this.m_BlendToColorAmount);
      blendToColorMaterial.SetColor("_Color", this.m_BlendToColor);
      Graphics.Blit((Texture) source, destination, blendToColorMaterial);
      flag = true;
    }
    if (flag)
      return;
    Material blendMaterial1 = this.blendMaterial;
    blendMaterial1.SetFloat("_Amount", 0.0f);
    blendMaterial1.SetTexture("_BlendTex", (Texture) null);
    Graphics.Blit((Texture) source, destination, blendMaterial1);
    if (this.m_DeactivateFrameCount > 2)
    {
      this.m_DeactivateFrameCount = 0;
      this.Disable();
    }
    else
      ++this.m_DeactivateFrameCount;
  }

  private void OnPreRender()
  {
    if (!this.m_WireframeRender)
      return;
    GL.wireframe = true;
  }

  private void OnPostRender()
  {
    GL.wireframe = false;
  }

  private bool OnProcessCheat_RenderWireframe(string func, string[] args, string rawArgs)
  {
    if (this.m_WireframeRender)
    {
      this.m_WireframeRender = false;
      return true;
    }
    this.m_WireframeRender = true;
    this.enabled = true;
    return true;
  }

  //private void UpdateUniversalInputManager()
  //{
  //  if ((Object) this.m_UniversalInputManager == (Object) null)
  //    this.m_UniversalInputManager = UniversalInputManager.Get();
  //  if ((Object) this.m_UniversalInputManager == (Object) null)
  //    return;
  //  this.m_UniversalInputManager.SetFullScreenEffectsCamera(this.m_Camera, this.isActive());
  //}
}
