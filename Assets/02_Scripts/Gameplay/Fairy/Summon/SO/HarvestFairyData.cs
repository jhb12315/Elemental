using Elemental.Gameplay.Resource;
using UnityEngine;

namespace Elemental.Gameplay.Fairy
{
    [CreateAssetMenu(fileName = "HarvestFairyData", menuName = "Scriptable Objects/Fairy/HarvestFairyData")]
    public class HarvestFairyData : ScriptableObject
    {
        public string fairyName;
        public GameObject fairyPrefab;
        public ResourceTag resourceTag;
        public float harvestTimeInterval;
        public float speed;
    }
}