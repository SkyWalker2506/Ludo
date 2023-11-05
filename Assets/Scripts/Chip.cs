using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chip : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Team team;
    public Action OnClick;

    public int CurrentIndex { get; private set; }

    private void Awake()
    {
        SetStartIndex();
    }

    public void SetStartIndex()
    {
        CurrentIndex = (int)team * 13;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
        Debug.Log("Click");
    }

    public void Move(TileData tileData, float moveTime)
    {
        transform.position = tileData.GetPosition();
        CurrentIndex = tileData.Index;
    }
}
