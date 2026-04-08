using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    // Fairy 소환 버튼
    public class SummonButton : MonoBehaviour
    {
        [SerializeField] SummonRecipeUI summonRecipe;
        [SerializeField] Altar alter;

        public void OnClick()
        {
            if (!summonRecipe.SelectFairy) return;

            alter.SummonFairy(summonRecipe.SelectFairy.fairyData);
        }
    }
}