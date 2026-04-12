using Elemental.Gameplay.item;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    // Fairy 소환 버튼
    public class SummonButton : MonoBehaviour
    {
        [SerializeField] SummonRecipeUI summonRecipe;
        [SerializeField] Inventory inventory;
        [SerializeField] Altar altar;

        ICraftable Craft => inventory;

        public void OnClick()
        {
            if (!summonRecipe.SelectFairy) return;
            altar.SummonFairy(summonRecipe.SelectFairy.fairyData);

            //Test: Fairy 소환
            //if (Craft.TryCraft(summonRecipe.SelectFairy.ingredient))
            //{
            //    altar.SummonFairy(summonRecipe.SelectFairy.fairyData);
            //    Debug.Log("소환됨");
            //}
            //else
            //{
            //    Debug.Log("재료 부족");
            //}
        }
    }
}