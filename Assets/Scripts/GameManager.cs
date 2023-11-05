using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LudoBoardManager boardManager;
    private int lastRolledNumber;
    private float moveInterval = 1;
    private void OnEnable()
    {
        uiManager?.OnRollDice?.AddListener(OnRollDice);
        boardManager.OnChipClick += (c)=>OnChipClicked(c);
    }


    private void OnDisable()
    {
        uiManager?.OnRollDice?.RemoveListener(OnRollDice);
        boardManager.OnChipClick -= (c)=>OnChipClicked(c);
    }

    async void OnRollDice()
    {
        uiManager.ImitateDiceRoll();
        lastRolledNumber = await RandomNumberFetcher.FetchRandomNumber();
        uiManager.ShowDice(lastRolledNumber);
        uiManager.ToggleDiceButton(false);
    }
    
    private async UniTask OnChipClicked(Chip chip)
    {
        int moveCount = 0;
        Debug.Log("OnChipClicked");

        while (moveCount < lastRolledNumber)
        {
            await UniTask.WaitForSeconds(moveInterval);
            chip.Move(boardManager.GetTileData(chip.CurrentIndex+1), moveInterval);
            moveCount++;
        }
        uiManager.ToggleDiceButton(true);
    }
}