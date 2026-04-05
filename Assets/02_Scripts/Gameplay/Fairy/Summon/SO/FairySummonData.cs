using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Summon
{
    [Serializable]
    public class SummonIngredient
    {
        public ItemData item;
        public int count;
    }

    [CreateAssetMenu(fileName = "FairySummonData", menuName = "Scriptable Objects/Fairy/FairySummonData")]
    public class FairySummonData : ScriptableObject
    {
        public Sprite fairySprite;
        public HarvestFairyData fairyData;
        public List<SummonIngredient> ingredient;
    }
}