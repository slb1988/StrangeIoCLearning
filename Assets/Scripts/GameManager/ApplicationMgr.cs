using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class ApplicationMgr : MonoBehaviour {

    public static readonly PlatformDependentValue<bool> CanQuitGame = new PlatformDependentValue<bool>(PlatformCategory.OS)
    {
        PC = true,
        Mac = true,
        Android = false,
        iOS = false
    };
    public static readonly PlatformDependentValue<bool> AllowResetFromFatalError = new PlatformDependentValue<bool>(PlatformCategory.OS)
    {
        PC = false,
        Mac = false,
        Android = true,
        iOS = true
    };

    private static ApplicationMgr s_instance;
    private static ApplicationMode s_mode;

    public event System.Action Resetting;
    private bool m_resetting;
    private float m_lastResetTime;

    public event System.Action Paused;

    public event System.Action Unpaused;
    public static ApplicationMgr Get()
    {
        return ApplicationMgr.s_instance;
    }

    void Awake()
    {
        ApplicationMgr.s_instance = this;
        this.Initialize();
    }
    public float LastResetTime()
    {
        return this.m_lastResetTime;
    }
    public void UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }


    private void Initialize()
    {
        InitializeUnity();
        InitializeWindowTitle();
        //this.Resetting += new System.Action(GameStrings.OnReset);
    }
    
    private void InitializeUnity()
    {
        Application.runInBackground = true;
        Application.targetFrameRate = 30;
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    private void InitializeGame()
    {

    }

    public void Exit()
    {

    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (UnityEngine.Time.frameCount == 0)
            return;
        if (pauseStatus)
        {
        }
        else
        {
            this.ResetImmediately(false);

        }
    }
    private void ResetImmediately(bool forceLogin)
    {
        Logger logger = Log.Reset;
        this.m_resetting = true;
        this.m_lastResetTime = UnityEngine.Time.realtimeSinceStartup;

        if (this.Resetting != null)
            this.Resetting();
        this.m_resetting = false;
        Log.Reset.Print("\tApplicationMgr.ResetImmediately completed");
    }

    [DllImport("user32.dll")]
    public static extern int SetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string text);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string className, string windowName);

    private void InitializeWindowTitle()
    {
        IntPtr window = ApplicationMgr.FindWindow((string)null, "StrangeIoCLearning");
        if (!(window != IntPtr.Zero))
            return;
        ApplicationMgr.SetWindowTextW(window, "暗黑奇迹");//GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
    }

}
