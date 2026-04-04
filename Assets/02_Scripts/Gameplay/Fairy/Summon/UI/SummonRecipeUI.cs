using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    public class SummonRecipeUI : MonoBehaviour
    {
        SummonIngredientUI[] summonIngredients;

        public GameObject SelectFairy { get; private set; }
        

        void Start()
        {
            summonIngredients = GetComponentsInChildren<SummonIngredientUI>();
           
            foreach (var ingredient in summonIngredients)
            {
                ingredient.gameObject.SetActive(false);
            }
        }

        public void OnFairyClicked(FairySummonData fairySummonData)
        {
            SummonIngredient[] ingredient = fairySummonData.ingredient;
            SelectFairy = fairySummonData.fairy;

            for (int i = 0; i < ingredient.Length; i++)
            {
                summonIngredients[i].gameObject.SetActive(true);
                summonIngredients[i].SetIngredient(ingredient[i].item.sprite, ingredient[i].count);
            }
        }
    }
}