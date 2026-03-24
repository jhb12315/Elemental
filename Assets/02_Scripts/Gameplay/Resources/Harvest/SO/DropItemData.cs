using UnityEngine;

namespace Elemental.Gameplay.Resource.Drop
{
    [CreateAssetMenu(fileName = "DropItemData", menuName = "Scriptable Objects/Resource/Drop/DropItemData", order = 4)]
    public class DropItemData : ScriptableObject
    {
        public GameObject prefab;
        public float weight;
        public int minDropCount;
        public int maxDropCount;
    }
}