using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chip : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Team team;
    public Action OnClick;
    public int CurrentIndex { get; private set; }
    public bool IsInteractable { get; set; } = true;

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
    }

    public void Move(TileData tileData, float duration)
    {
        transform.DOMove(tileData.GetPosition(), duration);
        CurrentIndex = tileData.Index;
    }
}
