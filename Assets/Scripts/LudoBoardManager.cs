using System;
using UnityEngine;

public class LudoBoardManager : MonoBehaviour
{
    [SerializeField] private LudoBoard ludoBoard;
    [SerializeField] private Chip chip;
    public Action<Chip> OnChipClick;
    private void Awake()
    {
        MoveChipToInitialPosition(chip);
    }

    private void MoveChipToInitialPosition(Chip c)
    {
        c.transform.position = ludoBoard.GetTilePosition(c.CurrentIndex);
    }

    private void OnEnable()
    {
        chip.OnClick += OnChipClicked;
    }

    private void OnDisable()
    {
        chip.OnClick -= OnChipClicked;
    }
    
    private void OnChipClicked()
    {
        OnChipClick?.Invoke(chip);
        Debug.Log("OnChipClicked");
    }


    public Vector3 GetPosition(int index)
    {
        return ludoBoard.GetTilePosition(index);
    }

    public TileData GetTileData(int index)
    {
        return ludoBoard.GetTileData(index);
    }
}