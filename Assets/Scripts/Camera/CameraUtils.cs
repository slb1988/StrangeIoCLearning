// Decompiled with JetBrains decompiler
// Type: CameraUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CameraUtils
{
  public static Camera FindFirstByLayer(int layer)
  {
    return CameraUtils.FindFirstByLayerMask((LayerMask) (1 << layer));
  }

  public static Camera FindFirstByLayer(GameLayer layer)
  {
    return CameraUtils.FindFirstByLayerMask((LayerMask) GameLayerExtensions.LayerBit(layer));
  }

  public static Camera FindFirstByLayerMask(LayerMask mask)
  {
    foreach (Camera camera in Camera.allCameras)
    {
      if ((camera.cullingMask & (int) mask) != 0)
        return camera;
    }
    return (Camera) null;
  }

  public static void FindAllByLayer(int layer, List<Camera> cameras)
  {
    CameraUtils.FindAllByLayerMask((LayerMask) (1 << layer), cameras);
  }

  public static void FindAllByLayer(GameLayer layer, List<Camera> cameras)
  {
    CameraUtils.FindAllByLayerMask((LayerMask) GameLayerExtensions.LayerBit(layer), cameras);
  }

  public static void FindAllByLayerMask(LayerMask mask, List<Camera> cameras)
  {
    foreach (Camera camera in Camera.allCameras)
    {
      if ((camera.cullingMask & (int) mask) != 0)
        cameras.Add(camera);
    }
  }

  public static Camera FindFullScreenEffectsCamera(bool activeOnly)
  {
    foreach (Camera camera in Camera.allCameras)
    {
      FullScreenEffects component = camera.GetComponent<FullScreenEffects>();
      if (!((Object) component == (Object) null) && (!activeOnly || component.isActive()))
        return camera;
    }
    return (Camera) null;
  }

  public static LayerMask CreateLayerMask(List<Camera> cameras)
  {
    LayerMask layerMask = (LayerMask) 0;
    using (List<Camera>.Enumerator enumerator = cameras.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        Camera current = enumerator.Current;
        layerMask = (LayerMask) ((int) layerMask | current.cullingMask);
      }
    }
    return layerMask;
  }

  public static Plane CreateTopPlane(Camera camera)
  {
    Vector3 inPoint = camera.ViewportToWorldPoint(new Vector3(0.0f, 1f, camera.nearClipPlane));
    Vector3 vector3 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
    Vector3 inNormal = Vector3.Cross(camera.ViewportToWorldPoint(new Vector3(0.0f, 1f, camera.farClipPlane)) - inPoint, vector3 - inPoint);
    inNormal.Normalize();
    return new Plane(inNormal, inPoint);
  }

  public static Plane CreateBottomPlane(Camera camera)
  {
    Vector3 inPoint = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, camera.nearClipPlane));
    Vector3 vector3 = camera.ViewportToWorldPoint(new Vector3(1f, 0.0f, camera.nearClipPlane));
    Vector3 inNormal = Vector3.Cross(camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, camera.farClipPlane)) - inPoint, vector3 - inPoint);
    inNormal.Normalize();
    return new Plane(inNormal, inPoint);
  }

  public static Bounds GetNearClipBounds(Camera camera)
  {
    Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
    Vector3 vector3_1 = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, camera.nearClipPlane));
    Vector3 vector3_2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
    Vector3 size = new Vector3(vector3_2.x - vector3_1.x, vector3_2.y - vector3_1.y, vector3_2.z - vector3_1.z);
    return new Bounds(center, size);
  }

  public static Bounds GetFarClipBounds(Camera camera)
  {
    Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.farClipPlane));
    Vector3 vector3_1 = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, camera.farClipPlane));
    Vector3 vector3_2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.farClipPlane));
    Vector3 size = new Vector3(vector3_2.x - vector3_1.x, vector3_2.y - vector3_1.y, vector3_2.z - vector3_1.z);
    return new Bounds(center, size);
  }

  public static Rect CreateGUIViewportRect(Camera camera, Component topLeft, Component bottomRight)
  {
    return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIViewportRect(Camera camera, GameObject topLeft, Component bottomRight)
  {
    return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIViewportRect(Camera camera, Component topLeft, GameObject bottomRight)
  {
    return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIViewportRect(Camera camera, GameObject topLeft, GameObject bottomRight)
  {
    return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIViewportRect(Camera camera, Vector3 worldTopLeft, Vector3 worldBottomRight)
  {
    Vector3 vector3_1 = camera.WorldToViewportPoint(worldTopLeft);
    Vector3 vector3_2 = camera.WorldToViewportPoint(worldBottomRight);
    return new Rect(vector3_1.x, 1f - vector3_1.y, vector3_2.x - vector3_1.x, vector3_1.y - vector3_2.y);
  }

  public static Rect CreateGUIScreenRect(Camera camera, Component topLeft, Component bottomRight)
  {
    return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIScreenRect(Camera camera, GameObject topLeft, Component bottomRight)
  {
    return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIScreenRect(Camera camera, Component topLeft, GameObject bottomRight)
  {
    return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIScreenRect(Camera camera, GameObject topLeft, GameObject bottomRight)
  {
    return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
  }

  public static Rect CreateGUIScreenRect(Camera camera, Vector3 worldTopLeft, Vector3 worldBottomRight)
  {
    Vector3 vector3_1 = camera.WorldToScreenPoint(worldTopLeft);
    Vector3 vector3_2 = camera.WorldToScreenPoint(worldBottomRight);
    return new Rect(vector3_1.x, vector3_2.y, vector3_2.x - vector3_1.x, vector3_1.y - vector3_2.y);
  }

  public static bool Raycast(Camera camera, Vector3 screenPoint, out RaycastHit hitInfo)
  {
    hitInfo = new RaycastHit();
    if (!camera.pixelRect.Contains(screenPoint))
      return false;
    return Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, camera.cullingMask);
  }

  public static bool Raycast(Camera camera, Vector3 screenPoint, LayerMask layerMask, out RaycastHit hitInfo)
  {
    hitInfo = new RaycastHit();
    if (!camera.pixelRect.Contains(screenPoint))
      return false;
    return Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, (int) layerMask);
  }

  public static GameObject CreateInputBlocker(Camera camera)
  {
    return CameraUtils.CreateInputBlocker(camera, string.Empty, (Component) null, (Component) null, 0.0f);
  }

  public static GameObject CreateInputBlocker(Camera camera, string name)
  {
    return CameraUtils.CreateInputBlocker(camera, name, (Component) null, (Component) null, 0.0f);
  }

  public static GameObject CreateInputBlocker(Camera camera, string name, Component parent)
  {
    return CameraUtils.CreateInputBlocker(camera, name, parent, parent, 0.0f);
  }

  public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, float worldOffset)
  {
    return CameraUtils.CreateInputBlocker(camera, name, parent, parent, worldOffset);
  }

  public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, Component relative)
  {
    return CameraUtils.CreateInputBlocker(camera, name, parent, relative, 0.0f);
  }

  public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, Component relative, float worldOffset)
  {
    GameObject gameObject = new GameObject(name);
    gameObject.layer = camera.gameObject.layer;
    gameObject.transform.parent = !((Object) parent == (Object) null) ? parent.transform : (Transform) null;
    gameObject.transform.localScale = Vector3.one;
    gameObject.transform.rotation = Quaternion.Inverse(camera.transform.rotation);
    gameObject.transform.position = !((Object) relative == (Object) null) ? CameraUtils.GetPosInFrontOfCamera(camera, relative.transform.position, worldOffset) : CameraUtils.GetPosInFrontOfCamera(camera, camera.nearClipPlane + worldOffset);
    Bounds farClipBounds = CameraUtils.GetFarClipBounds(camera);
    Vector3 vector3 = !((Object) parent == (Object) null) ? TransformUtil.ComputeWorldScale(parent) : Vector3.one;
    gameObject.AddComponent<BoxCollider>().size = new Vector3()
    {
      x = farClipBounds.size.x / vector3.x,
      y = (double) farClipBounds.size.z <= 0.0 ? farClipBounds.size.y / vector3.y : farClipBounds.size.z / vector3.z
    };
    return gameObject;
  }

  public static float ScreenToWorldDist(Camera camera, float screenDist)
  {
    return CameraUtils.ScreenToWorldDist(camera, screenDist, camera.nearClipPlane);
  }

  public static float ScreenToWorldDist(Camera camera, float screenDist, float worldDist)
  {
    Vector3 vector3 = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, worldDist));
    return camera.ScreenToWorldPoint(new Vector3(screenDist, 0.0f, worldDist)).x - vector3.x;
  }

  public static float ScreenToWorldDist(Camera camera, float screenDist, Vector3 worldPoint)
  {
    float worldDist = Vector3.Distance(camera.transform.position, worldPoint);
    return CameraUtils.ScreenToWorldDist(camera, screenDist, worldDist);
  }

  public static Vector3 GetPosInFrontOfCamera(Camera camera, float worldDistance)
  {
    Vector3 position1 = camera.transform.position + new Vector3(0.0f, 0.0f, worldDistance);
    Vector3 position2 = new Vector3(0.0f, 0.0f, camera.transform.InverseTransformPoint(position1).magnitude);
    return camera.transform.TransformPoint(position2);
  }

  public static Vector3 GetPosInFrontOfCamera(Camera camera, Vector3 worldPoint)
  {
    return CameraUtils.GetPosInFrontOfCamera(camera, worldPoint, 0.0f);
  }

  public static Vector3 GetPosInFrontOfCamera(Camera camera, Vector3 worldPoint, float worldOffset)
  {
    Vector3 position = camera.transform.position;
    Vector3 forward = camera.transform.forward;
    Vector3 vector3 = (new Plane(-forward, worldPoint).GetDistanceToPoint(position) + worldOffset) * forward;
    return position + vector3;
  }

  public static Vector3 GetNearestPosInFrontOfCamera(Camera camera, float worldOffset = 0.0f)
  {
    return CameraUtils.GetPosInFrontOfCamera(camera, camera.nearClipPlane + worldOffset);
  }
}
