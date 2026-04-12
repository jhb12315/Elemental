using UnityEngine;
using UnityEngine.UI;

namespace Elemental.Gameplay.Fairy
{
    // 페어리 선택 버튼
    public class FairyButton : MonoBehaviour
    {
        [SerializeField] FairySummonData fairySummonData;
        [SerializeField] SummonRecipeUI recipeUI;
        Image image;

        void Awake()
        {
            image = GetComponent<Image>();
            image.sprite = fairySummonData.fairySprite;
        }

        public void OnClick()
        {
            recipeUI.OnFairyClicked(fairySummonData);
        }
    }
}