using System.Collections.Generic;
using UnityEngine;
using Elemental.Gameplay.item;

namespace Elemental.Gameplay.Fairy.Summon
{
    [CreateAssetMenu(fileName = "FairySummonData", menuName = "Scriptable Objects/Fairy/FairySummonData")]
    public class FairySummonData : ScriptableObject
    {
        public Sprite fairySprite;
        public HarvestFairyData fairyData;
        public List<RecipeIngredient> ingredient;
    }
}