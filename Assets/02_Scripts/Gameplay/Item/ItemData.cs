using UnityEngine;

namespace Elemental.Gameplay.Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Resource/Drop/ItemData")]
    public class ItemData : ScriptableObject
    {
        public Sprite sprite;
        public string itemName;
        public int itemID;
        public int maxStackCount;
    }
}