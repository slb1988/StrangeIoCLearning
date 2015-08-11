// Decompiled with JetBrains decompiler
// Type: UniversalInputManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UniversalInputManager : MonoBehaviour
{
  private static readonly PlatformDependentValue<bool> IsTouchDevice = new PlatformDependentValue<bool>(PlatformCategory.Input)
  {
    Mouse = false,
    Touch = true
  };
  private List<UniversalInputManager.MouseOnOrOffScreenCallback> m_mouseOnOrOffScreenListeners = new List<UniversalInputManager.MouseOnOrOffScreenCallback>();
  private List<Camera> m_CameraMaskCameras = new List<Camera>();
  private List<Camera> m_ignoredCameras = new List<Camera>();
  public bool m_hideVirtualKeyboardOnComplete = true;
  private const float TEXT_INPUT_RECT_HEIGHT_OFFSET = 3f;
  private const int TEXT_INPUT_MAX_FONT_SIZE = 32;
  private const int TEXT_INPUT_MIN_FONT_SIZE = 2;
  private const int TEXT_INPUT_FONT_SIZE_INSET = 4;
  private const int TEXT_INPUT_IME_FONT_SIZE_INSET = 9;
  private const string TEXT_INPUT_NAME = "UniversalInputManagerTextInput";
  private static readonly GameLayer[] HIT_TEST_PRIORITY_ORDER;
  private static UniversalInputManager s_instance;
  private static bool IsIMEEverUsed;
  private bool m_mouseOnScreen;
  private Map<GameLayer, int> m_hitTestPriorityMap;
  private bool m_gameDialogActive;
  private bool m_systemDialogActive;
  private int m_offCameraHitTestMask;
  private Camera m_FullscreenEffectsCamera;
  private bool m_FullscreenEffectsCameraActive;
  private GameObject m_inputOwner;
  private UniversalInputManager.TextInputUpdatedCallback m_inputUpdatedCallback;
  private UniversalInputManager.TextInputPreprocessCallback m_inputPreprocessCallback;
  private UniversalInputManager.TextInputCompletedCallback m_inputCompletedCallback;
  private UniversalInputManager.TextInputCanceledCallback m_inputCanceledCallback;
  private bool m_inputPassword;
  private bool m_inputNumber;
  private bool m_inputMultiLine;
  private bool m_inputActive;
  private bool m_inputFocused;
  private bool m_inputKeepFocusOnComplete;
  private string m_inputText;
  private Rect m_inputNormalizedRect;
  private Vector2 m_inputInitialScreenSize;
  private int m_inputMaxCharacters;
  private Font m_inputFont;
  private TextAnchor m_inputAlignment;
  private Color? m_inputColor;
  private Font m_defaultInputFont;
  private TextAnchor m_defaultInputAlignment;
  private bool m_inputNeedsFocus;
  private bool m_tabKeyDown;
  private bool m_inputNeedsFocusFromTabKeyDown;
  private UniversalInputManager.TextInputIgnoreState m_inputIgnoreState;
  public static readonly PlatformDependentValue<bool> UsePhoneUI;

  static UniversalInputManager()
  {
    GameLayer[] gameLayerArray = new GameLayer[11];
    int index1 = 0;
    int num1 = 13;
    gameLayerArray[index1] = (GameLayer) num1;
    int index2 = 1;
    int num2 = 16;
    gameLayerArray[index2] = (GameLayer) num2;
    int index3 = 2;
    int num3 = 19;
    gameLayerArray[index3] = (GameLayer) num3;
    int index4 = 3;
    int num4 = 27;
    gameLayerArray[index4] = (GameLayer) num4;
    int index5 = 4;
    int num5 = 31;
    gameLayerArray[index5] = (GameLayer) num5;
    int index6 = 5;
    int num6 = 18;
    gameLayerArray[index6] = (GameLayer) num6;
    int index7 = 6;
    int num7 = 24;
    gameLayerArray[index7] = (GameLayer) num7;
    int index8 = 7;
    int num8 = 25;
    gameLayerArray[index8] = (GameLayer) num8;
    int index9 = 8;
    int num9 = 26;
    gameLayerArray[index9] = (GameLayer) num9;
    int index10 = 9;
    int num10 = 29;
    gameLayerArray[index10] = (GameLayer) num10;
    int index11 = 10;
    int num11 = 17;
    gameLayerArray[index11] = (GameLayer) num11;
    //UniversalInputManager.HIT_TEST_PRIORITY_ORDER = gameLayerArray;
    UniversalInputManager.IsIMEEverUsed = false;
    UniversalInputManager.UsePhoneUI = new PlatformDependentValue<bool>(PlatformCategory.Screen)
    {
      Phone = true,
      Tablet = false,
      PC = false
    };
  }

  private void Update()
  {
    this.UpdateMouseOnOrOffScreen();
    this.UpdateInput();
    this.CleanDeadCameras();
  }

  private void Awake()
  {
    UniversalInputManager.s_instance = this;
    this.CreateHitTestPriorityMap();
    this.m_FullscreenEffectsCamera = CameraUtils.FindFullScreenEffectsCamera(true);
    if (!((UnityEngine.Object) this.m_FullscreenEffectsCamera != (UnityEngine.Object) null))
      return;
    this.m_FullscreenEffectsCameraActive = true;
  }

  private void Start()
  {
//    this.m_mouseOnScreen = InputUtil.IsMouseOnScreen();
  }

  private void OnDestroy()
  {
    UniversalInputManager.s_instance = (UniversalInputManager) null;
  }

  private void OnGUI()
  {
    this.IgnoreGUIInput();
    this.HandleGUIInputInactive();
    this.HandleGUIInputActive();
  }

  public static UniversalInputManager Get()
  {
    return UniversalInputManager.s_instance;
  }

  public bool IsTouchMode()
  {
//    if (!(bool) UniversalInputManager.IsTouchDevice)
//      return Options.Get().GetBool(Option.TOUCH_MODE);
    return true;
  }

  public bool WasTouchCanceled()
  {
    if (!(bool) UniversalInputManager.IsTouchDevice)
      return false;
    foreach (Touch touch in Input.touches)
    {
      if (touch.phase == TouchPhase.Canceled)
        return true;
    }
    return false;
  }

  public bool IsMouseOnScreen()
  {
    return this.m_mouseOnScreen;
  }

  public bool RegisterMouseOnOrOffScreenListener(UniversalInputManager.MouseOnOrOffScreenCallback listener)
  {
    if (this.m_mouseOnOrOffScreenListeners.Contains(listener))
      return false;
    this.m_mouseOnOrOffScreenListeners.Add(listener);
    return true;
  }

  public bool UnregisterMouseOnOrOffScreenListener(UniversalInputManager.MouseOnOrOffScreenCallback listener)
  {
    return this.m_mouseOnOrOffScreenListeners.Remove(listener);
  }

  public void SetGameDialogActive(bool active)
  {
    this.m_gameDialogActive = active;
  }

  public void SetSystemDialogActive(bool active)
  {
    this.m_systemDialogActive = active;
  }

  public void UseTextInput(UniversalInputManager.TextInputParams parms, bool force = false)
  {
    if (!force && (UnityEngine.Object) parms.m_owner == (UnityEngine.Object) this.m_inputOwner)
      return;
    if ((UnityEngine.Object) this.m_inputOwner != (UnityEngine.Object) null && (UnityEngine.Object) this.m_inputOwner != (UnityEngine.Object) parms.m_owner)
      this.ObjectCancelTextInput(parms.m_owner);
    this.m_inputOwner = parms.m_owner;
    this.m_inputUpdatedCallback = parms.m_updatedCallback;
    this.m_inputPreprocessCallback = parms.m_preprocessCallback;
    this.m_inputCompletedCallback = parms.m_completedCallback;
    this.m_inputCanceledCallback = parms.m_canceledCallback;
    this.m_inputPassword = parms.m_password;
    this.m_inputNumber = parms.m_number;
    this.m_inputMultiLine = parms.m_multiLine;
    this.m_inputActive = true;
    this.m_inputFocused = false;
    this.m_inputText = parms.m_text ?? string.Empty;
    this.m_inputNormalizedRect = parms.m_rect;
    this.m_inputInitialScreenSize.x = (float) Screen.width;
    this.m_inputInitialScreenSize.y = (float) Screen.height;
    this.m_inputMaxCharacters = parms.m_maxCharacters;
    this.m_inputColor = parms.m_color;
    TextAnchor? nullable = parms.m_alignment;
    this.m_inputAlignment = !nullable.HasValue ? this.m_defaultInputAlignment : nullable.Value;
    this.m_inputFont = parms.m_font ?? this.m_defaultInputFont;
    this.m_inputNeedsFocus = true;
    this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.INVALID;
    this.m_inputKeepFocusOnComplete = parms.m_inputKeepFocusOnComplete;
    if (this.IsTextInputPassword())
      Input.imeCompositionMode = IMECompositionMode.Off;
    this.m_hideVirtualKeyboardOnComplete = parms.m_hideVirtualKeyboardOnComplete;
    if (!UniversalInputManager.Get().IsTouchMode() || !parms.m_showVirtualKeyboard)
      return;
  }

  public void CancelTextInput(GameObject requester, bool force = false)
  {
    if (!this.IsTextInputActive() || !force && (UnityEngine.Object) requester != (UnityEngine.Object) this.m_inputOwner)
      return;
    this.ObjectCancelTextInput(requester);
  }

  public void FocusTextInput(GameObject owner)
  {
    if ((UnityEngine.Object) owner != (UnityEngine.Object) this.m_inputOwner)
      return;
    if (this.m_tabKeyDown)
      this.m_inputNeedsFocusFromTabKeyDown = true;
    else
      this.m_inputNeedsFocus = true;
  }

  public void UpdateTextInputRect(GameObject owner, Rect rect)
  {
    if ((UnityEngine.Object) owner != (UnityEngine.Object) this.m_inputOwner)
      return;
    this.m_inputNormalizedRect = rect;
    this.m_inputInitialScreenSize.x = (float) Screen.width;
    this.m_inputInitialScreenSize.y = (float) Screen.height;
  }

  public bool IsTextInputPassword()
  {
    return this.m_inputPassword;
  }

  public bool IsTextInputActive()
  {
    return this.m_inputActive;
  }

  public string GetInputText()
  {
    return this.m_inputText;
  }

  public void SetInputText(string text)
  {
    this.m_inputText = text ?? string.Empty;
  }

  public bool InputIsOver(GameObject gameObj)
  {
    RaycastHit hitInfo;
    return this.InputIsOver(gameObj, out hitInfo);
  }

  public bool InputIsOver(GameObject gameObj, out RaycastHit hitInfo)
  {
    Camera camera;
    if (!this.Raycast((Camera) null, (LayerMask) GameLayerExtensions.LayerBit((GameLayer) gameObj.layer), out camera, out hitInfo, false))
      return false;
    return (UnityEngine.Object) hitInfo.collider.gameObject == (UnityEngine.Object) gameObj;
  }

  public bool InputIsOver(GameObject gameObj, int layerMask, out RaycastHit hitInfo)
  {
    Camera camera;
    if (!this.Raycast((Camera) null, (LayerMask) layerMask, out camera, out hitInfo, false))
      return false;
    return (UnityEngine.Object) hitInfo.collider.gameObject == (UnityEngine.Object) gameObj;
  }

  public bool InputIsOver(Camera camera, GameObject gameObj)
  {
    RaycastHit hitInfo;
    return this.InputIsOver(camera, gameObj, out hitInfo);
  }

  public bool InputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
  {
    LayerMask mask = (LayerMask) GameLayerExtensions.LayerBit((GameLayer) gameObj.layer);
    Camera camera1;
    if (!this.Raycast(camera, mask, out camera1, out hitInfo, false))
      return false;
    return (UnityEngine.Object) hitInfo.collider.gameObject == (UnityEngine.Object) gameObj;
  }

  public bool ForcedInputIsOver(Camera camera, GameObject gameObj)
  {
    RaycastHit hitInfo;
    return this.ForcedInputIsOver(camera, gameObj, out hitInfo);
  }

  public bool ForcedInputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
  {
    LayerMask layerMask = (LayerMask) GameLayerExtensions.LayerBit((GameLayer) gameObj.layer);
    if (!CameraUtils.Raycast(camera, this.GetMousePosition(), layerMask, out hitInfo))
      return false;
    return (UnityEngine.Object) hitInfo.collider.gameObject == (UnityEngine.Object) gameObj;
  }

  public bool InputHitAnyObject(GameLayer layer)
  {
    RaycastHit hitInfo;
    return this.GetInputHitInfo(layer, out hitInfo);
  }

  public bool InputHitAnyObject(LayerMask layerMask)
  {
    RaycastHit hitInfo;
    return this.GetInputHitInfo(layerMask, out hitInfo);
  }

  public bool InputHitAnyObject(Camera requestedCamera)
  {
    RaycastHit hitInfo;
    if ((UnityEngine.Object) requestedCamera == (UnityEngine.Object) null)
      return this.GetInputHitInfo(out hitInfo);
    return this.GetInputHitInfo(requestedCamera, (LayerMask) requestedCamera.cullingMask, out hitInfo);
  }

  public bool InputHitAnyObject(Camera requestedCamera, GameLayer layer)
  {
    RaycastHit hitInfo;
    return this.GetInputHitInfo(requestedCamera, layer, out hitInfo);
  }

  public bool InputHitAnyObject(Camera requestedCamera, LayerMask mask)
  {
    RaycastHit hitInfo;
    return this.GetInputHitInfo(requestedCamera, mask, out hitInfo);
  }

  public bool GetInputHitInfo(out RaycastHit hitInfo)
  {
    return this.GetInputHitInfo(GameLayer.Default, out hitInfo);
  }

  public bool GetInputHitInfo(GameLayer layer, out RaycastHit hitInfo)
  {
    return this.GetInputHitInfo((LayerMask) GameLayerExtensions.LayerBit(layer), out hitInfo);
  }

  public bool GetInputHitInfo(LayerMask mask, out RaycastHit hitInfo)
  {
    return this.GetInputHitInfo(this.GuessBestHitTestCamera(mask), mask, out hitInfo);
  }

  public bool GetInputHitInfo(Camera requestedCamera, out RaycastHit hitInfo)
  {
    if ((UnityEngine.Object) requestedCamera == (UnityEngine.Object) null)
      return this.GetInputHitInfo(out hitInfo);
    return this.GetInputHitInfo(requestedCamera, (LayerMask) requestedCamera.cullingMask, out hitInfo);
  }

  public bool GetInputHitInfo(Camera requestedCamera, GameLayer layer, out RaycastHit hitInfo)
  {
    Camera camera;
    return this.Raycast(requestedCamera, (LayerMask) GameLayerExtensions.LayerBit(layer), out camera, out hitInfo, false);
  }

  public bool GetInputHitInfo(Camera requestedCamera, LayerMask mask, out RaycastHit hitInfo)
  {
    Camera camera;
    return this.Raycast(requestedCamera, mask, out camera, out hitInfo, false);
  }

  public bool GetInputPointOnPlane(Vector3 origin, out Vector3 point)
  {
    return this.GetInputPointOnPlane(GameLayer.Default, origin, out point);
  }

  public bool GetInputPointOnPlane(GameLayer layer, Vector3 origin, out Vector3 point)
  {
    point = Vector3.zero;
    Camera camera;
    RaycastHit hitInfo;
    if (!this.Raycast((Camera) null, (LayerMask) GameLayerExtensions.LayerBit(layer), out camera, out hitInfo, false))
      return false;
    Ray ray = camera.ScreenPointToRay(this.GetMousePosition());
    float enter;
    if (!new Plane(-camera.transform.forward, origin).Raycast(ray, out enter))
      return false;
    point = ray.GetPoint(enter);
    return true;
  }

  public bool CanHitTestOffCamera(GameLayer layer)
  {
    return this.CanHitTestOffCamera((LayerMask) GameLayerExtensions.LayerBit(layer));
  }

  public bool CanHitTestOffCamera(LayerMask layerMask)
  {
    return (this.m_offCameraHitTestMask & (int) layerMask) != 0;
  }

  public void EnableHitTestOffCamera(GameLayer layer, bool enable)
  {
    this.EnableHitTestOffCamera((LayerMask) GameLayerExtensions.LayerBit(layer), enable);
  }

  public void EnableHitTestOffCamera(LayerMask mask, bool enable)
  {
    if (enable)
      this.m_offCameraHitTestMask |= (int) mask;
    else
      this.m_offCameraHitTestMask &= ~(int) mask;
  }

  public void SetFullScreenEffectsCamera(Camera camera, bool active)
  {
    this.m_FullscreenEffectsCamera = camera;
    this.m_FullscreenEffectsCameraActive = false;
  }

  public bool GetMouseButton(int button)
  {
    return Input.GetMouseButton(button);
  }

  public bool GetMouseButtonDown(int button)
  {
    return Input.GetMouseButtonDown(button);
  }

  public bool GetMouseButtonUp(int button)
  {
    return Input.GetMouseButtonUp(button);
  }

  public Vector3 GetMousePosition()
  {
    return Input.mousePosition;
  }

  public bool AddCameraMaskCamera(Camera camera)
  {
    if (this.m_CameraMaskCameras.Contains(camera))
      return false;
    this.m_CameraMaskCameras.Add(camera);
    return true;
  }

  public bool RemoveCameraMaskCamera(Camera camera)
  {
    return this.m_CameraMaskCameras.Remove(camera);
  }

  public bool AddIgnoredCamera(Camera camera)
  {
    if (this.m_ignoredCameras.Contains(camera))
      return false;
    this.m_ignoredCameras.Add(camera);
    return true;
  }

  public bool RemoveIgnoredCamera(Camera camera)
  {
    return this.m_ignoredCameras.Remove(camera);
  }

  private void CreateHitTestPriorityMap()
  {
    this.m_hitTestPriorityMap = new Map<GameLayer, int>();
    int num1 = 1;
    //for (int index = 0; index < UniversalInputManager.HIT_TEST_PRIORITY_ORDER.Length; ++index)
    //  this.m_hitTestPriorityMap.Add(UniversalInputManager.HIT_TEST_PRIORITY_ORDER[index], num1++);
    foreach (int num2 in Enum.GetValues(typeof (GameLayer)))
    {
      GameLayer key = (GameLayer) num2;
      if (!this.m_hitTestPriorityMap.ContainsKey(key))
        this.m_hitTestPriorityMap.Add(key, 0);
    }
  }

  private void CleanDeadCameras()
  {
    GeneralUtils.CleanDeadObjectsFromList<Camera>(this.m_CameraMaskCameras);
    GeneralUtils.CleanDeadObjectsFromList<Camera>(this.m_ignoredCameras);
  }

  private Camera GuessBestHitTestCamera(LayerMask mask)
  {
    foreach (Camera camera in Camera.allCameras)
    {
      if (!this.m_ignoredCameras.Contains(camera) && (camera.cullingMask & (int) mask) != 0)
        return camera;
    }
    return (Camera) null;
  }

  private bool Raycast(Camera requestedCamera, LayerMask mask, out Camera camera, out RaycastHit hitInfo, bool ignorePriority = false)
  {
    hitInfo = new RaycastHit();
    if (!ignorePriority)
    {
      using (List<Camera>.Enumerator enumerator = this.m_CameraMaskCameras.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Camera current = enumerator.Current;
          camera = current;
          LayerMask mask1 = (LayerMask) GameLayerExtensions.LayerBit(GameLayer.CameraMask);
          if (this.RaycastWithPriority(current, mask1, out hitInfo))
            return true;
        }
      }
      camera = this.m_FullscreenEffectsCamera;
      if ((UnityEngine.Object) camera != (UnityEngine.Object) null)
      {
        LayerMask mask1 = (LayerMask) GameLayerExtensions.LayerBit(GameLayer.IgnoreFullScreenEffects);
        if (this.RaycastWithPriority(camera, mask1, out hitInfo))
          return true;
      }
    }
    camera = requestedCamera;
    if ((UnityEngine.Object) camera != (UnityEngine.Object) null)
      return this.RaycastWithPriority(camera, mask, out hitInfo);
    camera = Camera.main;
    return this.RaycastWithPriority(camera, mask, out hitInfo);
  }

  private bool RaycastWithPriority(Camera camera, LayerMask mask, out RaycastHit hitInfo)
  {
    hitInfo = new RaycastHit();
    return !((UnityEngine.Object) camera == (UnityEngine.Object) null) && this.FilteredRaycast(camera, this.GetMousePosition(), mask, out hitInfo) && !this.HigherPriorityCollisionExists((GameLayer) hitInfo.collider.gameObject.layer);
  }

  private bool FilteredRaycast(Camera camera, Vector3 screenPoint, LayerMask mask, out RaycastHit hitInfo)
  {
    if (this.CanHitTestOffCamera(mask))
    {
      if (!Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, (int) mask))
        return false;
    }
    else if (!CameraUtils.Raycast(camera, screenPoint, mask, out hitInfo))
      return false;
    return true;
  }

  private bool HigherPriorityCollisionExists(GameLayer layer)
  {
    if (this.m_systemDialogActive && this.m_hitTestPriorityMap[layer] < this.m_hitTestPriorityMap[GameLayer.SystemDialog] || this.m_gameDialogActive && this.m_hitTestPriorityMap[layer] < this.m_hitTestPriorityMap[GameLayer.GameDialog] || this.m_FullscreenEffectsCameraActive && this.m_hitTestPriorityMap[layer] < this.m_hitTestPriorityMap[GameLayer.IgnoreFullScreenEffects])
      return true;
    LayerMask priorityLayerMask = this.GetHigherPriorityLayerMask(layer);
    foreach (Camera camera in Camera.allCameras)
    {
      RaycastHit hitInfo;
      if (!this.m_ignoredCameras.Contains(camera) && (camera.cullingMask & (int) priorityLayerMask) != 0 && this.FilteredRaycast(camera, this.GetMousePosition(), priorityLayerMask, out hitInfo))
      {
        GameLayer gameLayer = (GameLayer) hitInfo.collider.gameObject.layer;
        if ((camera.cullingMask & GameLayerExtensions.LayerBit(gameLayer)) != 0)
          return true;
      }
    }
    return false;
  }

  private LayerMask GetHigherPriorityLayerMask(GameLayer layer)
  {
    int num = this.m_hitTestPriorityMap[layer];
    LayerMask layerMask = (LayerMask) 0;
    using (Map<GameLayer, int>.Enumerator enumerator = this.m_hitTestPriorityMap.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<GameLayer, int> current = enumerator.Current;
        GameLayer key = current.Key;
        if (current.Value > num)
          layerMask = (LayerMask) ((int) layerMask | GameLayerExtensions.LayerBit(key));
      }
    }
    return layerMask;
  }

  private void UpdateMouseOnOrOffScreen()
  {
    //bool onScreen = InputUtil.IsMouseOnScreen();
    //if (onScreen == this.m_mouseOnScreen)
    //  return;
    //this.m_mouseOnScreen = onScreen;
    //foreach (UniversalInputManager.MouseOnOrOffScreenCallback offScreenCallback in this.m_mouseOnOrOffScreenListeners.ToArray())
    //  offScreenCallback(onScreen);
  }

  private void UpdateInput()
  {
    if (this.UpdateTextInput())
      return;
    //InputManager inputManager = InputManager.Get();
    //if ((UnityEngine.Object) inputManager != (UnityEngine.Object) null && inputManager.HandleKeyboardInput())
    //  return;
    //CheatMgr cheatMgr = CheatMgr.Get();
    //if ((UnityEngine.Object) cheatMgr != (UnityEngine.Object) null && cheatMgr.HandleKeyboardInput())
    //  return;
    //Cheats cheats = Cheats.Get();
    //if (cheats != null && cheats.HandleKeyboardInput())
    //  return;
    //if ((UnityEngine.Object) SceneMgr.Get() != (UnityEngine.Object) null)
    //{
    //  Scene scene = SceneMgr.Get().GetScene();
    //  if ((UnityEngine.Object) scene != (UnityEngine.Object) null && scene.HandleKeyboardInput())
    //    return;
    //}
  }

  private bool UpdateTextInput()
  {
    if (Input.imeIsSelected || !string.IsNullOrEmpty(Input.compositionString))
      UniversalInputManager.IsIMEEverUsed = true;
    if (this.m_inputNeedsFocusFromTabKeyDown)
    {
      this.m_inputNeedsFocusFromTabKeyDown = false;
      this.m_inputNeedsFocus = true;
    }
    if (!this.m_inputActive)
      return false;
    return this.m_inputFocused;
  }

  private void UserCancelTextInput()
  {
    this.CancelTextInput(true, (GameObject) null);
  }

  private void ObjectCancelTextInput(GameObject requester)
  {
    this.CancelTextInput(false, requester);
  }

  private void CancelTextInput(bool userRequested, GameObject requester)
  {
    if (this.IsTextInputPassword())
      Input.imeCompositionMode = IMECompositionMode.Auto;
    if ((UnityEngine.Object) requester != (UnityEngine.Object) null && (UnityEngine.Object) requester == (UnityEngine.Object) this.m_inputOwner)
    {
      this.ClearTextInputVars();
    }
    else
    {
      UniversalInputManager.TextInputCanceledCallback canceledCallback = this.m_inputCanceledCallback;
      this.ClearTextInputVars();
      if (canceledCallback != null)
        canceledCallback(userRequested, requester);
    }
    if (!this.IsTouchMode())
      return;
  }

  private void CompleteTextInput()
  {
    if (this.IsTextInputPassword())
      Input.imeCompositionMode = IMECompositionMode.Auto;
    UniversalInputManager.TextInputCompletedCallback completedCallback = this.m_inputCompletedCallback;
    if (!this.m_inputKeepFocusOnComplete)
      this.ClearTextInputVars();
    if (completedCallback != null)
      completedCallback(this.m_inputText);
    this.m_inputText = string.Empty;
    if (!this.IsTouchMode() || !this.m_hideVirtualKeyboardOnComplete)
      return;
  }

  private void ClearTextInputVars()
  {
    this.m_inputActive = false;
    this.m_inputFocused = false;
    this.m_inputOwner = (GameObject) null;
    this.m_inputMaxCharacters = 0;
    this.m_inputUpdatedCallback = (UniversalInputManager.TextInputUpdatedCallback) null;
    this.m_inputCompletedCallback = (UniversalInputManager.TextInputCompletedCallback) null;
    this.m_inputCanceledCallback = (UniversalInputManager.TextInputCanceledCallback) null;
  }

  private bool IgnoreGUIInput()
  {
    if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.INVALID || Event.current.type != EventType.KeyUp)
      return false;
    switch (Event.current.keyCode)
    {
      case KeyCode.Return:
        if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.COMPLETE_KEY_UP)
          this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.NEXT_CALL;
        return true;
      case KeyCode.Escape:
        if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.CANCEL_KEY_UP)
          this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.NEXT_CALL;
        return true;
      default:
        return false;
    }
  }

  private void HandleGUIInputInactive()
  {
    if (this.m_inputActive)
      return;
  }

  private void HandleGUIInputActive()
  {
    if (!this.m_inputActive || !this.PreprocessGUITextInput())
      return;
  }

  private bool PreprocessGUITextInput()
  {
    this.UpdateTabKeyDown();
    if (this.m_inputPreprocessCallback != null)
    {
      int num = this.m_inputPreprocessCallback(Event.current) ? 1 : 0;
      if (!this.m_inputActive)
        return false;
    }
    return !this.ProcessTextInputFinishKeys();
  }

  private void UpdateTabKeyDown()
  {
    this.m_tabKeyDown = Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Tab;
  }

  private bool ProcessTextInputFinishKeys()
  {
    if (!this.m_inputFocused || Event.current.type != EventType.KeyDown)
      return false;
    switch (Event.current.keyCode)
    {
      case KeyCode.Return:
        this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.COMPLETE_KEY_UP;
        this.CompleteTextInput();
        return true;
      case KeyCode.Escape:
        this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.CANCEL_KEY_UP;
        this.UserCancelTextInput();
        return true;
      default:
        return false;
    }
  }

  private void SetupTextInput(Vector2 screenSize, Rect inputScreenRect)
  {
    //GUI.skin = this.m_skin;
    //GUI.skin.textField.font = this.m_inputFont;
    //GUI.skin.textField.fontSize = this.ComputeTextInputFontSize(screenSize, inputScreenRect.height);
    //if (this.m_inputColor.HasValue)
    //  GUI.color = this.m_inputColor.Value;
    //GUI.skin.textField.alignment = this.m_inputAlignment;
    //GUI.SetNextControlName("UniversalInputManagerTextInput");
  }

  private string ShowTextInput(Rect inputScreenRect)
  {
    return !this.m_inputPassword ? (this.m_inputMaxCharacters > 0 ? GUI.TextField(inputScreenRect, this.m_inputText, this.m_inputMaxCharacters) : GUI.TextField(inputScreenRect, this.m_inputText)) : (this.m_inputMaxCharacters > 0 ? GUI.PasswordField(inputScreenRect, this.m_inputText, '*', this.m_inputMaxCharacters) : GUI.PasswordField(inputScreenRect, this.m_inputText, '*'));
  }

  private void UpdateTextInputFocus()
  {
    if (this.m_inputNeedsFocus)
    {
      GUI.FocusControl("UniversalInputManagerTextInput");
      this.m_inputFocused = true;
      this.m_inputNeedsFocus = false;
    }
    else
      this.m_inputFocused = GUI.GetNameOfFocusedControl() == "UniversalInputManagerTextInput";
  }

  private Rect ComputeTextInputRect(Vector2 screenSize)
  {
    float num1 = this.m_inputInitialScreenSize.x / this.m_inputInitialScreenSize.y / (screenSize.x / screenSize.y);
    float num2 = (0.5f - this.m_inputNormalizedRect.x) * this.m_inputInitialScreenSize.x * (screenSize.y / this.m_inputInitialScreenSize.y);
    return new Rect(screenSize.x * 0.5f - num2, (float) ((double) this.m_inputNormalizedRect.y * (double) screenSize.y - 1.5), this.m_inputNormalizedRect.width * screenSize.x * num1, (float) ((double) this.m_inputNormalizedRect.height * (double) screenSize.y + 1.5));
  }

  private int ComputeTextInputFontSize(Vector2 screenSize, float rectHeight)
  {
      int num = Mathf.CeilToInt(rectHeight);
      return num;
    //return Mathf.Clamp(Localization.IsIMELocale() || UniversalInputManager.IsIMEEverUsed ? num - 9 : num - 4, 2, 32);
  }

  public class TextInputParams
  {
    public bool m_showVirtualKeyboard = true;
    public bool m_hideVirtualKeyboardOnComplete = true;
    public GameObject m_owner;
    public bool m_password;
    public bool m_number;
    public bool m_multiLine;
    public Rect m_rect;
    public UniversalInputManager.TextInputUpdatedCallback m_updatedCallback;
    public UniversalInputManager.TextInputPreprocessCallback m_preprocessCallback;
    public UniversalInputManager.TextInputCompletedCallback m_completedCallback;
    public UniversalInputManager.TextInputCanceledCallback m_canceledCallback;
    public int m_maxCharacters;
    public Font m_font;
    public TextAnchor? m_alignment;
    public string m_text;
    public bool m_touchScreenKeyboardHideInput;
    public int m_touchScreenKeyboardType;
    public bool m_inputKeepFocusOnComplete;
    public Color? m_color;
    public bool m_useNativeKeyboard;
  }

  private enum TextInputIgnoreState
  {
    INVALID,
    COMPLETE_KEY_UP,
    CANCEL_KEY_UP,
    NEXT_CALL,
  }

  public delegate void MouseOnOrOffScreenCallback(bool onScreen);

  public delegate void TextInputUpdatedCallback(string input);

  public delegate bool TextInputPreprocessCallback(Event e);

  public delegate void TextInputCompletedCallback(string input);

  public delegate void TextInputCanceledCallback(bool userRequested, GameObject requester);
}
