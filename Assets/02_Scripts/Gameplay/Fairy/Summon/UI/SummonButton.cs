using Elemental.Gameplay.item;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    // Fairy 소환 버튼
    public class SummonButton : MonoBehaviour
    {
        [SerializeField] SummonRecipeUI summonRecipe;
        [SerializeField] Inventory inventory;
        [SerializeField] Altar alter;

        ICraftable craft;

        void Awake()
        {
            craft = inventory;
        }

        public void OnClick()
        {
            if (!summonRecipe.SelectFairy) return;
            alter.SummonFairy(summonRecipe.SelectFairy.fairyData);

            // Test : Fairy 소환
            //if (craft.TryCraft(summonRecipe.SelectFairy.ingredient))
            //{
            //    alter.SummonFairy(summonRecipe.SelectFairy.fairyData);
            //    Debug.Log("소환됨");
            //}
            //else
            //{
            //    Debug.Log("재료 부족");
            //}
        }
    }
}