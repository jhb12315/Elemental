using System;
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
        public GameObject fairy;
        public SummonIngredient[] ingredient;
    }
}