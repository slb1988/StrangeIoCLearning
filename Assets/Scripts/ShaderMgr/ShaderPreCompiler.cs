// Decompiled with JetBrains decompiler
// Type: ShaderPreCompiler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ShaderPreCompiler : MonoBehaviour
{
	private readonly string[] GOLDEN_UBER_KEYWORDS1 = new string[]
	{
		"FX3_ADDBLEND",
		"FX3_ALPHABLEND"
	};

	private readonly string[] GOLDEN_UBER_KEYWORDS2 = new string[]
	{
		"LAYER3",
		"FX3_FLOWMAP",
		"LAYER4"
	};

	private readonly Vector3[] MESH_VERTS = new Vector3[]
	{
		Vector3.zero,
		Vector3.zero,
		Vector3.zero
	};

	private readonly Vector2[] MESH_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f)
	};

	private readonly Vector3[] MESH_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	private readonly Vector4[] MESH_TANGENTS = new Vector4[]
	{
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(1f, 0f, 0f, 0f)
	};

	private readonly int[] MESH_TRIANGLES;

	public Shader m_GoldenUberShader;

	public Shader[] m_StartupCompileShaders;

	public Shader[] m_SceneChangeCompileShaders;

	protected static Map<string, Shader> s_shaderCache = new Map<string, Shader>();

	private bool SceneChangeShadersCompiled;

	private bool PremiumShadersCompiled;

	public ShaderPreCompiler()
	{
		int[] expr_19D = new int[3];
		expr_19D[0] = 2;
		expr_19D[1] = 1;
		this.MESH_TRIANGLES = expr_19D;
		//base..ctor();
	}

	private void Start()
	{
        //if (GraphicsManager.Get().RenderQualityLevel != GraphicsQuality.Low)
		{
			base.StartCoroutine(this.WarmupShaders(this.m_StartupCompileShaders));
		}
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.WarmupSceneChangeShader));
		this.AddShader(this.m_GoldenUberShader.name, this.m_GoldenUberShader);
		Shader[] startupCompileShaders = this.m_StartupCompileShaders;
		for (int i = 0; i < startupCompileShaders.Length; i++)
		{
			Shader shader = startupCompileShaders[i];
			this.AddShader(shader.name, shader);
		}
		Shader[] sceneChangeCompileShaders = this.m_SceneChangeCompileShaders;
		for (int j = 0; j < sceneChangeCompileShaders.Length; j++)
		{
			Shader shader2 = sceneChangeCompileShaders[j];
			this.AddShader(shader2.name, shader2);
		}
	}

	public static Shader GetShader(string shaderName)
	{
		Shader shader;
		if (ShaderPreCompiler.s_shaderCache.TryGetValue(shaderName, out shader))
		{
			return shader;
		}
		shader = Shader.Find(shaderName);
		if (shader != null)
		{
			ShaderPreCompiler.s_shaderCache.Add(shaderName, shader);
		}
		return shader;
	}

	private void AddShader(string shaderName, Shader shader)
	{
		if (ShaderPreCompiler.s_shaderCache.ContainsKey(shaderName))
		{
			return;
		}
		ShaderPreCompiler.s_shaderCache.Add(shaderName, shader);
	}

	private void WarmupSceneChangeShader(SceneMgr.Mode prevMode, Scene prevScene, object userData)
	{
		if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY
            || SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER
            || SceneMgr.Get().GetMode() == SceneMgr.Mode.TAVERN_BRAWL)
           // && global::Network.ShouldBeConnectedToAurora()
            )
		{
			base.StartCoroutine(this.WarmupGoldenUberShader());
			this.PremiumShadersCompiled = true;
		}
		if (prevMode != SceneMgr.Mode.HUB)
		{
			return;
		}
		if (this.SceneChangeShadersCompiled)
		{
			return;
		}
		this.SceneChangeShadersCompiled = true;
        //if (GraphicsManager.Get().RenderQualityLevel != GraphicsQuality.Low)
		{
			base.StartCoroutine(this.WarmupShaders(this.m_SceneChangeCompileShaders));
		}
		if (this.SceneChangeShadersCompiled && this.PremiumShadersCompiled)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.WarmupSceneChangeShader));
		}
	}

	[DebuggerHidden]
    private IEnumerator WarmupGoldenUberShader()
    {
        yield return null;
    //    ShaderPreCompiler.<WarmupGoldenUberShader>c__Iterator22B <WarmupGoldenUberShader>c__Iterator22B = new ShaderPreCompiler.<WarmupGoldenUberShader>c__Iterator22B();
    //    <WarmupGoldenUberShader>c__Iterator22B.<>f__this = this;
    //    return <WarmupGoldenUberShader>c__Iterator22B;
    }

    //[DebuggerHidden]
    private IEnumerator WarmupShaders(Shader[] shaders)
    {
        yield return null;
    //    ShaderPreCompiler.<WarmupShaders>c__Iterator22C <WarmupShaders>c__Iterator22C = new ShaderPreCompiler.<WarmupShaders>c__Iterator22C();
    //    <WarmupShaders>c__Iterator22C.shaders = shaders;
    //    <WarmupShaders>c__Iterator22C.<$>shaders = shaders;
    //    <WarmupShaders>c__Iterator22C.<>f__this = this;
    //    return <WarmupShaders>c__Iterator22C;
    }

	private GameObject CreateMesh(string name)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = name;
		gameObject.transform.parent = base.gameObject.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.vertices = this.MESH_VERTS;
		mesh.uv = this.MESH_UVS;
		mesh.normals = this.MESH_NORMALS;
		mesh.tangents = this.MESH_TANGENTS;
		mesh.triangles = this.MESH_TRIANGLES;
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		return gameObject;
	}

	private Material CreateMaterial(string name, Shader shader)
	{
		return new Material(shader)
		{
			name = name
		};
	}
}
