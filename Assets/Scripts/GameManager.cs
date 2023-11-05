using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LudoBoardManager boardManager;
    [SerializeField] private float moveDuration = 1;
    private int lastRolledNumber;
    
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
        lastRolledNumber = await RandomNumberFetcher.FetchRandomNumber();
        uiManager.ShowDice(lastRolledNumber);
    }
    
    private void OnResetChip()
    {
        var chip =  boardManager.GetCurrentChip();
        chip.SetStartIndex();
        chip.Move(boardManager.GetTileData(chip.CurrentIndex), moveDuration);
    }
    
    private async UniTask OnChipClicked(Chip chip)
    {
        if (!chip.IsInteractable)
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

        lastRolledNumber = 0;
        chip.IsInteractable = true;
        uiManager.ToggleDiceButton(true);
    }
}