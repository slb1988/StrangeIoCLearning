// Decompiled with JetBrains decompiler
// Type: HashPrimeNumbers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C928D2E8-D5BB-441E-8511-9C800A221D56
// Assembly location: E:\game\Hearthstone\Hearthstone_Data\Managed\Assembly-CSharp.dll

using System;

internal static class HashPrimeNumbers
{
  private static readonly int[] primeTbl = new int[34]
  {
    11,
    19,
    37,
    73,
    109,
    163,
    251,
    367,
    557,
    823,
    1237,
    1861,
    2777,
    4177,
    6247,
    9371,
    14057,
    21089,
    31627,
    47431,
    71143,
    106721,
    160073,
    240101,
    360163,
    540217,
    810343,
    1215497,
    1823231,
    2734867,
    4102283,
    6153409,
    9230113,
    13845163
  };

  public static bool TestPrime(int x)
  {
    if ((x & 1) == 0)
      return x == 2;
    int num1 = (int) Math.Sqrt((double) x);
    int num2 = 3;
    while (num2 < num1)
    {
      if (x % num2 == 0)
        return false;
      num2 += 2;
    }
    return true;
  }

  public static int CalcPrime(int x)
  {
    int x1 = (x & -2) - 1;
    while (x1 < int.MaxValue)
    {
      if (HashPrimeNumbers.TestPrime(x1))
        return x1;
      x1 += 2;
    }
    return x;
  }

  public static int ToPrime(int x)
  {
    for (int index = 0; index < HashPrimeNumbers.primeTbl.Length; ++index)
    {
      if (x <= HashPrimeNumbers.primeTbl[index])
        return HashPrimeNumbers.primeTbl[index];
    }
    return HashPrimeNumbers.CalcPrime(x);
  }
}
