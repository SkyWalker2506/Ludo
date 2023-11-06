using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.LudoBoard
{
    public class Chip : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AssetReferenceSprite chipSpriteReference; 
        [SerializeField] private Team team;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Action OnClick;
        public int InitialIndex { get; private set; }

        public int CurrentIndex{ get; private set; }
        public bool IsInteractable { get; set; } = true;

        private void Awake()
        {
            SetStartIndex();
            chipSpriteReference.LoadAssetAsync().Completed += SetChipSprite;
        }

        private void SetChipSprite(AsyncOperationHandle<Sprite> spriteAsset)
        {
            spriteRenderer.sprite = (Sprite)chipSpriteReference.Asset;
            switch (team)
            {
                case Team.Red:
                    spriteRenderer.color=Color.red; 
                    break;
                case Team.Green:
                    spriteRenderer.color=Color.green; 
                    break;
                case Team.Yellow:
                    spriteRenderer.color=Color.yellow; 
                    break;
                case Team.Blue:
                    spriteRenderer.color=Color.blue; 
                    break;
            }
        }
        
        public void SetStartIndex()
        {
            InitialIndex = (int)team * 13;
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
}