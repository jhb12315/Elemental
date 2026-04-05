using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    // 소환 실행 버튼
    public class SummonButton : MonoBehaviour
    {
        [SerializeField] SummonRecipeUI summonRecipe;
        [SerializeField] Alter alter;

        public void OnClick()
        {
            if (!summonRecipe.SelectFairy) return;

            alter.SummonFairy(summonRecipe.SelectFairy.fairyData);
        }
    }
}