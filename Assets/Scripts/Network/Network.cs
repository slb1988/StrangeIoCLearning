// Decompiled with JetBrains decompiler
// Type: Network
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

//using BobNetProto;
//using PegasusGame;
//using PegasusShared;
//using PegasusUtil;
//using SpectatorProto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public class Network
{
  private static Network s_instance = new Network();
  private static int s_numConnectionFailures = 0;
  public static readonly PlatformDependentValue<bool> LAUNCHES_WITH_BNET_APP = new PlatformDependentValue<bool>(PlatformCategory.OS)
  {
    PC = true,
    Mac = true,
    iOS = false,
    Android = false
  };
  public static readonly PlatformDependentValue<bool> TUTORIALS_WITHOUT_ACCOUNT = new PlatformDependentValue<bool>(PlatformCategory.OS)
  {
    PC = false,
    Mac = false,
    iOS = true,
    Android = true
  };
  private static bool s_running;

  public static bool IsRunning()
  {
    return Network.s_running;
  }

  public static void Initialize()
  {
    Network.s_running = true;
    //NetCache.Get().InitNetCache();
    //BattleNet.Init(ApplicationMgr.IsInternal());
  }

  public static void Reset()
  {
    Network.s_running = true;
  }

  public static void ApplicationPaused()
  {
  }

  public static void ApplicationUnpaused()
  {
  }

  public static void Heartbeat()
  {
    if (!Network.s_running)
      return;
    //NetCache.Get().Heartbeat();
    //ConnectAPI.Heartbeat();
    //StoreManager.Get().Heartbeat();
    //if (AchieveManager.Get() != null)
    //  AchieveManager.Get().Heartbeat();
    //TimeSpan span = DateTime.Now - Network.s_instance.lastCall;
    //if (span < Network.PROCESS_WARNING || DateTime.Now - Network.s_instance.lastCallReport < Network.PROCESS_WARNING_REPORT_GAP)
    //  return;
    //Network.s_instance.lastCallReport = DateTime.Now;
    //UnityEngine.Debug.LogWarning((object) string.Format("Network.ProcessNetwork not called for {0}", (object) TimeUtils.GetDevElapsedTimeString(span)));
  }

  public static void AppQuit()
  {
    if (!Network.s_running)
      return;
    //Network.TrackClient(Network.TrackLevel.LEVEL_INFO, Network.TrackWhat.TRACK_LOGOUT_STARTING);
    //Network.ConcedeIfReconnectDisabled();
    //Network.s_instance.CancelFindGame();
    //BattleNet.AppQuit();
    //PlayErrors.AppQuit();
    //BnetNearbyPlayerMgr.Get().Shutdown();
    //Network.s_running = false;
  }

  public static void AppAbort()
  {
    if (!Network.s_running)
      return;
    //Network.ConcedeIfReconnectDisabled();
    //Network.s_instance.CancelFindGame();
    //BattleNet.AppQuit();
    //PlayErrors.AppQuit();
    //BnetNearbyPlayerMgr.Get().Shutdown();
    //Network.s_running = false;
  }

  public static void ResetConnectionFailureCount()
  {
    Network.s_numConnectionFailures = 0;
  }


}
