using System;
using UnityEngine;

namespace Game.LudoBoard
{
    public class LudoBoardManager : MonoBehaviour
    {
        [SerializeField] private LudoBoard ludoBoard;
        [SerializeField] private Chip chip;
        public Action<Chip> OnChipClick;

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
        }

        public TileData GetTileData(int index)
        {
            return ludoBoard.GetTileData(index);
        }

        public Chip GetCurrentChip()
        {
            return chip;
        }
    }
}