using Elemental.Framework.Pool;
using UnityEngine;

namespace Elemental.Gameplay.Resource.item
{
    public class ItemReturner : MonoBehaviour
    {
        PooledObject pooledObject;

        public void OnCollected()
        {
            pooledObject.PoolReturn();
        }
    }
}