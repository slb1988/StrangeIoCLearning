using UnityEngine;

using System;
using System.ComponentModel;
using System.Collections.Generic;

public class SceneMgr : MonoBehaviour
{
    public enum Mode
    {
        INVALID,
        STARTUP,
        [Description("Login")]
        LOGIN,
        [Description("Hub")]
        HUB,
        [Description("Gameplay")]
        GAMEPLAY,
        [Description("CollectionManager")]
        COLLECTIONMANAGER,
        [Description("PackOpening")]
        PACKOPENING,
        [Description("Tournament")]
        TOURNAMENT,
        [Description("Friendly")]
        FRIENDLY,
        [Description("FatalError")]
        FATAL_ERROR,
        [Description("Draft")]
        DRAFT,
        [Description("Credits")]
        CREDITS,
        [Description("Reset")]
        RESET,
        [Description("Adventure")]
        ADVENTURE,
        [Description("TavernBrawl")]
        TAVERN_BRAWL,
    }
    private SceneMgr.Mode m_mode = SceneMgr.Mode.STARTUP;
    private List<SceneMgr.ScenePreUnloadListener> m_scenePreUnloadListeners = new List<SceneMgr.ScenePreUnloadListener>();
    private List<SceneMgr.SceneUnloadedListener> m_sceneUnloadedListeners = new List<SceneMgr.SceneUnloadedListener>();
    private List<SceneMgr.ScenePreLoadListener> m_scenePreLoadListeners = new List<SceneMgr.ScenePreLoadListener>();
    private List<SceneMgr.SceneLoadedListener> m_sceneLoadedListeners = new List<SceneMgr.SceneLoadedListener>();

    private static SceneMgr s_instance;
    private SceneMgr.Mode m_nextMode;
    private SceneMgr.Mode m_prevMode;
    private bool m_reloadMode;

    private Scene m_scene;
    private bool m_sceneLoaded;
    private bool m_transitioning;

    void Awake()
    {
        s_instance = this;
        this.m_transitioning = true;

    }
    // Use this for initialization
    void Start()
    {

    }

    void OnDestroy()
    {
        SceneMgr.s_instance = (SceneMgr)null;
    }

    public static SceneMgr Get()
    {
        return s_instance;
    }

    public void SetNextMode(SceneMgr.Mode mode)
    {
        if (this.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
            return;
        //this.CacheModeForResume(mode);
        this.m_nextMode = mode;
        this.m_reloadMode = false;
    }

    public void ReloadMode()
    {
        if (this.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
            return;
        this.m_nextMode = this.m_mode;
        this.m_reloadMode = true;
    }

    public SceneMgr.Mode GetPrevMode()
    {
        return this.m_prevMode;
    }

    public SceneMgr.Mode GetMode()
    {
        return this.m_mode;
    }

    public SceneMgr.Mode GetNextMode()
    {
        return this.m_nextMode;
    }

    public Scene GetScene()
    {
        return this.m_scene;
    }
    public void SetScene(Scene scene)
    {
        m_scene = scene;
    }

    public bool IsSceneLoaded()
    {
        return this.m_sceneLoaded;
    }

    public bool WillTransition()
    {
        return this.m_reloadMode || this.m_nextMode != SceneMgr.Mode.INVALID && this.m_nextMode != this.m_mode;
    }

    public bool IsTransitioning()
    {
        return this.m_transitioning;
    }

    public bool IsModeRequested(SceneMgr.Mode mode)
    {
        return this.m_mode == mode || this.m_nextMode == mode;
    }

    public bool IsInGame()
    {
        return this.IsModeRequested(SceneMgr.Mode.GAMEPLAY);
    }

    public void NotifySceneLoaded()
    {
        this.m_sceneLoaded = true;
        if (this.ShouldUseSceneLoadDelays())
        {
            //this.StartCoroutine(this.WaitThenFireSceneLoadedEvent());
        }
        else
            this.FireSceneLoadedEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.m_reloadMode)
        {
            if (this.m_nextMode == SceneMgr.Mode.INVALID)
                return;
            if (this.m_mode == this.m_nextMode)
            {
                this.m_nextMode = SceneMgr.Mode.INVALID;
                return;
            }
        }
        this.m_transitioning = true;

        this.m_prevMode = m_mode;
        this.m_mode = this.m_nextMode;
        this.m_nextMode = SceneMgr.Mode.INVALID;
        this.m_reloadMode = false;

        //if (this.m_scene != null)
        //{
        //    this.StopCoroutine("SwitchMode");
        //    this.StartCoroutine("SwitchMode");
        //}
        //else
            this.LoadMode();
    }

    //[DebuggerHidden]
    //private IEnumerator SwitchMode()
    //{
    //    return new SceneMgr.

    //}
    
    private bool ShouldUseSceneLoadDelays()
    {
        return this.m_mode != SceneMgr.Mode.LOGIN
            && this.m_mode != SceneMgr.Mode.HUB
            && this.m_mode != SceneMgr.Mode.FATAL_ERROR;
    }

    public void RegisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback)
    {
        this.RegisterScenePreUnloadEvent(callback, (object)null);
    }

