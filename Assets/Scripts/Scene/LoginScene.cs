using UnityEngine;
using System.Collections;

public class LoginScene : Scene {

    public static LoginScene s_instance;

    protected override void Awake()
    {
        base.Awake();

        s_instance = null;
    }
	// Use this for initialization
	void Start () {
	
	}

    void OnDestroy()
    {
        LoginScene.s_instance = null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
