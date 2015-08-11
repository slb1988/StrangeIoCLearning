// Decompiled with JetBrains decompiler
// Type: CameraFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraFade : MonoBehaviour
{
  public Color m_Color = Color.black;
  public float m_Fade = 1f;
  private float m_CameraDepth = 14f;
  public bool m_RenderOverAll;
  private Texture2D m_TempTexture;
  private GameObject m_PlaneGameObject;
  private Material m_Material;
  private Camera m_Camera;

  private void Awake()
  {
    this.m_TempTexture = new Texture2D(1, 1);
    this.m_TempTexture.SetPixel(0, 0, Color.white);
    this.m_TempTexture.Apply();
    this.m_Camera = this.GetComponent<Camera>();
    if ((Object) this.m_Camera == (Object) null)
    {
      Debug.LogError((object) "CameraFade faild to find camera component!");
      this.enabled = false;
    }
    this.m_CameraDepth = this.m_Camera.depth;
    this.SetupCamera();
  }

  private void Update()
  {
    if ((double) this.m_Fade <= 0.0)
    {
      if ((Object) this.renderer != (Object) null && this.renderer.enabled)
        this.renderer.enabled = false;
      if (!this.m_Camera.enabled)
        return;
      this.m_Camera.enabled = false;
    }
    else
    {
      if ((Object) this.renderer == (Object) null)
        this.CreateRenderPlane();
      if (!this.renderer.enabled)
        this.renderer.enabled = true;
      if (!this.m_Camera.enabled)
        this.m_Camera.enabled = true;
      if (this.m_RenderOverAll)
      {
        if ((double) this.m_Camera.depth < 100.0)
          this.m_Camera.depth = 100f;
      }
      else if ((double) this.m_Camera.depth != (double) this.m_CameraDepth)
        this.m_Camera.depth = this.m_CameraDepth;
      this.m_Material.color = new Color(this.m_Color.r, this.m_Color.g, this.m_Color.b, this.m_Fade);
    }
  }

  private void CreateRenderPlane()
  {
    this.gameObject.AddComponent<MeshFilter>();
    this.gameObject.AddComponent<MeshRenderer>();
    Mesh mesh1 = new Mesh();
    Mesh mesh2 = mesh1;
    Vector3[] vector3Array1 = new Vector3[4];
    int index1 = 0;
    vector3Array1[index1] = new Vector3(-10f, -10f, 0.0f);
    int index2 = 1;
    vector3Array1[index2] = new Vector3(10f, -10f, 0.0f);
    int index3 = 2;
    vector3Array1[index3] = new Vector3(-10f, 10f, 0.0f);
    int index4 = 3;
    vector3Array1[index4] = new Vector3(10f, 10f, 0.0f);
    mesh2.vertices = vector3Array1;
    Mesh mesh3 = mesh1;
    Color[] colorArray = new Color[4];
    int index5 = 0;
    colorArray[index5] = Color.white;
    int index6 = 1;
    colorArray[index6] = Color.white;
    int index7 = 2;
    colorArray[index7] = Color.white;
    int index8 = 3;
    colorArray[index8] = Color.white;
    mesh3.colors = colorArray;
    Mesh mesh4 = mesh1;
    Vector2[] vector2Array = new Vector2[4];
    int index9 = 0;
    vector2Array[index9] = new Vector2(0.0f, 0.0f);
    int index10 = 1;
    vector2Array[index10] = new Vector2(1f, 0.0f);
    int index11 = 2;
    vector2Array[index11] = new Vector2(0.0f, 1f);
    int index12 = 3;
    vector2Array[index12] = new Vector2(1f, 1f);
    mesh4.uv = vector2Array;
    Mesh mesh5 = mesh1;
    Vector3[] vector3Array2 = new Vector3[4];
    int index13 = 0;
    vector3Array2[index13] = Vector3.up;
    int index14 = 1;
    vector3Array2[index14] = Vector3.up;
    int index15 = 2;
    vector3Array2[index15] = Vector3.up;
    int index16 = 3;
    vector3Array2[index16] = Vector3.up;
    mesh5.normals = vector3Array2;
    mesh1.triangles = new int[6]
    {
      3,
      1,
      2,
      2,
      1,
      0
    };
    this.renderer.GetComponent<MeshFilter>().mesh = mesh1;
    //this.m_Material = new Material(ShaderUtils.FindShader("Hidden/CameraFade"));
    //this.renderer.sharedMaterial = this.m_Material;
  }

  private void SetupCamera()
  {
    this.m_Camera.farClipPlane = 1f;
    this.m_Camera.nearClipPlane = -1f;
    this.m_Camera.clearFlags = CameraClearFlags.Nothing;
    this.m_Camera.orthographicSize = 0.5f;
  }
}
