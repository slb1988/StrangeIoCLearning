// Decompiled with JetBrains decompiler
// Type: LoadingScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public float m_FadeOutSec = 1f;
    public iTween.EaseType m_FadeOutEaseType = iTween.EaseType.linear;
    public float m_FadeInSec = 1f;
    public iTween.EaseType m_FadeInEaseType = iTween.EaseType.linear;
    private LoadingScreen.TransitionParams m_transitionParams = new LoadingScreen.TransitionParams();
    private LoadingScreen.TransitionUnfriendlyData m_transitionUnfriendlyData = new LoadingScreen.TransitionUnfriendlyData();
    private List<LoadingScreen.PreviousSceneDestroyedListener> m_prevSceneDestroyedListeners = new List<LoadingScreen.PreviousSceneDestroyedListener>();
    private List<LoadingScreen.FinishedTransitionListener> m_finishedTransitionListeners = new List<LoadingScreen.FinishedTransitionListener>();
    private List<GameStringCategory> m_stringCategoriesToUnload = new List<GameStringCategory>();
    private const float MIDDLE_OF_NOWHERE_X = 5000f;
    private static LoadingScreen s_instance;
    private LoadingScreen.Phase m_phase;
    private bool m_previousSceneActive;
    private LoadingScreen.TransitionParams m_prevTransitionParams;
    private Camera m_fxCamera;
    private float m_originalPosX;
    private long m_assetLoadStartTimestamp;
    private long m_assetLoadEndTimestamp;
    private long m_assetLoadNextStartTimestamp;

    private void Awake()
    {
        LoadingScreen.s_instance = this;
        this.InitializeFxCamera();
        ApplicationMgr.Get().Resetting += new System.Action(this.OnReset);
    }

    private void OnDestroy()
    {
        ApplicationMgr.Get().Resetting -= new System.Action(this.OnReset);
        LoadingScreen.s_instance = (LoadingScreen)null;
    }

    private void Start()
    {
        FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
        this.RegisterSceneListeners();
    }

    public static LoadingScreen Get()
    {
        return LoadingScreen.s_instance;
    }

    public Camera GetFxCamera()
    {
        return this.m_fxCamera;
    }

    public void RegisterSceneListeners()
    {
        SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
        SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
        SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
    }

    public void UnregisterSceneListeners()
    {
        SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
        SceneMgr.Get().UnregisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
        SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
    }

    public static bool DoesShowLoadingScreen(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode)
    {
        return prevMode == SceneMgr.Mode.GAMEPLAY || nextMode == SceneMgr.Mode.GAMEPLAY;
    }

    private void OnReset()
    {
        //FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
    }

    public LoadingScreen.Phase GetPhase()
    {
        return this.m_phase;
    }

    public bool IsTransitioning()
    {
        return this.m_phase != LoadingScreen.Phase.INVALID;
    }

    public bool IsWaiting()
    {
        switch (this.m_phase)
        {
            case LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD:
            case LoadingScreen.Phase.WAITING_FOR_SCENE_LOAD:
            case LoadingScreen.Phase.WAITING_FOR_BLOCKERS:
                return true;
            default:
                return false;
        }
    }

    public bool IsFadingOut()
    {
        return this.m_phase == LoadingScreen.Phase.FADING_OUT;
    }

    public bool IsFadingIn()
    {
        return this.m_phase == LoadingScreen.Phase.FADING_IN;
    }

    public bool IsFading()
    {
        return this.IsFadingOut() || this.IsFadingIn();
    }

    public bool IsPreviousSceneActive()
    {
        return this.m_previousSceneActive;
    }

    public bool IsTransitionEnabled()
    {
        return this.m_transitionParams.IsEnabled();
    }

    public void EnableTransition(bool enable)
    {
        this.m_transitionParams.Enable(enable);
    }

    public void AddTransitionObject(GameObject go)
    {
        this.m_transitionParams.AddObject(go);
    }

    public void AddTransitionObject(Component c)
    {
        this.m_transitionParams.AddObject(c.gameObject);
    }

    public void AddTransitionBlocker()
    {
        this.m_transitionParams.AddBlocker();
    }

    public void AddTransitionBlocker(int count)
    {
        this.m_transitionParams.AddBlocker(count);
    }

    public Camera GetFreezeFrameCamera()
    {
        return this.m_transitionParams.GetFreezeFrameCamera();
    }

    public void SetFreezeFrameCamera(Camera camera)
    {
        this.m_transitionParams.SetFreezeFrameCamera(camera);
    }

    public AudioListener GetTransitionAudioListener()
    {
        return this.m_transitionParams.GetAudioListener();
    }

    public void EnableFadeOut(bool enable)
    {
        this.m_transitionParams.EnableFadeOut(enable);
    }

    public void EnableFadeIn(bool enable)
    {
        this.m_transitionParams.EnableFadeIn(enable);
    }

    public Color GetFadeColor()
    {
        return this.m_transitionParams.GetFadeColor();
    }

    public void SetFadeColor(Color color)
    {
        this.m_transitionParams.SetFadeColor(color);
    }

    public void NotifyTransitionBlockerComplete()
    {
        if (this.m_prevTransitionParams == null)
            return;
        this.m_prevTransitionParams.RemoveBlocker();
        this.TransitionIfPossible();
    }

    public void NotifyTransitionBlockerComplete(int count)
    {
        if (this.m_prevTransitionParams == null)
            return;
        this.m_prevTransitionParams.RemoveBlocker(count);
        this.TransitionIfPossible();
    }

    public void NotifyMainSceneObjectAwoke(GameObject mainObject)
    {
        if (!this.IsPreviousSceneActive())
            return;
        this.DisableTransitionUnfriendlyStuff(mainObject);
    }

    public long GetAssetLoadStartTimestamp()
    {
        return this.m_assetLoadStartTimestamp;
    }


    public void AddStringsToUnload(GameStringCategory category)
    {
        this.m_stringCategoriesToUnload.Add(category);
    }

    public bool IsUnloadingStrings(GameStringCategory category)
    {
        return this.m_stringCategoriesToUnload.Contains(category);
    }

    public void RemoveStringsToUnload(GameStringCategory category)
    {
        this.m_stringCategoriesToUnload.Remove(category);
    }

    public bool RegisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback)
    {
        return this.RegisterPreviousSceneDestroyedListener(callback, (object)null);
    }

    public bool RegisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback, object userData)
    {
        LoadingScreen.PreviousSceneDestroyedListener destroyedListener = new LoadingScreen.PreviousSceneDestroyedListener();
        destroyedListener.SetCallback(callback);
        destroyedListener.SetUserData(userData);
        if (this.m_prevSceneDestroyedListeners.Contains(destroyedListener))
            return false;
        this.m_prevSceneDestroyedListeners.Add(destroyedListener);
        return true;
    }

    public bool UnregisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback)
    {
        return this.UnregisterPreviousSceneDestroyedListener(callback, (object)null);
    }

    public bool UnregisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback, object userData)
    {
        LoadingScreen.PreviousSceneDestroyedListener destroyedListener = new LoadingScreen.PreviousSceneDestroyedListener();
        destroyedListener.SetCallback(callback);
        destroyedListener.SetUserData(userData);
        return this.m_prevSceneDestroyedListeners.Remove(destroyedListener);
    }

    public bool RegisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback)
    {
        return this.RegisterFinishedTransitionListener(callback, (object)null);
    }

    public bool RegisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback, object userData)
    {
        //LoadingScreen.FinishedTransitionListener transitionListener = new LoadingScreen.FinishedTransitionListener();
        //transitionListener.SetCallback(callback);
        //transitionListener.SetUserData(userData);
        //if (this.m_finishedTransitionListeners.Contains(transitionListener))
        //  return false;
        //this.m_finishedTransitionListeners.Add(transitionListener);
        return true;
    }

    public bool UnregisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback)
    {
        return this.UnregisterFinishedTransitionListener(callback, (object)null);
    }

    public bool UnregisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback, object userData)
    {
        LoadingScreen.FinishedTransitionListener transitionListener = new LoadingScreen.FinishedTransitionListener();
        transitionListener.SetCallback(callback);
        transitionListener.SetUserData(userData);
        return this.m_finishedTransitionListeners.Remove(transitionListener);
    }

    private void OnFatalError(FatalErrorMessage message, object userData)
    {
        FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
        this.EnableTransition(false);
    }

    private void OnScenePreUnload(SceneMgr.Mode prevMode, Scene prevScene, object userData)
    {
        Logger logger = Log.LoadingScreen;
        string format = "LoadingScreen.OnScenePreUnload() - prevMode={0} nextMode={1} m_phase={2}";
    }

    private void OnSceneUnloaded(SceneMgr.Mode prevMode, Scene prevScene, object userData)
    {
        Log.LoadingScreen.Print("LoadingScreen.OnSceneUnloaded() - prevMode={0} nextMode={1} m_phase={2}", new object[]
		{
			prevMode,
			SceneMgr.Get().GetMode(),
			this.m_phase
		});
        if (this.m_phase != LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD)
        {
            return;
        }
        this.m_assetLoadEndTimestamp = this.m_assetLoadNextStartTimestamp;
        Log.LoadingScreen.Print("LoadingScreen.OnSceneUnloaded() - m_assetLoadEndTimestamp={0}", new object[]
		{
			this.m_assetLoadEndTimestamp
		});
        this.m_phase = LoadingScreen.Phase.WAITING_FOR_SCENE_LOAD;
        this.m_prevTransitionParams = this.m_transitionParams;
        this.m_transitionParams = new LoadingScreen.TransitionParams();
        this.m_transitionParams.ClearPreviousAssets = (prevMode != SceneMgr.Get().GetMode());
        this.m_prevTransitionParams.AutoAddObjects();
        this.m_prevTransitionParams.FixupCameras(this.m_fxCamera);
        this.m_prevTransitionParams.PreserveObjects(base.transform);
        this.m_originalPosX = base.transform.position.x;
        TransformUtil.SetPosX(base.gameObject, 5000f);
    }

    void OnGUI()
    {
        if (GUILayout.Button("hahaha"))
        {
            OnSceneUnloaded(SceneMgr.Mode.GAMEPLAY, SceneMgr.Get().GetScene(), null);

        }
    }

    private void OnSceneLoaded(SceneMgr.Mode mode, Scene scene, object userData)
    {
    }

    private bool TransitionIfPossible()
    {
        if (this.m_prevTransitionParams.GetBlockerCount() > 0)
            return false;
        this.StartCoroutine("HackWaitThenStartTransitionEffects");
        return true;
    }

    [DebuggerHidden]
    private IEnumerator HackWaitThenStartTransitionEffects()
    {
        yield return null;
    }

    private void FirePreviousSceneDestroyedListeners()
    {
        foreach (LoadingScreen.PreviousSceneDestroyedListener destroyedListener in this.m_prevSceneDestroyedListeners.ToArray())
            destroyedListener.Fire();
    }

    private void FireFinishedTransitionListeners(bool cutoff)
    {
        foreach (LoadingScreen.FinishedTransitionListener transitionListener in this.m_finishedTransitionListeners.ToArray())
            transitionListener.Fire(cutoff);
    }

    private void FadeOut()
    {
        Log.LoadingScreen.Print("LoadingScreen.FadeOut()", new object[0]);
        this.m_phase = LoadingScreen.Phase.FADING_OUT;
        if (!this.m_prevTransitionParams.IsFadeOutEnabled())
        {
            this.OnFadeOutComplete();
            return;
        }
        CameraFade cameraFade = base.GetComponent<CameraFade>();
        if (cameraFade == null)
        {
            UnityEngine.Debug.LogError("LoadingScreen FadeOut(): Failed to find CameraFade component");
            return;
        }
        cameraFade.m_Color = this.m_prevTransitionParams.GetFadeColor();
        Action<object> action = delegate(object amount)
        {
            cameraFade.m_Fade = (float)amount;
        };
        Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeOutSec,
			"from",
			cameraFade.m_Fade,
			"to",
			1f,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"OnFadeOutComplete",
			"oncompletetarget",
			base.gameObject,
			"name",
			"Fade"
		});
        iTween.ValueTo(base.gameObject, args);
    }

    private void OnFadeOutComplete()
    {
        Log.LoadingScreen.Print("LoadingScreen.OnFadeOutComplete()");
        this.FinishPreviousScene();
        this.FadeIn();
    }
    CameraFade cameraFade;
    private void FadeIn()
    {
        Log.LoadingScreen.Print("LoadingScreen.FadeIn()", new object[0]);
        this.m_phase = LoadingScreen.Phase.FADING_IN;
        if (!this.m_prevTransitionParams.IsFadeInEnabled())
        {
            this.OnFadeInComplete();
            return;
        }
        CameraFade cameraFade = base.GetComponent<CameraFade>();
        if (cameraFade == null)
        {
            UnityEngine.Debug.LogError("LoadingScreen FadeIn(): Failed to find CameraFade component");
            return;
        }
        cameraFade.m_Color = this.m_prevTransitionParams.GetFadeColor();
        Action<object> action = delegate(object amount)
        {
            cameraFade.m_Fade = (float)amount;
        };
        action(1f);
        Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInSec,
			"from",
			1f,
			"to",
			0f,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"OnFadeInComplete",
			"oncompletetarget",
			base.gameObject,
			"name",
			"Fade"
		});
        iTween.ValueTo(base.gameObject, args);
    }

    private void OnFadeInComplete()
    {
        Log.LoadingScreen.Print("LoadingScreen.OnFadeInComplete()");
        this.FinishFxCamera();
        this.m_prevTransitionParams = (LoadingScreen.TransitionParams)null;
        this.m_phase = LoadingScreen.Phase.INVALID;
        this.FireFinishedTransitionListeners(false);
    }

    private void InitializeFxCamera()
    {
        this.m_fxCamera = this.GetComponent<Camera>();
    }

    private void FinishFxCamera()
    {
    }

    //private FullScreenEffects GetFullScreenEffects(Camera camera)
    //{
    //  FullScreenEffects component = camera.GetComponent<FullScreenEffects>();
    //  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
    //    return component;
    //  return camera.gameObject.AddComponent<FullScreenEffects>();
    //}

    //private void ShowFreezeFrame(Camera camera)
    //{
    //  if ((UnityEngine.Object) camera == (UnityEngine.Object) null)
    //    return;
    //  this.GetFullScreenEffects(camera).Freeze();
    //}

    private void CutoffTransition()
    {
        if (!this.IsTransitioning())
        {
            this.m_transitionParams = new LoadingScreen.TransitionParams();
        }
        else
        {
            this.StopFading();
            this.FinishPreviousScene();
            this.FinishFxCamera();
            this.m_prevTransitionParams = (LoadingScreen.TransitionParams)null;
            this.m_transitionParams = new LoadingScreen.TransitionParams();
            this.m_phase = LoadingScreen.Phase.INVALID;
            this.FireFinishedTransitionListeners(true);
        }
    }

    private void StopFading()
    {
        iTween.Stop(this.gameObject);
    }

    private void DoInterruptionCleanUp()
    {
    }

    private void FinishPreviousScene()
    {
        Log.LoadingScreen.Print("LoadingScreen.FinishPreviousScene()");
        if (this.m_prevTransitionParams != null)
        {
            this.m_prevTransitionParams.DestroyObjects();
            //      TransformUtil.SetPosX(this.gameObject, this.m_originalPosX);
        }
        if (this.m_transitionParams.ClearPreviousAssets)
            this.ClearPreviousSceneAssets();
        this.m_transitionUnfriendlyData.Restore();
        this.m_transitionUnfriendlyData.Clear();
        this.m_previousSceneActive = false;
        this.FirePreviousSceneDestroyedListeners();
    }

    private void ClearPreviousSceneAssets()
    {
    }

    private void ClearAssets(long startTimestamp, long endTimestamp)
    {
    }

    private void DisableTransitionUnfriendlyStuff(GameObject mainObject)
    {
    }

    public enum Phase
    {
        INVALID,
        WAITING_FOR_SCENE_UNLOAD,
        WAITING_FOR_SCENE_LOAD,
        WAITING_FOR_BLOCKERS,
        FADING_OUT,
        FADING_IN,
    }

    private class PreviousSceneDestroyedListener : EventListener<LoadingScreen.PreviousSceneDestroyedCallback>
    {
        public void Fire()
        {
            this.m_callback(this.m_userData);
        }
    }

    private class FinishedTransitionListener : EventListener<LoadingScreen.FinishedTransitionCallback>
    {
        public void Fire(bool cutoff)
        {
            this.m_callback(cutoff, this.m_userData);
        }
    }

    private class TransitionParams
    {
        private bool m_enabled = true;
        private List<GameObject> m_objects = new List<GameObject>();
        private List<Camera> m_cameras = new List<Camera>();
        private List<Light> m_lights = new List<Light>();
        private bool m_fadeOut = true;
        private bool m_fadeIn = true;
        private Color m_fadeColor = Color.black;
        private bool m_clearPreviousAssets = true;
        private Camera m_freezeFrameCamera;
        private AudioListener m_audioListener;
        private int m_blockerCount;

        public bool ClearPreviousAssets
        {
            get
            {
                return this.m_clearPreviousAssets;
            }
            set
            {
                this.m_clearPreviousAssets = value;
            }
        }

        public bool IsEnabled()
        {
            return this.m_enabled;
        }

        public void Enable(bool enable)
        {
            this.m_enabled = enable;
        }

        public void AddObject(Component c)
        {
            if ((UnityEngine.Object)c == (UnityEngine.Object)null)
                return;
            this.AddObject(c.gameObject);
        }

        public void AddObject(GameObject go)
        {
            if ((UnityEngine.Object)go == (UnityEngine.Object)null)
                return;
            for (Transform transform = go.transform; (UnityEngine.Object)transform != (UnityEngine.Object)null; transform = transform.parent)
            {
                if (this.m_objects.Contains(transform.gameObject))
                    return;
            }
            foreach (Camera camera in go.GetComponentsInChildren<Camera>())
            {
                if (!this.m_cameras.Contains(camera))
                    this.m_cameras.Add(camera);
            }
            this.m_objects.Add(go);
        }

        public void AddBlocker()
        {
            ++this.m_blockerCount;
        }

        public void AddBlocker(int count)
        {
            this.m_blockerCount += count;
        }

        public void RemoveBlocker()
        {
            --this.m_blockerCount;
        }

        public void RemoveBlocker(int count)
        {
            this.m_blockerCount -= count;
        }

        public int GetBlockerCount()
        {
            return this.m_blockerCount;
        }

        public void SetFreezeFrameCamera(Camera camera)
        {
            if ((UnityEngine.Object)camera == (UnityEngine.Object)null)
                return;
            this.m_freezeFrameCamera = camera;
            this.AddObject(camera.gameObject);
        }

        public Camera GetFreezeFrameCamera()
        {
            return this.m_freezeFrameCamera;
        }

        public AudioListener GetAudioListener()
        {
            return this.m_audioListener;
        }

        public void SetAudioListener(AudioListener listener)
        {
            if ((UnityEngine.Object)listener == (UnityEngine.Object)null)
                return;
            this.m_audioListener = listener;
            this.AddObject((Component)listener);
        }

        public void EnableFadeOut(bool enable)
        {
            this.m_fadeOut = enable;
        }

        public bool IsFadeOutEnabled()
        {
            return this.m_fadeOut;
        }

        public void EnableFadeIn(bool enable)
        {
            this.m_fadeIn = enable;
        }

        public bool IsFadeInEnabled()
        {
            return this.m_fadeIn;
        }

        public void SetFadeColor(Color color)
        {
            this.m_fadeColor = color;
        }

        public Color GetFadeColor()
        {
            return this.m_fadeColor;
        }

        public List<Camera> GetCameras()
        {
            return this.m_cameras;
        }

        public List<Light> GetLights()
        {
            return this.m_lights;
        }

        public void FixupCameras(Camera fxCamera)
        {
            if (this.m_cameras.Count == 0)
                return;
            Camera camera1 = this.m_cameras[0];
            camera1.tag = "Untagged";
            float depth = camera1.depth;
            for (int index = 1; index < this.m_cameras.Count; ++index)
            {
                Camera camera2 = this.m_cameras[index];
                camera2.tag = "Untagged";
                if ((double)camera2.depth > (double)depth)
                    depth = camera2.depth;
            }
            float num = fxCamera.depth - 1f - depth;
            for (int index = 0; index < this.m_cameras.Count; ++index)
                this.m_cameras[index].depth += num;
        }

        public void AutoAddObjects()
        {
            foreach (Light light in (Light[])UnityEngine.Object.FindObjectsOfType(typeof(Light)))
            {
                this.AddObject(light.gameObject);
                this.m_lights.Add(light);
            }
        }

        public void PreserveObjects(Transform parent)
        {
            using (List<GameObject>.Enumerator enumerator = this.m_objects.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    enumerator.Current.transform.parent = parent;
            }
        }

        public void DestroyObjects()
        {
            using (List<GameObject>.Enumerator enumerator = this.m_objects.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    UnityEngine.Object.DestroyImmediate((UnityEngine.Object)enumerator.Current);
            }
        }
    }

    private class TransitionUnfriendlyData
    {
        private List<Light> m_lights = new List<Light>();
        private AudioListener m_audioListener;

        public void Clear()
        {
            this.m_audioListener = (AudioListener)null;
            this.m_lights.Clear();
        }

        public AudioListener GetAudioListener()
        {
            return this.m_audioListener;
        }

        public void SetAudioListener(AudioListener listener)
        {
            if ((UnityEngine.Object)listener == (UnityEngine.Object)null || !listener.enabled)
                return;
            this.m_audioListener = listener;
            this.m_audioListener.enabled = false;
        }

        public List<Light> GetLights()
        {
            return this.m_lights;
        }

        public void AddLights(Light[] lights)
        {
            foreach (Light light in lights)
            {
                if (light.enabled)
                {
                    light.enabled = false;
                    Transform transform = light.transform;
                    while ((UnityEngine.Object)transform.parent != (UnityEngine.Object)null)
                        transform = transform.parent;
                    this.m_lights.Add(light);
                }
            }
        }

        public void Restore()
        {
            for (int index = 0; index < this.m_lights.Count; ++index)
            {
                Light light = this.m_lights[index];
                if ((UnityEngine.Object)light == (UnityEngine.Object)null)
                {
                    UnityEngine.Debug.LogError((object)string.Format("TransitionUnfriendlyData.Restore() - light {0} is null!", (object)index));
                }
                else
                {
                    Transform transform = light.transform;
                    while ((UnityEngine.Object)transform.parent != (UnityEngine.Object)null)
                        transform = transform.parent;
                    light.enabled = true;
                }
            }
            if (!((UnityEngine.Object)this.m_audioListener != (UnityEngine.Object)null))
                return;
            this.m_audioListener.enabled = true;
        }
    }

    public delegate void PreviousSceneDestroyedCallback(object userData);

    public delegate void FinishedTransitionCallback(bool cutoff, object userData);
}