    public void RegisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback, object userData)
    {
        SceneMgr.ScenePreUnloadListener preUnloadListener = new SceneMgr.ScenePreUnloadListener();
        preUnloadListener.SetCallback(callback);
        preUnloadListener.SetUserData(userData);
        if (this.m_scenePreUnloadListeners.Contains(preUnloadListener))
            return;
        this.m_scenePreUnloadListeners.Add(preUnloadListener);
    }

    public bool UnregisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback)
    {
        return this.UnregisterScenePreUnloadEvent(callback, (object)null);
    }

    public bool UnregisterScenePreUnloadEvent(SceneMgr.ScenePreUnloadCallback callback, object userData)
    {
        SceneMgr.ScenePreUnloadListener preUnloadListener = new SceneMgr.ScenePreUnloadListener();
        preUnloadListener.SetCallback(callback);
        preUnloadListener.SetUserData(userData);
        return this.m_scenePreUnloadListeners.Remove(preUnloadListener);
    }

    public void RegisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback)
    {
        this.RegisterSceneUnloadedEvent(callback, (object)null);
    }

    public void RegisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback, object userData)
    {
        SceneMgr.SceneUnloadedListener unloadedListener = new SceneMgr.SceneUnloadedListener();
        unloadedListener.SetCallback(callback);
        unloadedListener.SetUserData(userData);
        if (this.m_sceneUnloadedListeners.Contains(unloadedListener))
            return;
        this.m_sceneUnloadedListeners.Add(unloadedListener);
    }

    public bool UnregisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback)
    {
        return this.UnregisterSceneUnloadedEvent(callback, (object)null);
    }

    public bool UnregisterSceneUnloadedEvent(SceneMgr.SceneUnloadedCallback callback, object userData)
    {
        SceneMgr.SceneUnloadedListener unloadedListener = new SceneMgr.SceneUnloadedListener();
        unloadedListener.SetCallback(callback);
        unloadedListener.SetUserData(userData);
        return this.m_sceneUnloadedListeners.Remove(unloadedListener);
    }

    public void RegisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback)
    {
        this.RegisterScenePreLoadEvent(callback, (object)null);
    }

    public void RegisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback, object userData)
    {
        SceneMgr.ScenePreLoadListener scenePreLoadListener = new SceneMgr.ScenePreLoadListener();
        scenePreLoadListener.SetCallback(callback);
        scenePreLoadListener.SetUserData(userData);
        if (this.m_scenePreLoadListeners.Contains(scenePreLoadListener))
            return;
        this.m_scenePreLoadListeners.Add(scenePreLoadListener);
    }

    public bool UnregisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback)
    {
        return this.UnregisterScenePreLoadEvent(callback, (object)null);
    }

    public bool UnregisterScenePreLoadEvent(SceneMgr.ScenePreLoadCallback callback, object userData)
    {
        SceneMgr.ScenePreLoadListener scenePreLoadListener = new SceneMgr.ScenePreLoadListener();
        scenePreLoadListener.SetCallback(callback);
        scenePreLoadListener.SetUserData(userData);
        return this.m_scenePreLoadListeners.Remove(scenePreLoadListener);
    }

    public void RegisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback)
    {
        this.RegisterSceneLoadedEvent(callback, (object)null);
    }

    public void RegisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback, object userData)
    {
        SceneMgr.SceneLoadedListener sceneLoadedListener = new SceneMgr.SceneLoadedListener();
        sceneLoadedListener.SetCallback(callback);
        sceneLoadedListener.SetUserData(userData);
        if (this.m_sceneLoadedListeners.Contains(sceneLoadedListener))
            return;
        this.m_sceneLoadedListeners.Add(sceneLoadedListener);
    }

    public bool UnregisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback)
    {
        return this.UnregisterSceneLoadedEvent(callback, (object)null);
    }

    public bool UnregisterSceneLoadedEvent(SceneMgr.SceneLoadedCallback callback, object userData)
    {
        SceneMgr.SceneLoadedListener sceneLoadedListener = new SceneMgr.SceneLoadedListener();
        sceneLoadedListener.SetCallback(callback);
        sceneLoadedListener.SetUserData(userData);
        return this.m_sceneLoadedListeners.Remove(sceneLoadedListener);
    }

    private void FireScenePreUnloadEvent(Scene prevScene)
    {
        foreach (SceneMgr.ScenePreUnloadListener preUnloadListener in this.m_scenePreUnloadListeners.ToArray())
            preUnloadListener.Fire(this.m_prevMode, prevScene);
    }

    private void FireSceneUnloadedEvent(Scene prevScene)
    {
        foreach (SceneMgr.SceneUnloadedListener unloadedListener in this.m_sceneUnloadedListeners.ToArray())
            unloadedListener.Fire(this.m_prevMode, prevScene);
    }

    private void FireScenePreLoadEvent()
    {
        foreach (SceneMgr.ScenePreLoadListener scenePreLoadListener in this.m_scenePreLoadListeners.ToArray())
            scenePreLoadListener.Fire(this.m_prevMode, this.m_mode);
    }

    private void FireSceneLoadedEvent()
    {
        this.m_transitioning = false;
        foreach (SceneMgr.SceneLoadedListener sceneLoadedListener in this.m_sceneLoadedListeners.ToArray())
            sceneLoadedListener.Fire(this.m_mode, this.m_scene);
    }

    //private IEnumerator SwitchMode()
    //{
    //    return yield break;
    //    //return (IEnumerator)new SceneMgr.
    //}

    private void LoadMode()
    {
        this.FireScenePreLoadEvent();
        Application.LoadLevelAdditiveAsync(EnumUtils.GetString<SceneMgr.Mode>(this.m_mode));
    }

    private class ScenePreUnloadListener : EventListener<SceneMgr.ScenePreUnloadCallback>
    {
        public void Fire(SceneMgr.Mode prevMode, Scene prevScene)
        {
            this.m_callback(prevMode, prevScene, this.m_userData);
        }
    }

    private class SceneUnloadedListener : EventListener<SceneMgr.SceneUnloadedCallback>
    {
        public void Fire(SceneMgr.Mode prevMode, Scene prevScene)
        {
            this.m_callback(prevMode, prevScene, this.m_userData);
        }
    }

    private class ScenePreLoadListener : EventListener<SceneMgr.ScenePreLoadCallback>
    {
        public void Fire(SceneMgr.Mode prevMode, SceneMgr.Mode mode)
        {
            this.m_callback(prevMode, mode, this.m_userData);
        }
    }

    private class SceneLoadedListener : EventListener<SceneMgr.SceneLoadedCallback>
    {
        public void Fire(SceneMgr.Mode mode, Scene scene)
        {
            this.m_callback(mode, scene, this.m_userData);
        }
    }

    public delegate void ScenePreUnloadCallback(SceneMgr.Mode prevMode, Scene prevScene, object userData);

    public delegate void SceneUnloadedCallback(SceneMgr.Mode prevMode, Scene prevScene, object userData);

    public delegate void ScenePreLoadCallback(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData);

    public delegate void SceneLoadedCallback(SceneMgr.Mode mode, Scene scene, object userData);
}
