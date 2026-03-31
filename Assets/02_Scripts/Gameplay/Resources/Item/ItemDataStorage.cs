using Elemental.Gameplay.Resource.Drop;
using UnityEngine;

namespace Elemental.Gameplay.Resource.item
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