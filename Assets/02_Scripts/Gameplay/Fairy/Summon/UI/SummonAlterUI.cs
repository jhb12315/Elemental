using Elemental.Framework.UI;
using UnityEngine;

namespace Elemental.Gameplay.Fairy
{
    public class SummonAlterUI : MonoBehaviour, IUIPanel
    {
        SummonRecipeUI recipeUI;

        void Awake()
        {
            recipeUI = GetComponentInChildren<SummonRecipeUI>();
            recipeUI.OnCollected += InitalizeEnd;
        }

        void InitalizeEnd()
        {
            gameObject.SetActive(false);
            recipeUI.OnCollected -= OffUI;
        }

        public void OffUI()
        {
            gameObject.SetActive(false);
        }

        public void OnUI()
        {
            gameObject.SetActive(true);
        }
    }
}
