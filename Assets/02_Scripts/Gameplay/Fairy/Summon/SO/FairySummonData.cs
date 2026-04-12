using Elemental.Gameplay.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Fairy
{
    [CreateAssetMenu(fileName = "FairySummonData", menuName = "Scriptable Objects/Fairy/FairySummonData")]
    public class FairySummonData : ScriptableObject
    {
        public Sprite fairySprite;
        public HarvestFairyData fairyData;
        public List<RecipeIngredient> ingredient;
    }
}