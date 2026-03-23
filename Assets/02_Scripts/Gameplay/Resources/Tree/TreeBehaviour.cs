using Elemental.Framework.Pool;
using Elemental.Gameplay.Resource.Cut;
using UnityEngine;

namespace Elemental.Gameplay.Resource.Tree
{
    public class TreeBehaviour : MonoBehaviour, ICuttable
    {
        PooledObject pooledObject;

        public int cutCount;
        public int currentCutCount;

        void Awake()
        {
            pooledObject = GetComponent<PooledObject>();
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
            // TODO : 자원 드랍 및 풀 리턴
            pooledObject.PoolReturn();
        }
    }
}