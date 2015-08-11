using UnityEngine;
using System.Collections;

public class LoginScene : Scene {

    public static LoginScene s_instance;

    protected override void Awake()
    {
        base.Awake();

        s_instance = this;
    }
	// Use this for initialization
	void Start () {

        SceneMgr.Get().NotifySceneLoaded();
    }
    void OnGUI()
    {
        if (GUILayout.Button("StartLogin"))
        {
            Invoke("Login", 0.5f);
        }
    }
    void Login()
    {
        SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
    }

    void OnDestroy()
    {
        LoginScene.s_instance = null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
