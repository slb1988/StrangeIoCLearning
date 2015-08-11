using UnityEngine;
using System.Collections;

public class HubScene : Scene
{

	// Use this for initialization
	void Start () {

        SceneMgr.Get().NotifySceneLoaded();
    }
    void OnGUI()
    {
        if (GUILayout.Button("Start Game"))
        {
            Invoke("StartGame", 0.5f);
        }
        if (GUILayout.Button("ReturnToLogin"))
        {
            SceneMgr.Get().SetNextMode(SceneMgr.Mode.LOGIN);
        }
    }

    void StartGame()
    {
        SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAMEPLAY);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
