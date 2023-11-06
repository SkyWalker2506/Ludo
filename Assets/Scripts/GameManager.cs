using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.LudoBoard;
using Game.UI;
using RandomNumber;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LudoBoardManager boardManager;
        [SerializeField] private float moveDuration = 1;
        private int lastRolledNumber;

        private void Start()
        {
            MoveChipToInitialPosition(boardManager.GetCurrentChip());
        }

        private void OnEnable()
        {
            uiManager?.OnRollDice?.AddListener(OnRollDice);
            uiManager?.OnResetChip?.AddListener(OnResetChip);
            boardManager.OnChipClick += (c)=>OnChipClicked(c);
        }

        private void OnDisable()
        {
            uiManager?.OnRollDice?.RemoveListener(OnRollDice);
            boardManager.OnChipClick -= (c)=>OnChipClicked(c);
        }

        async void OnRollDice()
        {
            uiManager.ToggleDiceButton(false);
            uiManager.ImitateDiceRoll();
            var tasks = new List<UniTask>();

            async UniTask FetchTask()
            {
                lastRolledNumber = await RandomNumberFetcher.FetchRandomNumber();
            }
            tasks.Add(FetchTask());
            tasks.Add(UniTask.WaitForSeconds(2));
            await UniTask.WhenAll(tasks);
            uiManager.ShowDice(lastRolledNumber);
        }
        
        private void OnResetChip()
        {
            var chip =  boardManager.GetCurrentChip();
            MoveChipToInitialPosition(chip);
            ResetAfterMove(chip);
        }

        private void MoveChipToInitialPosition(Chip chip)
        {
            chip.SetStartIndex();
            chip.Move(boardManager.GetTileData(chip.InitialIndex), moveDuration);
        }

        private async UniTask OnChipClicked(Chip chip)
        {
            if (!chip.IsInteractable || lastRolledNumber<=0)
            {
                return;
            }
            chip.IsInteractable = false;

            int moveCount = 0;
            while (moveCount < lastRolledNumber)
            {
                chip.Move(boardManager.GetTileData(chip.CurrentIndex+1), moveDuration);
                moveCount++;
                await UniTask.WaitForSeconds(moveDuration);
            }

            ResetAfterMove(chip);
        }

        private void ResetAfterMove(Chip chip)
        {
            lastRolledNumber = 0;
            chip.IsInteractable = true;
            uiManager.ShowDice(0);
            uiManager.ToggleDiceButton(true);
        }
    }
}