using System;
using UnityEngine;

[Serializable]
public struct TileData
{
    public float X;
    public float Y;
    public int Index;

    public Vector3 GetPosition()
    {
       return new Vector3(X, Y, 0); 
    }
}