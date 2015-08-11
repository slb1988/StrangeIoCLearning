// Decompiled with JetBrains decompiler
// Type: SceneUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SceneUtils
{
  public static void SetLayer(GameObject go, int layer)
  {
    go.layer = layer;
    IEnumerator enumerator = go.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
        SceneUtils.SetLayer(((Component) enumerator.Current).gameObject, layer);
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
  }

  public static void SetLayer(Component c, int layer)
  {
    SceneUtils.SetLayer(c.gameObject, layer);
  }

  public static void SetLayer(GameObject go, GameLayer layer)
  {
    SceneUtils.SetLayer(go, (int) layer);
  }

  public static void SetLayer(Component c, GameLayer layer)
  {
    SceneUtils.SetLayer(c.gameObject, (int) layer);
  }

  public static void ReplaceLayer(GameObject parentObject, GameLayer newLayer, GameLayer oldLayer)
  {
    if ((GameLayer) parentObject.layer == oldLayer)
      parentObject.layer = (int) newLayer;
    IEnumerator enumerator = parentObject.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        Transform transform = (Transform) enumerator.Current;
        if ((GameLayer) transform.gameObject.layer == oldLayer)
          SceneUtils.SetLayer(transform.gameObject, newLayer);
      }
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
  }

  public static string LayerMaskToString(LayerMask mask)
  {
    if ((int) mask == 0)
      return "[NO LAYERS]";
    StringBuilder stringBuilder = new StringBuilder("[");
    foreach (int num in Enum.GetValues(typeof (GameLayer)))
    {
      GameLayer gameLayer = (GameLayer) num;
      if (((int) mask & GameLayerExtensions.LayerBit(gameLayer)) != 0)
      {
        stringBuilder.Append((object) gameLayer);
        stringBuilder.Append(", ");
      }
    }
    if (stringBuilder.Length == 1)
      return "[NO LAYERS]";
    stringBuilder.Remove(stringBuilder.Length - 2, 2);
    stringBuilder.Append("]");
    return stringBuilder.ToString();
  }

  public static void SetRenderQueue(GameObject go, int renderQueue)
  {
    foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
    {
      if (!((UnityEngine.Object) renderer.material == (UnityEngine.Object) null))
        renderer.material.renderQueue = renderQueue;
    }
  }

  public static GameObject FindChild(GameObject parentObject, string name)
  {
    if (parentObject.name.Equals(name, StringComparison.OrdinalIgnoreCase))
      return parentObject;
    IEnumerator enumerator = parentObject.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        GameObject child = SceneUtils.FindChild(((Component) enumerator.Current).gameObject, name);
        if ((bool) ((UnityEngine.Object) child))
          return child;
      }
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
    return (GameObject) null;
  }

  public static GameObject FindChildBySubstring(GameObject parentObject, string substr)
  {
    if (parentObject.name.IndexOf(substr, StringComparison.OrdinalIgnoreCase) >= 0)
      return parentObject;
    IEnumerator enumerator = parentObject.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        GameObject childBySubstring = SceneUtils.FindChildBySubstring(((Component) enumerator.Current).gameObject, substr);
        if ((bool) ((UnityEngine.Object) childBySubstring))
          return childBySubstring;
      }
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
    return (GameObject) null;
  }

  public static Transform FindFirstChild(Transform parent)
  {
    IEnumerator enumerator = parent.GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
        return (Transform) enumerator.Current;
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
    return (Transform) null;
  }

  public static bool IsAncestorOf(GameObject ancestor, GameObject descendant)
  {
    return SceneUtils.IsAncestorOf((Component) ancestor.transform, (Component) descendant.transform);
  }

  public static bool IsAncestorOf(Component ancestor, Component descendant)
  {
    for (Transform transform = descendant.transform; (UnityEngine.Object) transform != (UnityEngine.Object) null; transform = transform.parent)
    {
      if ((UnityEngine.Object) transform == (UnityEngine.Object) ancestor.transform)
        return true;
    }
    return false;
  }

  public static bool IsDescendantOf(GameObject descendant, GameObject ancestor)
  {
    return SceneUtils.IsDescendantOf((Component) descendant.transform, (Component) ancestor.transform);
  }

  public static bool IsDescendantOf(GameObject descendant, Component ancestor)
  {
    return SceneUtils.IsDescendantOf((Component) descendant.transform, (Component) ancestor.transform);
  }

  public static bool IsDescendantOf(Component descendant, GameObject ancestor)
  {
    return SceneUtils.IsDescendantOf((Component) descendant.transform, (Component) ancestor.transform);
  }

  public static bool IsDescendantOf(Component descendant, Component ancestor)
  {
    if ((UnityEngine.Object) descendant == (UnityEngine.Object) ancestor)
      return true;
    IEnumerator enumerator = ancestor.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        Transform transform = (Transform) enumerator.Current;
        if (SceneUtils.IsDescendantOf(descendant, (Component) transform))
          return true;
      }
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
    return false;
  }

  public static T FindComponentInParents<T>(Component child) where T : Component
  {
    if ((UnityEngine.Object) child == (UnityEngine.Object) null)
      return (T) null;
    for (Transform parent = child.transform.parent; (UnityEngine.Object) parent != (UnityEngine.Object) null; parent = parent.parent)
    {
      T component = parent.GetComponent<T>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        return component;
    }
    return (T) null;
  }

  public static T FindComponentInParents<T>(GameObject child) where T : Component
  {
    if ((UnityEngine.Object) child == (UnityEngine.Object) null)
      return (T) null;
    return SceneUtils.FindComponentInParents<T>((Component) child.transform);
  }

  public static T FindComponentInThisOrParents<T>(Component start) where T : Component
  {
    if ((UnityEngine.Object) start == (UnityEngine.Object) null)
      return (T) null;
    for (Transform transform = start.transform; (UnityEngine.Object) transform != (UnityEngine.Object) null; transform = transform.parent)
    {
      T component = transform.GetComponent<T>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        return component;
    }
    return (T) null;
  }

  public static T FindComponentInThisOrParents<T>(GameObject start) where T : Component
  {
    if ((UnityEngine.Object) start == (UnityEngine.Object) null)
      return (T) null;
    return SceneUtils.FindComponentInThisOrParents<T>((Component) start.transform);
  }

  public static T GetComponentInChildrenOnly<T>(GameObject go) where T : Component
  {
    if ((UnityEngine.Object) go != (UnityEngine.Object) null)
    {
      IEnumerator enumerator = go.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          T componentInChildren = ((Component) enumerator.Current).GetComponentInChildren<T>();
          if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
            return componentInChildren;
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }
    return (T) null;
  }

  public static T GetComponentInChildrenOnly<T>(Component c) where T : Component
  {
    if ((UnityEngine.Object) c == (UnityEngine.Object) null)
      return (T) null;
    return SceneUtils.GetComponentInChildrenOnly<T>(c.gameObject);
  }

  public static T[] GetComponentsInChildrenOnly<T>(Component c) where T : Component
  {
    if ((UnityEngine.Object) c == (UnityEngine.Object) null)
      return new T[0];
    return SceneUtils.GetComponentsInChildrenOnly<T>(c.gameObject);
  }

  public static T[] GetComponentsInChildrenOnly<T>(GameObject go) where T : Component
  {
    return SceneUtils.GetComponentsInChildrenOnly<T>(go, false);
  }

  public static T[] GetComponentsInChildrenOnly<T>(Component c, bool includeInactive) where T : Component
  {
    if ((UnityEngine.Object) c == (UnityEngine.Object) null)
      return new T[0];
    return SceneUtils.GetComponentsInChildrenOnly<T>(c.gameObject, includeInactive);
  }

  public static T[] GetComponentsInChildrenOnly<T>(GameObject go, bool includeInactive) where T : Component
  {
    if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
      return new T[0];
    List<T> list = new List<T>();
    IEnumerator enumerator = go.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        T[] componentsInChildren = ((Component) enumerator.Current).GetComponentsInChildren<T>(includeInactive);
        list.AddRange((IEnumerable<T>) componentsInChildren);
      }
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
    return list.ToArray();
  }

  public static GameObject FindTopParent(Component c)
  {
    Transform transform = c.transform;
    while ((UnityEngine.Object) transform.parent != (UnityEngine.Object) null)
      transform = transform.parent;
    return transform.gameObject;
  }

  public static GameObject FindTopParent(GameObject go)
  {
    return SceneUtils.FindTopParent((Component) go.transform);
  }

  public static GameObject FindChildByTag(GameObject go, string tag)
  {
    Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>();
    if (componentsInChildren == null)
      return (GameObject) null;
    foreach (Transform transform in componentsInChildren)
    {
      if (transform.CompareTag(tag))
        return transform.gameObject;
    }
    return (GameObject) null;
  }

  public static void EnableRenderers(Component c, bool enable)
  {
    SceneUtils.EnableRenderers(c.gameObject, enable);
  }

  public static void EnableRenderers(GameObject go, bool enable)
  {
    SceneUtils.EnableRenderers(go, enable, false);
  }

  public static void EnableRenderers(Component c, bool enable, bool includeInactive)
  {
    SceneUtils.EnableRenderers(c.gameObject, enable, false);
  }

  public static void EnableRenderers(GameObject go, bool enable, bool includeInactive)
  {
    Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
    if (componentsInChildren == null)
      return;
    foreach (Renderer renderer in componentsInChildren)
      renderer.enabled = enable;
  }

  public static void EnableColliders(Component c, bool enable)
  {
    SceneUtils.EnableColliders(c.gameObject, enable);
  }

  public static void EnableColliders(GameObject go, bool enable)
  {
    Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
    if (componentsInChildren == null)
      return;
    foreach (Collider collider in componentsInChildren)
      collider.enabled = enable;
  }

  public static void EnableRenderersAndColliders(Component c, bool enable)
  {
    SceneUtils.EnableRenderersAndColliders(c.gameObject, enable);
  }

  public static void EnableRenderersAndColliders(GameObject go, bool enable)
  {
    Collider component1 = go.GetComponent<Collider>();
    if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      component1.enabled = enable;
    Renderer component2 = go.GetComponent<Renderer>();
    if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
      component2.enabled = enable;
    IEnumerator enumerator = go.transform.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
        SceneUtils.EnableRenderersAndColliders(((Component) enumerator.Current).gameObject, enable);
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
  }

  public static void ResizeBoxCollider(GameObject go, Component worldCorner1, Component worldCorner2)
  {
    SceneUtils.ResizeBoxCollider((Component) go.collider, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(GameObject go, GameObject worldCorner1, Component worldCorner2)
  {
    SceneUtils.ResizeBoxCollider((Component) go.collider, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(GameObject go, Component worldCorner1, GameObject worldCorner2)
  {
    SceneUtils.ResizeBoxCollider((Component) go.collider, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(GameObject go, GameObject worldCorner1, GameObject worldCorner2)
  {
    SceneUtils.ResizeBoxCollider((Component) go.collider, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(Component c, Component worldCorner1, Component worldCorner2)
  {
    SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(Component c, GameObject worldCorner1, Component worldCorner2)
  {
    SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(Component c, Component worldCorner1, GameObject worldCorner2)
  {
    SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(Component c, GameObject worldCorner1, GameObject worldCorner2)
  {
    SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
  }

  public static void ResizeBoxCollider(GameObject go, Bounds bounds)
  {
    SceneUtils.ResizeBoxCollider((Component) go.collider, bounds.min, bounds.max);
  }

  public static void ResizeBoxCollider(Component c, Bounds bounds)
  {
    SceneUtils.ResizeBoxCollider((Component) c.collider, bounds.min, bounds.max);
  }

  public static void ResizeBoxCollider(GameObject go, Vector3 worldCorner1, Vector3 worldCorner2)
  {
    SceneUtils.ResizeBoxCollider((Component) go.collider, worldCorner1, worldCorner2);
  }

  public static void ResizeBoxCollider(Component c, Vector3 worldCorner1, Vector3 worldCorner2)
  {
    Vector3 lhs = c.transform.InverseTransformPoint(worldCorner1);
    Vector3 rhs = c.transform.InverseTransformPoint(worldCorner2);
    Vector3 vector3_1 = Vector3.Min(lhs, rhs);
    Vector3 vector3_2 = Vector3.Max(lhs, rhs);
    BoxCollider component = c.GetComponent<BoxCollider>();
    component.center = 0.5f * (vector3_1 + vector3_2);
    component.size = vector3_2 - vector3_1;
  }

  public static Transform CreateBone(GameObject template)
  {
    GameObject destination = new GameObject(string.Format("{0}Bone", (object) template.name));
    destination.transform.parent = template.transform.parent;
    TransformUtil.CopyLocal(destination, template);
    return destination.transform;
  }

  public static Transform CreateBone(Component template)
  {
    return SceneUtils.CreateBone(template.gameObject);
  }

  public static void SetHideFlags(UnityEngine.Object obj, HideFlags flags)
  {
  }
}
