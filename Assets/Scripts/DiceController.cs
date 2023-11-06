using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Game.UI
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] private AssetReferenceSprite defaultSpriteReference; 
        [SerializeField] private AssetReferenceSprite[] diceSpritesReference;

        [SerializeField] private Image diceImage;
        private bool immitateDiceRoll;
        private int min = 1, max = 6, previousValue, currentValue;
        private float changeInterval = .2f;


        private void Awake()
        {
            LoadSpriteReferences();
        }

        void LoadSpriteReferences()
        {
            defaultSpriteReference.LoadAssetAsync().Completed += (a) => SetDefaultSprite();
            foreach (var diceSpriteReference in diceSpritesReference)
            {
                diceSpriteReference.LoadAssetAsync();
            }
        }

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
                SetDiceSprite(value);
            }
            else
            {
                SetDefaultSprite();
            }
        }

        private void SetDiceSprite(int value)
        {
            diceImage.sprite = (Sprite)diceSpritesReference[value - min].Asset;
        }

        private void SetDefaultSprite()
        {
            diceImage.sprite = (Sprite)defaultSpriteReference.Asset;
        }
    }       
}
