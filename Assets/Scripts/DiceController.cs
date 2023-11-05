using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private TMP_Text diceText;
    private bool immitateDiceRoll;
    private int min = 1, max = 6, previousValue, currentValue;
    private float changeInterval = .2f;

    public async UniTaskVoid ImitateDiceRoll()
    {
        immitateDiceRoll = true;
        previousValue = 0;
        currentValue = 0;
        int value;
        while (immitateDiceRoll)
        {
            do
            {
                await UniTask.NextFrame();
                value = Random.Range(min, max+1);
            } while (previousValue == value);

            SetDiceValue(value);
            await UniTask.WaitForSeconds(changeInterval);
        }
    }

    public async UniTaskVoid ShowDice(int number)
    {
        immitateDiceRoll = false;
        if (currentValue == number)
        {
            int value;

            do
            {
                await UniTask.NextFrame();
                value = Random.Range(min, max+1);
            } while (number == value);

            SetDiceValue(value);
            await UniTask.WaitForSeconds(changeInterval);
        }
        SetDiceValue(number);
        diceText.SetText(currentValue.ToString());
    }
    
    void SetDiceValue(int value)
    {
        if (value >= min&&value <= max)
        {
            previousValue = currentValue;
            currentValue = value;
            diceText.SetText(currentValue.ToString());   
        }
        else
        {
            diceText.SetText("?");   
        }

    }
}