// Decompiled with JetBrains decompiler
// Type: OrientedBounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OrientedBounds
{
  public Vector3[] Extents;
  public Vector3 Origin;
  public Vector3 CenterOffset;

  public Vector3 GetTrueCenterPosition()
  {
    return this.Origin + this.CenterOffset;
  }
}
