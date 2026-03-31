using Elemental.Framework.Pool;
using Elemental.Gameplay.Resource.Drop;
using UnityEngine;

namespace Elemental.Gameplay.Resource.Cut
{
    public class ResourceCut : MonoBehaviour, ICuttable
    {
        PooledObject pooledObject;
        ResourceDrop resourceDrop;

        public int cutCount;
        public int currentCutCount;

        void Awake()
        {
            pooledObject = GetComponent<PooledObject>();
            resourceDrop = GetComponent<ResourceDrop>();
            currentCutCount = 0;
        }

        public void Cut()
        {
            currentCutCount++;
            if (cutCount <= currentCutCount)
            {
                CutComplete();
            }
        }

        void CutComplete()
        {
            resourceDrop.Drop();
            pooledObject.PoolReturn();
        }
    }
}