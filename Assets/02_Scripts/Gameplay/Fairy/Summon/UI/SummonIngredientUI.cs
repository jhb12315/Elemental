using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elemental.Gameplay.Fairy
{
    // 소환 재료 표시 CurrentOpenUI
    public class SummonIngredientUI : MonoBehaviour
    {
        Image ingredientImage;
        TextMeshProUGUI countText;

        void Awake()
        {
            ingredientImage = GetComponent<Image>();
            countText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetIngredient(Sprite sprite, int count)
        {
            ingredientImage.sprite = sprite;
            countText.text = $"{count}";
        }
    }
}