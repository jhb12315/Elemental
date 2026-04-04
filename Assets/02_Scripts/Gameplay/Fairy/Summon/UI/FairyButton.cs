using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    public class FairyButton : MonoBehaviour
    {
        [SerializeField] FairySummonData fairySummonData;
        [SerializeField] SummonRecipeUI recipeUI;

        public void OnClick()
        {
            recipeUI.OnFairyClicked(fairySummonData);
        }
    }
}