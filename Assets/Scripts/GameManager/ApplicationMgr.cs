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
    private bool m_exiting;

    public event System.Action Resetting;
    private bool m_resetting;
    private float m_lastResetTime;
    private LinkedList<ApplicationMgr.SchedulerContext> m_schedulerContexts;

    public event System.Action Paused;

    public event System.Action Unpaused;
    public static ApplicationMgr Get()
    {
        return ApplicationMgr.s_instance;
    }
    public static ApplicationMode GetMode()
    {
        ApplicationMgr.InitializeMode();
        return ApplicationMgr.s_mode;
    }
    public static bool IsInternal()
    {
        return ApplicationMgr.GetMode() == ApplicationMode.INTERNAL;
    }

    public static bool IsPublic()
    {
        return ApplicationMgr.GetMode() == ApplicationMode.PUBLIC;
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

    public bool ScheduleCallback(float secondsToWait, bool realTime, ApplicationMgr.ScheduledCallback cb, object userData = null)
    {
        if (!GeneralUtils.IsCallbackValid((Delegate)cb))
            return false;
        if (this.m_schedulerContexts == null)
        {
            this.m_schedulerContexts = new LinkedList<ApplicationMgr.SchedulerContext>();
        }
        else
        {
            using (LinkedList<ApplicationMgr.SchedulerContext>.Enumerator enumerator = this.m_schedulerContexts.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ApplicationMgr.SchedulerContext current = enumerator.Current;
                    if (!((MulticastDelegate)current.m_callback != (MulticastDelegate)cb) && current.m_userData == userData)
                        return false;
                }
            }
        }
        ApplicationMgr.SchedulerContext schedulerContext = new ApplicationMgr.SchedulerContext();
        schedulerContext.m_startTime = UnityEngine.Time.realtimeSinceStartup;
        schedulerContext.m_secondsToWait = secondsToWait;
        schedulerContext.m_realTime = realTime;
        schedulerContext.m_callback = cb;
        schedulerContext.m_userData = userData;
        float num = schedulerContext.EstimateTargetTime();
        bool flag = false;
        for (LinkedListNode<ApplicationMgr.SchedulerContext> node = this.m_schedulerContexts.Last; node != null; node = node.Previous)
        {
            if ((double)node.Value.EstimateTargetTime() <= (double)num)
            {
                flag = true;
                this.m_schedulerContexts.AddAfter(node, schedulerContext);
                break;
            }
        }
        if (!flag)
            this.m_schedulerContexts.AddFirst(schedulerContext);
        return true;
    }

    public bool CancelScheduledCallback(ApplicationMgr.ScheduledCallback cb, object userData = null)
    {
        if (!GeneralUtils.IsCallbackValid((Delegate)cb) || this.m_schedulerContexts == null || this.m_schedulerContexts.Count == 0)
            return false;
        for (LinkedListNode<ApplicationMgr.SchedulerContext> node = this.m_schedulerContexts.First; node != null; node = node.Next)
        {
            ApplicationMgr.SchedulerContext schedulerContext = node.Value;
            if ((MulticastDelegate)schedulerContext.m_callback == (MulticastDelegate)cb && schedulerContext.m_userData == userData)
            {
                this.m_schedulerContexts.Remove(node);
                return true;
            }
        }
        return false;
    }

    private void ProcessScheduledCallbacks()
    {
        if (this.m_schedulerContexts == null || this.m_schedulerContexts.Count == 0)
            return;
        LinkedList<ApplicationMgr.SchedulerContext> linkedList = (LinkedList<ApplicationMgr.SchedulerContext>)null;
        float realtimeSinceStartup = UnityEngine.Time.realtimeSinceStartup;
        LinkedListNode<ApplicationMgr.SchedulerContext> node = this.m_schedulerContexts.First;
        while (node != null)
        {
            ApplicationMgr.SchedulerContext schedulerContext = node.Value;
            if (schedulerContext.m_realTime)
                schedulerContext.m_secondsWaited = realtimeSinceStartup - schedulerContext.m_startTime;
            else
                schedulerContext.m_secondsWaited += UnityEngine.Time.deltaTime;
            if ((double)schedulerContext.m_secondsWaited >= (double)schedulerContext.m_secondsToWait)
            {
                if (linkedList == null)
                    linkedList = new LinkedList<ApplicationMgr.SchedulerContext>();
                linkedList.AddLast(schedulerContext);
                LinkedListNode<ApplicationMgr.SchedulerContext> next = node.Next;
                this.m_schedulerContexts.Remove(node);
                node = next;
            }
            else if (!GeneralUtils.IsCallbackValid((Delegate)schedulerContext.m_callback))
            {
                LinkedListNode<ApplicationMgr.SchedulerContext> next = node.Next;
                this.m_schedulerContexts.Remove(node);
                node = next;
            }
            else
                node = node.Next;
        }
        if (linkedList == null)
            return;
        using (LinkedList<ApplicationMgr.SchedulerContext>.Enumerator enumerator = linkedList.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                ApplicationMgr.SchedulerContext current = enumerator.Current;
                current.m_callback(current.m_userData);
            }
        }
    }


    private void Initialize()
    {
        ApplicationMgr.InitializeMode();
        InitializeUnity();
        InitializeWindowTitle();
        //this.Resetting += new System.Action(GameStrings.OnReset);
    }

    private static void InitializeMode()
    {
        if (ApplicationMgr.s_mode != ApplicationMode.INVALID)
            return;
        ApplicationMgr.s_mode = ApplicationMode.PUBLIC;
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
        this.m_exiting = true;

        this.UnloadUnusedAssets();
    }
    public bool IsExiting()
    {
        return this.m_exiting;
    }


    void Update()
    {
        this.ProcessScheduledCallbacks();

    }

    private void OnGUI()
    {
        UnityEngine.Debug.developerConsoleVisible = false;
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

    private class SchedulerContext
    {
        public float m_startTime;
        public float m_secondsToWait;
        public bool m_realTime;
        public ApplicationMgr.ScheduledCallback m_callback;
        public object m_userData;
        public float m_secondsWaited;

        public float EstimateTargetTime()
        {
            return this.m_startTime + (!this.m_realTime ? this.m_secondsToWait * UnityEngine.Time.timeScale : this.m_secondsToWait);
        }
    }
    
    public delegate void ScheduledCallback(object userData);
}
