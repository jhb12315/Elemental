using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    public class SummonButton : MonoBehaviour
    {
        [SerializeField] SummonRecipeUI summonRecipe;
        [SerializeField] Alter alter;

        public void OnClick()
        {
            if (!summonRecipe.SelectFairy) return;

            alter.SummonFairy(summonRecipe.SelectFairy);
        }
    }
}