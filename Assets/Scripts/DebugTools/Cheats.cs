// Decompiled with JetBrains decompiler
// Type: Cheats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Cheats
{
  private static Cheats s_instance;
  //private string m_board;
  //private bool m_loadingStoreChallengePrompt;
  //private AlertPopup m_alert;
  //private static bool s_hasSubscribedToPartyEvents;

  public static Cheats Get()
  {
    return Cheats.s_instance;
  }

  public static void Initialize()
  {
    Cheats.s_instance = new Cheats();
    Cheats.s_instance.InitializeImpl();
  }


  public bool IsLaunchingQuickGame()
  {
    return false;
  }

  public bool QuickGameSkipMulligan()
  {
    return false;
  }

  public bool QuickGameFlipHeroes()
  {
    return false;
  }

  public bool QuickGameMirrorHeroes()
  {
    return false;
  }

  public bool HandleKeyboardInput()
  {
    return false;
  }

  private void InitializeImpl()
  {
      CheatMgr cheatMgr = CheatMgr.Get();
      if (ApplicationMgr.IsInternal())
      {
          cheatMgr.RegisterCheatHandler("collectionfirstxp", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_collectionfirstxp), "Set the number of page and cover flips to zero", string.Empty, string.Empty);
          cheatMgr.RegisterCheatHandler("board", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_board), "Set which board will be loaded on the next game", "<BRM|STW|GVG>", "BRM");
          cheatMgr.RegisterCheatHandler("brode", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_brode), "Brode's personal cheat", string.Empty, string.Empty);
          cheatMgr.RegisterCheatHandler("resettips", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_resettips), "Resets Innkeeper tips for collection manager", string.Empty, string.Empty);
          cheatMgr.RegisterCheatHandler("questcomplete", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_questcomplete), "Shows the quest complete achievement screen", "<quest_id>", null);
          cheatMgr.RegisterCheatHandler("questprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_questprogress), "Pop up a quest progress toast", "<title> <description> <progress> <maxprogress>", "Hello World 3 10");
          cheatMgr.RegisterCheatHandler("questwelcome", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_questwelcome), "Open list of daily quests", "<fromLogin>", "true");
          cheatMgr.RegisterCheatHandler("newquest", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_newquest), "Shows a new quest, only usable while a quest popup is active", null, null);
          cheatMgr.RegisterCheatHandler("storepassword", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_storepassword), "Show store challenge popup", string.Empty, string.Empty);
          cheatMgr.RegisterCheatHandler("retire", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_retire), "Retires your draft deck", string.Empty, string.Empty);
          cheatMgr.RegisterCheatHandler("defaultcardback", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_defaultcardback), "Set your cardback as if through the options menu", "<cardback id>", null);
          cheatMgr.RegisterCheatHandler("disconnect", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_disconnect), "Disconnects you from a game in progress.", null, null);
          cheatMgr.RegisterCheatHandler("seasonroll", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_seasonroll), "Open the season end dialog", "<season number> <ending rank>", "14 3");
          cheatMgr.RegisterCheatHandler("playnullsound", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playnullsound), "Tell SoundManager to play a null sound.", null, null);
          cheatMgr.RegisterCheatHandler("spectate", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_spectate), "Connects to a game server to spectate", "<ip_address> <port> <game_handle> <spectator_password> [gameType] [missionId]", null);
          cheatMgr.RegisterCheatHandler("party", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_party), "Run a variety of party related commands", "[sub command] [subcommand args]", "list");
          cheatMgr.RegisterCheatHandler("cheat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_cheat), "Send a cheat command to the server", "<command> <arguments>", null);
          cheatMgr.RegisterCheatHandler("autohand", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_autohand), "Set whether PhoneUI automatically hides your hand after playing a card", "<true/false>", "true");
          cheatMgr.RegisterCheatHandler("fixedrewardcomplete", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_fixedrewardcomplete), "Shows the visual for a fixed reward", "<fixed_reward_map_id>", null);
          cheatMgr.RegisterCheatHandler("iks", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_iks), "Open InnKeepersSpecial with a custom url", "<url>", null);
          cheatMgr.RegisterCheatHandler("adventureChallengeUnlock", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_adventureChallengeUnlock), "Show adventure challenge unlock", "<wing number>", null);
          cheatMgr.RegisterCheatHandler("quote", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_quote), string.Empty, "<character> <line> [sound]", "Innkeeper VO_INNKEEPER_FORGE_COMPLETE_22 VO_INNKEEPER_ARENA_COMPLETE");
          cheatMgr.RegisterCheatHandler("favoritehero", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_favoritehero), "Change your favorite hero for a class (only works from CollectionManager)", "<class_id> <hero_card_id> <hero_premium>", null);
          cheatMgr.RegisterCheatHandler("help", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_help), "Get help for a specific command or list of commands", "<command name>", string.Empty);
          cheatMgr.RegisterCheatHandler("example", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_example), "Run an example of this command if one exists", "<command name>", null);
      }
      cheatMgr.RegisterCheatHandler("has", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_HasOption), "Query whether a Game Option exists.", null, null);
      cheatMgr.RegisterCheatHandler("get", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_GetOption), "Get the value of a Game Option.", null, null);
      cheatMgr.RegisterCheatHandler("set", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetOption), "Set the value of a Game Option.", null, null);
      cheatMgr.RegisterCheatHandler("delete", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_DeleteOption), "Delete a Game Option; the absence of option may trigger default behavior", null, null);
      cheatMgr.RegisterCheatHandler("warning", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_warning), "Show a warning message", "<message>", "Test You're a cheater and you've been warned!");
      cheatMgr.RegisterCheatHandler("fatal", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_fatal), "Brings up the Fatal Error screen", "<error to display>", "Hearthstone cheated and failed!");
      cheatMgr.RegisterCheatHandler("exit", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_exit), "Exit the application", string.Empty, string.Empty);
      cheatMgr.RegisterCheatHandler("log", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_log), null, null, null);
      cheatMgr.RegisterCheatHandler("alert", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_alert), "Show a popup alert", "header=<string> text=<string> icon=<bool> response=<ok|confirm|cancel|confirm_cancel> oktext=<string> confirmtext=<string>", "header=header text=body text icon=true response=confirm");
      //cheatMgr.RegisterCheatAlias("cheat", new string[]
      //  {
      //      "c"
      //  });
      cheatMgr.RegisterCheatAlias("delete", new string[]
		{
			"del"
		});
      cheatMgr.RegisterCheatAlias("alert", new string[]
		{
			"popup",
			"dialog"
		});
      cheatMgr.RegisterCheatAlias("exit", new string[]
		{
			"quit"
		});
  }

  private void ParseErrorText(string[] args, string rawArgs, out string header, out string message)
  {
    header = args.Length != 0 ? args[0] : "[PH] Header";
    if (args.Length <= 1)
    {
      message = "[PH] Message";
    }
    else
    {
      int startIndex = 0;
      bool flag = false;
      for (int index = 0; index < rawArgs.Length; ++index)
      {
        if (char.IsWhiteSpace(rawArgs[index]))
        {
          if (flag)
          {
            startIndex = index;
            break;
          }
        }
        else
          flag = true;
      }
      message = rawArgs.Substring(startIndex).Trim();
    }
  }

  private Map<string, string> ParseAlertArgs(string rawArgs)
  {
      Map<string, string> map = new Map<string, string>();
      int num = -1;
      string text = null;
      int num3;
      for (int i = 0; i < rawArgs.Length; i++)
      {
          char c = rawArgs[i];
          if (c == '=')
          {
              int num2 = -1;
              for (int j = i - 1; j >= 0; j--)
              {
                  char c2 = rawArgs[j];
                  char c3 = rawArgs[j + 1];
                  if (!char.IsWhiteSpace(c2))
                  {
                      num2 = j;
                  }
                  if (char.IsWhiteSpace(c2) && !char.IsWhiteSpace(c3))
                  {
                      break;
                  }
              }
              if (num2 >= 0)
              {
                  num3 = num2 - 2;
                  if (text != null)
                  {
                      map[text] = rawArgs.Substring(num, num3 - num + 1);
                  }
                  num = i + 1;
                  text = rawArgs.Substring(num2, i - num2).Trim().ToLowerInvariant();
              }
          }
      }
      num3 = rawArgs.Length - 1;
      if (text != null)
      {
          map[text] = rawArgs.Substring(num, num3 - num + 1);
      }
      return map;
  }

  //private bool OnAlertProcessed(DialogBase dialog, object userData)
  //{
  //  this.m_alert = (AlertPopup) dialog;
  //  return true;
  //}

  //private void OnAlertResponse(AlertPopup.Response response, object userData)
  //{
  //    this.m_alert = (AlertPopup)null;
  //}

  private bool OnProcessCheat_HasOption(string func, string[] args, string rawArgs)
  {
    return true;
  }

  private bool OnProcessCheat_GetOption(string func, string[] args, string rawArgs)
  {
    return true;
  }

  private bool OnProcessCheat_SetOption(string func, string[] args, string rawArgs)
  {
    return true;
  }

  private bool OnProcessCheat_DeleteOption(string func, string[] args, string rawArgs)
  {
    return true;
  }

  private bool OnProcessCheat_collectionfirstxp(string func, string[] args, string rawArgs)
  {
    //Options.Get().SetInt(Option.COVER_MOUSE_OVERS, 0);
    //Options.Get().SetInt(Option.PAGE_MOUSE_OVERS, 0);
    return true;
  }

  private bool OnProcessCheat_board(string func, string[] args, string rawArgs)
  {
//    this.m_board = args[0].ToUpperInvariant();
    return true;
  }

  private bool OnProcessCheat_resettips(string func, string[] args, string rawArgs)
  {
    //Options.Get().SetBool(Option.HAS_SEEN_COLLECTIONMANAGER, false);
    return true;
  }

  private bool OnProcessCheat_brode(string func, string[] args, string rawArgs)
  {
    //NotificationManager.Get().CreateInnkeeperQuote(new Vector3(427f, -666f, 0.0f), GameStrings.Get("VO_INNKEEPER_FORGE_1WIN"), "VO_INNKEEPER_ARENA_1WIN", 0.0f, (System.Action) null);
    return true;
  }

  private bool OnProcessCheat_questcomplete(string func, string[] args, string rawArgs)
  {
    //QuestToast.ShowQuestToast((QuestToast.DelOnCloseQuestToast) null, false, AchieveManager.Get().GetAchievement(int.Parse(rawArgs)));
    return true;
  }

  private bool OnProcessCheat_questwelcome(string func, string[] args, string rawArgs)
  {
    bool boolVal = false;
    if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
      GeneralUtils.TryParseBool(args[0], out boolVal);
    //WelcomeQuests.Show(boolVal, (WelcomeQuests.DelOnWelcomeQuestsClosed) null, false);
    return true;
  }

  private bool OnProcessCheat_newquest(string func, string[] args, string rawArgs)
  {
    //if ((UnityEngine.Object) WelcomeQuests.Get() == (UnityEngine.Object) null)
    //  return false;
    //WelcomeQuests.Get().GetFirstQuestTile().SetupTile(AchieveManager.Get().GetAchievement(int.Parse(rawArgs)));
    return true;
  }

  private bool OnProcessCheat_questprogress(string func, string[] args, string rawArgs)
  {
    if (args.Length != 4)
      return false;
    string questName = args[0];
    string questDescription = args[1];
    int progress = int.Parse(args[2]);
    int maxProgress = int.Parse(args[3]);
    //if (!((UnityEngine.Object) GameToastMgr.Get() != (UnityEngine.Object) null))
    //  return false;
    //GameToastMgr.Get().AddQuestProgressToast(questName, questDescription, progress, maxProgress);
    return true;
  }

  private bool OnProcessCheat_retire(string func, string[] args, string rawArgs)
  {
    return true;
  }

  private bool OnProcessCheat_storepassword(string func, string[] args, string rawArgs)
  {
    //if (this.m_loadingStoreChallengePrompt)
    //  return true;
    //if ((UnityEngine.Object) this.m_storeChallengePrompt == (UnityEngine.Object) null)
    //{
    //  this.m_loadingStoreChallengePrompt = true;
    //  AssetLoader.Get().LoadGameObject("StoreChallengePrompt", (AssetLoader.GameObjectCallback) ((name, go, callbackData) =>
    //  {
    //    this.m_loadingStoreChallengePrompt = false;
    //    this.m_storeChallengePrompt = go.GetComponent<StoreChallengePrompt>();
    //    long num2 = (long) this.m_storeChallengePrompt.Hide();
    //    this.m_storeChallengePrompt.Show(ChallengeType.PASSWORD, 0UL, false);
    //  }), (object) null, false);
    //}
    //else if (this.m_storeChallengePrompt.IsShown())
    //{
    //  long num1 = (long) this.m_storeChallengePrompt.Hide();
    //}
    //else
    //  this.m_storeChallengePrompt.Show(ChallengeType.PASSWORD, 0UL, false);
    return true;
  }

  private bool OnProcessCheat_defaultcardback(string func, string[] args, string rawArgs)
  {
    int result;
    if (args.Length == 0 || !int.TryParse(args[0].ToLowerInvariant(), out result))
      return false;
    //ConnectAPI.SetDefaultCardBack(result);
    return true;
  }

  private bool OnProcessCheat_disconnect(string func, string[] args, string rawArgs)
  {
    //if (!Network.IsConnectedToGameServer())
    //  return false;
    //Logger logger = Log.LoadingScreen;
    //string format = "Cheats.OnProcessCheat_disconnect() - reconnect={0}";
    //object[] objArray = new object[1];
    //int index = 0;
    //// ISSUE: variable of a boxed type
    //__Boxed<bool> local = (ValueType) (bool) (ReconnectMgr.Get().IsReconnectEnabled() ? 1 : 0);
    //objArray[index] = (object) local;
    //logger.Print(format, objArray);
    //if (ReconnectMgr.Get().IsReconnectEnabled())
    //  Network.DisconnectFromGameServer();
    //else
    //  Network.Concede();
    return true;
  }

  private bool OnProcessCheat_warning(string func, string[] args, string rawArgs)
  {
    string header;
    string message;
    this.ParseErrorText(args, rawArgs, out header, out message);
    //Error.AddWarning(header, message);
    return true;
  }

  private bool OnProcessCheat_fatal(string func, string[] args, string rawArgs)
  {
    //Error.AddFatal(rawArgs);
    return true;
  }

  private bool OnProcessCheat_suicide(string func, string[] args, string rawArgs)
  {
    string s = args[0].ToLowerInvariant();
    int result = 0;
    int.TryParse(s, out result);
    Application.CommitSuicide(result);
    return true;
  }

  private bool OnProcessCheat_exit(string func, string[] args, string rawArgs)
  {
    GeneralUtils.ExitApplication();
    return true;
  }

  private bool OnProcessCheat_log(string func, string[] args, string rawArgs)
  {
    string str = args[0].ToLowerInvariant();
    if (!(str == "load") && !(str == "reload"))
      return false;
    Log.Get().Load();
    return true;
  }

  private bool OnProcessCheat_alert(string func, string[] args, string rawArgs)
  {
    //AlertPopup.PopupInfo info = this.GenerateAlertInfo(rawArgs);
    //if ((UnityEngine.Object) this.m_alert == (UnityEngine.Object) null)
    //  DialogManager.Get().ShowPopup(info, new DialogManager.DialogProcessCallback(this.OnAlertProcessed));
    //else
    //  this.m_alert.UpdateInfo(info);
    return true;
  }

  private bool GetBonusStarsAndLevel(int lastSeasonRank, out int bonusStars, out int newSeasonRank)
  {
    int num1 = 26 - lastSeasonRank;
    bonusStars = num1 - 1;
    int num2 = 1;
    newSeasonRank = 26 - num2;
    int num3;
    switch (num1)
    {
      case 1:
        num3 = 1;
        break;
      case 2:
        num3 = 1;
        break;
      case 3:
        num3 = 1;
        break;
      case 4:
        num3 = 2;
        break;
      case 5:
        num3 = 2;
        break;
      case 6:
        num3 = 3;
        break;
      case 7:
        num3 = 3;
        break;
      case 8:
        num3 = 4;
        break;
      case 9:
        num3 = 4;
        break;
      case 10:
        num3 = 5;
        break;
      case 11:
        num3 = 5;
        break;
      case 12:
        num3 = 6;
        break;
      case 13:
        num3 = 6;
        break;
      case 14:
        num3 = 6;
        break;
      case 15:
        num3 = 7;
        break;
      case 16:
        num3 = 7;
        break;
      case 17:
        num3 = 7;
        break;
      case 18:
        num3 = 8;
        break;
      case 19:
        num3 = 8;
        break;
      case 20:
        num3 = 8;
        break;
      case 21:
        num3 = 9;
        break;
      case 22:
        num3 = 9;
        break;
      case 23:
        num3 = 9;
        break;
      case 24:
        num3 = 10;
        break;
      case 25:
        num3 = 10;
        break;
      case 26:
        num3 = 10;
        break;
      default:
        return false;
    }
    newSeasonRank = 26 - num3;
    return true;
  }

  private bool OnProcessCheat_seasonroll(string func, string[] args, string rawArgs)
  {
    return true;
  }

  private bool OnProcessCheat_playnullsound(string func, string[] args, string rawArgs)
  {
    //SoundManager.Get().Play((AudioSource) null);
    return true;
  }

  private bool OnProcessCheat_spectate(string func, string[] args, string rawArgs)
  {
    if (args.Length < 4 || Enumerable.Any<string>((IEnumerable<string>) args, (Func<string, bool>) (a => string.IsNullOrEmpty(a))))
    {
      //Error.AddWarning("Spectate Cheat Error", "spectate cheat must have the following args:\n\nspectate ipaddress port game_handle spectator_password [gameType] [missionId]");
      return false;
    }
    return true;
  }

  private bool OnProcessCheat_party(string func, string[] args, string rawArgs)
  {
      return false;
  }

  //private static string GetPartyInviteSummary(PartyInvite invite, int index)
  //{
  //  string format = "{0}: inviteId={1} sender={2} recipient={3} party={4}";
  //    return format;
  //}

  //private static string GetPartySummary(PartyInfo info, int index)
  //{
  //  string format = "{0}{1}: members={2} invites={3} privacy={4} leader={5}";
  //  return format;
  //}

  private bool OnProcessCheat_cheat(string func, string[] args, string rawArgs)
  {
    //Network.SendConsoleCmdToServer(rawArgs);
    return true;
  }

  private bool OnProcessCheat_autohand(string func, string[] args, string rawArgs)
  {
    //bool boolVal;
    //if (args.Length == 0 || !GeneralUtils.TryParseBool(args[0], out boolVal) || (UnityEngine.Object) InputManager.Get() == (UnityEngine.Object) null)
    //  return false;
    //string message = !boolVal ? "auto hand hiding is off" : "auto hand hiding is on";
    //Debug.Log((object) message);
    //UIStatus.Get().AddInfo(message);
    //InputManager.Get().SetHideHandAfterPlayingCard(boolVal);
    return true;
  }

  private bool OnProcessCheat_adventureChallengeUnlock(string func, string[] args, string rawArgs)
  {
    int result;
    if (args.Length < 1 || !int.TryParse(args[0].ToLowerInvariant(), out result))
      return false;
    //AdventureMissionDisplay.Get().ShowClassChallengeUnlock(new List<int>()
    //{
    //  result
    //});
    return true;
  }

  private bool OnProcessCheat_iks(string func, string[] args, string rawArgs)
  {
    if (args.Length < 1)
      return false;
    //InnKeepersSpecial.Get().adUrlOverride = args[0];
    //WelcomeQuests.Show(true, (WelcomeQuests.DelOnWelcomeQuestsClosed) null, false);
    return true;
  }

  private bool OnProcessCheat_quote(string func, string[] args, string rawArgs)
  {
    if (args.Length < 2)
      return false;
    string prefabName = args[0];
    string key = args[1];
    string soundName = key;
    if (args.Length > 2)
      soundName = args[2];
    if (prefabName.ToLowerInvariant().Contains("innkeeper"))
    {
      //NotificationManager.Get().CreateInnkeeperQuote(NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get(key), soundName, 0.0f, (System.Action) null);
    }
    else
    {
      if (!prefabName.EndsWith("_Quote"))
        prefabName += "_Quote";
      //NotificationManager.Get().CreateCharacterQuote(prefabName, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get(key), soundName, true, 0.0f, (System.Action) null);
    }
    return true;
  }

  private bool OnProcessCheat_favoritehero(string func, string[] args, string rawArgs)
  {
    //if (!(SceneMgr.Get().GetScene() is CollectionManagerScene))
    //{
    //  Debug.LogWarning((object) "OnProcessCheat_favoritehero must be used from the CollectionManagaer!");
    //  return false;
    //}
    return true;
  }

  private bool OnProcessCheat_help(string func, string[] args, string rawArgs)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string key = (string) null;
    if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
      key = args[0];
    List<string> list1 = new List<string>();
    if (key != null)
    {
      using (Map<string, List<CheatMgr.ProcessCheatCallback>>.KeyCollection.Enumerator enumerator = CheatMgr.Get().GetCheatCommands().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          if (current.Contains(key))
            list1.Add(current);
        }
      }
    }
    else
    {
      using (Map<string, List<CheatMgr.ProcessCheatCallback>>.KeyCollection.Enumerator enumerator = CheatMgr.Get().GetCheatCommands().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          list1.Add(current);
        }
      }
    }
    return true;
  }

  private bool OnProcessCheat_fixedrewardcomplete(string func, string[] args, string rawArgs)
  {
      return false;
    //Scene scene = SceneMgr.Get().GetScene();
    //int val;
    //if (!(scene is Login) && !(scene is Hub) || args.Length < 1 || (string.IsNullOrEmpty(args[0]) || !GeneralUtils.TryParseInt(args[0], out val)))
    //  return false;
    //return FixedRewardsMgr.Get().Cheat_ShowFixedReward(val, new FixedRewardsMgr.DelPositionNonToastReward(this.PositionFixedReward), (Vector3) Login.REWARD_PUNCH_SCALE, (Vector3) Login.REWARD_SCALE);
  }

  //private void PositionFixedReward(Reward reward)
  //{
  //  Scene scene = SceneMgr.Get().GetScene();
  //  reward.transform.parent = scene.transform;
  //  reward.transform.localRotation = Quaternion.identity;
  //  reward.transform.localPosition = Login.REWARD_LOCAL_POS;
  //}

  private bool OnProcessCheat_example(string func, string[] args, string rawArgs)
  {
    if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
      return false;
    string key = args[0];
    string str = string.Empty;
    if (!CheatMgr.Get().cheatExamples.TryGetValue(key, out str))
      return false;
    CheatMgr.Get().ProcessCheat(key + " " + str);
    return true;
  }
}
