// Decompiled with JetBrains decompiler
// Type: TransformUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TransformUtil
{
  private const float MIN_PHONE_ASPECT_RATIO = 1.5f;
  private const float MAX_PHONE_ASPECT_RATIO = 1.777778f;
  private const float ASPECT_RANGE = 0.2777778f;

  public static Vector3 GetUnitAnchor(Anchor anchor)
  {
    Vector3 vector3 = new Vector3();
    switch (anchor)
    {
      case Anchor.TOP_LEFT:
        vector3.x = 0.0f;
        vector3.y = 1f;
        vector3.z = 0.0f;
        break;
      case Anchor.TOP:
        vector3.x = 0.5f;
        vector3.y = 1f;
        vector3.z = 0.0f;
        break;
      case Anchor.TOP_RIGHT:
        vector3.x = 1f;
        vector3.y = 1f;
        vector3.z = 0.0f;
        break;
      case Anchor.LEFT:
        vector3.x = 0.0f;
        vector3.y = 0.5f;
        vector3.z = 0.0f;
        break;
      case Anchor.CENTER:
        vector3.x = 0.5f;
        vector3.y = 0.5f;
        vector3.z = 0.0f;
        break;
      case Anchor.RIGHT:
        vector3.x = 1f;
        vector3.y = 0.5f;
        vector3.z = 0.0f;
        break;
      case Anchor.BOTTOM_LEFT:
        vector3.x = 0.0f;
        vector3.y = 0.0f;
        vector3.z = 0.0f;
        break;
      case Anchor.BOTTOM:
        vector3.x = 0.5f;
        vector3.y = 0.0f;
        vector3.z = 0.0f;
        break;
      case Anchor.BOTTOM_RIGHT:
        vector3.x = 1f;
        vector3.y = 0.0f;
        vector3.z = 0.0f;
        break;
      case Anchor.FRONT:
        vector3.x = 0.5f;
        vector3.y = 0.0f;
        vector3.z = 1f;
        break;
      case Anchor.BACK:
        vector3.x = 0.5f;
        vector3.y = 0.0f;
        vector3.z = 0.0f;
        break;
    }
    return vector3;
  }

  public static Vector3 ComputeWorldPoint(Bounds bounds, Vector3 selfUnitAnchor)
  {
    return new Vector3()
    {
      x = Mathf.Lerp(bounds.min.x, bounds.max.x, selfUnitAnchor.x),
      y = Mathf.Lerp(bounds.min.y, bounds.max.y, selfUnitAnchor.y),
      z = Mathf.Lerp(bounds.min.z, bounds.max.z, selfUnitAnchor.z)
    };
  }

  public static Vector2 ComputeUnitAnchor(Bounds bounds, Vector2 worldPoint)
  {
    return new Vector2()
    {
      x = (worldPoint.x - bounds.min.x) / bounds.size.x,
      y = (worldPoint.y - bounds.min.y) / bounds.size.y
    };
  }

  //public static Bounds ComputeSetPointBounds(Component c)
  //{
  //  return TransformUtil.ComputeSetPointBounds(c.gameObject, false);
  //}

  //public static Bounds ComputeSetPointBounds(GameObject go)
  //{
  //  return TransformUtil.ComputeSetPointBounds(go, false);
  //}

  //public static Bounds ComputeSetPointBounds(Component c, bool includeInactive)
  //{
  //  return TransformUtil.ComputeSetPointBounds(c.gameObject, includeInactive);
  //}

  //public static Bounds ComputeSetPointBounds(GameObject go, bool includeInactive)
  //{
  //  //UberText component1 = go.GetComponent<UberText>();
  //  //if ((Object) component1 != (Object) null)
  //  //  return component1.GetTextWorldSpaceBounds();
  //  //Renderer renderer = go.renderer;
  //  //if ((Object) renderer != (Object) null)
  //  //  return renderer.bounds;
  //  Collider collider = go.collider;
  //  //if (!((Object) collider != (Object) null))
  //  //  return TransformUtil.GetBoundsOfChildren(go, includeInactive);
  //  Bounds bounds;
  //  if (collider.enabled)
  //  {
  //    bounds = collider.bounds;
  //  }
  //  else
  //  {
  //    collider.enabled = true;
  //    bounds = collider.bounds;
  //    collider.enabled = false;
  //  }
  //  //MobileHitBox component2 = go.GetComponent<MobileHitBox>();
  //  //if ((Object) component2 != (Object) null && component2.HasExecuted())
  //  //  bounds.size = new Vector3(bounds.size.x / component2.m_scaleX, bounds.size.y / component2.m_scaleY, bounds.size.z / component2.m_scaleY);
  //  return bounds;
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, true, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, true, ignoreMeshes, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, true, minLocalPadding, maxLocalPadding, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, Vector3 minLocalPadding, Vector3 maxLocalPadding, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, true, minLocalPadding, maxLocalPadding, ignoreMeshes, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, includeUberText, Vector3.zero, Vector3.zero, (List<GameObject>) null, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, includeUberText, Vector3.zero, Vector3.zero, ignoreMeshes, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool includeAllChildren = true)
  //{
  //  return TransformUtil.ComputeOrientedWorldBounds(go, includeUberText, minLocalPadding, maxLocalPadding, (List<GameObject>) null, includeAllChildren);
  //}

  //public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, Vector3 minLocalPadding, Vector3 maxLocalPadding, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
  //{


  //public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeAllChildren = true)
  //{
  //  return TransformUtil.CanComputeOrientedWorldBounds(go, true, includeAllChildren);
  //}

  //public static bool CanComputeOrientedWorldBounds(GameObject go, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
  //{
  //  return TransformUtil.CanComputeOrientedWorldBounds(go, true, ignoreMeshes, includeAllChildren);
  //}

  //public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeUberText, bool includeAllChildren = true)
  //{
  //  return TransformUtil.CanComputeOrientedWorldBounds(go, includeUberText, (List<GameObject>) null, includeAllChildren);
  //}

  //public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeUberText, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
  //{
  //  if ((Object) go == (Object) null || !go.activeSelf)
  //    return false;
  //  List<MeshFilter> componentsWithIgnore1 = TransformUtil.GetComponentsWithIgnore<MeshFilter>(go, ignoreMeshes, includeAllChildren);
  //  if (componentsWithIgnore1 != null && componentsWithIgnore1.Count > 0)
  //    return true;
  //  if (!includeUberText)
  //    return false;
  //  List<UberText> componentsWithIgnore2 = TransformUtil.GetComponentsWithIgnore<UberText>(go, ignoreMeshes, includeAllChildren);
  //  if (componentsWithIgnore2 != null)
  //    return componentsWithIgnore2.Count > 0;
  //  return false;
  //}

  public static List<T> GetComponentsWithIgnore<T>(GameObject obj, List<GameObject> ignoreObjects, bool includeAllChildren = true) where T : Component
  {
    List<T> list = new List<T>();
    if (includeAllChildren)
      obj.GetComponentsInChildren<T>(list);
    T component = obj.GetComponent<T>();
    if ((Object) component != (Object) null)
      list.Add(component);
    if (ignoreObjects != null && ignoreObjects.Count > 0)
    {
      T[] objArray = list.ToArray();
      list.Clear();
      foreach (T obj1 in objArray)
      {
        bool flag = true;
        using (List<GameObject>.Enumerator enumerator = ignoreObjects.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            GameObject current = enumerator.Current;
            if ((Object) current == (Object) null || (Object) obj1.transform == (Object) current.transform || obj1.transform.IsChildOf(current.transform))
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
          list.Add(obj1);
      }
    }
    return list;
  }

  public static Vector3[] GetBoundCorners(Vector3 origin, Vector3 xExtent, Vector3 yExtent, Vector3 zExtent)
  {
    Vector3 vector3_1 = origin + xExtent;
    Vector3 vector3_2 = origin - xExtent;
    Vector3 vector3_3 = yExtent + zExtent;
    Vector3 vector3_4 = yExtent - zExtent;
    Vector3 vector3_5 = -yExtent + zExtent;
    Vector3 vector3_6 = -yExtent - zExtent;
    Vector3[] vector3Array = new Vector3[8];
    int index1 = 0;
    vector3Array[index1] = vector3_1 + vector3_3;
    int index2 = 1;
    vector3Array[index2] = vector3_1 + vector3_4;
    int index3 = 2;
    vector3Array[index3] = vector3_1 + vector3_5;
    int index4 = 3;
    vector3Array[index4] = vector3_1 + vector3_6;
    int index5 = 4;
    vector3Array[index5] = vector3_2 - vector3_3;
    int index6 = 5;
    vector3Array[index6] = vector3_2 - vector3_4;
    int index7 = 6;
    vector3Array[index7] = vector3_2 - vector3_5;
    int index8 = 7;
    vector3Array[index8] = vector3_2 - vector3_6;
    return vector3Array;
  }

  public static void GetBoundsMinMax(Vector3 origin, Vector3 xExtent, Vector3 yExtent, Vector3 zExtent, ref Vector3 min, ref Vector3 max)
  {
    Vector3[] boundCorners = TransformUtil.GetBoundCorners(origin, xExtent, yExtent, zExtent);
    for (int index = 0; index < boundCorners.Length; ++index)
    {
      min.x = Mathf.Min(boundCorners[index].x, min.x);
      min.y = Mathf.Min(boundCorners[index].y, min.y);
      min.z = Mathf.Min(boundCorners[index].z, min.z);
      max.x = Mathf.Max(boundCorners[index].x, max.x);
      max.y = Mathf.Max(boundCorners[index].y, max.y);
      max.z = Mathf.Max(boundCorners[index].z, max.z);
    }
  }

  //public static void SetLocalScaleToWorldDimension(GameObject obj, params WorldDimensionIndex[] dimensions)
  //{
  //  TransformUtil.SetLocalScaleToWorldDimension(obj, (List<GameObject>) null, dimensions);
  //}

  //public static void SetLocalScaleToWorldDimension(GameObject obj, List<GameObject> ignoreMeshes, params WorldDimensionIndex[] dimensions)
  //{
  //    Vector3 localScale = obj.transform.localScale;
  //    OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(obj, ignoreMeshes, true);
  //    for (int i = 0; i < dimensions.Length; i++)
  //    {
  //        float num = orientedBounds.Extents[dimensions[i].Index].magnitude * 2f;
  //        int index;
  //        int expr_50 = index = dimensions[i].Index;
  //        float num2 = localScale[index];
  //        localScale[expr_50] = num2 * ((num > 1.401298E-45f) ? (dimensions[i].Dimension / num) : 0.001f);
  //        if (Mathf.Abs(localScale[dimensions[i].Index]) < 0.001f)
  //        {
  //            localScale[dimensions[i].Index] = 0.001f;
  //        }
  //    }
  //    obj.transform.localScale = localScale;
  //}

  public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, bool includeInactive)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, bool includeInactive)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, bool includeInactive)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, bool includeInactive)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
  }

  public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
  }

  public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
  }

  public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor)
  {
    TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, Vector3.zero, false);
  }

  public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor)
  {
    TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, Vector3.zero, false);
  }

  public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor)
  {
    TransformUtil.SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, Vector3.zero, false);
  }

  public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor)
  {
    TransformUtil.SetPoint(self, selfUnitAnchor, relative, relativeUnitAnchor, Vector3.zero, false);
  }

  public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, false);
  }

  public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, offset, false);
  }

  public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, false);
  }

  public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset)
  {
    TransformUtil.SetPoint(self, selfUnitAnchor, relative, relativeUnitAnchor, offset, false);
  }

  public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive);
  }

  public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, offset, includeInactive);
  }

  public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
  {
    TransformUtil.SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive);
  }

  public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
  {
    //Bounds setPointBounds1 = TransformUtil.ComputeSetPointBounds(self, includeInactive);
    //Bounds setPointBounds2 = TransformUtil.ComputeSetPointBounds(relative, includeInactive);
    //Vector3 worldPoint1 = TransformUtil.ComputeWorldPoint(setPointBounds1, selfUnitAnchor);
    //Vector3 worldPoint2 = TransformUtil.ComputeWorldPoint(setPointBounds2, relativeUnitAnchor);
    //Vector3 translation = new Vector3(worldPoint2.x - worldPoint1.x + offset.x, worldPoint2.y - worldPoint1.y + offset.y, worldPoint2.z - worldPoint1.z + offset.z);
    //self.transform.Translate(translation, Space.World);
  }

  public static Bounds GetBoundsOfChildren(Component c)
  {
    return TransformUtil.GetBoundsOfChildren(c.gameObject, false);
  }

  public static Bounds GetBoundsOfChildren(GameObject go)
  {
    return TransformUtil.GetBoundsOfChildren(go, false);
  }

  public static Bounds GetBoundsOfChildren(Component c, bool includeInactive)
  {
    return TransformUtil.GetBoundsOfChildren(c.gameObject, includeInactive);
  }

  public static Bounds GetBoundsOfChildren(GameObject go, bool includeInactive)
  {
    Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
    if (componentsInChildren.Length == 0)
      return new Bounds(go.transform.position, Vector3.zero);
    Bounds bounds1 = componentsInChildren[0].bounds;
    for (int index = 1; index < componentsInChildren.Length; ++index)
    {
      Bounds bounds2 = componentsInChildren[index].bounds;
      Vector3 max = Vector3.Max(bounds2.max, bounds1.max);
      Vector3 min = Vector3.Min(bounds2.min, bounds1.min);
      bounds1.SetMinMax(min, max);
    }
    return bounds1;
  }

  public static Vector3 Divide(Vector3 v1, Vector3 v2)
  {
    return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
  }

  public static Vector3 Multiply(Vector3 v1, Vector3 v2)
  {
    return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
  }

  public static void SetLocalPosX(GameObject go, float x)
  {
    Transform transform = go.transform;
    transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
  }

  public static void SetLocalPosX(Component component, float x)
  {
    Transform transform = component.transform;
    transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
  }

  public static void SetLocalPosY(GameObject go, float y)
  {
    Transform transform = go.transform;
    transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
  }

  public static void SetLocalPosY(Component component, float y)
  {
    Transform transform = component.transform;
    transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
  }

  public static void SetLocalPosZ(GameObject go, float z)
  {
    Transform transform = go.transform;
    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
  }

  public static void SetLocalPosZ(Component component, float z)
  {
    Transform transform = component.transform;
    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
  }

  public static void SetPosX(GameObject go, float x)
  {
    Transform transform = go.transform;
    transform.position = new Vector3(x, transform.position.y, transform.position.z);
  }

  public static void SetPosX(Component component, float x)
  {
    Transform transform = component.transform;
    transform.position = new Vector3(x, transform.position.y, transform.position.z);
  }

  public static void SetPosY(GameObject go, float y)
  {
    Transform transform = go.transform;
    transform.position = new Vector3(transform.position.x, y, transform.position.z);
  }

  public static void SetPosY(Component component, float y)
  {
    Transform transform = component.transform;
    transform.position = new Vector3(transform.position.x, y, transform.position.z);
  }

  public static void SetPosZ(GameObject go, float z)
  {
    Transform transform = go.transform;
    transform.position = new Vector3(transform.position.x, transform.position.y, z);
  }

  public static void SetPosZ(Component component, float z)
  {
    Transform transform = component.transform;
    transform.position = new Vector3(transform.position.x, transform.position.y, z);
  }

  public static void SetLocalEulerAngleX(GameObject go, float x)
  {
    Transform transform = go.transform;
    transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
  }

  public static void SetLocalEulerAngleX(Component c, float x)
  {
    Transform transform = c.transform;
    transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
  }

  public static void SetLocalEulerAngleY(GameObject go, float y)
  {
    Transform transform = go.transform;
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
  }

  public static void SetLocalEulerAngleY(Component c, float y)
  {
    Transform transform = c.transform;
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
  }

  public static void SetLocalEulerAngleZ(GameObject go, float z)
  {
    Transform transform = go.transform;
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
  }

  public static void SetLocalEulerAngleZ(Component c, float z)
  {
    Transform transform = c.transform;
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
  }

  public static void SetEulerAngleX(GameObject go, float x)
  {
    Transform transform = go.transform;
    transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
  }

  public static void SetEulerAngleX(Component c, float x)
  {
    Transform transform = c.transform;
    transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
  }

  public static void SetEulerAngleY(GameObject go, float y)
  {
    Transform transform = go.transform;
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
  }

  public static void SetEulerAngleY(Component c, float y)
  {
    Transform transform = c.transform;
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
  }

  public static void SetEulerAngleZ(GameObject go, float z)
  {
    Transform transform = go.transform;
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
  }

  public static void SetEulerAngleZ(Component c, float z)
  {
    Transform transform = c.transform;
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
  }

  public static void SetLocalScaleX(Component component, float x)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
  }

  public static void SetLocalScaleX(GameObject go, float x)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
  }

  public static void SetLocalScaleY(Component component, float y)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
  }

  public static void SetLocalScaleY(GameObject go, float y)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
  }

  public static void SetLocalScaleZ(Component component, float z)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
  }

  public static void SetLocalScaleZ(GameObject go, float z)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
  }

  public static void SetLocalScaleXY(Component component, float x, float y)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(x, y, transform.localScale.z);
  }

  public static void SetLocalScaleXY(GameObject go, float x, float y)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(x, y, transform.localScale.z);
  }

  public static void SetLocalScaleXY(Component component, Vector2 v)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(v.x, v.y, transform.localScale.z);
  }

  public static void SetLocalScaleXY(GameObject go, Vector2 v)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(v.x, v.y, transform.localScale.z);
  }

  public static void SetLocalScaleXZ(Component component, float x, float z)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(x, transform.localScale.y, z);
  }

  public static void SetLocalScaleXZ(GameObject go, float x, float z)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(x, transform.localScale.y, z);
  }

  public static void SetLocalScaleXZ(Component component, Vector2 v)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(v.x, transform.localScale.y, v.y);
  }

  public static void SetLocalScaleXZ(GameObject go, Vector2 v)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(v.x, transform.localScale.y, v.y);
  }

  public static void SetLocalScaleYZ(Component component, float y, float z)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(transform.localScale.x, y, z);
  }

  public static void SetLocalScaleYZ(GameObject go, float y, float z)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(transform.localScale.x, y, z);
  }

  public static void SetLocalScaleYZ(Component component, Vector2 v)
  {
    Transform transform = component.transform;
    transform.localScale = new Vector3(transform.localScale.x, v.x, v.y);
  }

  public static void SetLocalScaleYZ(GameObject go, Vector2 v)
  {
    Transform transform = go.transform;
    transform.localScale = new Vector3(transform.localScale.x, v.x, v.y);
  }

  public static void Identity(Component c)
  {
    c.transform.localScale = Vector3.one;
    c.transform.localRotation = Quaternion.identity;
    c.transform.localPosition = Vector3.zero;
  }

  public static void Identity(GameObject go)
  {
    go.transform.localScale = Vector3.one;
    go.transform.localRotation = Quaternion.identity;
    go.transform.localPosition = Vector3.zero;
  }

  public static void CopyLocal(Component destination, Component source)
  {
    TransformUtil.CopyLocal(destination.gameObject, source.gameObject);
  }

  public static void CopyLocal(Component destination, GameObject source)
  {
    TransformUtil.CopyLocal(destination.gameObject, source);
  }

  public static void CopyLocal(GameObject destination, Component source)
  {
    TransformUtil.CopyLocal(destination, source.gameObject);
  }

  public static void CopyLocal(GameObject destination, GameObject source)
  {
    destination.transform.localScale = source.transform.localScale;
    destination.transform.localRotation = source.transform.localRotation;
    destination.transform.localPosition = source.transform.localPosition;
  }

  public static void CopyLocal(Component destination, TransformProps source)
  {
    TransformUtil.CopyLocal(destination.gameObject, source);
  }

  public static void CopyLocal(GameObject destination, TransformProps source)
  {
    destination.transform.localScale = source.scale;
    destination.transform.localRotation = source.rotation;
    destination.transform.localPosition = source.position;
  }

  public static void CopyLocal(TransformProps destination, Component source)
  {
    TransformUtil.CopyLocal(destination, source.gameObject);
  }

  public static void CopyLocal(TransformProps destination, GameObject source)
  {
    destination.scale = source.transform.localScale;
    destination.rotation = source.transform.localRotation;
    destination.position = source.transform.localPosition;
  }

  public static void CopyWorld(Component destination, Component source)
  {
    TransformUtil.CopyWorld(destination.gameObject, source);
  }

  public static void CopyWorld(Component destination, GameObject source)
  {
    TransformUtil.CopyWorld(destination.gameObject, source);
  }

  public static void CopyWorld(GameObject destination, Component source)
  {
    TransformUtil.CopyWorld(destination, source.gameObject);
  }

  public static void CopyWorld(GameObject destination, GameObject source)
  {
    TransformUtil.CopyWorldScale(destination, source);
    destination.transform.rotation = source.transform.rotation;
    destination.transform.position = source.transform.position;
  }

  public static void CopyWorld(Component destination, TransformProps source)
  {
    TransformUtil.CopyWorld(destination.gameObject, source);
  }

  public static void CopyWorld(GameObject destination, TransformProps source)
  {
    TransformUtil.SetWorldScale(destination, source.scale);
    destination.transform.rotation = source.rotation;
    destination.transform.position = source.position;
  }

  public static void CopyWorld(TransformProps destination, Component source)
  {
    TransformUtil.CopyWorld(destination, source.gameObject);
  }

  public static void CopyWorld(TransformProps destination, GameObject source)
  {
    destination.scale = TransformUtil.ComputeWorldScale(source);
    destination.rotation = source.transform.rotation;
    destination.position = source.transform.position;
  }

  public static void CopyWorldScale(Component destination, Component source)
  {
    TransformUtil.CopyWorldScale(destination.gameObject, source.gameObject);
  }

  public static void CopyWorldScale(Component destination, GameObject source)
  {
    TransformUtil.CopyWorldScale(destination.gameObject, source);
  }

  public static void CopyWorldScale(GameObject destination, Component source)
  {
    TransformUtil.CopyWorldScale(destination, source.gameObject);
  }

  public static void CopyWorldScale(GameObject destination, GameObject source)
  {
    Vector3 worldScale = TransformUtil.ComputeWorldScale(source);
    TransformUtil.SetWorldScale(destination, worldScale);
  }

  public static void SetWorldScale(Component destination, Vector3 scale)
  {
    TransformUtil.SetWorldScale(destination.gameObject, scale);
  }

  public static void SetWorldScale(GameObject destination, Vector3 scale)
  {
    if ((Object) destination.transform.parent != (Object) null)
    {
      for (Transform parent = destination.transform.parent; (Object) parent != (Object) null; parent = parent.parent)
        scale.Scale(TransformUtil.Vector3Reciprocal(parent.localScale));
    }
    destination.transform.localScale = scale;
  }

  public static Vector3 ComputeWorldScale(Component c)
  {
    return TransformUtil.ComputeWorldScale(c.gameObject);
  }

  public static Vector3 ComputeWorldScale(GameObject go)
  {
    Vector3 localScale = go.transform.localScale;
    if ((Object) go.transform.parent != (Object) null)
    {
      for (Transform parent = go.transform.parent; (Object) parent != (Object) null; parent = parent.parent)
        localScale.Scale(parent.localScale);
    }
    return localScale;
  }

  public static Vector3 Vector3Reciprocal(Vector3 source)
  {
    Vector3 vector3 = source;
    if ((double) vector3.x != 0.0)
      vector3.x = 1f / vector3.x;
    if ((double) vector3.y != 0.0)
      vector3.y = 1f / vector3.y;
    if ((double) vector3.z != 0.0)
      vector3.z = 1f / vector3.z;
    return vector3;
  }

  public static void OrientTo(GameObject source, GameObject target)
  {
    TransformUtil.OrientTo(source.transform, target.transform);
  }

  public static void OrientTo(GameObject source, Component target)
  {
    TransformUtil.OrientTo(source.transform, target.transform);
  }

  public static void OrientTo(Component source, GameObject target)
  {
    TransformUtil.OrientTo(source.transform, target.transform);
  }

  public static void OrientTo(Component source, Component target)
  {
    TransformUtil.OrientTo(source.transform, target.transform);
  }

  public static void OrientTo(Transform source, Transform target)
  {
    TransformUtil.OrientTo(source, source.transform.position, target.transform.position);
  }

  public static void OrientTo(Transform source, Vector3 sourcePosition, Vector3 targetPosition)
  {
    Vector3 forward = targetPosition - sourcePosition;
    if ((double) forward.sqrMagnitude <= 1.40129846432482E-45)
      return;
    source.rotation = Quaternion.LookRotation(forward);
  }

  public static Vector3 RandomVector3(Vector3 min, Vector3 max)
  {
    return new Vector3()
    {
      x = Random.Range(min.x, max.x),
      y = Random.Range(min.y, max.y),
      z = Random.Range(min.z, max.z)
    };
  }

  public static void AttachAndPreserveLocalTransform(Transform child, Transform parent)
  {
    TransformProps transformProps = new TransformProps();
    TransformUtil.CopyLocal(transformProps, (Component) child);
    child.parent = parent;
    TransformUtil.CopyLocal((Component) child, transformProps);
  }

  public static Vector3 GetAspectRatioDependentPosition(Vector3 aspect3to2, Vector3 aspect16to9)
  {
    float num1 = TransformUtil.PhoneAspectRatioScale();
    float num2 = 1f - num1;
    return aspect16to9 * num1 + aspect3to2 * num2;
  }

  public static float GetAspectRatioDependentValue(float aspect3to2, float aspect16to9)
  {
    float num1 = TransformUtil.PhoneAspectRatioScale();
    float num2 = 1f - num1;
    return (float) ((double) aspect16to9 * (double) num1 + (double) aspect3to2 * (double) num2);
  }

  public static float PhoneAspectRatioScale()
  {
    return (float) (((double) Mathf.Clamp((float) Screen.width / (float) Screen.height, 1.5f, 1.777778f) - 1.5) / 0.277777791023254);
  }
}
