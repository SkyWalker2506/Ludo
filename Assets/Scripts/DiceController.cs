using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    [SerializeField] private Image diceImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite[] diceSprites;
    private bool immitateDiceRoll;
    private int min = 1, max = 6, previousValue, currentValue;
    private float changeInterval = .2f;
    
    public async UniTaskVoid ImitateDiceRoll()
    {
        immitateDiceRoll = true;
        previousValue = 0;
        currentValue = 0;
        SetDiceValue(0);
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
    }
    
    void SetDiceValue(int value)
    {
        if (value >= min && value <= max)
        {
            previousValue = currentValue;
            currentValue = value;
            diceImage.sprite = diceSprites[value - min];
        }
        else
        {
            diceImage.sprite = defaultSprite;
        }
    }
    
}