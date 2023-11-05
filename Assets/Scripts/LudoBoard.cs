using UnityEngine;
using UnityEngine.Serialization;

public class LudoBoard : MonoBehaviour
{
    [FormerlySerializedAs("tilePositions")] [SerializeField] private TileData[] tileDatas;

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