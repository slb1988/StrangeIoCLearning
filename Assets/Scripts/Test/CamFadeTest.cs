using UnityEngine;
using System.Collections;

public class CamFadeTest : MonoBehaviour {
    //[SerialField]
    public CameraFade camFade;

    public CameraShaker camShaker;
	// Use this for initialization
	void Start () {
        
        //camFade = GetComponent<CameraFade>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))
        {
            camFade.m_Color = new Color(1, 0, 0);
            camFade.m_Fade = 0;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            camFade.m_Color = new Color(0, 1, 0);
            camFade.m_Fade = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            camShaker.StartShake();
        }
	}
}
