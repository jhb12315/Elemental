using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    // 소환 재료 표시 UI 관리
    public class SummonRecipeUI : MonoBehaviour
    {
        SummonIngredientUI[] summonIngredientUI;

        public FairySummonData SelectFairy { get; private set; }

        void Start()
        {
            summonIngredientUI = GetComponentsInChildren<SummonIngredientUI>();
           
            foreach (var ingredient in summonIngredientUI)
            {
                ingredient.gameObject.SetActive(false);
            }
        }

        public void OnFairyClicked(FairySummonData fairySummonData)
        {
            SelectFairy = fairySummonData;

            for (int i = 0; i < fairySummonData.ingredient.Count; i++)
            {
                SummonIngredient ingredient = fairySummonData.ingredient[i];
                summonIngredientUI[i].gameObject.SetActive(true);
                summonIngredientUI[i].SetIngredient(ingredient.item.sprite, ingredient.count);
            }

            // 비어있는 summonIngredientUI 비활성화
            for (int i = fairySummonData.ingredient.Count; i < summonIngredientUI.Length; i++)
            {
                summonIngredientUI[i].gameObject.SetActive(false);
            }
        }
    }
}