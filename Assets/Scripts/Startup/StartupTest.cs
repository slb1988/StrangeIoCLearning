using UnityEngine;
using System.Collections;

public class StartupTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {
        if (GUILayout.Button("Login"))
        {
            SceneMgr.Get().SetNextMode(SceneMgr.Mode.LOGIN);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
