using UnityEngine;

namespace Elemental.Gameplay.item
{
    public class ItemDataStorage : MonoBehaviour
    {
        [SerializeField] ItemData itemData;

        public ItemData ItemData { get; private set; }

        void Awake()
        {
            ItemData = itemData;
        }
    }
}