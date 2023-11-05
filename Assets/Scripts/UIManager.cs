using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button resetChipButton;
    [SerializeField] private Button rollDiceButton;
    public Button.ButtonClickedEvent OnResetChip => resetChipButton?.onClick;
    public Button.ButtonClickedEvent OnRollDice => rollDiceButton?.onClick;
    [SerializeField] private DiceController diceController;


    public void ImitateDiceRoll()
    {
        diceController.ImitateDiceRoll().Forget();
    }

    public void ShowDice(int number)
    {
        diceController.ShowDice(number).Forget();

    }

    public void ToggleDiceButton(bool isInteractable)
    {
        rollDiceButton.interactable=isInteractable;
    }
}