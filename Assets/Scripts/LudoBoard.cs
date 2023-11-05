using System;
using UnityEngine;

public class LudoBoard : MonoBehaviour
{
    [SerializeField] private TileData[] tileDatas;

    private void Awake()
    {
        for (int i = 0; i < tileDatas.Length; i++)
        {
            tileDatas[i].Index = i;
        }
    }

    public Vector3 GetTilePosition(int index)
    {
        index %= tileDatas.Length;
        return tileDatas[index].GetPosition();
    }

    public TileData GetTileData(int index)
    {
        index %= tileDatas.Length;
        return tileDatas[index];
    }
}