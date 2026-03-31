using UnityEngine;

namespace Elemental.Gameplay.Resource.Drop
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Resource/Drop/ItemData")]
    public class ItemData : ScriptableObject
    {
        public Sprite sprite;
        public string itemName;
        public int itemID;
        public int maxOverlapCount;
    }
}