// Decompiled with JetBrains decompiler
// Type: DrawWireframe
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DrawWireframe : MonoBehaviour
{
  private void OnPreRender()
  {
    GL.wireframe = true;
  }

  private void OnPostRender()
  {
    GL.wireframe = false;
  }
}
