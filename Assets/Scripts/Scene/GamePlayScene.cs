﻿using UnityEngine;
using System.Collections;

public class GamePlayScene : Scene
{

	// Use this for initialization
	void Start () {

        SceneMgr.Get().NotifySceneLoaded();
	}

    void OnGUI()
    {
        if (GUILayout.Button("ReturnToHub"))
        {
            SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
        }
        if (GUILayout.Button("Login"))
        {
            SceneMgr.Get().SetNextMode(SceneMgr.Mode.LOGIN);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
